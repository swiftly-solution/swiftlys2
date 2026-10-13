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
#include <memory/hooks/function.h>

#include <api/interfaces/interfaces.h>
#include <fmt/format.h>

#ifdef _WIN32
#include <Windows.h>

void __fastcall PreloadDLLHook(HMODULE hModule);
extern FunctionHook g_PreloadDLLHook;

void __fastcall PreloadDLLHook(HMODULE hModule)
{
    if (!g_PreloadDLLHook.GetOriginal())
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
                g_pLogger->Info("PreloadDLL", fmt::format("Skipping DLL: {}\n", modulePath));
                return;
            }
        }
    }

    return reinterpret_cast<decltype(&PreloadDLLHook)>(g_PreloadDLLHook.GetOriginal())(hModule);
}

FunctionHook g_PreloadDLLHook(reinterpret_cast<void*>(PreloadDLLHook), []() -> void* {
    void* preloadDLLFunction = nullptr;
    g_pS2BinLib->FindFuncWithString("engine2", "Could not PreloadLibrary %s - error %d: %s", &preloadDLLFunction);
    return preloadDLLFunction;
}, "PreloadDLL");
#endif
