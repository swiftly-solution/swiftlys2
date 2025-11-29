/************************************************************************************************
 *  SwiftlyS2 is a scripting framework for Source2-based games.
 *  Copyright (C) 2025 Swiftly Solution SRL via Sava Andrei-Sebastian and it's contributors
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

#include "entitysystem.h"

#include <cstdint>

#include <core/bridge/metamod.h>

#include <public/entity2/entitykeyvalues.h>
#include <public/entity2/entitysystem.h>
#include <public/iserver.h>
#include <public/gametrace.h>

#include "listener.h"

#include <api/interfaces/manager.h>
#include <s2binlib/s2binlib.h>


typedef void (*CBaseEntity_DispatchSpawn)(void*, void*);
typedef void (*UTIL_Remove)(void*);
typedef void* (*UTIL_CreateEntityByName)(const char*, int);
typedef void (*CEntityInstance_AcceptInput)(void*, const char*, void*, void*, variant_t*, int);
typedef void (*CEntitySystem_AddEntityIOEvent)(void*, void*, const char*, void*, void*, variant_t*, float, int, void*, void*);

CGameEntitySystem* g_pGameEntitySystem = nullptr;

void* g_pGameRules = nullptr;

extern void* g_pOnEntityTakeDamageCallback;
extern void* g_pTraceManager;
extern void* g_pOnStartupServerCallback;

IFunctionHook* g_pOnEntityTakeDamageHook = nullptr;
IFunctionHook* g_pTraceShapeHook = nullptr;
IVFunctionHook* g_pStartupServerHook = nullptr;

bool g_bDone = false;

CGameEntitySystem* GameEntitySystem()
{
    return g_pGameEntitySystem;
}

int64_t TakeDamageHook(void* baseEntity, void* info, void* idk);
void TraceShapeHook(void* _this, Ray_t& ray, Vector& start, Vector& end, CTraceFilter* filter, trace_t* trace);
void StartupServerHook(void* _this, const GameSessionConfiguration_t& config, ISource2WorldSession* a, const char* b);

void CEntSystem::Initialize()
{
    auto hooksmanager = g_ifaceService.FetchInterface<IHooksManager>(HOOKSMANAGER_INTERFACE_VERSION);
    auto gamedata = g_ifaceService.FetchInterface<IGameDataManager>(GAMEDATA_INTERFACE_VERSION);

    g_pOnEntityTakeDamageHook = hooksmanager->CreateFunctionHook();
    g_pOnEntityTakeDamageHook->SetHookFunction(gamedata->GetSignatures()->Fetch("CBaseEntity::TakeDamage"), reinterpret_cast<void*>(TakeDamageHook));
    g_pOnEntityTakeDamageHook->Enable();

    g_pTraceShapeHook = hooksmanager->CreateFunctionHook();
    g_pTraceShapeHook->SetHookFunction(gamedata->GetSignatures()->Fetch("TraceShape"), reinterpret_cast<void*>(TraceShapeHook));
    g_pTraceShapeHook->Enable();

    void* netserverservice = nullptr;
    s2binlib_find_vtable("engine2", "CNetworkServerService", &netserverservice);

    g_pStartupServerHook = hooksmanager->CreateVFunctionHook();
    g_pStartupServerHook->SetHookFunction(netserverservice, gamedata->GetOffsets()->Fetch("INetworkServerService::StartupServer"), reinterpret_cast<void*>(StartupServerHook), true);
    g_pStartupServerHook->Enable();
}

void CEntSystem::Shutdown()
{
    auto hooksmanager = g_ifaceService.FetchInterface<IHooksManager>(HOOKSMANAGER_INTERFACE_VERSION);

    g_pOnEntityTakeDamageHook->Disable();
    hooksmanager->DestroyFunctionHook(g_pOnEntityTakeDamageHook);
    g_pOnEntityTakeDamageHook = nullptr;

    g_pTraceShapeHook->Disable();
    hooksmanager->DestroyFunctionHook(g_pTraceShapeHook);
    g_pTraceShapeHook = nullptr;

    g_pStartupServerHook->Disable();
    hooksmanager->DestroyVFunctionHook(g_pStartupServerHook);
    g_pStartupServerHook = nullptr;

    g_pGameEntitySystem->RemoveListenerEntity(&g_entityListener);
}

void TraceShapeHook(void* _this, Ray_t& ray, Vector& start, Vector& end, CTraceFilter* filter, trace_t* trace)
{
    if (g_pTraceManager == nullptr)
    {
        g_pTraceManager = _this;
    }

    reinterpret_cast<void(*)(void*, Ray_t&, Vector&, Vector&, CTraceFilter*, trace_t*)>(g_pTraceShapeHook->GetOriginal())(_this, ray, start, end, filter, trace);
}

int64_t TakeDamageHook(void* baseEntity, void* info, void* damageResult)
{
    if (g_pOnEntityTakeDamageCallback)
        if (reinterpret_cast<bool(*)(void*, void*, void*)>(g_pOnEntityTakeDamageCallback)(baseEntity, info, damageResult) == false)
            return 0;

    return reinterpret_cast<int64_t(*)(void*, void*, void*)>(g_pOnEntityTakeDamageHook->GetOriginal())(baseEntity, info, damageResult);
}

void StartupServerHook(void* _this, const GameSessionConfiguration_t& config, ISource2WorldSession* a, const char* b)
{
    reinterpret_cast<decltype(&StartupServerHook)>(g_pStartupServerHook->GetOriginal())(_this, config, a, b);

    if (g_pOnStartupServerCallback)
    {
        reinterpret_cast<void(*)()>(g_pOnStartupServerCallback)();
    }
    
    if (g_bDone) return;

    auto pGameResService = g_ifaceService.FetchInterface<IGameResourceService>(GAMERESOURCESERVICESERVER_INTERFACE_VERSION);
    if (!pGameResService) return;

    auto gamedata = g_ifaceService.FetchInterface<IGameDataManager>(GAMEDATA_INTERFACE_VERSION);

    CGameEntitySystem* entSystem = *reinterpret_cast<CGameEntitySystem**>((uintptr_t)(pGameResService)+gamedata->GetOffsets()->Fetch("GameEntitySystem"));
    g_pGameEntitySystem = entSystem;
    g_pGameEntitySystem->AddListenerEntity(&g_entityListener);

    g_bDone = true;
}

void CEntSystem::Spawn(void* pEntity, void* pKeyValues)
{
    static auto gamedata = g_ifaceService.FetchInterface<IGameDataManager>(GAMEDATA_INTERFACE_VERSION);
    static auto sig = gamedata->GetSignatures()->Fetch("CBaseEntity::DispatchSpawn");

    reinterpret_cast<CBaseEntity_DispatchSpawn>(sig)(pEntity, pKeyValues);
}

void CEntSystem::Despawn(void* pEntity)
{
    static auto gamedata = g_ifaceService.FetchInterface<IGameDataManager>(GAMEDATA_INTERFACE_VERSION);
    static auto sig = gamedata->GetSignatures()->Fetch("UTIL::Remove");

    reinterpret_cast<UTIL_Remove>(sig)(pEntity);
}

void* CEntSystem::CreateEntityByName(const char* name)
{
    static auto gamedata = g_ifaceService.FetchInterface<IGameDataManager>(GAMEDATA_INTERFACE_VERSION);
    static auto sig = gamedata->GetSignatures()->Fetch("UTIL::CreateEntityByName");

    return reinterpret_cast<UTIL_CreateEntityByName>(sig)(name, -1);
}

void CEntSystem::AcceptInput(void* pEntity, const char* input, void* activator, void* caller, InputType value, int outputID)
{
    static auto gamedata = g_ifaceService.FetchInterface<IGameDataManager>(GAMEDATA_INTERFACE_VERSION);

    variant_t var;
    if (std::holds_alternative<int32_t>(value))
        var = std::get<int32_t>(value);
    else if (std::holds_alternative<uint32_t>(value))
        var = std::get<uint32_t>(value);
    else if (std::holds_alternative<int64_t>(value))
        var = (int64)std::get<int64_t>(value);
    else if (std::holds_alternative<uint64_t>(value))
        var = (uint64)std::get<uint64_t>(value);
    else if (std::holds_alternative<float>(value))
        var = std::get<float>(value);
    else if (std::holds_alternative<double>(value))
        var = std::get<double>(value);
    else if (std::holds_alternative<bool>(value))
        var = std::get<bool>(value);
    else if (std::holds_alternative<const char*>(value))
        var = std::get<const char*>(value);
    else
        var = 0;

    static auto sig = gamedata->GetSignatures()->Fetch("CEntityInstance::AcceptInput");
    reinterpret_cast<CEntityInstance_AcceptInput>(sig)(pEntity, input, activator, caller, &var, outputID);
}

void CEntSystem::AddEntityIOEvent(void* pEntity, const char* input, void* activator, void* caller, InputType value, float delay)
{
    static auto gamedata = g_ifaceService.FetchInterface<IGameDataManager>(GAMEDATA_INTERFACE_VERSION);

    variant_t var;
    if (std::holds_alternative<int32_t>(value))
        var = std::get<int32_t>(value);
    else if (std::holds_alternative<uint32_t>(value))
        var = std::get<uint32_t>(value);
    else if (std::holds_alternative<int64_t>(value))
        var = (int64)std::get<int64_t>(value);
    else if (std::holds_alternative<uint64_t>(value))
        var = (uint64)std::get<uint64_t>(value);
    else if (std::holds_alternative<float>(value))
        var = std::get<float>(value);
    else if (std::holds_alternative<double>(value))
        var = std::get<double>(value);
    else if (std::holds_alternative<bool>(value))
        var = std::get<bool>(value);
    else if (std::holds_alternative<const char*>(value))
        var = std::get<const char*>(value);
    else
        var = 0;

    static auto sig = gamedata->GetSignatures()->Fetch("CEntitySystem::AddEntityIOEvent");
    reinterpret_cast<CEntitySystem_AddEntityIOEvent>(sig)(g_pGameEntitySystem, pEntity, input, activator, caller, &var, delay, 0, nullptr, nullptr);
}

bool CEntSystem::IsValidEntity(void* pEntity)
{
    return pEntity != nullptr;
}

void CEntSystem::AddEntityListener(IEntityListener* listener)
{
    g_pGameEntitySystem->AddListenerEntity(listener);
}

void CEntSystem::RemoveEntityListener(IEntityListener* listener)
{
    g_pGameEntitySystem->RemoveListenerEntity(listener);
}

void* CEntSystem::GetGameRules()
{
    return g_pGameRules;
}

CEntitySystem* CEntSystem::GetEntitySystem()
{
    return g_pGameEntitySystem;
}