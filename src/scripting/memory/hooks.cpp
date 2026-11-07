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
#include <api/shared/profiler.h>
#include <scripting/scripting.h>

void* Bridge_Hooks_AllocateHook()
{
    PROFILE_FUNCTION();

    return g_pHooksManager->CreateFunctionHook();
}

void* Bridge_Hooks_AllocateMHook()
{
    PROFILE_FUNCTION();

    return g_pHooksManager->CreateMFunctionHook();
}

void Bridge_Hooks_DeallocateHook(void* hook)
{
    PROFILE_FUNCTION();

    g_pHooksManager->DestroyFunctionHook((IFunctionHook*)hook);
}

void Bridge_Hooks_DeallocateMHook(void* hook)
{
    PROFILE_FUNCTION();

    g_pHooksManager->DestroyMFunctionHook((IMFunctionHook*)hook);
}

void Bridge_Hooks_SetHook(void* hook, void* func, void* callback)
{
    PROFILE_FUNCTION();

    ((IFunctionHook*)hook)->SetHookFunction(func, callback);
}

void Bridge_Hooks_SetMHook(void* hook, void* addr, void* callback)
{
    PROFILE_FUNCTION();

    ((IMFunctionHook*)hook)->SetHookFunction(addr, callback);
}

void Bridge_Hooks_EnableHook(void* hook)
{
    PROFILE_FUNCTION();

    ((IFunctionHook*)hook)->Enable();
}

void Bridge_Hooks_EnableMHook(void* hook)
{
    PROFILE_FUNCTION();

    ((IMFunctionHook*)hook)->Enable();
}

void Bridge_Hooks_DisableHook(void* hook)
{
    PROFILE_FUNCTION();

    ((IFunctionHook*)hook)->Disable();
}

void Bridge_Hooks_DisableMHook(void* hook)
{
    PROFILE_FUNCTION();

    ((IMFunctionHook*)hook)->Disable();
}

bool Bridge_Hooks_IsHookEnabled(void* hook)
{
    PROFILE_FUNCTION();

    return ((IFunctionHook*)hook)->IsEnabled();
}

bool Bridge_Hooks_IsMHookEnabled(void* hook)
{
    PROFILE_FUNCTION();

    return ((IMFunctionHook*)hook)->IsEnabled();
}

void* Bridge_Hooks_GetHookOriginal(void* hook)
{
    PROFILE_FUNCTION();

    return ((IFunctionHook*)hook)->GetOriginal();
}

DEFINE_NATIVE("Hooks.AllocateHook", Bridge_Hooks_AllocateHook);
DEFINE_NATIVE("Hooks.AllocateMHook", Bridge_Hooks_AllocateMHook);
DEFINE_NATIVE("Hooks.DeallocateHook", Bridge_Hooks_DeallocateHook);
DEFINE_NATIVE("Hooks.DeallocateMHook", Bridge_Hooks_DeallocateMHook);
DEFINE_NATIVE("Hooks.SetHook", Bridge_Hooks_SetHook);
DEFINE_NATIVE("Hooks.SetMHook", Bridge_Hooks_SetMHook);
DEFINE_NATIVE("Hooks.EnableHook", Bridge_Hooks_EnableHook);
DEFINE_NATIVE("Hooks.EnableMHook", Bridge_Hooks_EnableMHook);
DEFINE_NATIVE("Hooks.DisableHook", Bridge_Hooks_DisableHook);
DEFINE_NATIVE("Hooks.DisableMHook", Bridge_Hooks_DisableMHook);
DEFINE_NATIVE("Hooks.IsHookEnabled", Bridge_Hooks_IsHookEnabled);
DEFINE_NATIVE("Hooks.IsMHookEnabled", Bridge_Hooks_IsMHookEnabled);
DEFINE_NATIVE("Hooks.GetHookOriginal", Bridge_Hooks_GetHookOriginal);