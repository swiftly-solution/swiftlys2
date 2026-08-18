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

#include <scripting/scripting.h>
#include <api/interfaces/interfaces.h>

void Bridge_GameData_Patches_Apply(const char* name)
{
    g_pGameDataManager->GetPatches()->Apply(name);
}

void Bridge_GameData_Patches_Revert(const char* name)
{
    g_pGameDataManager->GetPatches()->Revert(name);
}

bool Bridge_GameData_Patches_Exists(const char* name)
{
    return g_pGameDataManager->GetPatches()->Exists(name);
}

DEFINE_NATIVE("Patches.Apply", Bridge_GameData_Patches_Apply);
DEFINE_NATIVE("Patches.Revert", Bridge_GameData_Patches_Revert);
DEFINE_NATIVE("Patches.Exists", Bridge_GameData_Patches_Exists);