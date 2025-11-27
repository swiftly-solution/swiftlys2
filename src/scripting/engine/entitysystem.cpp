/************************************************************************************************
 * SwiftlyS2 is a scripting framework for Source2-based games.
 * Copyright (C) 2025 Swiftly Solution SRL via Sava Andrei-Sebastian and it's contributors
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

typedef void (*CEntityInstance_AcceptInput)(void*, const char*, void*, void*, void*, int);
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

    reinterpret_cast<CEntityInstance_AcceptInput>(sig)(pEntity, input, pActivator, pCaller, variant, outputID);
}

void Bridge_EntitySystem_AddEntityIOEvent(void* pEntity, const char* input, void* pActivator, void* pCaller, void* variant, float delay)
{
    static auto entsystem = g_ifaceService.FetchInterface<IEntitySystem>(ENTITYSYSTEM_INTERFACE_VERSION);
    static auto gamedata = g_ifaceService.FetchInterface<IGameDataManager>(GAMEDATA_INTERFACE_VERSION);
    static auto sig = gamedata->GetSignatures()->Fetch("CEntitySystem::AddEntityIOEvent");

    reinterpret_cast<CEntitySystem_AddEntityIOEvent>(sig)(entsystem->GetEntitySystem(), pEntity, input, pActivator, pCaller, variant, delay, 0, nullptr, nullptr);
}

bool Bridge_EntitySystem_IsValidEntity(void* pEntity)
{
    static auto entsystem = g_ifaceService.FetchInterface<IEntitySystem>(ENTITYSYSTEM_INTERFACE_VERSION);
    return entsystem->IsValidEntity(pEntity);
}

void* Bridge_EntitySystem_GetGameRules()
{
    static auto entsystem = g_ifaceService.FetchInterface<IEntitySystem>(ENTITYSYSTEM_INTERFACE_VERSION);
    return entsystem->GetGameRules();
}

void* Bridge_EntitySystem_GetEntitySystem()
{
    static auto entsystem = g_ifaceService.FetchInterface<IEntitySystem>(ENTITYSYSTEM_INTERFACE_VERSION);
    return entsystem->GetEntitySystem();
}


void* Bridge_EntitySystem_GetFirstActiveEntity()
{
    static auto entsystem = g_ifaceService.FetchInterface<IEntitySystem>(ENTITYSYSTEM_INTERFACE_VERSION);
    return entsystem->GetEntitySystem()->m_EntityList.m_pFirstActiveEntity;
}


bool Bridge_EntitySystem_EntityHandleIsValid(uint32 ihandle)
{
    CEntityHandle handle(ihandle);
    return handle.IsValid() && handle.Get() != nullptr;
}

void* Bridge_EntitySystem_EntityHandleGet(uint32 ihandle)
{
    CEntityHandle handle(ihandle);
    return handle.Get();
}

int Bridge_EntitySystem_GetEntityHandleFromEntity(CEntityInstance* pEntity)
{
    return pEntity->GetRefEHandle().ToInt();
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


void* Bridge_EntitySystem_GetEntityByIndex(uint32_t index)
{
    static auto entsystem = g_ifaceService.FetchInterface<IEntitySystem>(ENTITYSYSTEM_INTERFACE_VERSION);
    return entsystem->GetEntitySystem()->GetEntityInstance(CEntityIndex(index));
}

DEFINE_NATIVE("EntitySystem.Spawn", Bridge_EntitySystem_Spawn);
DEFINE_NATIVE("EntitySystem.Despawn", Bridge_EntitySystem_Despawn);
DEFINE_NATIVE("EntitySystem.CreateEntityByName", Bridge_EntitySystem_CreateEntityByName);
DEFINE_NATIVE("EntitySystem.AcceptInput", Bridge_EntitySystem_AcceptInput);
DEFINE_NATIVE("EntitySystem.AddEntityIOEvent", Bridge_EntitySystem_AddEntityIOEvent);
DEFINE_NATIVE("EntitySystem.IsValidEntity", Bridge_EntitySystem_IsValidEntity);
DEFINE_NATIVE("EntitySystem.GetGameRules", Bridge_EntitySystem_GetGameRules);
DEFINE_NATIVE("EntitySystem.GetEntitySystem", Bridge_EntitySystem_GetEntitySystem);
DEFINE_NATIVE("EntitySystem.EntityHandleIsValid", Bridge_EntitySystem_EntityHandleIsValid);
DEFINE_NATIVE("EntitySystem.EntityHandleGet", Bridge_EntitySystem_EntityHandleGet);
DEFINE_NATIVE("EntitySystem.GetEntityHandleFromEntity", Bridge_EntitySystem_GetEntityHandleFromEntity);
DEFINE_NATIVE("EntitySystem.GetFirstActiveEntity", Bridge_EntitySystem_GetFirstActiveEntity);
DEFINE_NATIVE("EntitySystem.HookEntityOutput", Bridge_EntitySystem_HookEntityOutput);
DEFINE_NATIVE("EntitySystem.UnhookEntityOutput", Bridge_EntitySystem_UnhookEntityOutput);
DEFINE_NATIVE("EntitySystem.GetEntityByIndex", Bridge_EntitySystem_GetEntityByIndex);