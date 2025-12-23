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
#include <api/interfaces/manager.h>

uint8_t Bridge_Core_PluginManualLoadState()
{
    static auto config = g_ifaceService.FetchInterface<IConfiguration>(CONFIGURATION_INTERFACE_VERSION);
    if (bool* b = std::get_if<bool>(&config->GetValue("core.ManualLoadPlugins")))
    {
        return *b ? 1 : 0;
    }
    return 0;
}

int Bridge_Core_PluginLoadOrder(char* out)
{
    static auto config = g_ifaceService.FetchInterface<IConfiguration>(CONFIGURATION_INTERFACE_VERSION);
    if (std::string* vec = std::get_if<std::string>(&config->GetValue("core.PluginLoadOrder")))
    {
        if (out != nullptr) strcpy(out, vec->c_str());
        return static_cast<int>(vec->size());
    }
    return 0;
}

uint8_t Bridge_Core_EnableProfilerByDefault()
{
    static auto config = g_ifaceService.FetchInterface<IConfiguration>(CONFIGURATION_INTERFACE_VERSION);
    if (bool* b = std::get_if<bool>(&config->GetValue("core.EnableProfiler")))
    {
        return *b ? 1 : 0;
    }
    return 0;
}

DEFINE_NATIVE("Core.PluginManualLoadState", Bridge_Core_PluginManualLoadState);
DEFINE_NATIVE("Core.PluginLoadOrder", Bridge_Core_PluginLoadOrder);
DEFINE_NATIVE("Core.EnableProfilerByDefault", Bridge_Core_EnableProfilerByDefault);