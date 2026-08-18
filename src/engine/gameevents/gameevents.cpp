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

#include <api/interfaces/interfaces.h>

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

std::function<int(std::string&, IGameEvent*, bool&, uint32_t&)> g_fnEventFireHandler;
std::function<int(std::string&, IGameEvent*, bool&, uint32_t&)> g_fnPostEventFireHandler;

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

void CEventManager::Initialize()
{
    void* CGameEventManagerVTable;
    s2binlib_find_vtable("server", "CGameEventManager", &CGameEventManagerVTable);

    void* netserverservice = nullptr;
    s2binlib_find_vtable("engine2", "CNetworkServerService", &netserverservice);

    g_pStartupServerEventHook = g_pHooksManager->CreateVFunctionHook();
    g_pStartupServerEventHook->SetHookFunction(netserverservice, g_pGameDataManager->GetOffsets()->Fetch("INetworkServerService::StartupServer"), reinterpret_cast<void*>(StartupServerEventHook), true);
    g_pStartupServerEventHook->Enable();

    uintptr_t rawGameEventManager = (uintptr_t)(g_pGameDataManager->GetSignatures()->Fetch("CSource2Server::g_GameEventManager"));

    rawGameEventManager += WIN_LINUX(95, 103) + 3;
    rawGameEventManager += 4 + *(int*)(rawGameEventManager);

    g_gameEventManager = *(IGameEventManager2**)(rawGameEventManager);

    g_pFireEventHook = g_pHooksManager->CreateVFunctionHook();
    g_pFireEventHook->SetHookFunction(g_gameEventManager, g_pGameDataManager->GetOffsets()->Fetch("IGameEventManager2::FireEvent"), reinterpret_cast<void*>(FireEventHook), false);
    g_pFireEventHook->Enable();

    void* servervtable = nullptr;
    s2binlib_find_vtable("server", "CSource2Server", &servervtable);

    g_PreworldUpdateHook = g_pHooksManager->CreateVFunctionHook();
    g_PreworldUpdateHook->SetHookFunction(servervtable, g_pGameDataManager->GetOffsets()->Fetch("IServerGameDLL::PreWorldUpdate"), reinterpret_cast<void*>(PreworldUpdateHook), true);
    g_PreworldUpdateHook->Enable();

    RegisterGameEventListener("player_spawn");
}

void CEventManager::Shutdown()
{
    if (g_pStartupServerEventHook)
    {
        g_pStartupServerEventHook->Disable();
        g_pHooksManager->DestroyVFunctionHook(g_pStartupServerEventHook);
        g_pStartupServerEventHook = nullptr;
    }

    if (g_GameFrameHookEventManager)
    {
        g_GameFrameHookEventManager->Disable();
        g_pHooksManager->DestroyFunctionHook(g_GameFrameHookEventManager);
        g_GameFrameHookEventManager = nullptr;
    }

    if (g_pFireEventHook)
    {
        g_pFireEventHook->Disable();
        g_pHooksManager->DestroyVFunctionHook(g_pFireEventHook);
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

bool FireEventHook(IGameEventManager2* _this, IGameEvent* event, bool bDontBroadcast)
{
    if (!event) return reinterpret_cast<decltype(&FireEventHook)>(g_pFireEventHook->GetOriginal())(_this, event, bDontBroadcast);

    std::string event_name = event->GetName();
    bool shouldBroadcast = bDontBroadcast;
    uint32_t event_hash = hash_32_fnv1a_const(event_name.c_str());
    bool stopOriginal = false;

    if (g_fnEventFireHandler)
    {
        auto res = g_fnEventFireHandler(event_name, event, shouldBroadcast, event_hash);
        if (res == 1) {
            g_gameEventManager->FreeEvent(event);
            return false;
        }
        else if (res == 3) stopOriginal = true;
    }

    if (stopOriginal)
    {
        g_gameEventManager->FreeEvent(event);
        return false;
    }

    IGameEvent* dupEvent = g_gameEventManager->DuplicateEvent(event);

    bool result = reinterpret_cast<decltype(&FireEventHook)>(g_pFireEventHook->GetOriginal())(_this, event, shouldBroadcast);

    static constexpr uint32_t k_uPlayerSpawnHash = hash_32_fnv1a_const("player_spawn");
    if (event_hash == k_uPlayerSpawnHash)
    {
        int userid = dupEvent->GetInt("userid", -1);
        if (userid != -1) {
            auto player = g_pPlayerManager->GetPlayer(userid);
            if (player) player->SetFirstSpawn(false);
        }
    }

    if (g_fnPostEventFireHandler)
    {
        auto res = g_fnPostEventFireHandler(event_name, dupEvent, shouldBroadcast, event_hash);
        if (res == 1) {
            g_gameEventManager->FreeEvent(dupEvent);
            return false;
        }
        else if (res == 3) stopOriginal = true;
    }

    g_gameEventManager->FreeEvent(dupEvent);

    return stopOriginal ? false : result;
}

void StartupServerEventHook(void* _this, const GameSessionConfiguration_t& config, ISource2WorldSession* a, const char* b)
{
    reinterpret_cast<decltype(&StartupServerEventHook)>(g_pStartupServerEventHook->GetOriginal())(_this, config, a, b);
    g_pGameEventManager->RegisterGameEventsListeners(true);
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

        g_pLogger->Debug("Game Events", fmt::format("Registered listener for event '{}'.\n", event_name));
    }
}

void CEventManager::SetGameEventFireHandler(std::function<int(std::string&, IGameEvent*, bool&, uint32_t&)> handler)
{
    g_fnEventFireHandler = handler;
}

void CEventManager::SetPostGameEventFireHandler(std::function<int(std::string&, IGameEvent*, bool&, uint32_t&)> handler)
{
    g_fnPostEventFireHandler = handler;
}

IGameEventManager2* CEventManager::GetGameEventManager()
{
    return g_gameEventManager;
}

void CEventManager::FireGameEvent(IGameEvent* event) {}