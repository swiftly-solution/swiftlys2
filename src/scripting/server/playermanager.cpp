/************************************************************************************************
 * SwiftlyS2 is a scripting framework for Source2-based games.
 * Copyright (C) 2023-2026 Swiftly Solution SRL via Sava Andrei-Sebastian and it's contributors
 *
 * This program is free software: you can redistribute it and/or modify
 * it under the terms of the GNU General Public License as published by
 * the Free Software Foundation, either version 3 of the License, or
 * (at your option) any later version.
 *
 * This program is distributed in the hope that it will be useful,
 * but WITHOUT ANY WARRANTY; without even the implied warranty of
 * MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.  See the
 * GNU General Public License for more details.
 *
 * You should have received a copy of the GNU General Public License
 * along with this program.  If not, see <https://www.gnu.org/licenses/>.
 ************************************************************************************************/

#include <api/interfaces/manager.h>
#include <scripting/scripting.h>

int Bridge_PlayerManager_GetPlayerCount()
{
    static auto playerManager = g_ifaceService.FetchInterface<IPlayerManager>(PLAYERMANAGER_INTERFACE_VERSION);
    return playerManager->GetPlayerCount();
}

int Bridge_PlayerManager_GetPlayerCap()
{
    static auto playerManager = g_ifaceService.FetchInterface<IPlayerManager>(PLAYERMANAGER_INTERFACE_VERSION);
    return playerManager->GetPlayerCap();
}

void Bridge_PlayerManager_SendMessage(int kind, const char* message, int duration)
{
    static auto playerManager = g_ifaceService.FetchInterface<IPlayerManager>(PLAYERMANAGER_INTERFACE_VERSION);
    playerManager->SendMsg((MessageType)kind, message, duration);
}

void Bridge_Player_ShouldBlockTransmitEntity(int playerid, int entityidx, bool shouldBlockTransmit);

void Bridge_PlayerManager_ShouldBlockTransmitEntity(int entityidx, bool shouldBlockTransmit)
{
    static auto playerManager = g_ifaceService.FetchInterface<IPlayerManager>(PLAYERMANAGER_INTERFACE_VERSION);
    for (int i = 0; i < playerManager->GetPlayerCap(); i++)
        Bridge_Player_ShouldBlockTransmitEntity(i, entityidx, shouldBlockTransmit);
}

void Bridge_Player_ClearTransmitEntityBlocked(int playerid);

void Bridge_PlayerManager_ClearAllBlockedTransmitEntity()
{
    static auto playerManager = g_ifaceService.FetchInterface<IPlayerManager>(PLAYERMANAGER_INTERFACE_VERSION);
    for (int i = 0; i < playerManager->GetPlayerCap(); i++)
        Bridge_Player_ClearTransmitEntityBlocked(i);
}

DEFINE_NATIVE("PlayerManager.GetPlayerCount", Bridge_PlayerManager_GetPlayerCount);
DEFINE_NATIVE("PlayerManager.GetPlayerCap", Bridge_PlayerManager_GetPlayerCap);
DEFINE_NATIVE("PlayerManager.SendMessage", Bridge_PlayerManager_SendMessage);
DEFINE_NATIVE("PlayerManager.ShouldBlockTransmitEntity", Bridge_PlayerManager_ShouldBlockTransmitEntity);
DEFINE_NATIVE("PlayerManager.ClearAllBlockedTransmitEntity", Bridge_PlayerManager_ClearAllBlockedTransmitEntity);