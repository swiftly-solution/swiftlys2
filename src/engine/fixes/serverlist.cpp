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
#include <api/shared/profiler.h>
#include <memory/hooks/vfunction.h>
#include <core/entrypoint.h>

#include <public/tier0/platform.h>
#include <public/steamworks/isteamclient.h>
#include <public/steamworks/isteamgameserver.h>

static constexpr double UPDATE_INTERVAL_SECONDS = 5.0;

void GameFrame(void* _this, bool simulate, bool first, bool last);
VFunctionHook g_GameFrameHook(reinterpret_cast<void*>(GameFrame), "server", "CSource2Server", "IServerGameDLL::GameFrame");

void GameFrame(void* _this, bool simulate, bool first, bool last)
{
    reinterpret_cast<decltype(&GameFrame)>(g_GameFrameHook.GetOriginal())(_this, simulate, first, last);

    PROFILE_FUNCTION();

    static double nextUpdate = 0.0;

    double curtime = Plat_FloatTime();
    if (curtime < nextUpdate) return;
    nextUpdate = curtime + UPDATE_INTERVAL_SECONDS;

    auto steamGameServer = SteamGameServer();
    if (!steamGameServer) return;

    for (int i = 0; i < g_pPlayerManager->GetPlayerCap(); i++)
    {
        auto steamid = g_pGameEngine->GetClientSteamID(i);
        if (!steamid) continue;

        auto player = g_pPlayerManager->GetPlayer(i);
        if (!player) continue;

        if (!player->GetController()) continue;

        steamGameServer->BUpdateUserData(*steamid, player->GetName(), player->GetKills());
    }
}
