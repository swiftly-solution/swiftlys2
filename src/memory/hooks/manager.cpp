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

#include "manager.h"

#include <vector>

#include <api/interfaces/interfaces.h>
#include <api/shared/profiler.h>

#include <algorithm>
#include <format>

static std::vector<IManagedHook*>* g_pManagedHooks = nullptr;

static bool g_bHooksInitialized = false;

void RegisterManagedHook(IManagedHook* hook)
{
    PROFILE_FUNCTION();

    if (!g_pManagedHooks)
        g_pManagedHooks = new std::vector<IManagedHook*>();

    g_pManagedHooks->push_back(hook);
}

void UnregisterManagedHook(IManagedHook* hook)
{
    PROFILE_FUNCTION();

    if (!g_pManagedHooks) return;

    g_pManagedHooks->erase(std::remove(g_pManagedHooks->begin(), g_pManagedHooks->end(), hook), g_pManagedHooks->end());
}

IFunctionHook* HooksManager::CreateFunctionHook()
{
    PROFILE_FUNCTION();

    return new FunctionHook();
}

IVFunctionHook* HooksManager::CreateVFunctionHook()
{
    PROFILE_FUNCTION();

    return new VFunctionHook();
}

IMFunctionHook* HooksManager::CreateMFunctionHook()
{
    PROFILE_FUNCTION();

    return new MFunctionHook();
}

void HooksManager::DestroyFunctionHook(IFunctionHook* hook)
{
    PROFILE_FUNCTION();

    delete (FunctionHook*)hook;
}

void HooksManager::DestroyVFunctionHook(IVFunctionHook* hook)
{
    PROFILE_FUNCTION();

    delete (VFunctionHook*)hook;
}

void HooksManager::DestroyMFunctionHook(IMFunctionHook* hook)
{
    PROFILE_FUNCTION();

    delete (MFunctionHook*)hook;
}

void HooksManager::Initialize()
{
    PROFILE_FUNCTION();

    if (g_bHooksInitialized) return;
    g_bHooksInitialized = true;

    if (!g_pManagedHooks) return;

    for (auto* hook : *g_pManagedHooks)
    {
        if (!hook->Install())
            g_pLogger->Warning("Hooks", std::format("Couldn't install the hook '{}'.\n", hook->GetName()));
    }
}

void HooksManager::Shutdown()
{
    PROFILE_FUNCTION();

    if (!g_bHooksInitialized) return;
    g_bHooksInitialized = false;

    if (!g_pManagedHooks) return;

    for (auto it = g_pManagedHooks->rbegin(); it != g_pManagedHooks->rend(); ++it)
        (*it)->Uninstall();

    delete g_pManagedHooks;
    g_pManagedHooks = nullptr;
}
