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

#include <api/server/players/manager.h>

#include <public/eiface.h>

#include "player.h"

#include "networkbasetypes.pb.h"

#include <steam/steam_api_common.h>
#include <steam/isteamuser.h>
#include <steam/steamclientpublic.h>

class CPlayerManager : public IPlayerManager
{
public:
    virtual void Initialize() override;
    virtual void Shutdown() override;

    virtual IPlayer* RegisterPlayer(int playerid) override;
    virtual void UnregisterPlayer(int playerid) override;

    virtual IPlayer* GetPlayer(int playerid) override;

    virtual bool IsPlayerOnline(int playerid) override;

    virtual int GetPlayerCount() override;
    virtual int GetPlayerCap() override;

    virtual void SendMsg(MessageType type, const std::string& message, int duration) override;

    virtual void SteamAPIServerActivated() override;

    STEAM_GAMESERVER_CALLBACK_MANUAL(CPlayerManager, OnValidateAuthTicket, ValidateAuthTicketResponse_t, m_CallbackValidateAuthTicketResponse);
private:
    CPlayer** g_Players = nullptr;
};