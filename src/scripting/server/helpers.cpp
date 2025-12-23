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

#include <scripting/scripting.h>
#include <api/interfaces/manager.h>

int Bridge_ServerHelpers_GetServerLanguage(char* out)
{
    static auto configuration = g_ifaceService.FetchInterface<IConfiguration>(CONFIGURATION_INTERFACE_VERSION);

    static std::string s;
    s = std::get<std::string>(configuration->GetValue("core.Language"));

    if (out != nullptr) strcpy(out, s.c_str());

    return s.size();
}

bool Bridge_ServerHelpers_UsePlayerLanguage()
{
    static auto configuration = g_ifaceService.FetchInterface<IConfiguration>(CONFIGURATION_INTERFACE_VERSION);
    return std::get<bool>(configuration->GetValue("core.UsePlayerLanguage"));
}

bool Bridge_ServerHelpers_IsFollowingServerGuidelines()
{
    static auto configuration = g_ifaceService.FetchInterface<IConfiguration>(CONFIGURATION_INTERFACE_VERSION);
    return std::get<bool>(configuration->GetValue("core.FollowCS2ServerGuidelines"));
}

bool Bridge_ServerHelpers_UseAutoHotReload()
{
    static auto configuration = g_ifaceService.FetchInterface<IConfiguration>(CONFIGURATION_INTERFACE_VERSION);
    return std::get<bool>(configuration->GetValue("core.AutoHotReload"));
}

DEFINE_NATIVE("ServerHelpers.GetServerLanguage", Bridge_ServerHelpers_GetServerLanguage);
DEFINE_NATIVE("ServerHelpers.UsePlayerLanguage", Bridge_ServerHelpers_UsePlayerLanguage);
DEFINE_NATIVE("ServerHelpers.IsFollowingServerGuidelines", Bridge_ServerHelpers_IsFollowingServerGuidelines);
DEFINE_NATIVE("ServerHelpers.UseAutoHotReload", Bridge_ServerHelpers_UseAutoHotReload);