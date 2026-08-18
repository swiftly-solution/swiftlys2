/************************************************************************************************
 * SwiftlyS2 is a scripting framework for Source2-based games.
 * Copyright (C) 2023-2026 Swiftly Solution SRL via Sava Andrei-Sebastian and it's contributors
 *
 * This program is free software: you can redistribute it and/or modify
 * it under the terms of the GNU General Public License as published by
 * the Free Software Foundation, either version 3 of the License, or
 * (at your option) any later version.
 *
 * This program is distributed in the hope that it will be useful,
 * but WITHOUT ANY WARRANTY; without even the implied warranty of
 * MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.  See the
 * GNU General Public License for more details.
 *
 * You should have received a copy of the GNU General Public License
 * along with this program.  If not, see <https://www.gnu.org/licenses/>.
 ************************************************************************************************/

#include <api/interfaces/interfaces.h>
#include <scripting/scripting.h>

#include <entityhandle.h>
#include "ehandle.h"

void Bridge_EntitySystem_Spawn(void* pEntity, void* pKeyValues)
{
    g_pEntSystem->Spawn(pEntity, pKeyValues);
}

void Bridge_EntitySystem_Despawn(void* pEntity)
{
    g_pEntSystem->Despawn(pEntity);
}

void* Bridge_EntitySystem_GetEntitySystem()
{
    return g_pEntSystem->GetEntitySystem();
}

bool Bridge_EntitySystem_IsValid()
{
    return Bridge_EntitySystem_GetEntitySystem() != nullptr;
}

DEFINE_NATIVE("EntitySystem.Spawn", Bridge_EntitySystem_Spawn);
DEFINE_NATIVE("EntitySystem.Despawn", Bridge_EntitySystem_Despawn);
DEFINE_NATIVE("EntitySystem.GetEntitySystem", Bridge_EntitySystem_GetEntitySystem);
DEFINE_NATIVE("EntitySystem.IsValid", Bridge_EntitySystem_IsValid);
