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

#include <regex>

#include <s2binlib/s2binlib.h>

#include "preload.h"
#include <api/interfaces/manager.h>

IFunctionHook* g_pPreloadDLLHook = nullptr;

#ifdef _WIN32
#include <Windows.h>

void __fastcall PreloadDLLHook(HMODULE hModule)
{
    if (!g_pPreloadDLLHook || !g_pPreloadDLLHook->GetOriginal())
    {
        return;
    }

    if (hModule)
    {
        char modulePath[MAX_PATH] = { 0 };
        DWORD len = GetModuleFileNameA(hModule, modulePath, MAX_PATH);
        if (len > 0 && len < MAX_PATH)
        {
            static const std::regex skipPattern(R"([/\\](bin[/\\]managed|resources[/\\]exports)[/\\])", std::regex_constants::icase);
            if (std::regex_search(modulePath, skipPattern))
            {
                auto logger = g_ifaceService.FetchInterface<ILogger>(LOGGER_INTERFACE_VERSION);
                if (logger)
                {
                    logger->Info("PreloadDLL", fmt::format("Skipping DLL: {}\n", modulePath));
                }
                return;
            }
        }
    }

    return reinterpret_cast<decltype(&PreloadDLLHook)>(g_pPreloadDLLHook->GetOriginal())(hModule);
}
#endif

void PreloadDLLFix::Start()
{
#ifdef _WIN32
    void* preloadDLLFunction = nullptr;
    s2binlib_find_func_with_string("engine2", "Could not PreloadLibrary %s - error %d: %s", &preloadDLLFunction);
    if (!preloadDLLFunction) return;

    auto hooksmanager = g_ifaceService.FetchInterface<IHooksManager>(HOOKSMANAGER_INTERFACE_VERSION);
    g_pPreloadDLLHook = hooksmanager->CreateFunctionHook();
    g_pPreloadDLLHook->SetHookFunction(preloadDLLFunction, reinterpret_cast<void*>(PreloadDLLHook));
    g_pPreloadDLLHook->Enable();
#endif
}

void PreloadDLLFix::Stop()
{
#ifdef _WIN32
    if (!g_pPreloadDLLHook) return;

    auto hooksmanager = g_ifaceService.FetchInterface<IHooksManager>(HOOKSMANAGER_INTERFACE_VERSION);

    g_pPreloadDLLHook->Disable();
    hooksmanager->DestroyFunctionHook(g_pPreloadDLLHook);
    g_pPreloadDLLHook = nullptr;
#endif
}