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

#ifndef src_api_engine_gameevents_gameevents_h
#define src_api_engine_gameevents_gameevents_h

#include <string>
#include <functional>

#include <public/igameevents.h>

class IEventManager
{
public:
    virtual void Initialize() = 0;
    virtual void Shutdown() = 0;

    virtual void RegisterGameEventsListeners(bool shouldRegister) = 0;
    virtual void RegisterGameEventListener(std::string event_name) = 0;

    // to supercede, return 1
    // std::string& event_name, IGameEvent* event, bool& dont_broadcast, uint32_t& hash
    virtual void SetGameEventFireHandler(std::function<int(std::string&, IGameEvent*, bool&, uint32_t&)> handler) = 0;
    // to supercede, return 1
    // std::string& event_name, IGameEvent* event, bool& dont_broadcast, uint32_t& hash
    virtual void SetPostGameEventFireHandler(std::function<int(std::string&, IGameEvent*, bool&, uint32_t&)> handler) = 0;

    virtual IGameEventManager2* GetGameEventManager() = 0;
};

#endif