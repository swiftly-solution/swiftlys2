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

#include "core.h"

#include <api/extension/host.h>
#include <api/interfaces/interfaces.h>
#include <api/shared/profiler.h>
#include <api/shared/string.h>
#include <core/entrypoint.h>

#include <exception>
#include <format>

static std::string GetSubcommandName(std::string_view argument)
{
    PROFILE_FUNCTION();

    const auto start = argument.find_first_not_of(" \t\r\n");
    if (start == std::string_view::npos)
        return {};
    return str_tolower(argument.substr(start, argument.find_last_not_of(" \t\r\n") - start + 1));
}

static void ShowExtensionHelp()
{
    PROFILE_FUNCTION();

    g_SwiftlyCore.SendConsoleMessage("sw ext list         - List all loaded extensions\n");
    g_SwiftlyCore.SendConsoleMessage("sw ext load <id>    - Load an extension from extensions/<id>/\n");
    g_SwiftlyCore.SendConsoleMessage("sw ext unload <id>  - Unload an extension\n");
}

static void ShowExtensionList()
{
    PROFILE_FUNCTION();

    const auto extensions = g_pExtensionHost->GetExtensions();
    g_SwiftlyCore.SendConsoleMessage(std::format("Extensions ({} total):\n", extensions.size()));
    g_SwiftlyCore.SendConsoleMessage("Status | Id | Name (ver.) | Author | Location\n");
    for (const auto& extension : extensions)
    {
        g_SwiftlyCore.SendConsoleMessage(std::format("{} | {} | {} {} | {} | {}\n",
            extension.initialized ? "Loaded" : "Uninitialized", extension.id,
            extension.name, extension.version, extension.author, extension.path));
    }
}

bool HandleCoreCommand(int playerId, const std::vector<std::string>& args, bool managedAvailable)
{
    PROFILE_FUNCTION();

    // Core administration commands are only available from the server console.
    if (playerId != -1)
        return true;

    if (args.empty() || GetSubcommandName(args[0]) != "ext")
    {
        if (managedAvailable)
            return false;

        if (!args.empty() && GetSubcommandName(args[0]) != "help")
            g_pLogger->Warning("Commands", "This subcommand requires the managed runtime. Available native commands:\n");
        ShowExtensionHelp();
        return true;
    }

    if (args.size() == 1)
    {
        ShowExtensionHelp();
        return true;
    }

    const auto command = GetSubcommandName(args[1]);
    try
    {
        if (command == "list")
        {
            ShowExtensionList();
        }
        else if (command == "load" || command == "unload")
        {
            if (args.size() < 3 || args[2].empty())
            {
                g_pLogger->Warning("Extensions", std::format("Usage: sw ext {} <id>\n", command));
                return true;
            }

            const bool succeeded = command == "load"
                ? g_pExtensionHost->LoadExtension(args[2])
                : g_pExtensionHost->UnloadExtension(args[2]);
            if (succeeded)
                g_pLogger->Info("Extensions", std::format("{} extension: {}\n", command == "load" ? "Loaded" : "Unloaded", args[2]));
            else
                g_pLogger->Warning("Extensions", std::format("Failed to {} extension: {}\n", command, args[2]));
        }
        else
        {
            g_pLogger->Warning("Extensions", std::format("Unknown extension command: {}. Use 'sw ext' for help.\n", args[1]));
        }
    }
    catch (const std::exception& error)
    {
        g_pLogger->Error("Extensions", std::format("Failed to execute extension command: {}\n", error.what()));
    }

    return true;
}
