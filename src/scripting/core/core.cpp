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
#include <thread>

extern std::thread::id g_mainThreadId;

static char* Bridge_Core_CopyString(const std::string& value, int* size)
{
    int outSize = static_cast<int>(value.size());
    *size = outSize;

    char* out = (char*)g_pMemoryAllocator->Alloc(outSize + 1);
    g_pMemoryAllocator->Copy(out, (void*)value.c_str(), outSize);
    out[outSize] = '\0';
    return out;
}

uint8_t Bridge_Core_PluginManualLoadState()
{
    if (bool* b = std::get_if<bool>(&g_pConfiguration->GetValue("core.ManualLoadPlugins")))
    {
        return *b ? 1 : 0;
    }
    return 0;
}

char* Bridge_Core_PluginLoadOrder(int* size)
{
    if (std::string* vec = std::get_if<std::string>(&g_pConfiguration->GetValue("core.PluginLoadOrder")))
    {
        int stringSize = vec->size();
        *size = stringSize;
        void* buffer = g_pMemoryAllocator->Alloc(stringSize + 1);
        g_pMemoryAllocator->Copy(buffer, (void*)vec->c_str(), stringSize);
        return (char*)buffer;
    }

    return Bridge_Core_CopyString("", size);
}

uint8_t Bridge_Core_EnableProfilerByDefault()
{
    if (bool* b = std::get_if<bool>(&g_pConfiguration->GetValue("core.EnableProfiler")))
    {
        return *b ? 1 : 0;
    }
    return 0;
}

uint8_t Bridge_Core_IsMainThread()
{
    return std::this_thread::get_id() == g_mainThreadId ? 1 : 0;
}

DEFINE_NATIVE("Core.PluginManualLoadState", Bridge_Core_PluginManualLoadState);
DEFINE_NATIVE("Core.PluginLoadOrder", Bridge_Core_PluginLoadOrder);
DEFINE_NATIVE("Core.EnableProfilerByDefault", Bridge_Core_EnableProfilerByDefault);
DEFINE_NATIVE("Core.IsMainThread", Bridge_Core_IsMainThread);