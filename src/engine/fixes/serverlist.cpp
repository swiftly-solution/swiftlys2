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

#include <api/interfaces/interfaces.h>
#include <memory/hooks/vfunction.h>
#include <core/entrypoint.h>

#include <public/tier0/platform.h>
#include <public/steam/isteamclient.h>
#include <public/steam/isteamgameserver.h>

void GameFrame(void* _this, bool simulate, bool first, bool last);
VFunctionHook g_GameFrameHook(reinterpret_cast<void*>(GameFrame), "server", "CSource2Server", "IServerGameDLL::GameFrame");

void GameFrame(void* _this, bool simulate, bool first, bool last)
{
    reinterpret_cast<decltype(&GameFrame)>(g_GameFrameHook.GetOriginal())(_this, simulate, first, last);

    static double l_NextUpdate = 0.0;

    if (double curtime = Plat_FloatTime(); curtime >= l_NextUpdate)
    {
        l_NextUpdate = curtime + 5.0;

        auto steamGameServer = SteamGameServer();
        if (!steamGameServer) return;

        for (int i = 0; i < g_pPlayerManager->GetPlayerCap(); i++)
        {
            auto steamid = g_pGameEngine->GetClientSteamID(i);
            if (!steamid) continue;

            auto player = g_pPlayerManager->GetPlayer(i);
            if (!player) continue;

            auto controller = player->GetController();
            if (!controller) continue;

            steamGameServer->BUpdateUserData(*steamid, (const char*)(g_pSDKSchema->GetPropPtr(controller, 4141622183986322747)), g_pGameClientsService->GetPlayerScore(i)); // CBasePlayerController::PlayerName
        }
    }
}