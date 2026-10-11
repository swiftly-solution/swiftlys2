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
#include <api/utils/mutex.h>

#include <public/tier1/convar.h>

#include <set>
#include <unordered_map>

class CServerCommands : public IServerCommands
{
public:
    virtual void Initialize() override;
    virtual void Shutdown() override;

    virtual int HandleCommand(int playerid, const std::string& text, bool dryrun) override;
    virtual void HandleRegisteredCommand(int playerid, const char* commandLine) override;
    virtual bool HandleClientCommand(int playerid, const char* text) override;
    virtual bool HandleClientChat(int playerid, const std::string& text, bool teamonly) override;

    virtual uint64_t RegisterCommand(std::string command_name, bool registerRaw, std::string helpText) override;
    virtual void SetCommandHandler(CommandHandler handler) override;
    virtual void UnregisterCommand(uint64_t command_id) override;
    virtual bool IsCommandRegistered(std::string command_name) override;

    virtual uint64_t RegisterAlias(std::string alias_command, std::string command_name, bool registerRaw) override;
    virtual void UnregisterAlias(uint64_t alias_id) override;

    virtual void SetClientCommandHandler(ClientCommandHandler handler) override;
    virtual void SetClientChatHandler(ClientChatHandler handler) override;
private:
    bool ResolveCommandName(std::string& command_name);
    void LoadPrefixes();

    QueueMutex m_mtxCommands;
    std::unordered_map<std::string, ConCommand*> m_commands;
    std::unordered_map<uint64_t, std::string> m_commandNames;
    uint64_t m_lastCommandId = 0;

    CommandHandler m_commandHandler;
    ClientCommandHandler m_clientCommandHandler;
    ClientChatHandler m_clientChatHandler;

    bool m_prefixesLoaded = false;
    std::set<std::string> m_commandPrefixes;
    std::set<std::string> m_silentCommandPrefixes;
};

#endif
