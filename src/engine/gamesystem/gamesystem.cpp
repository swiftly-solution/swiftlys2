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

#include <s2binlib/s2binlib.h>
#include <api/interfaces/manager.h>

CBaseGameSystemFactory** CBaseGameSystemFactory::sm_pFirst = nullptr;
extern void* g_pOnPrecacheResourceCallback;

IVFunctionHook* pOnPrecacheResourceCallbackHook = nullptr;

void BuildGameSessionManifestHook(void* _this, EventBuildGameSessionManifest_t* msg)
{
    reinterpret_cast<decltype(&BuildGameSessionManifestHook)>(pOnPrecacheResourceCallbackHook->GetOriginal())(_this, msg);

    IEntityResourceManifest* pResourceManifest = msg->m_pResourceManifest;
    if (g_pOnPrecacheResourceCallback) reinterpret_cast<void(*)(void*)>(g_pOnPrecacheResourceCallback)(pResourceManifest);
}

bool InitGameSystem()
{
    auto hooksmanager = g_ifaceService.FetchInterface<IHooksManager>(HOOKSMANAGER_INTERFACE_VERSION);
    auto logger = g_ifaceService.FetchInterface<ILogger>(LOGGER_INTERFACE_VERSION);
    auto gamedata = g_ifaceService.FetchInterface<IGameDataManager>(GAMEDATA_INTERFACE_VERSION);

    void* gameRulesGameSystemVTable = nullptr;
    s2binlib_find_vtable("server", "CGameRulesGameSystem", &gameRulesGameSystemVTable);

    pOnPrecacheResourceCallbackHook = hooksmanager->CreateVFunctionHook();
    pOnPrecacheResourceCallbackHook->SetHookFunction(gameRulesGameSystemVTable, gamedata->GetOffsets()->Fetch("IGameSystem::BuildGameSessionManifest"), (void*)BuildGameSessionManifestHook, true);
    pOnPrecacheResourceCallbackHook->Enable();

    void* ptr = gamedata->GetSignatures()->Fetch("IGameSystem::InitAllSystems->pFirst");
    if (!ptr) {
        logger->Error("Game System", "Couldn't find signature for 'IGameSystem::InitAllSystems->pFirst'!\n");
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
    auto hooksmanager = g_ifaceService.FetchInterface<IHooksManager>(HOOKSMANAGER_INTERFACE_VERSION);

    if (pOnPrecacheResourceCallbackHook != nullptr)
    {
        pOnPrecacheResourceCallbackHook->Disable();
        hooksmanager->DestroyVFunctionHook(pOnPrecacheResourceCallbackHook);
        pOnPrecacheResourceCallbackHook = nullptr;
    }
    return true;
}