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

#ifndef src_engine_gameevents_gameevents_h
#define src_engine_gameevents_gameevents_h

#include <api/engine/gameevents/gameevents.h>

#include <array>
#include <atomic>
#include <memory>
#include <mutex>

#include <set>
#include <unordered_set>
#include <string>
#include <utility>
#include <vector>

 // WHY THE ACTUAL FUCK YOU NEED THIS TO COMPILE ?????????????
#include <public/entity2/entitysystem.h>

class CEventManager : public IEventManager, public IGameEventListener2
{
public:
    virtual void Initialize() override;

    virtual void OnServerStartup() override;

    virtual void RegisterGameEventListener(std::string event_name) override;
    virtual void UnregisterGameEventListener(std::string event_name) override;
    virtual bool IsEventRegistered(uint32_t event_hash) override;

    virtual void SetGameEventFireHandler(GameEventFireHandler handler) override;
    virtual void SetPostGameEventFireHandler(GameEventFireHandler handler) override;
    
    virtual int GetGameEventListenerID(std::string event_name) override;
    virtual void RegisterGameEventListenerID(std::string event_name, int id) override;

    virtual IGameEventManager2* GetGameEventManager() override;

    virtual void FireGameEvent(IGameEvent* event) override;
private:
    void QueueListener(const std::string& event_name);
    void AddEngineListener(const std::string& event_name);
    void DrainEngineListeners();

    std::mutex m_mtxLock;
    std::set<std::string> m_pendingListeners;
    bool m_bListenersReady = false;
    bool m_bDrainingListeners = false;

    std::mutex m_mtxRegisteredEvents;
    std::unordered_set<uint32_t> m_registeredEvents;

    struct EventListenerID
    {
        const std::string name;
        std::atomic<int> id;
        EventListenerID* const next;

        EventListenerID(std::string event_name, int value, EventListenerID* next_entry)
            : name(std::move(event_name)), id(value), next(next_entry) {}
    };

    // Entries stay alive until manager destruction; published names and links never change.
    std::array<std::atomic<EventListenerID*>, 256> m_eventListenerIDs{};
    std::vector<std::unique_ptr<EventListenerID>> m_eventListenerIDEntries;
};

#endif
