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

#include <api/interfaces/manager.h>
#include <scripting/scripting.h>

#include <entityhandle.h>
#include "ehandle.h"

void Bridge_EntitySystem_Spawn(void* pEntity, void* pKeyValues)
{
    static auto entsystem = g_ifaceService.FetchInterface<IEntitySystem>(ENTITYSYSTEM_INTERFACE_VERSION);
    entsystem->Spawn(pEntity, pKeyValues);
}

void Bridge_EntitySystem_Despawn(void* pEntity)
{
    static auto entsystem = g_ifaceService.FetchInterface<IEntitySystem>(ENTITYSYSTEM_INTERFACE_VERSION);
    entsystem->Despawn(pEntity);
}

void* Bridge_EntitySystem_CreateEntityByName(const char* name)
{
    static auto entsystem = g_ifaceService.FetchInterface<IEntitySystem>(ENTITYSYSTEM_INTERFACE_VERSION);
    return entsystem->CreateEntityByName(name);
}

void* Bridge_EntitySystem_GetEntitySystem()
{
    static auto entsystem = g_ifaceService.FetchInterface<IEntitySystem>(ENTITYSYSTEM_INTERFACE_VERSION);
    return entsystem->GetEntitySystem();
}

bool Bridge_EntitySystem_IsValid()
{
    static auto entsystem = g_ifaceService.FetchInterface<IEntitySystem>(ENTITYSYSTEM_INTERFACE_VERSION);
    return entsystem->GetEntitySystem() != nullptr;
}

DEFINE_NATIVE("EntitySystem.Spawn", Bridge_EntitySystem_Spawn);
DEFINE_NATIVE("EntitySystem.Despawn", Bridge_EntitySystem_Despawn);
DEFINE_NATIVE("EntitySystem.CreateEntityByName", Bridge_EntitySystem_CreateEntityByName);
DEFINE_NATIVE("EntitySystem.GetEntitySystem", Bridge_EntitySystem_GetEntitySystem);
DEFINE_NATIVE("EntitySystem.IsValid", Bridge_EntitySystem_IsValid);
