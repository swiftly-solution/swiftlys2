/************************************************************************************************
 *  SwiftlyS2 is a scripting framework for Source2-based games.
 *  Copyright (C) 2023-2026 Swiftly Solution SRL via Sava Andrei-Sebastian and it's contributors (samyycX)
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

#include <scripting/scripting.h>
#include <api/interfaces/interfaces.h>
#include <api/shared/profiler.h>
#include <thread>

extern std::thread::id g_mainThreadId;

uint8_t Bridge_Core_PluginManualLoadState()
{
    PROFILE_FUNCTION();

    if (bool* b = std::get_if<bool>(&g_pConfiguration->GetValue("core.ManualLoadPlugins")))
    {
        return *b ? 1 : 0;
    }
    return 0;
}

char* Bridge_Core_PluginLoadOrder(int* size)
{
    PROFILE_FUNCTION();

    if (std::string* vec = std::get_if<std::string>(&g_pConfiguration->GetValue("core.PluginLoadOrder")))
    {
        return Bridge_CopyString(*vec, size);
    }

    return Bridge_CopyString("", size);
}

int32_t Bridge_Core_GetProfilerLevelByDefault()
{
    PROFILE_FUNCTION();

    if (int* i = std::get_if<int>(&g_pConfiguration->GetValue("core.ProfilerLevel")))
    {
        return (int32_t)*i;
    }
    return 0;
}

uint8_t Bridge_Core_IsMainThread()
{
    PROFILE_FUNCTION();

    return std::this_thread::get_id() == g_mainThreadId ? 1 : 0;
}

DEFINE_NATIVE("Core.PluginManualLoadState", Bridge_Core_PluginManualLoadState);
DEFINE_NATIVE("Core.PluginLoadOrder", Bridge_Core_PluginLoadOrder);
DEFINE_NATIVE("Core.GetProfilerLevelByDefault", Bridge_Core_GetProfilerLevelByDefault);
DEFINE_NATIVE("Core.IsMainThread", Bridge_Core_IsMainThread);