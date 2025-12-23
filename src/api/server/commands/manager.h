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

#ifndef src_api_server_commands_manager_h
#define src_api_server_commands_manager_h

#include <string>
#include <functional>
#include <vector>
#include <cstdint>

class IServerCommands
{
public:
    virtual void Initialize() = 0;
    virtual void Shutdown() = 0;

    virtual int HandleCommand(int playerid, const std::string& text, bool dryrun) = 0;
    virtual bool HandleClientCommand(int playerid, const std::string& text) = 0;
    virtual bool HandleClientChat(int playerid, const std::string& text, bool teamonly) = 0;

    // playerid, args, command_name, prefix, silent
    virtual uint64_t RegisterCommand(std::string command_name, std::function<void(int, std::vector<std::string>, std::string, std::string, bool)> handler, bool registerRaw) = 0;
    virtual void UnregisterCommand(uint64_t command_id) = 0;
    virtual bool IsCommandRegistered(std::string command_name) = 0;

    virtual uint64_t RegisterAlias(std::string alias_command, std::string command_name, bool registerRaw) = 0;
    virtual void UnregisterAlias(uint64_t alias_id) = 0;

    // playerid, command
    virtual uint64_t RegisterClientCommandsListener(std::function<int(int, const std::string&)> listener) = 0;
    virtual void UnregisterClientCommandsListener(uint64_t listener_id) = 0;

    // playerid, text, teamonly
    virtual uint64_t RegisterClientChatListener(std::function<int(int, const std::string&, bool)> listener) = 0;
    virtual void UnregisterClientChatListener(uint64_t listener_id) = 0;
};

#endif