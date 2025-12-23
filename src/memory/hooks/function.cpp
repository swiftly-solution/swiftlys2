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

#include <api/interfaces/manager.h>

void FunctionHook::Enable()
{
    if (IsEnabled()) return;

    m_oHook.enable();
}

void FunctionHook::Disable()
{
    if (!IsEnabled()) return;

    m_oHook.disable();
}

void* FunctionHook::GetOriginal()
{
    return (void*)(m_oHook.trampoline().address());
}

bool FunctionHook::IsEnabled()
{
    return m_oHook.enabled();
}

void FunctionHook::SetHookFunction(const std::string& functionSignature, void* callback)
{
    static auto gamedata = g_ifaceService.FetchInterface<IGameDataManager>(GAMEDATA_INTERFACE_VERSION);
    void* functionAddress = gamedata->GetSignatures()->Fetch(functionSignature);
    if (!functionAddress) return;

    m_oHook = safetyhook::create_inline(functionAddress, callback, safetyhook::InlineHook::Flags::StartDisabled);
}

void FunctionHook::SetHookFunction(void* functionAddress, void* callback)
{
    if (!functionAddress) return;

    m_oHook = safetyhook::create_inline(functionAddress, callback, safetyhook::InlineHook::Flags::StartDisabled);
}