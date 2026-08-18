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

#include <api/utils/mutex.h>

 // WHY THE ACTUAL FUCK YOU NEED THIS TO COMPILE ?????????????
#include <public/entity2/entitysystem.h>

class CEventManager : public IEventManager, public IGameEventListener2
{
public:
    virtual void Initialize() override;
    virtual void Shutdown() override;

    virtual void RegisterGameEventsListeners(bool shouldRegister) override;
    virtual void RegisterGameEventListener(std::string event_name) override;

    virtual void SetGameEventFireHandler(std::function<int(std::string&, IGameEvent*, bool&, uint32_t&)> handler) override;
    virtual void SetPostGameEventFireHandler(std::function<int(std::string&, IGameEvent*, bool&, uint32_t&)> handler) override;

    virtual IGameEventManager2* GetGameEventManager() override;

    virtual void FireGameEvent(IGameEvent* event) override;
private:
    QueueMutex m_mtxLock;
};

#endif