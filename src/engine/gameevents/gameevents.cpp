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
#include <memory/hooks/vfunction.h>

#include <api/interfaces/interfaces.h>

#include <api/shared/hash.h>
#include <api/shared/hookresult.h>
#include <api/shared/profiler.h>

#include <public/tier0/keyvalues.h>

#include <format>
#include <scripting/engine/events.h>
#include <public/engine2/gamesessionconfiguration.h>

class ISource2WorldSession;

GameEventFireHandler g_fnEventFireHandler;
GameEventFireHandler g_fnPostEventFireHandler;

IGameEventManager2* g_gameEventManager = nullptr;

static IGameEventManager2* ResolveGameEventManager()
{
    PROFILE_FUNCTION();

    uintptr_t rawGameEventManager = (uintptr_t)(g_pGameDataManager->GetSignatures()->Fetch("CSource2Server::g_GameEventManager"));
    if (!rawGameEventManager) return nullptr;

    rawGameEventManager += 3;
    rawGameEventManager += 4 + *(int*)(rawGameEventManager);

    return *(IGameEventManager2**)(rawGameEventManager);
}

void PreworldUpdateHook(void* _this, bool simulate);
VFunctionHook g_PreworldUpdateHook(reinterpret_cast<void*>(PreworldUpdateHook), "server", "CSource2Server", "IServerGameDLL::PreWorldUpdate");

void StartupServerEventHook(void* _this, const GameSessionConfiguration_t& config, ISource2WorldSession* a, const char* b);
VFunctionHook g_StartupServerEventHook(reinterpret_cast<void*>(StartupServerEventHook), "engine2", "CNetworkServerService", "INetworkServerService::StartupServer");

bool FireEventHook(IGameEventManager2* _this, IGameEvent* event, bool bDontBroadcast);
VFunctionHook g_FireEventHook(reinterpret_cast<void*>(FireEventHook), []() -> void* { return ResolveGameEventManager(); }, "IGameEventManager2::FireEvent");

void CEventManager::Initialize()
{
    PROFILE_FUNCTION();

    g_gameEventManager = ResolveGameEventManager();

    QueueListener("player_spawn");
}


void PreworldUpdateHook(void* _this, bool simulate)
{
    reinterpret_cast<decltype(&PreworldUpdateHook)>(g_PreworldUpdateHook.GetOriginal())(_this, simulate);

    PROFILE_FUNCTION();

    if (g_pOnPreworldUpdateCallback)
        g_pOnPreworldUpdateCallback(simulate);
}

bool FireEventHook(IGameEventManager2* _this, IGameEvent* event, bool bDontBroadcast)
{
    static constexpr uint32_t k_uPlayerSpawnHash = hash_32_fnv1a_const("player_spawn");

    uint32_t event_hash = 0;
    bool isPlayerSpawn = false;
    bool isRegistered = false;
    bool shouldBroadcast = bDontBroadcast;
    IGameEvent* dupEvent = nullptr;

    PROFILE_FUNCTION();

    if (event)
    {
        event_hash = hash_32_fnv1a_const(event->GetName());
        isPlayerSpawn = event_hash == k_uPlayerSpawnHash;
        isRegistered = g_pGameEventManager->IsEventRegistered(event_hash);
    }

    if (isRegistered && g_fnEventFireHandler)
    {
        auto res = g_fnEventFireHandler(event, shouldBroadcast, event_hash);
        if (res == HookResult_Stop || res == HookResult_CancelOriginal) {
            g_gameEventManager->FreeEvent(event);
            return false;
        }
    }

    bool needsCopy = isPlayerSpawn || (isRegistered && g_fnPostEventFireHandler);
    dupEvent = needsCopy ? g_gameEventManager->DuplicateEvent(event) : nullptr;

    PROFILE_PAUSE();

    bool result = reinterpret_cast<decltype(&FireEventHook)>(g_FireEventHook.GetOriginal())(_this, event, shouldBroadcast);

    PROFILE_RESUME();

    if (!event || (!isRegistered && !isPlayerSpawn))
        return result;

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
        if (res == HookResult_Stop || res == HookResult_CancelOriginal) result = false;
    }

    if (dupEvent)
        g_gameEventManager->FreeEvent(dupEvent);

    return result;
}

void StartupServerEventHook(void* _this, const GameSessionConfiguration_t& config, ISource2WorldSession* a, const char* b)
{
    reinterpret_cast<decltype(&StartupServerEventHook)>(g_StartupServerEventHook.GetOriginal())(_this, config, a, b);

    PROFILE_FUNCTION();
    g_pGameEventManager->OnServerStartup();
}

void CEventManager::OnServerStartup()
{
    PROFILE_FUNCTION();

    {
        std::lock_guard<std::mutex> lock(m_mtxLock);
        if (!g_gameEventManager || m_bListenersReady) return;
        m_bListenersReady = true;
        if (m_bDrainingListeners) return;
        m_bDrainingListeners = true;
    }

    DrainEngineListeners();
}

void CEventManager::RegisterGameEventListener(std::string event_name)
{
    PROFILE_FUNCTION();

    {
        std::lock_guard<std::mutex> lock(m_mtxRegisteredEvents);
        m_registeredEvents.insert(hash_32_fnv1a_const(event_name.c_str()));
    }

    QueueListener(event_name);
}

void CEventManager::UnregisterGameEventListener(std::string event_name)
{
    PROFILE_FUNCTION();

    std::lock_guard<std::mutex> lock(m_mtxRegisteredEvents);
    m_registeredEvents.erase(hash_32_fnv1a_const(event_name.c_str()));
}

bool CEventManager::IsEventRegistered(uint32_t event_hash)
{
    PROFILE_FUNCTION();

    std::lock_guard<std::mutex> lock(m_mtxRegisteredEvents);
    return m_registeredEvents.contains(event_hash);
}

void CEventManager::SetGameEventFireHandler(GameEventFireHandler handler)
{
    PROFILE_FUNCTION();

    g_fnEventFireHandler = handler;
}

void CEventManager::SetPostGameEventFireHandler(GameEventFireHandler handler)
{
    PROFILE_FUNCTION();

    g_fnPostEventFireHandler = handler;
}

IGameEventManager2* CEventManager::GetGameEventManager()
{
    PROFILE_FUNCTION();

    return g_gameEventManager;
}

void CEventManager::FireGameEvent(IGameEvent* event)
{
    PROFILE_FUNCTION();
}

void CEventManager::QueueListener(const std::string& event_name)
{
    PROFILE_FUNCTION();

    bool registerImmediately = false;
    {
        std::lock_guard<std::mutex> lock(m_mtxLock);
        if (!m_bListenersReady || m_bDrainingListeners || !m_pendingListeners.empty())
        {
            m_pendingListeners.insert(event_name);
            if (!m_bListenersReady || m_bDrainingListeners) return;
        }
        else
        {
            registerImmediately = true;
        }
        m_bDrainingListeners = true;
    }

    if (registerImmediately)
    {
        try
        {
            AddEngineListener(event_name);
        }
        catch (...)
        {
            std::lock_guard<std::mutex> lock(m_mtxLock);
            m_bDrainingListeners = false;
            m_pendingListeners.insert(event_name);
            throw;
        }
    }

    DrainEngineListeners();
}

void CEventManager::DrainEngineListeners()
{
    PROFILE_FUNCTION();

    std::set<std::string> pending;
    try
    {
        while (true)
        {
            {
                std::lock_guard<std::mutex> lock(m_mtxLock);
                if (m_pendingListeners.empty())
                {
                    m_bDrainingListeners = false;
                    return;
                }
                pending.swap(m_pendingListeners);
            }

            for (const auto& event_name : pending)
                AddEngineListener(event_name);
            pending.clear();
        }
    }
    catch (...)
    {
        std::lock_guard<std::mutex> lock(m_mtxLock);
        m_pendingListeners.merge(pending);
        m_bDrainingListeners = false;
        throw;
    }
}

bool bInitializedEvents = false;

void CEventManager::AddEngineListener(const std::string& event_name)
{
    PROFILE_FUNCTION();

    if (!g_gameEventManager) return;

    if (!g_gameEventManager->FindListener(this, event_name.c_str()))
        g_gameEventManager->AddListener(this, event_name.c_str(), true);

    g_pLogger->Debug("Game Events", std::format("Registered listener for event '{}'.\n", event_name));
    if(!bInitializedEvents)
    {
        bInitializedEvents = true;
        std::vector<std::string> fileNames = {"resource/mod.gameevents", "resource/core.gameevents", "resource/game.gameevents"};
        for(const auto& file : fileNames)
        {
            KeyValues v("");
            v.LoadFromFile(g_pGameFileSystem, file.c_str());
            FOR_EACH_SUBKEY((&v), sub)
                RegisterGameEventListenerID(sub->GetName(), g_gameEventManager->LookupEventId(sub->GetName()));
        }
    }
}

int CEventManager::GetGameEventListenerID(std::string event_name)
{
    PROFILE_FUNCTION();

    auto& bucket = m_eventListenerIDs[std::hash<std::string>{}(event_name) % m_eventListenerIDs.size()];
    for (auto* entry = bucket.load(std::memory_order_acquire); entry; entry = entry->next)
        if (entry->name == event_name) return entry->id.load(std::memory_order_acquire);

    return -1;
}

void CEventManager::RegisterGameEventListenerID(std::string event_name, int id)
{
    PROFILE_FUNCTION();

    std::lock_guard<std::mutex> lock(m_mtxRegisteredEvents);
    auto& bucket = m_eventListenerIDs[std::hash<std::string>{}(event_name) % m_eventListenerIDs.size()];
    auto* head = bucket.load(std::memory_order_relaxed);
    for (auto* entry = head; entry; entry = entry->next)
    {
        if (entry->name == event_name)
        {
            entry->id.store(id, std::memory_order_release);
            return;
        }
    }

    auto entry = std::make_unique<EventListenerID>(std::move(event_name), id, head);
    auto* published = entry.get();
    m_eventListenerIDEntries.push_back(std::move(entry));
    bucket.store(published, std::memory_order_release);
}
