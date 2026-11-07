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

#include "function.h"

#include <api/interfaces/interfaces.h>
#include <api/shared/profiler.h>

FunctionHook::FunctionHook(void* callback, std::string signatureName)
    : m_sName(signatureName), m_bManaged(true)
{
    PROFILE_FUNCTION();

    m_fnResolve = [this, callback, signatureName = std::move(signatureName)]() {
        SetHookFunction(signatureName, callback);
    };
    RegisterManagedHook(this);
}

FunctionHook::FunctionHook(void* callback, std::function<void*()> addressResolver, std::string name)
    : m_sName(std::move(name)), m_bManaged(true)
{
    PROFILE_FUNCTION();

    m_fnResolve = [this, callback, addressResolver = std::move(addressResolver)]() {
        SetHookFunction(addressResolver(), callback);
    };
    RegisterManagedHook(this);
}

FunctionHook::~FunctionHook()
{
    PROFILE_FUNCTION();

    if (m_bManaged)
        UnregisterManagedHook(this);
}

bool FunctionHook::Install()
{
    PROFILE_FUNCTION();

    if (!m_bManaged) return false;
    if (m_oHook) return true;

    m_fnResolve();
    if (!m_oHook) return false;

    Enable();
    return true;
}

void FunctionHook::Uninstall()
{
    PROFILE_FUNCTION();

    Disable();
    m_oHook.reset();
}

const char* FunctionHook::GetName() const
{
    PROFILE_FUNCTION();

    return m_sName.c_str();
}

void FunctionHook::Enable()
{
    PROFILE_FUNCTION();

    if (!m_oHook || IsEnabled()) return;

    auto result = m_oHook.enable(); // due to nodiscard
}

void FunctionHook::Disable()
{
    PROFILE_FUNCTION();

    if (!m_oHook || !IsEnabled()) return;

    auto result = m_oHook.disable(); // due to nodiscard
}

void* FunctionHook::GetOriginal()
{
    PROFILE_FUNCTION();

    return (void*)(m_oHook.trampoline().address());
}

bool FunctionHook::IsEnabled()
{
    PROFILE_FUNCTION();

    return m_oHook.enabled();
}

void FunctionHook::SetHookFunction(const std::string& functionSignature, void* callback)
{
    PROFILE_FUNCTION();

    void* functionAddress = g_pGameDataManager->GetSignatures()->Fetch(functionSignature);
    if (!functionAddress) return;

    m_oHook = safetyhook::create_inline(functionAddress, callback, safetyhook::InlineHook::Flags::StartDisabled);
}

void FunctionHook::SetHookFunction(void* functionAddress, void* callback)
{
    PROFILE_FUNCTION();

    if (!functionAddress) return;

    m_oHook = safetyhook::create_inline(functionAddress, callback, safetyhook::InlineHook::Flags::StartDisabled);
}
