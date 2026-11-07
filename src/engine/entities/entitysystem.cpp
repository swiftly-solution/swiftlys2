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

#include "entitysystem.h"
#include <memory/hooks/vfunction.h>

#include <cstdint>

#include <public/entity2/entitysystem.h>
#include <public/game/gameentitysystem.h>
#include <public/engine2/gamesessionconfiguration.h>

#include "listener.h"

#include <api/interfaces/interfaces.h>
#include <api/shared/profiler.h>
#include <scripting/engine/events.h>

class ISource2WorldSession;

CGameEntitySystem* g_pGameEntitySystem = nullptr;

CGameEntitySystem* GameEntitySystem()
{
    PROFILE_FUNCTION();

    return g_pGameEntitySystem;
}

void StartupServerHook(void* _this, const GameSessionConfiguration_t& config, ISource2WorldSession* a, const char* b);

VFunctionHook g_StartupServerHook(reinterpret_cast<void*>(StartupServerHook), "engine2", "CNetworkServerService", "INetworkServerService::StartupServer");

void CEntSystem::Shutdown()
{
    PROFILE_FUNCTION();

    if (g_pGameEntitySystem)
        g_pGameEntitySystem->RemoveListenerEntity(&g_entityListener);
}

void StartupServerHook(void* _this, const GameSessionConfiguration_t& config, ISource2WorldSession* a, const char* b)
{
    reinterpret_cast<decltype(&StartupServerHook)>(g_StartupServerHook.GetOriginal())(_this, config, a, b);

    PROFILE_FUNCTION();

    if (!g_pGameEntitySystem)
    {
        auto offset = g_pGameDataManager->GetOffsets()->Fetch("GameEntitySystem");
        g_pGameEntitySystem = *reinterpret_cast<CGameEntitySystem**>(reinterpret_cast<uintptr_t>(g_pGameResources) + offset);
        g_pGameEntitySystem->AddListenerEntity(&g_entityListener);
    }

    if (g_pOnStartupServerCallback)
        g_pOnStartupServerCallback();
}

CEntitySystem* CEntSystem::GetEntitySystem()
{
    PROFILE_FUNCTION();

    return g_pGameEntitySystem;
}
