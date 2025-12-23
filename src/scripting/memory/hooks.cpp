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
#include <cstdio>
#include <scripting/scripting.h>

void* Bridge_Hooks_AllocateHook()
{
    auto hooksmanager = g_ifaceService.FetchInterface<IHooksManager>(HOOKSMANAGER_INTERFACE_VERSION);
    return hooksmanager->CreateFunctionHook();
}

void* Bridge_Hooks_AllocateVHook()
{
    auto hooksmanager = g_ifaceService.FetchInterface<IHooksManager>(HOOKSMANAGER_INTERFACE_VERSION);
    return hooksmanager->CreateVFunctionHook();
}

void* Bridge_Hooks_AllocateMHook()
{
    auto hooksmanager = g_ifaceService.FetchInterface<IHooksManager>(HOOKSMANAGER_INTERFACE_VERSION);
    return hooksmanager->CreateMFunctionHook();
}

void Bridge_Hooks_DeallocateHook(void* hook)
{
    auto hooksmanager = g_ifaceService.FetchInterface<IHooksManager>(HOOKSMANAGER_INTERFACE_VERSION);
    hooksmanager->DestroyFunctionHook((IFunctionHook*)hook);
}

void Bridge_Hooks_DeallocateVHook(void* hook)
{
    auto hooksmanager = g_ifaceService.FetchInterface<IHooksManager>(HOOKSMANAGER_INTERFACE_VERSION);
    hooksmanager->DestroyVFunctionHook((IVFunctionHook*)hook);
}

void Bridge_Hooks_DeallocateMHook(void* hook)
{
    auto hooksmanager = g_ifaceService.FetchInterface<IHooksManager>(HOOKSMANAGER_INTERFACE_VERSION);
    hooksmanager->DestroyMFunctionHook((IMFunctionHook*)hook);
}

void Bridge_Hooks_SetHook(void* hook, void* func, void* callback)
{
    ((IFunctionHook*)hook)->SetHookFunction(func, callback);
}

void Bridge_Hooks_SetVHook(void* hook, void* entityOrVTable, int index, void* callback, bool isVTable)
{
    ((IVFunctionHook*)hook)->SetHookFunction(entityOrVTable, index, callback, isVTable);
}

void Bridge_Hooks_SetMHook(void* hook, void* addr, void* callback)
{
    ((IMFunctionHook*)hook)->SetHookFunction(addr, callback);
}

void Bridge_Hooks_EnableHook(void* hook)
{
    ((IFunctionHook*)hook)->Enable();
}

void Bridge_Hooks_EnableVHook(void* hook)
{
    ((IVFunctionHook*)hook)->Enable();
}

void Bridge_Hooks_EnableMHook(void* hook)
{
    ((IMFunctionHook*)hook)->Enable();
}

void Bridge_Hooks_DisableHook(void* hook)
{
    ((IFunctionHook*)hook)->Disable();
}

void Bridge_Hooks_DisableVHook(void* hook)
{
    ((IVFunctionHook*)hook)->Disable();
}

void Bridge_Hooks_DisableMHook(void* hook)
{
    ((IMFunctionHook*)hook)->Disable();
}

bool Bridge_Hooks_IsHookEnabled(void* hook)
{
    return ((IFunctionHook*)hook)->IsEnabled();
}

bool Bridge_Hooks_IsVHookEnabled(void* hook)
{
    return ((IVFunctionHook*)hook)->IsEnabled();
}

bool Bridge_Hooks_IsMHookEnabled(void* hook)
{
    return ((IMFunctionHook*)hook)->IsEnabled();
}

void* Bridge_Hooks_GetHookOriginal(void* hook)
{
    return ((IFunctionHook*)hook)->GetOriginal();
}

void* Bridge_Hooks_GetVHookOriginal(void* hook)
{
    return ((IVFunctionHook*)hook)->GetOriginal();
}

DEFINE_NATIVE("Hooks.AllocateHook", Bridge_Hooks_AllocateHook);
DEFINE_NATIVE("Hooks.AllocateVHook", Bridge_Hooks_AllocateVHook);
DEFINE_NATIVE("Hooks.AllocateMHook", Bridge_Hooks_AllocateMHook);
DEFINE_NATIVE("Hooks.DeallocateHook", Bridge_Hooks_DeallocateHook);
DEFINE_NATIVE("Hooks.DeallocateVHook", Bridge_Hooks_DeallocateVHook);
DEFINE_NATIVE("Hooks.DeallocateMHook", Bridge_Hooks_DeallocateMHook);
DEFINE_NATIVE("Hooks.SetHook", Bridge_Hooks_SetHook);
DEFINE_NATIVE("Hooks.SetVHook", Bridge_Hooks_SetVHook);
DEFINE_NATIVE("Hooks.SetMHook", Bridge_Hooks_SetMHook);
DEFINE_NATIVE("Hooks.EnableHook", Bridge_Hooks_EnableHook);
DEFINE_NATIVE("Hooks.EnableVHook", Bridge_Hooks_EnableVHook);
DEFINE_NATIVE("Hooks.EnableMHook", Bridge_Hooks_EnableMHook);
DEFINE_NATIVE("Hooks.DisableHook", Bridge_Hooks_DisableHook);
DEFINE_NATIVE("Hooks.DisableVHook", Bridge_Hooks_DisableVHook);
DEFINE_NATIVE("Hooks.DisableMHook", Bridge_Hooks_DisableMHook);
DEFINE_NATIVE("Hooks.IsHookEnabled", Bridge_Hooks_IsHookEnabled);
DEFINE_NATIVE("Hooks.IsVHookEnabled", Bridge_Hooks_IsVHookEnabled);
DEFINE_NATIVE("Hooks.IsMHookEnabled", Bridge_Hooks_IsMHookEnabled);
DEFINE_NATIVE("Hooks.GetHookOriginal", Bridge_Hooks_GetHookOriginal);
DEFINE_NATIVE("Hooks.GetVHookOriginal", Bridge_Hooks_GetVHookOriginal);