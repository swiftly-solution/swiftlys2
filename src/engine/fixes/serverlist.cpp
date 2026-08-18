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
#include <core/entrypoint.h>

#include <public/tier0/platform.h>
#include <public/steam/isteamclient.h>
#include <public/steam/isteamgameserver.h>

#include <s2binlib/s2binlib.h>

#include "serverlist.h"

IVFunctionHook* g_GameFrameHook = nullptr;

void GameFrame(void* _this, bool simulate, bool first, bool last);

void ServerListFix::Start()
{
    void* servervtable = nullptr;
    s2binlib_find_vtable("server", "CSource2Server", &servervtable);

    g_GameFrameHook = g_pHooksManager->CreateVFunctionHook();
    g_GameFrameHook->SetHookFunction(servervtable, g_pGameDataManager->GetOffsets()->Fetch("IServerGameDLL::GameFrame"), reinterpret_cast<void*>(GameFrame), true);
    g_GameFrameHook->Enable();
}

void ServerListFix::Stop()
{
    if (g_GameFrameHook)
    {
        g_GameFrameHook->Disable();
        g_pHooksManager->DestroyVFunctionHook(g_GameFrameHook);
    }
}

void GameFrame(void* _this, bool simulate, bool first, bool last)
{
    reinterpret_cast<decltype(&GameFrame)>(g_GameFrameHook->GetOriginal())(_this, simulate, first, last);

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