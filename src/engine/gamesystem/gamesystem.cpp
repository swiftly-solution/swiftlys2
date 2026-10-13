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

#include "gamesystem.h"
#include <memory/hooks/vfunction.h>

#include <api/interfaces/interfaces.h>

CBaseGameSystemFactory** CBaseGameSystemFactory::sm_pFirst = nullptr;
extern void* g_pOnPrecacheResourceCallback;

void BuildGameSessionManifestHook(void* _this, EventBuildGameSessionManifest_t* msg);
VFunctionHook g_BuildGameSessionManifestHook(reinterpret_cast<void*>(BuildGameSessionManifestHook), "server", "CGameRulesGameSystem", "IGameSystem::BuildGameSessionManifest");

void BuildGameSessionManifestHook(void* _this, EventBuildGameSessionManifest_t* msg)
{
    reinterpret_cast<decltype(&BuildGameSessionManifestHook)>(g_BuildGameSessionManifestHook.GetOriginal())(_this, msg);

    IEntityResourceManifest* pResourceManifest = msg->m_pResourceManifest;
    if (g_pOnPrecacheResourceCallback) reinterpret_cast<void(*)(void*)>(g_pOnPrecacheResourceCallback)(pResourceManifest);
}

bool InitGameSystem()
{
    void* ptr = g_pGameDataManager->GetSignatures()->Fetch("IGameSystem::InitAllSystems->pFirst");
    if (!ptr) {
        g_pLogger->Error("Game System", "Couldn't find signature for 'IGameSystem::InitAllSystems->pFirst'!\n");
        return false;
    }

    uintptr_t pAddr = (uintptr_t)ptr;

    pAddr += 3;

    uint32_t offset = *(uint32_t*)pAddr;

    pAddr += 4;

    CBaseGameSystemFactory::sm_pFirst = (CBaseGameSystemFactory**)(pAddr + offset);

    return true;
}

bool ShutdownGameSystem()
{
    return true;
}