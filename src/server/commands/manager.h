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

#ifndef src_server_commands_manager_h
#define src_server_commands_manager_h

#include <api/server/commands/manager.h>
#include <public/tier1/convar.h>

class CServerCommands : public IServerCommands
{
public:
    virtual void Initialize() override;
    virtual void Shutdown() override;

    virtual int HandleCommand(int playerid, const std::string& text, bool dryrun) override;
    virtual bool HandleClientCommand(int playerid, const std::string& text) override;
    virtual bool HandleClientChat(int playerid, const std::string& text, bool teamonly) override;

    // playerid, args, command_name, prefix, silent
    virtual uint64_t RegisterCommand(std::string command_name, std::function<void(int, std::vector<std::string>, std::string, std::string, bool)> handler, bool registerRaw) override;
    virtual void UnregisterCommand(uint64_t command_id) override;
    virtual bool IsCommandRegistered(std::string command_name) override;

    virtual uint64_t RegisterAlias(std::string alias_command, std::string command_name, bool registerRaw) override;
    virtual void UnregisterAlias(uint64_t alias_id) override;

    // playerid, command
    virtual uint64_t RegisterClientCommandsListener(std::function<int(int, const std::string&)> listener) override;
    virtual void UnregisterClientCommandsListener(uint64_t listener_id) override;

    // playerid, text, teamonly
    virtual uint64_t RegisterClientChatListener(std::function<int(int, const std::string&, bool)> listener) override;
    virtual void UnregisterClientChatListener(uint64_t listener_id) override;
};

#endif