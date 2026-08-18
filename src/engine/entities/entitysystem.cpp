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

#include "entitysystem.h"

#include <cstdint>

#include <public/entity2/entitykeyvalues.h>
#include <public/entity2/entitysystem.h>
#include <public/iserver.h>
#include <public/gametrace.h>

#include "listener.h"

#include <api/interfaces/interfaces.h>
#include <s2binlib/s2binlib.h>

typedef void (*CBaseEntity_DispatchSpawn)(void*, void*);
typedef void (*UTIL_Remove)(void*);

CGameEntitySystem* g_pGameEntitySystem = nullptr;

extern void* g_pTraceManager;
extern void* g_pOnStartupServerCallback;

IFunctionHook* g_pTraceShapeHook = nullptr;
IVFunctionHook* g_pStartupServerHook = nullptr;

bool g_bDone = false;

CGameEntitySystem* GameEntitySystem()
{
    return g_pGameEntitySystem;
}

void TraceShapeHook(void* _this, Ray_t& ray, Vector& start, Vector& end, CTraceFilter* filter, trace_t* trace);
void StartupServerHook(void* _this, const GameSessionConfiguration_t& config, ISource2WorldSession* a, const char* b);

void CEntSystem::Initialize()
{
    g_pTraceShapeHook = g_pHooksManager->CreateFunctionHook();
    g_pTraceShapeHook->SetHookFunction(g_pGameDataManager->GetSignatures()->Fetch("TraceShape"), reinterpret_cast<void*>(TraceShapeHook));
    g_pTraceShapeHook->Enable();

    void* netserverservice = nullptr;
    s2binlib_find_vtable("engine2", "CNetworkServerService", &netserverservice);

    g_pStartupServerHook = g_pHooksManager->CreateVFunctionHook();
    g_pStartupServerHook->SetHookFunction(netserverservice, g_pGameDataManager->GetOffsets()->Fetch("INetworkServerService::StartupServer"), reinterpret_cast<void*>(StartupServerHook), true);
    g_pStartupServerHook->Enable();
}

void CEntSystem::Shutdown()
{
    g_pTraceShapeHook->Disable();
    g_pHooksManager->DestroyFunctionHook(g_pTraceShapeHook);
    g_pTraceShapeHook = nullptr;

    g_pStartupServerHook->Disable();
    g_pHooksManager->DestroyVFunctionHook(g_pStartupServerHook);
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

void StartupServerHook(void* _this, const GameSessionConfiguration_t& config, ISource2WorldSession* a, const char* b)
{
    reinterpret_cast<decltype(&StartupServerHook)>(g_pStartupServerHook->GetOriginal())(_this, config, a, b);

    if (g_pOnStartupServerCallback)
    {
        reinterpret_cast<void(*)()>(g_pOnStartupServerCallback)();
    }

    if (g_bDone) return;

    CGameEntitySystem* entSystem = *reinterpret_cast<CGameEntitySystem**>((uintptr_t)(g_pGameResources)+g_pGameDataManager->GetOffsets()->Fetch("GameEntitySystem"));
    g_pGameEntitySystem = entSystem;
    g_pGameEntitySystem->AddListenerEntity(&g_entityListener);

    g_bDone = true;
}

void CEntSystem::Spawn(void* pEntity, void* pKeyValues)
{
    static auto sig = g_pGameDataManager->GetSignatures()->Fetch("CBaseEntity::DispatchSpawn");

    reinterpret_cast<CBaseEntity_DispatchSpawn>(sig)(pEntity, pKeyValues);
}

void CEntSystem::Despawn(void* pEntity)
{
    static auto sig = g_pGameDataManager->GetSignatures()->Fetch("UTIL::Remove");

    reinterpret_cast<UTIL_Remove>(sig)(pEntity);
}

void CEntSystem::AddEntityListener(IEntityListener* listener)
{
    g_pGameEntitySystem->AddListenerEntity(listener);
}

void CEntSystem::RemoveEntityListener(IEntityListener* listener)
{
    g_pGameEntitySystem->RemoveListenerEntity(listener);
}

CEntitySystem* CEntSystem::GetEntitySystem()
{
    return g_pGameEntitySystem;
}
