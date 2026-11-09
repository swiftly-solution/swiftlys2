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

#include <api/interfaces/interfaces.h>
#include <api/shared/profiler.h>
#include <public/game/igamesystemfactory.h>

bool InitGameSystem()
{
    PROFILE_FUNCTION();

    void* ptr = g_pGameDataManager->GetSignatures()->Fetch("IGameSystem::InitAllSystems->pFirst");
    if (!ptr) {
        g_pLogger->Error("Game System", "Couldn't find signature for 'IGameSystem::InitAllSystems->pFirst'!\n");
        return false;
    }

    auto address = reinterpret_cast<uintptr_t>(ptr) + 3;
    const auto offset = *reinterpret_cast<uint32_t*>(address);
    address += 4;

    CBaseGameSystemFactory::SetFirstPtr(reinterpret_cast<CBaseGameSystemFactory**>(address + offset));

    return true;
}
