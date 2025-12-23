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

#include "listener.h"
#include "entitysystem.h"

#include <api/interfaces/manager.h>
#include <api/shared/plat.h>

#define CCSGameRulesProxy_m_pGameRules 0x242D3ADB925C1F40

CEntityListener g_entityListener;

extern void* g_pOnEntityCreatedCallback;
extern void* g_pOnEntityDeletedCallback;
extern void* g_pOnEntityParentChangedCallback;
extern void* g_pOnEntitySpawnedCallback;

void CEntityListener::OnEntitySpawned(CEntityInstance* pEntity)
{
    if (g_pOnEntitySpawnedCallback)
        reinterpret_cast<void(*)(void*)>(g_pOnEntitySpawnedCallback)(pEntity);
}

void CEntityListener::OnEntityParentChanged(CEntityInstance* pEntity, CEntityInstance* pNewParent)
{
    if (g_pOnEntityParentChangedCallback)
        reinterpret_cast<void(*)(void*, void*)>(g_pOnEntityParentChangedCallback)(pEntity, pNewParent);
}

void EntityAllowHammerID(CEntityInstance* pEntity)
{
    auto gamedata = g_ifaceService.FetchInterface<IGameDataManager>(GAMEDATA_INTERFACE_VERSION);
    Plat_WriteMemory((*(void***)pEntity)[gamedata->GetOffsets()->Fetch("GetHammerUniqueID")], (uint8_t*)"\xB0\x01", 2);
}

bool bDone = false;

void CEntityListener::OnEntityCreated(CEntityInstance* pEntity)
{
    if (!bDone) {
        bDone = true;
        EntityAllowHammerID(pEntity);
    }

    if (g_pOnEntityCreatedCallback)
        reinterpret_cast<void(*)(void*)>(g_pOnEntityCreatedCallback)(pEntity);

    if (std::string(pEntity->GetClassname()) == "cs_gamerules") {
        static auto schema = g_ifaceService.FetchInterface<ISDKSchema>(SDKSCHEMA_INTERFACE_VERSION);
        g_pGameRules = *(void**)(schema->GetPropPtr(pEntity, CCSGameRulesProxy_m_pGameRules));
    }
}

void CEntityListener::OnEntityDeleted(CEntityInstance* pEntity)
{
    static auto playermanager = g_ifaceService.FetchInterface<IPlayerManager>(PLAYERMANAGER_INTERFACE_VERSION);
    for (int i = 0; i < 64; i++)
        if (playermanager->IsPlayerOnline(i)) {
            auto player = playermanager->GetPlayer(i);
            if (!player) continue;
            auto& transmittingBits = player->GetBlockedTransmittingBits();

            auto entindex = pEntity->m_pEntity->m_EHandle.GetEntryIndex();
            auto dword = entindex / 64;

            auto result = std::find(transmittingBits.activeMasks.begin(), transmittingBits.activeMasks.end(), dword);

            if (result == transmittingBits.activeMasks.end()) {
                continue;
            }

            transmittingBits.blockedMask[dword] &= ~(1ULL << (entindex % 64));
            if (transmittingBits.blockedMask[dword] == 0) transmittingBits.activeMasks.erase(result);
        }

    if (g_pOnEntityDeletedCallback)
        reinterpret_cast<void(*)(void*)>(g_pOnEntityDeletedCallback)(pEntity);
}