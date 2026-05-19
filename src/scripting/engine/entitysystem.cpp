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

typedef void (*CEntityInstance_AcceptInput)(void*, const char*, void*, void*, void*, int, void*);
typedef void (*CEntitySystem_AddEntityIOEvent)(void*, void*, const char*, void*, void*, void*, float, int, void*, void*);

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

void Bridge_EntitySystem_AcceptInput(void* pEntity, const char* input, void* pActivator, void* pCaller, void* variant, int32_t outputID)
{
    static auto gamedata = g_ifaceService.FetchInterface<IGameDataManager>(GAMEDATA_INTERFACE_VERSION);
    static auto sig = gamedata->GetSignatures()->Fetch("CEntityInstance::AcceptInput");

    reinterpret_cast<CEntityInstance_AcceptInput>(sig)(pEntity, input, pActivator, pCaller, variant, outputID, nullptr);
}

void Bridge_EntitySystem_AddEntityIOEvent(void* pEntity, const char* input, void* pActivator, void* pCaller, void* variant, float delay)
{
    static auto entsystem = g_ifaceService.FetchInterface<IEntitySystem>(ENTITYSYSTEM_INTERFACE_VERSION);
    static auto gamedata = g_ifaceService.FetchInterface<IGameDataManager>(GAMEDATA_INTERFACE_VERSION);
    static auto sig = gamedata->GetSignatures()->Fetch("CEntitySystem::AddEntityIOEvent");

    reinterpret_cast<CEntitySystem_AddEntityIOEvent>(sig)(entsystem->GetEntitySystem(), pEntity, input, pActivator, pCaller, variant, delay, 0, nullptr, nullptr);
}

void* Bridge_EntitySystem_GetEntitySystem()
{
    static auto entsystem = g_ifaceService.FetchInterface<IEntitySystem>(ENTITYSYSTEM_INTERFACE_VERSION);
    return entsystem->GetEntitySystem();
}

uint64_t Bridge_EntitySystem_HookEntityOutput(const char* className, const char* outputName, void* callback)
{
    static auto hooksystem = g_ifaceService.FetchInterface<IHooksManager>(HOOKSMANAGER_INTERFACE_VERSION);
    return hooksystem->CreateEntityHookOutput(className, outputName, callback);
}

void Bridge_EntitySystem_UnhookEntityOutput(uint64_t hookid)
{
    static auto hooksystem = g_ifaceService.FetchInterface<IHooksManager>(HOOKSMANAGER_INTERFACE_VERSION);
    hooksystem->DestroyEntityHookOutput(hookid);
}

bool Bridge_EntitySystem_IsValid()
{
    static auto entsystem = g_ifaceService.FetchInterface<IEntitySystem>(ENTITYSYSTEM_INTERFACE_VERSION);
    return entsystem->GetEntitySystem() != nullptr;
}

DEFINE_NATIVE("EntitySystem.Spawn", Bridge_EntitySystem_Spawn);
DEFINE_NATIVE("EntitySystem.Despawn", Bridge_EntitySystem_Despawn);
DEFINE_NATIVE("EntitySystem.CreateEntityByName", Bridge_EntitySystem_CreateEntityByName);
DEFINE_NATIVE("EntitySystem.AcceptInput", Bridge_EntitySystem_AcceptInput);
DEFINE_NATIVE("EntitySystem.AddEntityIOEvent", Bridge_EntitySystem_AddEntityIOEvent);
DEFINE_NATIVE("EntitySystem.GetEntitySystem", Bridge_EntitySystem_GetEntitySystem);
DEFINE_NATIVE("EntitySystem.HookEntityOutput", Bridge_EntitySystem_HookEntityOutput);
DEFINE_NATIVE("EntitySystem.UnhookEntityOutput", Bridge_EntitySystem_UnhookEntityOutput);
DEFINE_NATIVE("EntitySystem.IsValid", Bridge_EntitySystem_IsValid);
