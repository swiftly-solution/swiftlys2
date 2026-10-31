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

#include "manager.h"

#include <memory>
#include <memory/hooks/vfunction.h>

#include <api/interfaces/interfaces.h>
#include <api/shared/profiler.h>

#include <api/shared/hookresult.h>
#include <api/shared/string.h>

#include <algorithm>
#include <cstdlib>
#include <cstring>
#include <format>
#include <mutex>
#include <shared_mutex>
#include <string_view>
#include <vector>

void DispatchConCommand(void* thisPtr, ConCommandRef cmd, const CCommandContext& ctx, const CCommand& args);
void ClientCommandHook2(void* thisPtr, CPlayerSlot slot, const CCommand& args);

VFunctionHook dispatchConCommandHook(reinterpret_cast<void*>(DispatchConCommand), "tier0", "CCvar", "ICvar::DispatchConCommand");
VFunctionHook clientCommandHook2(reinterpret_cast<void*>(ClientCommandHook2), "server", "CSource2GameClients", "IServerGameClients::ClientCommand");

void CommandsCallback(const CCommandContext& context, const CCommand& args)
{
    PROFILE_FUNCTION();

    g_pServerCommands->HandleRegisteredCommand(context.GetPlayerSlot().Get(), args.GetCommandString());
}

static const std::string* FindPrefix(const std::vector<std::string>& prefixes, std::string_view text)
{
    PROFILE_FUNCTION();

    const auto it = std::ranges::find_if(prefixes, [text](const std::string& prefix) { return text.starts_with(prefix); });
    return it != prefixes.end() ? &*it : nullptr;
}

static std::vector<std::string> ParsePrefixes(const std::string& value)
{
    PROFILE_FUNCTION();

    auto prefixes = explodeToSet(value, " ");
    prefixes.erase("");
    return { prefixes.begin(), prefixes.end() };
}

void CServerCommands::LoadPrefixes()
{
    PROFILE_FUNCTION();

    if (m_prefixesLoaded) return;
    m_prefixesLoaded = true;

    m_commandPrefixes = ParsePrefixes(std::get<std::string>(g_pConfiguration->GetValue("core.CommandPrefixes")));
    m_silentCommandPrefixes = ParsePrefixes(std::get<std::string>(g_pConfiguration->GetValue("core.CommandSilentPrefixes")));
}

bool CServerCommands::ResolveCommandName(std::string& commandName)
{
    PROFILE_FUNCTION();

    auto command = m_commands.find(commandName);
    if (command != m_commands.end() && command->second)
        return true;

    commandName.insert(0, "sw_");
    command = m_commands.find(commandName);
    if (command != m_commands.end() && command->second)
        return true;

    commandName.erase(0, 3);
    return false;
}

// @returns 1 - command is not silent
// @returns 2 - command is silent
// @returns -1 - invalid controller
// @returns 0 - is not command
int CServerCommands::HandleCommand(int playerid, const std::string& text, bool dryrun)
{
    PROFILE_FUNCTION();

    if (text.empty() || !g_pPlayerManager->GetPlayer(playerid))
    {
        return -1;
    }

    LoadPrefixes();

    bool isSilentCommand = false;
    const std::string* selectedPrefix = FindPrefix(m_commandPrefixes, text);
    if (!selectedPrefix)
    {
        selectedPrefix = FindPrefix(m_silentCommandPrefixes, text);
        isSilentCommand = selectedPrefix != nullptr;
    }

    if (!selectedPrefix)
    {
        return 0;
    }

    std::vector<std::string> args = TokenizeCommand(text);
    if (args.empty())
    {
        return 0;
    }

    const std::string_view firstToken = args[0];
    std::string originalCommandName = str_tolower(firstToken.substr((std::min)(selectedPrefix->size(), firstToken.size())));
    std::string commandName = originalCommandName;
    args.erase(args.begin());

    {
        std::shared_lock lock(m_mtxCommands);
        if (!ResolveCommandName(commandName))
        {
            return 0;
        }
    }

    if (!dryrun && m_commandHandler)
        m_commandHandler(commandName, playerid, args, originalCommandName, *selectedPrefix, isSilentCommand);

    return isSilentCommand ? 2 : 1;
}

void CServerCommands::HandleRegisteredCommand(int playerid, const char* commandLine)
{
    PROFILE_FUNCTION();

    std::vector<std::string> args = TokenizeCommand(commandLine);
    if (args.empty() || !m_commandHandler)
    {
        return;
    }

    std::string originalCommandName = str_tolower(args[0]);
    std::string commandName = originalCommandName;
    args.erase(args.begin());

    {
        std::shared_lock lock(m_mtxCommands);
        if (!ResolveCommandName(commandName))
        {
            return;
        }
    }

    m_commandHandler(commandName, playerid, args, originalCommandName, "sw_", true);
}

bool CServerCommands::HandleClientCommand(int playerid, const char* text)
{
    PROFILE_FUNCTION();

    if (!m_clientCommandHandler) return true;

    const int res = m_clientCommandHandler(playerid, text);
    return res != HookResult_Stop && res != HookResult_CancelOriginal;
}

bool CServerCommands::HandleClientChat(int playerid, const std::string& text, bool teamonly)
{
    PROFILE_FUNCTION();

    if (!m_clientChatHandler) return true;

    const int res = m_clientChatHandler(playerid, text, teamonly);
    return res != HookResult_Stop && res != HookResult_CancelOriginal;
}

uint64_t CServerCommands::RegisterCommand(std::string commandName, bool registerRaw, std::string helpText)
{
    PROFILE_FUNCTION();

    commandName = str_tolower(commandName);

    uint64_t commandId;
    {
        std::lock_guard lock(m_mtxCommands);
        if (!registerRaw)
        {
            if (m_commands.contains(commandName)) return 0;
            commandName.insert(0, "sw_");
        }

        if (!m_commands.try_emplace(commandName, nullptr).second) return 0;

        commandId = ++m_lastCommandId;
        m_commandNames.emplace(commandId, commandName);
    }

    if (g_pGameCvar && g_pGameCvar->FindConCommand(commandName.c_str(), true).IsValidRef())
    {
        std::lock_guard lock(m_mtxCommands);
        m_commandNames.erase(commandId);
        m_commands.erase(commandName);
        g_pLogger->Warning("Commands", std::format("Cannot register command \"{}\": a command with this name already exists in the engine. Use a different name (e.g. without registerRaw so it gets the \"sw_\" prefix).", commandName));
        return 0;
    }

    char* helpCopy = strdup(helpText.c_str());
    std::unique_ptr<ConCommand> conCommand(new ConCommand(commandName.c_str(), CommandsCallback, helpCopy, CVarFlags::CLIENT_CAN_EXECUTE | CVarFlags::LINKED_CONCOMMAND));

    if (conCommand)
        conCommand->RemoveFlags(CVarFlags::SERVER_CAN_EXECUTE);

    std::lock_guard lock(m_mtxCommands);
    if (!conCommand)
    {
        free(helpCopy);
        m_commandNames.erase(commandId);
        m_commands.erase(commandName);
        return 0;
    }

    m_commands.find(commandName)->second = conCommand.release();
    return commandId;
}

void CServerCommands::SetCommandHandler(CommandHandler handler)
{
    PROFILE_FUNCTION();

    m_commandHandler = std::move(handler);
}

void CServerCommands::UnregisterCommand(uint64_t commandId)
{
    PROFILE_FUNCTION();

    ConCommand* command = nullptr;
    std::string commandName;
    {
        std::lock_guard lock(m_mtxCommands);
        auto nameIt = m_commandNames.find(commandId);
        if (nameIt == m_commandNames.end()) return;

        commandName = std::move(nameIt->second);
        auto commandIt = m_commands.find(commandName);
        if (commandIt != m_commands.end())
        {
            command = commandIt->second;
            commandIt->second = nullptr;
        }
        m_commandNames.erase(nameIt);
    }

    delete command;
    std::lock_guard lock(m_mtxCommands);
    m_commands.erase(commandName);
}

bool CServerCommands::IsCommandRegistered(std::string commandName)
{
    PROFILE_FUNCTION();

    commandName = str_tolower(commandName);

    std::shared_lock lock(m_mtxCommands);
    return ResolveCommandName(commandName);
}

uint64_t CServerCommands::RegisterAlias(std::string aliasCommand, std::string commandName, bool registerRaw)
{
    PROFILE_FUNCTION();

    std::string helpText;
    {
        std::shared_lock lock(m_mtxCommands);

        commandName = str_tolower(commandName);
        if (!ResolveCommandName(commandName))
        {
            return 0;
        }

        helpText = m_commands.find(commandName)->second->GetHelpText();
    }

    return RegisterCommand(aliasCommand, registerRaw, helpText);
}

void CServerCommands::UnregisterAlias(uint64_t aliasId)
{
    PROFILE_FUNCTION();

    UnregisterCommand(aliasId);
}

void CServerCommands::SetClientCommandHandler(ClientCommandHandler handler)
{
    PROFILE_FUNCTION();

    m_clientCommandHandler = std::move(handler);
}

void CServerCommands::SetClientChatHandler(ClientChatHandler handler)
{
    PROFILE_FUNCTION();

    m_clientChatHandler = std::move(handler);
}

void ClientCommandHook2(void* thisPtr, CPlayerSlot slot, const CCommand& args)
{
    PROFILE_FUNCTION();

    if (!g_pServerCommands->HandleClientCommand(slot.Get(), args.GetCommandString()))
    {
        return;
    }

    PROFILE_PAUSE();

    return reinterpret_cast<decltype(&ClientCommandHook2)>(clientCommandHook2.GetOriginal())(thisPtr, slot, args);
}

static std::string GetChatText(const CCommand& args)
{
    PROFILE_FUNCTION();

    std::string_view text = args.GetCommandString();

    size_t textStart;
    if (text.starts_with('"'))
    {
        size_t closeQuote = text.find('"', 1);
        textStart = closeQuote != std::string_view::npos ? closeQuote + 1 : text.size();
    }
    else
    {
        textStart = strlen(args.Arg(0));
    }

    text.remove_prefix((std::min)(textStart, text.size()));
    text.remove_prefix((std::min)(text.find_first_not_of(' '), text.size()));
    if (text.starts_with('"'))
        text.remove_prefix(1);
    if (text.ends_with('"'))
        text.remove_suffix(1);

    return std::string(text);
}

void DispatchConCommand(void* thisPtr, ConCommandRef cmd, const CCommandContext& ctx, const CCommand& args)
{
    PROFILE_FUNCTION();

    int playerid = ctx.GetPlayerSlot().Get();

    std::string chatText;
    bool shouldSend = true;

    if (playerid != -1)
    {
        if (!g_pServerCommands->HandleClientCommand(playerid, args.GetCommandString()))
        {
            return;
        }

        const std::string command = str_tolower(args.Arg(0));
        const bool isTeamSay = command == "say_team";
        if (isTeamSay || command == "say")
        {
            if (!g_pPlayerManager->GetPlayer(playerid))
            {
                return;
            }

            chatText = GetChatText(args);
            if (chatText.empty())
            {
                return;
            }

            shouldSend = g_pServerCommands->HandleCommand(playerid, chatText, true) != 2
                && g_pServerCommands->HandleClientChat(playerid, chatText, isTeamSay);
        }
    }

    PROFILE_PAUSE();

    if (shouldSend) reinterpret_cast<decltype(&DispatchConCommand)>(dispatchConCommandHook.GetOriginal())(thisPtr, cmd, ctx, args);

    PROFILE_RESUME();

    if (!chatText.empty()) g_pServerCommands->HandleCommand(playerid, chatText, false);
}
