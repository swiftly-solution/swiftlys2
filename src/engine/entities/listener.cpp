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

#include <api/interfaces/interfaces.h>
#include <api/shared/profiler.h>
#include <scripting/engine/events.h>

#include <mutex>

CEntityListener g_entityListener;

void CEntityListener::OnEntitySpawned(CEntityInstance* pEntity)
{
    PROFILE_FUNCTION();

    if (g_pOnEntitySpawnedCallback)
        g_pOnEntitySpawnedCallback(pEntity);
}

void CEntityListener::OnEntityParentChanged(CEntityInstance* pEntity, CEntityInstance* pNewParent)
{
    PROFILE_FUNCTION();

    if (g_pOnEntityParentChangedCallback)
        g_pOnEntityParentChangedCallback(pEntity, pNewParent);
}

void CEntityListener::OnEntityCreated(CEntityInstance* pEntity)
{
    PROFILE_FUNCTION();

    if (g_pOnEntityCreatedCallback)
        g_pOnEntityCreatedCallback(pEntity);
}

void CEntityListener::OnEntityDeleted(CEntityInstance* pEntity)
{
    PROFILE_FUNCTION();

    auto entindex = pEntity->m_pEntity->m_EHandle.GetEntryIndex();

    for (int i = 0; i < g_pPlayerManager->GetPlayerCap(); i++)
    {
        auto player = g_pPlayerManager->GetPlayer(i);
        if (!player) continue;

        auto& transmittingBits = player->GetBlockedTransmittingBits();
        std::lock_guard lock(transmittingBits.mutex);

        transmittingBits.Unblock(entindex);
    }

    if (g_pOnEntityDeletedCallback)
        g_pOnEntityDeletedCallback(pEntity);
}
