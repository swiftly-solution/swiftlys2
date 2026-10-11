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

#include <api/shared/string.h>

#include <memory/gamedata/manager.h>
#include <api/memory/virtual/call.h>

#include <public/iserver.h>

#include <fmt/format.h>

GameEventFireHandler g_fnEventFireHandler;
GameEventFireHandler g_fnPostEventFireHandler;

IGameEventManager2* g_gameEventManager = nullptr;

IVFunctionHook* g_PreworldUpdateHook = nullptr;
void PreworldUpdateHook(void* _this, bool simulate);

IVFunctionHook* g_pStartupServerEventHook = nullptr;
void StartupServerEventHook(void* _this, const GameSessionConfiguration_t& config, ISource2WorldSession* a, const char* b);

IVFunctionHook* g_pFireEventHook = nullptr;
bool FireEventHook(IGameEventManager2* _this, IGameEvent* event, bool bDontBroadcast);

void CEventManager::Initialize()
{
    void* netserverservice = nullptr;
    g_pS2BinLib->FindVtable("engine2", "CNetworkServerService", &netserverservice);

    g_pStartupServerEventHook = g_pHooksManager->CreateVFunctionHook();
    g_pStartupServerEventHook->SetHookFunction(netserverservice, g_pGameDataManager->GetOffsets()->Fetch("INetworkServerService::StartupServer"), reinterpret_cast<void*>(StartupServerEventHook), true);
    g_pStartupServerEventHook->Enable();

    uintptr_t rawGameEventManager = (uintptr_t)(g_pGameDataManager->GetSignatures()->Fetch("CSource2Server::g_GameEventManager"));

    rawGameEventManager += 3;
    rawGameEventManager += 4 + *(int*)(rawGameEventManager);

    g_gameEventManager = *(IGameEventManager2**)(rawGameEventManager);

    g_pFireEventHook = g_pHooksManager->CreateVFunctionHook();
    g_pFireEventHook->SetHookFunction(g_gameEventManager, g_pGameDataManager->GetOffsets()->Fetch("IGameEventManager2::FireEvent"), reinterpret_cast<void*>(FireEventHook), false);
    g_pFireEventHook->Enable();

    void* servervtable = nullptr;
    g_pS2BinLib->FindVtable("server", "CSource2Server", &servervtable);

    g_PreworldUpdateHook = g_pHooksManager->CreateVFunctionHook();
    g_PreworldUpdateHook->SetHookFunction(servervtable, g_pGameDataManager->GetOffsets()->Fetch("IServerGameDLL::PreWorldUpdate"), reinterpret_cast<void*>(PreworldUpdateHook), true);
    g_PreworldUpdateHook->Enable();

    QueueListener("player_spawn");
}

void CEventManager::Shutdown()
{
    if (g_pStartupServerEventHook)
    {
        g_pStartupServerEventHook->Disable();
        g_pHooksManager->DestroyVFunctionHook(g_pStartupServerEventHook);
        g_pStartupServerEventHook = nullptr;
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
    auto originalFireEvent = reinterpret_cast<decltype(&FireEventHook)>(g_pFireEventHook->GetOriginal());
    if (!event) return originalFireEvent(_this, event, bDontBroadcast);

    static constexpr uint32_t k_uPlayerSpawnHash = hash_32_fnv1a_const("player_spawn");

    uint32_t event_hash = hash_32_fnv1a_const(event->GetName());
    bool isPlayerSpawn = event_hash == k_uPlayerSpawnHash;
    bool isRegistered = g_pGameEventManager->IsEventRegistered(event_hash);

    if (!isRegistered && !isPlayerSpawn)
        return originalFireEvent(_this, event, bDontBroadcast);

    bool shouldBroadcast = bDontBroadcast;

    if (isRegistered && g_fnEventFireHandler)
    {
        auto res = g_fnEventFireHandler(event, shouldBroadcast, event_hash);
        if (res == 1 || res == 3) {
            g_gameEventManager->FreeEvent(event);
            return false;
        }
    }

    bool needsCopy = isPlayerSpawn || (isRegistered && g_fnPostEventFireHandler);
    IGameEvent* dupEvent = needsCopy ? g_gameEventManager->DuplicateEvent(event) : nullptr;

    bool result = originalFireEvent(_this, event, shouldBroadcast);

    if (isPlayerSpawn)
    {
        int userid = dupEvent->GetInt("userid", -1);
        if (userid != -1) {
            auto player = g_pPlayerManager->GetPlayer(userid);
            if (player) player->SetFirstSpawn(false);
        }
    }

    if (isRegistered && g_fnPostEventFireHandler)
    {
        auto res = g_fnPostEventFireHandler(dupEvent, shouldBroadcast, event_hash);
        if (res == 1 || res == 3) result = false;
    }

    if (dupEvent)
        g_gameEventManager->FreeEvent(dupEvent);

    return result;
}

void StartupServerEventHook(void* _this, const GameSessionConfiguration_t& config, ISource2WorldSession* a, const char* b)
{
    reinterpret_cast<decltype(&StartupServerEventHook)>(g_pStartupServerEventHook->GetOriginal())(_this, config, a, b);
    g_pGameEventManager->OnServerStartup();
}

void CEventManager::OnServerStartup()
{
    QueueLockGuard lock(m_mtxLock);
    if (!g_gameEventManager || m_bListenersReady) return;

    m_bListenersReady = true;

    for (const auto& event_name : m_pendingListeners)
        AddEngineListener(event_name);

    m_pendingListeners.clear();
}

void CEventManager::RegisterGameEventListener(std::string event_name)
{
    {
        QueueLockGuard lock(m_mtxRegisteredEvents);
        m_registeredEvents.insert(hash_32_fnv1a_const(event_name.c_str()));
    }

    QueueListener(event_name);
}

void CEventManager::UnregisterGameEventListener(std::string event_name)
{
    QueueLockGuard lock(m_mtxRegisteredEvents);
    m_registeredEvents.erase(hash_32_fnv1a_const(event_name.c_str()));
}

bool CEventManager::IsEventRegistered(uint32_t event_hash)
{
    QueueLockGuard lock(m_mtxRegisteredEvents);
    return m_registeredEvents.contains(event_hash);
}

void CEventManager::SetGameEventFireHandler(GameEventFireHandler handler)
{
    g_fnEventFireHandler = handler;
}

void CEventManager::SetPostGameEventFireHandler(GameEventFireHandler handler)
{
    g_fnPostEventFireHandler = handler;
}

IGameEventManager2* CEventManager::GetGameEventManager()
{
    return g_gameEventManager;
}

void CEventManager::FireGameEvent(IGameEvent* event) {}

void CEventManager::QueueListener(const std::string& event_name)
{
    QueueLockGuard lock(m_mtxLock);
    if (!m_bListenersReady)
    {
        m_pendingListeners.insert(event_name);
        return;
    }

    AddEngineListener(event_name);
}

void CEventManager::AddEngineListener(const std::string& event_name)
{
    if (!g_gameEventManager) return;

    if (!g_gameEventManager->FindListener(this, event_name.c_str()))
        g_gameEventManager->AddListener(this, event_name.c_str(), true);

    g_pLogger->Debug("Game Events", fmt::format("Registered listener for event '{}'.\n", event_name));
}
