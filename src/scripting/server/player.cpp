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

#include <api/interfaces/manager.h>
#include <scripting/scripting.h>

#include <api/memory/virtual/call.h>

void Bridge_Player_SendMessage(int playerid, int kind, const char* message, int duration)
{
    static auto playerManager = g_ifaceService.FetchInterface<IPlayerManager>(PLAYERMANAGER_INTERFACE_VERSION);
    auto player = playerManager->GetPlayer(playerid);
    if (!player)
        return;

    player->SendMsg((MessageType)kind, message, duration);
}

bool Bridge_Player_IsFakeClient(int playerid)
{
    static auto playerManager = g_ifaceService.FetchInterface<IPlayerManager>(PLAYERMANAGER_INTERFACE_VERSION);
    auto player = playerManager->GetPlayer(playerid);
    if (!player)
        return true;

    return player->IsFakeClient();
}

bool Bridge_Player_IsAuthorized(int playerid)
{
    static auto playerManager = g_ifaceService.FetchInterface<IPlayerManager>(PLAYERMANAGER_INTERFACE_VERSION);
    auto player = playerManager->GetPlayer(playerid);
    if (!player)
        return false;

    return player->IsAuthorized();
}

uint32_t Bridge_Player_GetConnectedTime(int playerid)
{
    static auto playerManager = g_ifaceService.FetchInterface<IPlayerManager>(PLAYERMANAGER_INTERFACE_VERSION);
    auto player = playerManager->GetPlayer(playerid);
    if (!player)
        return 0;

    return player->GetConnectedTime();
}

uint64_t Bridge_Player_GetUnauthorizedSteamID(int playerid)
{
    static auto playerManager = g_ifaceService.FetchInterface<IPlayerManager>(PLAYERMANAGER_INTERFACE_VERSION);
    auto player = playerManager->GetPlayer(playerid);
    if (!player)
        return 0;

    return player->GetUnauthorizedSteamID();
}

uint64_t Bridge_Player_GetSteamID(int playerid)
{
    static auto playerManager = g_ifaceService.FetchInterface<IPlayerManager>(PLAYERMANAGER_INTERFACE_VERSION);
    auto player = playerManager->GetPlayer(playerid);
    if (!player)
        return 0;

    return player->GetSteamID();
}

void* Bridge_Player_GetController(int playerid)
{
    static auto playerManager = g_ifaceService.FetchInterface<IPlayerManager>(PLAYERMANAGER_INTERFACE_VERSION);
    auto player = playerManager->GetPlayer(playerid);
    if (!player)
        return nullptr;

    return player->GetController();
}

void* Bridge_Player_GetPawn(int playerid)
{
    static auto playerManager = g_ifaceService.FetchInterface<IPlayerManager>(PLAYERMANAGER_INTERFACE_VERSION);
    auto player = playerManager->GetPlayer(playerid);
    if (!player)
        return nullptr;

    return player->GetPawn();
}

void* Bridge_Player_GetPlayerPawn(int playerid)
{
    static auto playerManager = g_ifaceService.FetchInterface<IPlayerManager>(PLAYERMANAGER_INTERFACE_VERSION);
    auto player = playerManager->GetPlayer(playerid);
    if (!player)
        return nullptr;

    return player->GetPlayerPawn();
}

uint64_t Bridge_Player_GetPressedButtons(int playerid)
{
    static auto playerManager = g_ifaceService.FetchInterface<IPlayerManager>(PLAYERMANAGER_INTERFACE_VERSION);
    auto player = playerManager->GetPlayer(playerid);
    if (!player)
        return 0;

    return player->GetPressedButtons();
}

void Bridge_Player_PerformCommand(int playerid, const char* command)
{
    static auto playerManager = g_ifaceService.FetchInterface<IPlayerManager>(PLAYERMANAGER_INTERFACE_VERSION);
    auto player = playerManager->GetPlayer(playerid);
    if (!player)
        return;

    player->PerformCommand(command);
}

int Bridge_Player_GetIPAddress(char* out, int playerid)
{
    static auto playerManager = g_ifaceService.FetchInterface<IPlayerManager>(PLAYERMANAGER_INTERFACE_VERSION);
    auto player = playerManager->GetPlayer(playerid);
    if (!player)
        return 0;

    static std::string s;
    s = player->GetIPAddress();

    if (out != nullptr)
        strcpy(out, s.c_str());

    return s.size();
}

void Bridge_Player_Kick(int playerid, const char* reason, int gamereason)
{
    static auto playerManager = g_ifaceService.FetchInterface<IPlayerManager>(PLAYERMANAGER_INTERFACE_VERSION);
    auto player = playerManager->GetPlayer(playerid);
    if (!player)
        return;

    player->Kick(reason, gamereason);
}

void Bridge_Player_ShouldBlockTransmitEntity(int playerid, int entityidx, bool shouldBlockTransmit)
{
    if (playerid + 1 == entityidx)
    {
        return;
    }

    static auto playerManager = g_ifaceService.FetchInterface<IPlayerManager>(PLAYERMANAGER_INTERFACE_VERSION);
    auto player = playerManager->GetPlayer(playerid);
    if (!player)
    {
        return;
    }

    auto& bv = player->GetBlockedTransmittingBits();

    auto dword = entityidx / 64;
    if (shouldBlockTransmit)
    {
        bool wasEmpty = (bv.blockedMask[dword] == 0);
        bv.blockedMask[dword] |= (1ULL << (entityidx % 64));
        if (wasEmpty)
        {
            bv.activeMasks.push_back(dword);
        }
    }
    else
    {
        bv.blockedMask[dword] &= ~(1ULL << (entityidx % 64));
        if (bv.blockedMask[dword] == 0)
        {
            auto it = std::find(bv.activeMasks.begin(), bv.activeMasks.end(), dword);
            if (it != bv.activeMasks.end())
            {
                bv.activeMasks.erase(it);
            }
        }
    }
}

bool Bridge_Player_IsTransmitEntityBlocked(int playerid, int entityidx)
{
    if (playerid + 1 == entityidx)
    {
        return false;
    }

    static auto playerManager = g_ifaceService.FetchInterface<IPlayerManager>(PLAYERMANAGER_INTERFACE_VERSION);
    auto player = playerManager->GetPlayer(playerid);
    if (!player)
    {
        return false;
    }

    auto& bv = player->GetBlockedTransmittingBits();
    return (bv.blockedMask[entityidx / 64] & (1ULL << (entityidx % 64))) != 0;
}

void Bridge_Player_ClearTransmitEntityBlocked(int playerid)
{
    static auto playerManager = g_ifaceService.FetchInterface<IPlayerManager>(PLAYERMANAGER_INTERFACE_VERSION);
    auto player = playerManager->GetPlayer(playerid);
    if (!player)
        return;

    auto& bv = player->GetBlockedTransmittingBits();
    for (int i = 0; i < 512; i++)
        bv.blockedMask[i] = 0;
    bv.activeMasks.clear();
}

void Bridge_Player_ChangeTeam(int playerid, int newteam)
{
    static auto playerManager = g_ifaceService.FetchInterface<IPlayerManager>(PLAYERMANAGER_INTERFACE_VERSION);
    auto player = playerManager->GetPlayer(playerid);
    if (!player)
        return;

    static auto gamedata = g_ifaceService.FetchInterface<IGameDataManager>(GAMEDATA_INTERFACE_VERSION);
    CALL_VIRTUAL(void, gamedata->GetOffsets()->Fetch("CCSPlayerController::ChangeTeam"), player->GetController(), newteam);
}

void Bridge_Player_SwitchTeam(int playerid, int newteam)
{
    static auto playerManager = g_ifaceService.FetchInterface<IPlayerManager>(PLAYERMANAGER_INTERFACE_VERSION);
    auto player = playerManager->GetPlayer(playerid);
    if (!player)
        return;

    static auto gamedata = g_ifaceService.FetchInterface<IGameDataManager>(GAMEDATA_INTERFACE_VERSION);
    if (newteam == 0 || newteam == 1)
        CALL_VIRTUAL(void, gamedata->GetOffsets()->Fetch("CCSPlayerController::ChangeTeam"), player->GetController(), newteam);
    else
        reinterpret_cast<void (*)(void*, int)>(gamedata->GetSignatures()->Fetch("CCSPlayerController::SwitchTeam"))(player->GetController(), newteam);
}

void Bridge_Player_TakeDamage(int playerid, void* dmginfo)
{
    static auto playerManager = g_ifaceService.FetchInterface<IPlayerManager>(PLAYERMANAGER_INTERFACE_VERSION);
    auto player = playerManager->GetPlayer(playerid);
    if (!player)
        return;

    static auto gamedata = g_ifaceService.FetchInterface<IGameDataManager>(GAMEDATA_INTERFACE_VERSION);
    reinterpret_cast<int64_t(*)(void*, void*, void*)>(gamedata->GetSignatures()->Fetch("CBaseEntity::TakeDamage"))(player->GetPawn(), dmginfo, 0);
}

void Bridge_Player_Teleport(int playerid, Vector pos, QAngle angle, Vector vel)
{
    static auto playerManager = g_ifaceService.FetchInterface<IPlayerManager>(PLAYERMANAGER_INTERFACE_VERSION);
    auto player = playerManager->GetPlayer(playerid);
    if (!player)
        return;

    static auto gamedata = g_ifaceService.FetchInterface<IGameDataManager>(GAMEDATA_INTERFACE_VERSION);
    CALL_VIRTUAL(void, gamedata->GetOffsets()->Fetch("CBaseEntity::Teleport"), player->GetPawn(), &pos, &angle, &vel);
}

int Bridge_Player_GetLanguage(char* out, int playerid)
{
    static auto playerManager = g_ifaceService.FetchInterface<IPlayerManager>(PLAYERMANAGER_INTERFACE_VERSION);
    auto player = playerManager->GetPlayer(playerid);
    if (!player)
        return 0;

    static std::string s;
    s = player->GetLanguage();

    if (out != nullptr)
        strcpy(out, s.c_str());

    return s.size();
}

void Bridge_Player_SetCenterMenuRender(int playerid, const char* text)
{
    static auto playerManager = g_ifaceService.FetchInterface<IPlayerManager>(PLAYERMANAGER_INTERFACE_VERSION);
    auto player = playerManager->GetPlayer(playerid);
    if (!player)
        return;

    player->RenderMenuCenterText(text);
}

void Bridge_Player_ClearCenterMenuRender(int playerid)
{
    static auto playerManager = g_ifaceService.FetchInterface<IPlayerManager>(PLAYERMANAGER_INTERFACE_VERSION);
    auto player = playerManager->GetPlayer(playerid);
    if (!player)
        return;

    player->ClearRenderMenuCenterText();
}

bool Bridge_Player_HasMenuShown(int playerid)
{
    static auto playerManager = g_ifaceService.FetchInterface<IPlayerManager>(PLAYERMANAGER_INTERFACE_VERSION);
    auto player = playerManager->GetPlayer(playerid);
    if (!player)
        return false;

    return player->HasMenuShown();
}

void Bridge_Player_ExecuteCommand(int playerid, const char* command)
{
    static auto playerManager = g_ifaceService.FetchInterface<IPlayerManager>(PLAYERMANAGER_INTERFACE_VERSION);
    auto player = playerManager->GetPlayer(playerid);
    if (!player)
        return;

    CCommand cmd;
    cmd.Tokenize(command);

    ConCommandRef cmdRef(cmd[0]);

    if (cmdRef.IsValidRef() && !cmdRef.IsFlagSet(FCVAR_SERVER_CAN_EXECUTE))
    {
        CCommandContext context(CommandTarget_t::CT_FIRST_SPLITSCREEN_CLIENT, CPlayerSlot(player->GetSlot()));
        cmdRef.Dispatch(context, cmd);
    }
    else
    {
        static auto engine = g_ifaceService.FetchInterface<IVEngineServer2>(INTERFACEVERSION_VENGINESERVER);
        engine->ClientCommand(player->GetSlot(), command);
    }
}

uint8_t Bridge_Player_IsFirstSpawn(int playerid)
{
    static auto playerManager = g_ifaceService.FetchInterface<IPlayerManager>(PLAYERMANAGER_INTERFACE_VERSION);
    auto player = playerManager->GetPlayer(playerid);
    if (!player)
        return 0;

    return player->IsFirstSpawn() ? 1 : 0;
}

int Bridge_Player_GetUserID(int playerid)
{
    static auto engine = g_ifaceService.FetchInterface<IVEngineServer2>(INTERFACEVERSION_VENGINESERVER);
    return engine->GetPlayerUserId(CPlayerSlot(playerid)).Get();
}

DEFINE_NATIVE("Player.SendMessage", Bridge_Player_SendMessage);
DEFINE_NATIVE("Player.IsFakeClient", Bridge_Player_IsFakeClient);
DEFINE_NATIVE("Player.IsAuthorized", Bridge_Player_IsAuthorized);
DEFINE_NATIVE("Player.GetConnectedTime", Bridge_Player_GetConnectedTime);
DEFINE_NATIVE("Player.GetUnauthorizedSteamID", Bridge_Player_GetUnauthorizedSteamID);
DEFINE_NATIVE("Player.GetSteamID", Bridge_Player_GetSteamID);
DEFINE_NATIVE("Player.GetController", Bridge_Player_GetController);
DEFINE_NATIVE("Player.GetPawn", Bridge_Player_GetPawn);
DEFINE_NATIVE("Player.GetPlayerPawn", Bridge_Player_GetPlayerPawn);
DEFINE_NATIVE("Player.GetPressedButtons", Bridge_Player_GetPressedButtons);
DEFINE_NATIVE("Player.PerformCommand", Bridge_Player_PerformCommand);
DEFINE_NATIVE("Player.GetIPAddress", Bridge_Player_GetIPAddress);
DEFINE_NATIVE("Player.Kick", Bridge_Player_Kick);
DEFINE_NATIVE("Player.ShouldBlockTransmitEntity", Bridge_Player_ShouldBlockTransmitEntity);
DEFINE_NATIVE("Player.IsTransmitEntityBlocked", Bridge_Player_IsTransmitEntityBlocked);
DEFINE_NATIVE("Player.ClearTransmitEntityBlocked", Bridge_Player_ClearTransmitEntityBlocked);
DEFINE_NATIVE("Player.ChangeTeam", Bridge_Player_ChangeTeam);
DEFINE_NATIVE("Player.SwitchTeam", Bridge_Player_SwitchTeam);
DEFINE_NATIVE("Player.TakeDamage", Bridge_Player_TakeDamage);
DEFINE_NATIVE("Player.Teleport", Bridge_Player_Teleport);
DEFINE_NATIVE("Player.GetLanguage", Bridge_Player_GetLanguage);
DEFINE_NATIVE("Player.SetCenterMenuRender", Bridge_Player_SetCenterMenuRender);
DEFINE_NATIVE("Player.ClearCenterMenuRender", Bridge_Player_ClearCenterMenuRender);
DEFINE_NATIVE("Player.HasMenuShown", Bridge_Player_HasMenuShown);
DEFINE_NATIVE("Player.ExecuteCommand", Bridge_Player_ExecuteCommand);
DEFINE_NATIVE("Player.IsFirstSpawn", Bridge_Player_IsFirstSpawn);
DEFINE_NATIVE("Player.GetUserID", Bridge_Player_GetUserID);