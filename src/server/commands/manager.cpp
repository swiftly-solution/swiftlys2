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

#include <api/interfaces/interfaces.h>

#include <api/shared/string.h>

#include <algorithm>
#include <cstdio>
#include <public/icvar.h>

void DispatchConCommand(void* thisPtr, ConCommandRef cmd, const CCommandContext& ctx, const CCommand& args);
IVFunctionHook* dispatchConCommandHook = nullptr;

void ClientCommandHook2(void* thisPtr, CPlayerSlot slot, const CCommand& args);
IVFunctionHook* clientCommandHook2 = nullptr;

void CommandsCallback(const CCommandContext& context, const CCommand& args)
{
    g_pServerCommands->HandleRegisteredCommand(context.GetPlayerSlot().Get(), args.GetCommandString());
}

void CServerCommands::Initialize()
{
    void* ccvarVTable;
    g_pS2BinLib->FindVtable("tier0", "CCvar", &ccvarVTable);

    dispatchConCommandHook = g_pHooksManager->CreateVFunctionHook();
    dispatchConCommandHook->SetHookFunction(ccvarVTable, g_pGameDataManager->GetOffsets()->Fetch("ICvar::DispatchConCommand"), (void*)DispatchConCommand, true);
    dispatchConCommandHook->Enable();

    void* gameclientsvtable = nullptr;
    g_pS2BinLib->FindVtable("server", "CSource2GameClients", &gameclientsvtable);

    clientCommandHook2 = g_pHooksManager->CreateVFunctionHook();
    clientCommandHook2->SetHookFunction(gameclientsvtable, g_pGameDataManager->GetOffsets()->Fetch("IServerGameClients::ClientCommand"), (void*)ClientCommandHook2, true);
    clientCommandHook2->Enable();
}

void CServerCommands::Shutdown()
{
    if (dispatchConCommandHook)
    {
        dispatchConCommandHook->Disable();
        g_pHooksManager->DestroyVFunctionHook(dispatchConCommandHook);
        dispatchConCommandHook = nullptr;
    }

    if (clientCommandHook2)
    {
        clientCommandHook2->Disable();
        g_pHooksManager->DestroyVFunctionHook(clientCommandHook2);
        clientCommandHook2 = nullptr;
    }
}

static const std::string* FindPrefix(const std::set<std::string>& prefixes, const std::string& text)
{
    for (const auto& prefix : prefixes)
    {
        if (text.starts_with(prefix))
            return &prefix;
    }

    return nullptr;
}

void CServerCommands::LoadPrefixes()
{
    if (m_prefixesLoaded) return;
    m_prefixesLoaded = true;

    m_commandPrefixes = explodeToSet(std::get<std::string>(g_pConfiguration->GetValue("core.CommandPrefixes")), " ");
    m_silentCommandPrefixes = explodeToSet(std::get<std::string>(g_pConfiguration->GetValue("core.CommandSilentPrefixes")), " ");
}

bool CServerCommands::ResolveCommandName(std::string& commandName)
{
    if (m_commands.contains(commandName))
        return true;

    std::string prefixedName = "sw_" + commandName;
    if (!m_commands.contains(prefixedName))
        return false;

    commandName = std::move(prefixedName);
    return true;
}

// @returns 1 - command is not silent
// @returns 2 - command is silent
// @returns -1 - invalid controller
// @returns 0 - is not command
int CServerCommands::HandleCommand(int playerid, const std::string& text, bool dryrun)
{
    if (text.empty())
    {
        return -1;
    }

    if (g_pPlayerManager->GetPlayer(playerid) == nullptr)
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

    std::string originalCommandName = str_tolower(args[0].substr(
        (((selectedPrefix->size()) < (args[0].size())) ? (selectedPrefix->size()) : (args[0].size()))
    ));
    std::string commandName = originalCommandName;
    args.erase(args.begin());

    {
        QueueLockGuard lock(m_mtxCommands);
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
    std::vector<std::string> args = TokenizeCommand(commandLine);
    if (args.empty() || !m_commandHandler)
    {
        return;
    }

    std::string originalCommandName = str_tolower(args[0]);
    std::string commandName = originalCommandName;
    args.erase(args.begin());

    {
        QueueLockGuard lock(m_mtxCommands);
        if (!ResolveCommandName(commandName))
        {
            return;
        }
    }

    m_commandHandler(commandName, playerid, args, originalCommandName, "sw_", true);
}

bool CServerCommands::HandleClientCommand(int playerid, const char* text)
{
    if (!m_clientCommandHandler) return true;

    auto res = m_clientCommandHandler(playerid, text);
    return res != 1 && res != 3;
}

bool CServerCommands::HandleClientChat(int playerid, const std::string& text, bool teamonly)
{
    if (!m_clientChatHandler) return true;

    auto res = m_clientChatHandler(playerid, text, teamonly);
    return res != 1 && res != 3;
}

uint64_t CServerCommands::RegisterCommand(std::string commandName, bool registerRaw, std::string helpText)
{
    commandName = str_tolower(commandName);

    QueueLockGuard lock(m_mtxCommands);
    if (!registerRaw)
    {
        if (m_commands.contains(commandName))
        {
            return 0;
        }

        commandName.insert(0, "sw_");
    }

    if (m_commands.contains(commandName))
    {
        return 0;
    }

    auto* conCommand = new ConCommand(commandName.c_str(), CommandsCallback, strdup(helpText.c_str()), FCVAR_CLIENT_CAN_EXECUTE | FCVAR_LINKED_CONCOMMAND);
    conCommand->RemoveFlags(FCVAR_SERVER_CAN_EXECUTE);

    m_commands[commandName] = conCommand;
    m_commandNames[++m_lastCommandId] = commandName;
    return m_lastCommandId;
}

void CServerCommands::SetCommandHandler(CommandHandler handler)
{
    m_commandHandler = std::move(handler);
}

void CServerCommands::UnregisterCommand(uint64_t commandId)
{
    QueueLockGuard lock(m_mtxCommands);

    auto nameIt = m_commandNames.find(commandId);
    if (nameIt == m_commandNames.end())
    {
        return;
    }

    auto commandIt = m_commands.find(nameIt->second);
    if (commandIt != m_commands.end())
    {
        delete commandIt->second;
        m_commands.erase(commandIt);
    }

    m_commandNames.erase(nameIt);
}

bool CServerCommands::IsCommandRegistered(std::string commandName)
{
    commandName = str_tolower(commandName);

    QueueLockGuard lock(m_mtxCommands);
    return ResolveCommandName(commandName);
}

uint64_t CServerCommands::RegisterAlias(std::string aliasCommand, std::string commandName, bool registerRaw)
{
    std::string helpText;
    {
        QueueLockGuard lock(m_mtxCommands);

        commandName = str_tolower(commandName);
        if (!ResolveCommandName(commandName))
        {
            return 0;
        }

        helpText = m_commands[commandName]->GetHelpText();
    }

    return RegisterCommand(aliasCommand, registerRaw, helpText);
}

void CServerCommands::UnregisterAlias(uint64_t aliasId)
{
    UnregisterCommand(aliasId);
}

void CServerCommands::SetClientCommandHandler(ClientCommandHandler handler)
{
    m_clientCommandHandler = std::move(handler);
}

void CServerCommands::SetClientChatHandler(ClientChatHandler handler)
{
    m_clientChatHandler = std::move(handler);
}

void ClientCommandHook2(void* thisPtr, CPlayerSlot slot, const CCommand& args)
{
    if (!g_pServerCommands->HandleClientCommand(slot.Get(), args.GetCommandString()))
    {
        return;
    }
    return reinterpret_cast<decltype(&ClientCommandHook2)>(clientCommandHook2->GetOriginal())(thisPtr, slot, args);
}

static std::string GetChatText(const CCommand& args)
{
    std::string rawCmd = args.GetCommandString();

    size_t textStart;
    if (!rawCmd.empty() && rawCmd[0] == '"')
    {
        size_t closeQuote = rawCmd.find('"', 1);
        textStart = (closeQuote != std::string::npos) ? closeQuote + 1 : rawCmd.size();
    }
    else
    {
        textStart = strlen(args.Arg(0));
    }

    while (textStart < rawCmd.size() && rawCmd[textStart] == ' ')
        textStart++;

    std::string text = rawCmd.substr(textStart);
    if (!text.empty() && text.front() == '"')
        text.erase(0, 1);
    if (!text.empty() && text.back() == '"')
        text.pop_back();

    return text;
}

void DispatchConCommand(void* thisPtr, ConCommandRef cmd, const CCommandContext& ctx, const CCommand& args)
{
    CPlayerSlot slot = ctx.GetPlayerSlot();

    std::string gameText = "";
    bool shouldSend = true;

    if (slot.Get() != -1)
    {
        if (!g_pServerCommands->HandleClientCommand(slot.Get(), args.GetCommandString()))
        {
            return;
        }

        std::string command = str_tolower(args.Arg(0));
        if (command == "say" || command == "say_team")
        {
            if (!g_pPlayerManager->GetPlayer(slot.Get()))
            {
                return;
            }

            std::string text = GetChatText(args);
            if (text.empty())
            {
                return;
            }

            gameText = text;
            shouldSend = (g_pServerCommands->HandleCommand(slot.Get(), text, true) != 2);

            if (shouldSend && !g_pServerCommands->HandleClientChat(slot.Get(), text, command == "say_team"))
            {
                shouldSend = false;
            }
        }
    }

    if (shouldSend) reinterpret_cast<decltype(&DispatchConCommand)>(dispatchConCommandHook->GetOriginal())(thisPtr, cmd, ctx, args);

    if (gameText != "") g_pServerCommands->HandleCommand(slot.Get(), gameText, false);
}
