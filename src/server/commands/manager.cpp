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

#include <api/interfaces/manager.h>

#include <api/shared/string.h>

#include <core/bridge/metamod.h>

#include <cstdio>
#include <public/icvar.h>
#include <s2binlib/s2binlib.h>

std::map<std::string, ConCommand*> conCommandCreated;
std::map<uint64_t, std::string> conCommandMapping;

std::map<std::string, std::function<void(int, std::vector<std::string>, std::string, std::string, bool)>> commandHandlers;

std::map<uint64_t, std::function<int(int, const std::string&)>> clientCommandListeners;
std::map<uint64_t, std::function<int(int, const std::string&, bool)>> clientChatListeners;

std::set<std::string> commandPrefixes;
std::set<std::string> silentCommandPrefixes;

void DispatchConCommand(void* thisPtr, ConCommandRef cmd, const CCommandContext& ctx, const CCommand& args);
IVFunctionHook* dispatchConCommandHook = nullptr;

void ClientCommandHook2(void* thisPtr, CPlayerSlot slot, const CCommand& args);
IVFunctionHook* clientCommandHook2 = nullptr;

void CommandsCallback(const CCommandContext& context, const CCommand& args)
{
    CCommand tokenizedArgs;
    tokenizedArgs.Tokenize(args.GetCommandString());

    std::string commandName = tokenizedArgs[0];

    std::transform(commandName.begin(), commandName.end(), commandName.begin(), ::tolower);
    std::string originalCommandName = commandName;
    if (!commandHandlers.contains(commandName))
    {
        commandName = "sw_" + commandName;
    }
    if (!commandHandlers.contains(commandName))
    {
        return;
    }

    std::vector<std::string> argsplit = TokenizeCommand(args.GetCommandString());
    argsplit.erase(argsplit.begin());

    auto& handler = commandHandlers[commandName];
    handler(context.GetPlayerSlot().Get(), argsplit, originalCommandName, "sw_", true);
}

void CServerCommands::Initialize()
{
    static auto hooksmanager = g_ifaceService.FetchInterface<IHooksManager>(HOOKSMANAGER_INTERFACE_VERSION);
    static auto gamedata = g_ifaceService.FetchInterface<IGameDataManager>(GAMEDATA_INTERFACE_VERSION);

    void* ccvarVTable;
    s2binlib_find_vtable("tier0", "CCvar", &ccvarVTable);

    dispatchConCommandHook = hooksmanager->CreateVFunctionHook();
    dispatchConCommandHook->SetHookFunction(ccvarVTable, gamedata->GetOffsets()->Fetch("ICvar::DispatchConCommand"), (void*)DispatchConCommand, true);
    dispatchConCommandHook->Enable();

    void* gameclientsvtable = nullptr;
    s2binlib_find_vtable("server", "CSource2GameClients", &gameclientsvtable);

    clientCommandHook2 = hooksmanager->CreateVFunctionHook();
    clientCommandHook2->SetHookFunction(gameclientsvtable, gamedata->GetOffsets()->Fetch("IServerGameClients::ClientCommand"), (void*)ClientCommandHook2, true);
    clientCommandHook2->Enable();
}

void CServerCommands::Shutdown()
{
    static auto hooksmanager = g_ifaceService.FetchInterface<IHooksManager>(HOOKSMANAGER_INTERFACE_VERSION);
    if (dispatchConCommandHook)
    {
        dispatchConCommandHook->Disable();
        hooksmanager->DestroyVFunctionHook(dispatchConCommandHook);
        dispatchConCommandHook = nullptr;
    }

    if (clientCommandHook2)
    {
        clientCommandHook2->Disable();
        hooksmanager->DestroyVFunctionHook(clientCommandHook2);
        clientCommandHook2 = nullptr;
    }
}

// @returns 1 - command is not silent
// @returns 2 - command is silent
// @returns -1 - invalid controller
// @returns 0 - is not command
int CServerCommands::HandleCommand(int playerid, const std::string& text, bool dryrun)
{
    if (text == "" || text.size() == 0)
    {
        return -1;
    }

    static auto playermanager = g_ifaceService.FetchInterface<IPlayerManager>(PLAYERMANAGER_INTERFACE_VERSION);
    static auto configuration = g_ifaceService.FetchInterface<IConfiguration>(CONFIGURATION_INTERFACE_VERSION);

    IPlayer* player = playermanager->GetPlayer(playerid);
    if (player == nullptr)
    {
        return -1;
    }

    if (commandPrefixes.size() == 0)
    {
        commandPrefixes = explodeToSet(std::get<std::string>(configuration->GetValue("core.CommandPrefixes")), " ");
    }

    if (silentCommandPrefixes.size() == 0)
    {
        silentCommandPrefixes = explodeToSet(std::get<std::string>(configuration->GetValue("core.CommandSilentPrefixes")), " ");
    }

    bool isCommand = false;
    bool isSilentCommand = false;
    std::string selectedPrefix = "";

    if (commandPrefixes.size() > 0)
    {
        for (auto it = commandPrefixes.begin(); it != commandPrefixes.end(); ++it)
        {
            std::string prefix = *it;
            auto strPrefix = text.substr(0, prefix.size());

            if (prefix == strPrefix)
            {
                isCommand = true;
                selectedPrefix = prefix;
                break;
            }
        }
    }

    if (!isCommand && silentCommandPrefixes.size() > 0)
    {
        for (auto it = silentCommandPrefixes.begin(); it != silentCommandPrefixes.end(); ++it)
        {
            std::string prefix = *it;
            auto strPrefix = text.substr(0, prefix.size());

            if (prefix == strPrefix)
            {
                isSilentCommand = true;
                selectedPrefix = prefix;
                break;
            }
        }
    }

    if (isCommand || isSilentCommand)
    {
        CCommand tokenizedArgs;
        tokenizedArgs.Tokenize(text.c_str());

        std::vector<std::string> cmdString = TokenizeCommand(text);
        cmdString.erase(cmdString.begin());

        if (tokenizedArgs.ArgC() < 1)
        {
            return 0;
        }

        std::string commandName = tokenizedArgs[0];
        if (commandName.size() < 1)
        {
            return 0;
        }

        commandName.erase(0, selectedPrefix.size());
        std::transform(commandName.begin(), commandName.end(), commandName.begin(), ::tolower);
        std::string originalCommandName = commandName;
        if (!commandHandlers.contains(commandName))
        {
            commandName = "sw_" + commandName;
        }
        if (!commandHandlers.contains(commandName))
        {
            return 0;
        }

        commandHandlers[commandName](playerid, cmdString, originalCommandName, selectedPrefix, isSilentCommand);
    }

    if (isCommand)
    {
        return 1;
    }
    else if (isSilentCommand)
    {
        return 2;
    }
    else
    {
        return 0;
    }
}

bool CServerCommands::HandleClientCommand(int playerid, const std::string& text)
{
    for (const auto& [id, listener] : clientCommandListeners)
    {
        auto res = listener(playerid, text);
        if (res == 1)
        {
            return false;
        }
        else if (res == 2)
        {
            break;
        }
    }

    return true;
}

bool CServerCommands::HandleClientChat(int playerid, const std::string& text, bool teamonly)
{
    for (const auto& [id, listener] : clientChatListeners)
    {
        auto res = listener(playerid, text, teamonly);
        if (res == 1)
        {
            return false;
        }
        else if (res == 2)
        {
            break;
        }
    }

    return true;
}

uint64_t CServerCommands::RegisterCommand(std::string commandName, std::function<void(int, std::vector<std::string>, std::string, std::string, bool)> handler, bool registerRaw)
{
    std::transform(commandName.begin(), commandName.end(), commandName.begin(), ::tolower);

    if (!registerRaw)
    {
        if (conCommandCreated.contains(commandName))
        {
            return 0;
        }
        commandName = "sw_" + commandName;
    }

    static uint64_t commandId = 0;
    if (!conCommandCreated.contains(commandName))
    {
        // printf("RegisterCommand -> commandName: %s, handler: %p, registerRaw: %d\n", commandName.c_str(), handler, registerRaw);
        conCommandCreated[commandName] = new ConCommand(commandName.c_str(), CommandsCallback, "SwiftlyS2 registered command", FCVAR_CLIENT_CAN_EXECUTE | FCVAR_LINKED_CONCOMMAND);
        conCommandMapping[++commandId] = commandName;
        commandHandlers[commandName] = handler;
        conCommandCreated[commandName]->RemoveFlags(FCVAR_SERVER_CAN_EXECUTE);
    }
    return commandId;
}

void CServerCommands::UnregisterCommand(uint64_t commandId)
{
    if (commandId == 0)
    {
        return;
    }

    auto mappingIt = conCommandMapping.find(commandId);
    if (mappingIt == conCommandMapping.end())
    {
        return;
    }

    const std::string& commandName = mappingIt->second;
    auto createdIt = conCommandCreated.find(commandName);
    auto handlerIt = commandHandlers.find(commandName);

    ConCommand* conCommand = nullptr;
    if (createdIt != conCommandCreated.end())
    {
        conCommand = createdIt->second;
    }

    conCommandMapping.erase(mappingIt);
    if (handlerIt != commandHandlers.end())
    {
        commandHandlers.erase(handlerIt);
    }
    if (createdIt != conCommandCreated.end())
    {
        conCommandCreated.erase(createdIt);
    }

    delete conCommand;
}

bool CServerCommands::IsCommandRegistered(std::string commandName)
{
    std::transform(commandName.begin(), commandName.end(), commandName.begin(), ::tolower);
    if (commandHandlers.contains(commandName))
    {
        return true;
    }

    commandName = "sw_" + commandName;
    if (commandHandlers.contains(commandName))
    {
        return true;
    }

    return false;
}

uint64_t CServerCommands::RegisterAlias(std::string aliasCommand, std::string commandName, bool registerRaw)
{
    std::transform(commandName.begin(), commandName.end(), commandName.begin(), ::tolower);
    if (!commandHandlers.contains(commandName))
    {
        commandName = "sw_" + commandName;
        if (!commandHandlers.contains(commandName))
        {
            return 0;
        }
    }
    return RegisterCommand(aliasCommand, commandHandlers[commandName], registerRaw);
}

void CServerCommands::UnregisterAlias(uint64_t aliasId)
{
    return UnregisterCommand(aliasId);
}

uint64_t CServerCommands::RegisterClientCommandsListener(std::function<int(int, const std::string&)> listener)
{
    static uint64_t listenerId = 0;
    clientCommandListeners[++listenerId] = listener;
    return listenerId;
}

void CServerCommands::UnregisterClientCommandsListener(uint64_t listenerId)
{
    clientCommandListeners.erase(listenerId);
}

uint64_t CServerCommands::RegisterClientChatListener(std::function<int(int, const std::string&, bool)> listener)
{
    static uint64_t listenerId = 0;
    clientChatListeners[++listenerId] = listener;
    return listenerId;
}

void CServerCommands::UnregisterClientChatListener(uint64_t listenerId)
{
    clientChatListeners.erase(listenerId);
}

void ClientCommandHook2(void* thisPtr, CPlayerSlot slot, const CCommand& args)
{
    static auto servercommands = g_ifaceService.FetchInterface<IServerCommands>(SERVERCOMMANDS_INTERFACE_VERSION);
    if (!servercommands->HandleClientCommand(slot.Get(), args.GetCommandString()))
    {
        return;
    }
    return reinterpret_cast<decltype(&ClientCommandHook2)>(clientCommandHook2->GetOriginal())(thisPtr, slot, args);
}

void DispatchConCommand(void* thisPtr, ConCommandRef cmd, const CCommandContext& ctx, const CCommand& args)
{
    CPlayerSlot slot = ctx.GetPlayerSlot();
    static auto servercommands = g_ifaceService.FetchInterface<IServerCommands>(SERVERCOMMANDS_INTERFACE_VERSION);
    static auto playermanager = g_ifaceService.FetchInterface<IPlayerManager>(PLAYERMANAGER_INTERFACE_VERSION);
    static auto eventmanager = g_ifaceService.FetchInterface<IEventManager>(GAMEEVENTMANAGER_INTERFACE_VERSION);

    if (slot.Get() != -1)
    {
        if (!servercommands->HandleClientCommand(slot.Get(), args.GetCommandString()))
        {
            return;
        }

        std::string command = args.Arg(0);
        if (command == "say" || command == "say_team")
        {
            auto player = playermanager->GetPlayer(slot.Get());
            if (!player)
            {
                return;
            }

            void* controller = player->GetController();
            bool teamonly = (command == "say_team");
            auto text = args[1];
            if (strlen(text) == 0)
            {
                return;
            }

            if (controller)
            {
                IGameEvent* pEvent = eventmanager->GetGameEventManager()->CreateEvent("player_chat");
                if (pEvent)
                {
                    pEvent->SetBool("teamonly", teamonly);
                    pEvent->SetInt("userid", slot.Get());
                    pEvent->SetString("text", text);
                    eventmanager->GetGameEventManager()->FireEvent(pEvent, true);
                }
            }

            int handleCommandReturn = servercommands->HandleCommand(slot.Get(), text, false);
            if (handleCommandReturn == 2 || !servercommands->HandleClientChat(slot.Get(), text, teamonly))
            {
                return;
            }
        }
    }

    return reinterpret_cast<decltype(&DispatchConCommand)>(dispatchConCommandHook->GetOriginal())(thisPtr, cmd, ctx, args);
}