/************************************************************************************************
 *  SwiftlyS2 is a scripting framework for Source2-based games.
 *  Copyright (C) 2023-2026 Swiftly Solution SRL via Sava Andrei-Sebastian and it's contributors
 *
 *  This program is free software: you can redistribute it and/or modify
 *  it under the terms of the GNU General Public License as published by
 *  the Free Software Foundation, either version 3 of the License, or
 *  (at your option) any later version.
 *
 *  This program is distributed in the hope that it will be useful,
 *  but WITHOUT ANY WARRANTY; without even the implied warranty of
 *  MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.  See the
 *  GNU General Public License for more details.
 *
 *  You should have received a copy of the GNU General Public License
 *  along with this program.  If not, see <https://www.gnu.org/licenses/>.
 ************************************************************************************************/

#include "gameevents.h"

#include <core/bridge/metamod.h>

#include <api/interfaces/manager.h>

#include <api/shared/files.h>
#include <api/shared/jsonc.h>
#include <api/shared/string.h>

#include <memory/gamedata/manager.h>
#include <api/memory/virtual/call.h>
#include <api/shared/plat.h>

#include <public/iserver.h>
#include <public/filesystem.h>

#include <map>
#include <stack>
#include <list>
#include <nlohmann/json.hpp>

#include <fmt/format.h>

#include <s2binlib/s2binlib.h>

using json = nlohmann::json;

std::map<uint64_t, std::function<int(std::string, IGameEvent*, bool&)>> g_mEventListeners;
std::map<uint64_t, std::function<int(std::string, IGameEvent*, bool&)>> g_mPostEventListeners;

std::list<std::list<std::pair<int64_t, std::function<void()>>>::iterator> queueRemoveTimeouts;
std::list<std::pair<int64_t, std::function<void()>>> timeoutsArray;
bool processingTimeouts = false;

std::set<std::string> g_sDumpedFiles;
json dumpedEvents;

std::set<std::string> g_sEnqueueListenEvents;
bool g_bEventsLoaded = false;

int g_uLoadEventFromFileHookID = 0;

IGameEventManager2* g_gameEventManager = nullptr;
IFunctionHook* g_GameFrameHookEventManager = nullptr;

IVFunctionHook* g_PreworldUpdateHook = nullptr;
void PreworldUpdateHook(void* _this, bool simulate);

IVFunctionHook* g_pStartupServerEventHook = nullptr;
void StartupServerEventHook(void* _this, const GameSessionConfiguration_t& config, ISource2WorldSession* a, const char* b);

IVFunctionHook* g_pFireEventHook = nullptr;
bool FireEventHook(IGameEventManager2* _this, IGameEvent* event, bool bDontBroadcast);

void GameFrameEventManager(void* _this, bool simulate, bool first, bool last);

void CEventManager::Initialize(std::string game_name)
{
    void* CGameEventManagerVTable;
    s2binlib_find_vtable("server", "CGameEventManager", &CGameEventManagerVTable);

    auto hooksmanager = g_ifaceService.FetchInterface<IHooksManager>(HOOKSMANAGER_INTERFACE_VERSION);
    auto gamedata = g_ifaceService.FetchInterface<IGameDataManager>(GAMEDATA_INTERFACE_VERSION);
    auto server = g_ifaceService.FetchInterface<ISource2Server>(SOURCE2SERVER_INTERFACE_VERSION);

    void* netserverservice = nullptr;
    s2binlib_find_vtable("engine2", "CNetworkServerService", &netserverservice);

    g_pStartupServerEventHook = hooksmanager->CreateVFunctionHook();
    g_pStartupServerEventHook->SetHookFunction(netserverservice, gamedata->GetOffsets()->Fetch("INetworkServerService::StartupServer"), reinterpret_cast<void*>(StartupServerEventHook), true);
    g_pStartupServerEventHook->Enable();

    uintptr_t rawGameEventManager = (uintptr_t)(gamedata->GetSignatures()->Fetch("CSource2Server::g_GameEventManager"));

    rawGameEventManager += WIN_LINUX(95, 103) + 3;
    rawGameEventManager += 4 + *(int*)(rawGameEventManager);

    g_gameEventManager = *(IGameEventManager2**)(rawGameEventManager);

    g_pFireEventHook = hooksmanager->CreateVFunctionHook();
    g_pFireEventHook->SetHookFunction(g_gameEventManager, gamedata->GetOffsets()->Fetch("IGameEventManager2::FireEvent"), reinterpret_cast<void*>(FireEventHook), false);
    g_pFireEventHook->Enable();

    void* servervtable = nullptr;
    s2binlib_find_vtable("server", "CSource2Server", &servervtable);

    void* gameFrameAddr;
    s2binlib_find_vfunc_by_vtbname("server", "CSource2Server", gamedata->GetOffsets()->Fetch("IServerGameDLL::GameFrame"), &gameFrameAddr);
    g_GameFrameHookEventManager = hooksmanager->CreateFunctionHook();
    g_GameFrameHookEventManager->SetHookFunction(gameFrameAddr, reinterpret_cast<void*>(GameFrameEventManager));
    g_GameFrameHookEventManager->Enable();

    g_PreworldUpdateHook = hooksmanager->CreateVFunctionHook();
    g_PreworldUpdateHook->SetHookFunction(servervtable, gamedata->GetOffsets()->Fetch("IServerGameDLL::PreWorldUpdate"), reinterpret_cast<void*>(PreworldUpdateHook), true);
    g_PreworldUpdateHook->Enable();

    RegisterGameEventListener("round_start");
    RegisterGameEventListener("player_spawn");
    AddGameEventFireListener([](std::string event_name, IGameEvent* event, bool& dont_broadcast) -> int {
        if (event_name == "round_start") {
            static auto vgui = g_ifaceService.FetchInterface<IVGUI>(VGUI_INTERFACE_VERSION);

            vgui->ResetStateOfScreenTexts();

            timeoutsArray.push_back({ GetTime() + 100, []() -> void {
                vgui->RegenerateScreenTexts();
            } });

            processingTimeouts = true;
        }
        return 0;
        });

    AddPostGameEventFireListener([](std::string event_name, IGameEvent* event, bool& dont_broadcast) -> int {
        if (event_name == "player_spawn") {
            int userid = event->GetInt("userid", -1);
            if (userid != -1) {
                static auto playerManager = g_ifaceService.FetchInterface<IPlayerManager>(PLAYERMANAGER_INTERFACE_VERSION);
                auto player = playerManager->GetPlayer(userid);
                if (player) {
                    player->SetFirstSpawn(false);
                }
            }
        }
        return 0;
        });
}

void CEventManager::Shutdown()
{
    static auto hooksmanager = g_ifaceService.FetchInterface<IHooksManager>(HOOKSMANAGER_INTERFACE_VERSION);

    if (g_pStartupServerEventHook)
    {
        g_pStartupServerEventHook->Disable();
        hooksmanager->DestroyVFunctionHook(g_pStartupServerEventHook);
        g_pStartupServerEventHook = nullptr;
    }

    if (g_GameFrameHookEventManager)
    {
        g_GameFrameHookEventManager->Disable();
        hooksmanager->DestroyFunctionHook(g_GameFrameHookEventManager);
        g_GameFrameHookEventManager = nullptr;
    }

    if (g_pFireEventHook)
    {
        g_pFireEventHook->Disable();
        hooksmanager->DestroyVFunctionHook(g_pFireEventHook);
        g_pFireEventHook = nullptr;
    }
}

extern void* g_pOnPreworldUpdateCallback;

void PreworldUpdateHook(void* _this, bool simulate)
{
    reinterpret_cast<decltype(&PreworldUpdateHook)>(g_PreworldUpdateHook->GetOriginal())(_this, simulate);

    if (g_pOnPreworldUpdateCallback)
        reinterpret_cast<void(*)(bool)>(g_pOnPreworldUpdateCallback)(simulate);
}

void GameFrameEventManager(void* _this, bool simulate, bool first, bool last)
{
    reinterpret_cast<decltype(&GameFrameEventManager)>(g_GameFrameHookEventManager->GetOriginal())(_this, simulate, first, last);

    if (processingTimeouts)
    {
        int64_t t = GetTime();
        for (auto it = timeoutsArray.begin(); it != timeoutsArray.end(); ++it) {
            if (it->first <= t) {
                queueRemoveTimeouts.push_back(it);
                it->second();
            }
        }

        for (auto it = queueRemoveTimeouts.rbegin(); it != queueRemoveTimeouts.rend(); ++it)
            timeoutsArray.erase(*it);

        queueRemoveTimeouts.clear();
        processingTimeouts = (timeoutsArray.size() > 0);
    }
}

bool FireEventHook(IGameEventManager2* _this, IGameEvent* event, bool bDontBroadcast)
{
    if (!event) return reinterpret_cast<decltype(&FireEventHook)>(g_pFireEventHook->GetOriginal())(_this, event, bDontBroadcast);

    std::string event_name = event->GetName();
    bool shouldBroadcast = bDontBroadcast;
    for (const auto& [id, callback] : g_mEventListeners) {
        auto res = callback(event_name, event, shouldBroadcast);
        if (res == 1) {
            g_gameEventManager->FreeEvent(event);
            return false;
        }
        else if (res == 2) break;
    }

    IGameEvent* dupEvent = g_gameEventManager->DuplicateEvent(event);

    bool result = reinterpret_cast<decltype(&FireEventHook)>(g_pFireEventHook->GetOriginal())(_this, event, shouldBroadcast);

    for (const auto& [id, callback] : g_mPostEventListeners) {
        auto res = callback(event_name, dupEvent, shouldBroadcast);
        if (res == 1) {
            g_gameEventManager->FreeEvent(dupEvent);
            return false;
        }
        else if (res == 2) break;
    }

    g_gameEventManager->FreeEvent(dupEvent);

    return result;
}

void StartupServerEventHook(void* _this, const GameSessionConfiguration_t& config, ISource2WorldSession* a, const char* b)
{
    reinterpret_cast<decltype(&StartupServerEventHook)>(g_pStartupServerEventHook->GetOriginal())(_this, config, a, b);

    auto evmanager = g_ifaceService.FetchInterface<IEventManager>(GAMEEVENTMANAGER_INTERFACE_VERSION);
    evmanager->RegisterGameEventsListeners(true);
}

void CEventManager::RegisterGameEventsListeners(bool shouldRegister)
{
    QueueLockGuard lock(m_mtxLock);
    if (!g_gameEventManager) return;

    if (shouldRegister && !g_bEventsLoaded) {
        g_bEventsLoaded = true;

        for (auto it = g_sEnqueueListenEvents.begin(); it != g_sEnqueueListenEvents.end(); ++it)
            RegisterGameEventListener(*it);

        g_sEnqueueListenEvents.clear();
    }
}

void CEventManager::RegisterGameEventListener(std::string event_name)
{
    QueueLockGuard lock(m_mtxLock);
    if (!g_bEventsLoaded) {
        g_sEnqueueListenEvents.insert(event_name);
    }
    else {
        if (!g_gameEventManager) return;

        if (!g_gameEventManager->FindListener(this, event_name.c_str()))
            g_gameEventManager->AddListener(this, event_name.c_str(), true);

        auto logger = g_ifaceService.FetchInterface<ILogger>(LOGGER_INTERFACE_VERSION);
        logger->Debug("Game Events", fmt::format("Registered listener for event '{}'.\n", event_name));
    }
}

uint64_t CEventManager::AddGameEventFireListener(std::function<int(std::string, IGameEvent*, bool&)> callback)
{
    QueueLockGuard lock(m_mtxLock);
    static uint64_t s_uiListenerID = 0;
    g_mEventListeners[++s_uiListenerID] = callback;
    return s_uiListenerID;
}

uint64_t CEventManager::AddPostGameEventFireListener(std::function<int(std::string, IGameEvent*, bool&)> callback)
{
    QueueLockGuard lock(m_mtxLock);
    static uint64_t s_uiListenerID = 0;
    g_mPostEventListeners[++s_uiListenerID] = callback;
    return s_uiListenerID;
}

void CEventManager::RemoveGameEventFireListener(uint64_t listener_id)
{
    QueueLockGuard lock(m_mtxLock);
    auto it = g_mEventListeners.find(listener_id);
    if (it != g_mEventListeners.end())
    {
        g_mEventListeners.erase(it);
    }
}

void CEventManager::RemovePostGameEventFireListener(uint64_t listener_id)
{
    QueueLockGuard lock(m_mtxLock);
    auto it = g_mPostEventListeners.find(listener_id);
    if (it != g_mPostEventListeners.end())
    {
        g_mPostEventListeners.erase(it);
    }
}

IGameEventManager2* CEventManager::GetGameEventManager()
{
    return g_gameEventManager;
}

void CEventManager::FireGameEvent(IGameEvent* event) {}