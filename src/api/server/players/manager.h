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

#ifndef src_api_server_players_manager_h
#define src_api_server_players_manager_h

#include "player.h"

class IPlayerManager
{
public:
    virtual void Initialize() = 0;
    virtual void Shutdown() = 0;

    virtual IPlayer* RegisterPlayer(int playerid) = 0;
    virtual void UnregisterPlayer(int playerid) = 0;

    virtual IPlayer* GetPlayer(int playerid) = 0;

    virtual bool IsPlayerOnline(int playerid) = 0;

    virtual int GetPlayerCount() = 0;
    virtual int GetPlayerCap() = 0;

    virtual void SendMsg(MessageType type, const std::string& message, int duration) = 0;

    virtual void SteamAPIServerActivated() = 0;
};

#endif