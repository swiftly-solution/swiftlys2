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

CGameEntitySystem* g_pGameEntitySystem = nullptr;

extern void* g_pOnStartupServerCallback;

IVFunctionHook* g_pStartupServerHook = nullptr;

bool g_bDone = false;

CGameEntitySystem* GameEntitySystem()
{
    return g_pGameEntitySystem;
}

void StartupServerHook(void* _this, const GameSessionConfiguration_t& config, ISource2WorldSession* a, const char* b);

void CEntSystem::Initialize()
{
    void* netserverservice = nullptr;
    g_pS2BinLib->FindVtable("engine2", "CNetworkServerService", &netserverservice);

    g_pStartupServerHook = g_pHooksManager->CreateVFunctionHook();
    g_pStartupServerHook->SetHookFunction(netserverservice, g_pGameDataManager->GetOffsets()->Fetch("INetworkServerService::StartupServer"), reinterpret_cast<void*>(StartupServerHook), true);
    g_pStartupServerHook->Enable();
}

void CEntSystem::Shutdown()
{
    g_pStartupServerHook->Disable();
    g_pHooksManager->DestroyVFunctionHook(g_pStartupServerHook);
    g_pStartupServerHook = nullptr;

    g_pGameEntitySystem->RemoveListenerEntity(&g_entityListener);
}

void StartupServerHook(void* _this, const GameSessionConfiguration_t& config, ISource2WorldSession* a, const char* b)
{
    reinterpret_cast<decltype(&StartupServerHook)>(g_pStartupServerHook->GetOriginal())(_this, config, a, b);

    if (!g_bDone)
    {
        CGameEntitySystem* entSystem = *reinterpret_cast<CGameEntitySystem**>((uintptr_t)(g_pGameResources)+g_pGameDataManager->GetOffsets()->Fetch("GameEntitySystem"));
        g_pGameEntitySystem = entSystem;
        g_pGameEntitySystem->AddListenerEntity(&g_entityListener);

        g_bDone = true;
    }

    if (g_pOnStartupServerCallback)
    {
        reinterpret_cast<void(*)()>(g_pOnStartupServerCallback)();
    }
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
