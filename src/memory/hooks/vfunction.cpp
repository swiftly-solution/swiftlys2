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

#include "vfunction.h"

#include <api/interfaces/interfaces.h>
#include <api/shared/profiler.h>
#include <core/entrypoint.h>

VFunctionHook::VFunctionHook(void* callback, std::string moduleName, std::string vtableName, std::string offsetName)
    : m_sName(offsetName), m_bManaged(true)
{
    PROFILE_FUNCTION();

    m_fnResolve = [this, callback, moduleName = std::move(moduleName), vtableName = std::move(vtableName), offsetName = std::move(offsetName)]() {
        void* vtable = nullptr;
        g_pS2BinLib->FindVtable(moduleName.c_str(), vtableName.c_str(), &vtable);
        if (!vtable) return;

        SetHookFunction(vtable, g_pGameDataManager->GetOffsets()->Fetch(offsetName), callback, true);
    };
    RegisterManagedHook(this);
}

VFunctionHook::VFunctionHook(void* callback, std::string interfaceName, std::string offsetName)
    : m_sName(offsetName), m_bManaged(true)
{
    PROFILE_FUNCTION();

    m_fnResolve = [this, callback, interfaceName = std::move(interfaceName), offsetName = std::move(offsetName)]() {
        SetHookFunction(interfaceName, g_pGameDataManager->GetOffsets()->Fetch(offsetName), callback);
    };
    RegisterManagedHook(this);
}

VFunctionHook::VFunctionHook(void* callback, std::function<void*()> instanceResolver, std::string offsetName)
    : m_sName(offsetName), m_bManaged(true)
{
    PROFILE_FUNCTION();

    m_fnResolve = [this, callback, instanceResolver = std::move(instanceResolver), offsetName = std::move(offsetName)]() {
        SetHookFunction(instanceResolver(), g_pGameDataManager->GetOffsets()->Fetch(offsetName), callback, false);
    };
    RegisterManagedHook(this);
}

VFunctionHook::~VFunctionHook()
{
    PROFILE_FUNCTION();

    if (m_bManaged)
        UnregisterManagedHook(this);
}

bool VFunctionHook::Install()
{
    PROFILE_FUNCTION();

    if (!m_bManaged) return false;
    if (m_oHook) return true;

    m_fnResolve();
    if (!m_oHook) return false;

    Enable();
    return true;
}

void VFunctionHook::Uninstall()
{
    PROFILE_FUNCTION();

    Disable();
    m_oHook.reset();
}

const char* VFunctionHook::GetName() const
{
    PROFILE_FUNCTION();

    return m_sName.c_str();
}

void VFunctionHook::SetHookFunction(const std::string& iface_name, int index, void* callback)
{
    PROFILE_FUNCTION();

    auto iface = g_SwiftlyCore.GetInterface(iface_name);
    if (!iface) return;

    void* trampoline_addr = nullptr;
    g_pS2BinLib->InstallTrampoline(((void*)((uintptr_t)(*(void**)iface) + 8 * index)), &trampoline_addr);
    if (!trampoline_addr) return;

    m_oHook = safetyhook::create_inline(trampoline_addr, callback, safetyhook::InlineHook::Flags::StartDisabled);
}

void VFunctionHook::SetHookFunction(void* instance, int index, void* callback, bool is_vtable)
{
    PROFILE_FUNCTION();

    if (!instance) return;

    void* trampoline_addr = nullptr;
    g_pS2BinLib->InstallTrampoline(is_vtable ? (void*)((uintptr_t)instance + 8 * index) : ((void*)((uintptr_t)(*(void**)instance) + 8 * index)), &trampoline_addr);
    if (!trampoline_addr) return;

    m_oHook = safetyhook::create_inline(trampoline_addr, callback, safetyhook::InlineHook::Flags::StartDisabled);
}

void VFunctionHook::Enable()
{
    PROFILE_FUNCTION();

    if (!m_oHook || IsEnabled()) return;

    auto res = m_oHook.enable(); // due to nodiscard
}

void VFunctionHook::Disable()
{
    PROFILE_FUNCTION();

    if (!m_oHook || !IsEnabled()) return;

    auto res = m_oHook.disable(); // due to nodiscard
}

void* VFunctionHook::GetOriginal()
{
    PROFILE_FUNCTION();

    return (void*)(m_oHook.trampoline().address());
}

bool VFunctionHook::IsEnabled()
{
    PROFILE_FUNCTION();

    return m_oHook.enabled();
}
