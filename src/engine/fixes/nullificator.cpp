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

#include <api/interfaces/manager.h>
#include "nullificator.h"

#include <s2binlib/s2binlib.h>

IFunctionHook* g_pNullificatorHook = nullptr;

void Hook_Plat_DebugString_Buffered(void* something, void* something_else)
{
    // gtfo
    if (something == nullptr) return;

    return reinterpret_cast<decltype(&Hook_Plat_DebugString_Buffered)>(g_pNullificatorHook->GetOriginal())(something, something_else);
}

void NullificatorFix::Start()
{
#ifndef _WIN32
    auto hooksmanager = g_ifaceService.FetchInterface<IHooksManager>(HOOKSMANAGER_INTERFACE_VERSION);
    g_pNullificatorHook = hooksmanager->CreateFunctionHook();

    void* Plat_DebugString_Buffered = nullptr;
    s2binlib_find_symbol("tier0", "Plat_DebugString_Buffered", &Plat_DebugString_Buffered);
    if (!Plat_DebugString_Buffered) return;

    g_pNullificatorHook->SetHookFunction(Plat_DebugString_Buffered, reinterpret_cast<void*>(Hook_Plat_DebugString_Buffered));
    g_pNullificatorHook->Enable();
#endif
}

void NullificatorFix::Stop()
{
    if (g_pNullificatorHook)
    {
        g_pNullificatorHook->Disable();
        auto hooksmanager = g_ifaceService.FetchInterface<IHooksManager>(HOOKSMANAGER_INTERFACE_VERSION);
        hooksmanager->DestroyFunctionHook(g_pNullificatorHook);
    }
}