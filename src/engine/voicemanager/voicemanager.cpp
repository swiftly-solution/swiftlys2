/************************************************************************************************
 *  SwiftlyS2 is a scripting framework for Source2-based games.
 *  Copyright (C) 2023-2026 Swiftly Solution SRL via Sava Andrei-Sebastian and it's contributors
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

#include "voicemanager.h"

#include <core/bridge/metamod.h>

#include <api/interfaces/manager.h>
#include <s2binlib/s2binlib.h>

IVFunctionHook* g_pSetClientListeningHook = nullptr;
IVFunctionHook* g_pClientCommandHook = nullptr;

bool SetClientListeningHook(void* _this, CPlayerSlot iReceiver, CPlayerSlot iSender, bool bListen);
void ClientCommandHook(void* _this, CPlayerSlot slot, const CCommand& args);

#define CBaseEntity_m_iTeamNum 0x9DC483B8A5BFEFB3

void CVoiceManager::Initialize()
{
    auto hooksmanager = g_ifaceService.FetchInterface<IHooksManager>(HOOKSMANAGER_INTERFACE_VERSION);
    auto gamedata = g_ifaceService.FetchInterface<IGameDataManager>(GAMEDATA_INTERFACE_VERSION);

    g_pSetClientListeningHook = hooksmanager->CreateVFunctionHook();
    g_pSetClientListeningHook->SetHookFunction(INTERFACEVERSION_VENGINESERVER, gamedata->GetOffsets()->Fetch("IVEngineServer2::SetClientListening"), (void*)SetClientListeningHook);
    g_pSetClientListeningHook->Enable();

    void* gameclientsvtable = nullptr;
    s2binlib_find_vtable("server", "CSource2GameClients", &gameclientsvtable);

    g_pClientCommandHook = hooksmanager->CreateVFunctionHook();
    g_pClientCommandHook->SetHookFunction(gameclientsvtable, gamedata->GetOffsets()->Fetch("IServerGameClients::ClientCommand"), (void*)ClientCommandHook, true);
    g_pClientCommandHook->Enable();
}

void CVoiceManager::Shutdown()
{
    auto hooksmanager = g_ifaceService.FetchInterface<IHooksManager>(HOOKSMANAGER_INTERFACE_VERSION);

    if (g_pSetClientListeningHook)
    {
        g_pSetClientListeningHook->Disable();
        hooksmanager->DestroyVFunctionHook(g_pSetClientListeningHook);
        g_pSetClientListeningHook = nullptr;
    }

    if (g_pClientCommandHook)
    {
        g_pClientCommandHook->Disable();
        hooksmanager->DestroyVFunctionHook(g_pClientCommandHook);
        g_pClientCommandHook = nullptr;
    }
}

bool SetClientListeningHook(void* _this, CPlayerSlot iReceiver, CPlayerSlot iSender, bool bListen)
{
    static auto playermanager = g_ifaceService.FetchInterface<IPlayerManager>(PLAYERMANAGER_INTERFACE_VERSION);
    static auto sdkschema = g_ifaceService.FetchInterface<ISDKSchema>(SDKSCHEMA_INTERFACE_VERSION);

    IPlayer* receiver = playermanager->GetPlayer(iReceiver.Get());
    if (!receiver) return reinterpret_cast<decltype(&SetClientListeningHook)>(g_pSetClientListeningHook->GetOriginal())(_this, iReceiver, iSender, bListen);

    IPlayer* sender = playermanager->GetPlayer(iSender.Get());
    if (!sender) return reinterpret_cast<decltype(&SetClientListeningHook)>(g_pSetClientListeningHook->GetOriginal())(_this, iReceiver, iSender, bListen);

    auto& listenOverride = receiver->GetListenOverride(iSender.Get());
    auto& senderFlags = sender->GetVoiceFlags();
    auto& receiverFlags = receiver->GetVoiceFlags();
    auto& selfmutes = receiver->GetSelfMutes();

    if (selfmutes.Get(iSender.Get()))
    {
        return reinterpret_cast<decltype(&SetClientListeningHook)>(g_pSetClientListeningHook->GetOriginal())(_this, iReceiver, iSender, false);
    }

    if (senderFlags & VoiceFlagValue::Speak_Muted)
    {
        return reinterpret_cast<decltype(&SetClientListeningHook)>(g_pSetClientListeningHook->GetOriginal())(_this, iReceiver, iSender, false);
    }

    if (listenOverride == ListenOverride::Listen_Mute)
    {
        return reinterpret_cast<decltype(&SetClientListeningHook)>(g_pSetClientListeningHook->GetOriginal())(_this, iReceiver, iSender, false);
    }
    else if (listenOverride == ListenOverride::Listen_Hear)
    {
        return reinterpret_cast<decltype(&SetClientListeningHook)>(g_pSetClientListeningHook->GetOriginal())(_this, iReceiver, iSender, true);
    }

    if ((senderFlags & VoiceFlagValue::Speak_All) || (receiverFlags & VoiceFlagValue::Speak_ListenAll))
    {
        return reinterpret_cast<decltype(&SetClientListeningHook)>(g_pSetClientListeningHook->GetOriginal())(_this, iReceiver, iSender, true);
    }

    if ((senderFlags & VoiceFlagValue::Speak_Team) || (receiverFlags & VoiceFlagValue::Speak_ListenTeam))
    {
        auto senderController = sender->GetController();
        auto receiverController = receiver->GetController();
        if (!senderController || !receiverController)
            return reinterpret_cast<decltype(&SetClientListeningHook)>(g_pSetClientListeningHook->GetOriginal())(_this, iReceiver, iSender, bListen);

        bListen = (*(int*)(sdkschema->GetPropPtr(senderController, CBaseEntity_m_iTeamNum))) == (*(int*)(sdkschema->GetPropPtr(receiverController, CBaseEntity_m_iTeamNum)));
        return reinterpret_cast<decltype(&SetClientListeningHook)>(g_pSetClientListeningHook->GetOriginal())(_this, iReceiver, iSender, bListen);
    }

    return reinterpret_cast<decltype(&SetClientListeningHook)>(g_pSetClientListeningHook->GetOriginal())(_this, iReceiver, iSender, bListen);
}

void ClientCommandHook(void* _this, CPlayerSlot slot, const CCommand& args)
{
    static auto playermanager = g_ifaceService.FetchInterface<IPlayerManager>(PLAYERMANAGER_INTERFACE_VERSION);
    IPlayer* receiver = playermanager->GetPlayer(slot.Get());
    if (!receiver) return reinterpret_cast<decltype(&ClientCommandHook)>(g_pClientCommandHook->GetOriginal())(_this, slot, args);

    if (args.ArgC() > 1 && std::string(args.Arg(0)) == "vban")
    {
        uint32_t mask = 0;
        sscanf(args.Arg(1), "%x", &mask);
        auto& selfmutes = receiver->GetSelfMutes();
        selfmutes.SetDWord(0, mask);
    }

    return reinterpret_cast<decltype(&ClientCommandHook)>(g_pClientCommandHook->GetOriginal())(_this, slot, args);
}

void CVoiceManager::SetClientListenOverride(int playerid, int targetid, ListenOverride override)
{
    auto playermanager = g_ifaceService.FetchInterface<IPlayerManager>(PLAYERMANAGER_INTERFACE_VERSION);
    auto player = playermanager->GetPlayer(playerid);
    if (!player) return;

    auto& listenOverrider = player->GetListenOverride(targetid);
    listenOverrider = override;
}

ListenOverride CVoiceManager::GetClientListenOverride(int playerid, int targetid)
{
    auto playermanager = g_ifaceService.FetchInterface<IPlayerManager>(PLAYERMANAGER_INTERFACE_VERSION);
    auto player = playermanager->GetPlayer(playerid);
    if (!player) return Listen_Default;

    auto& listenOverrider = player->GetListenOverride(targetid);
    return listenOverrider;
}

void CVoiceManager::SetClientVoiceFlags(int playerid, VoiceFlagValue flags)
{
    auto playermanager = g_ifaceService.FetchInterface<IPlayerManager>(PLAYERMANAGER_INTERFACE_VERSION);
    auto player = playermanager->GetPlayer(playerid);
    if (!player) return;

    auto& voiceFlags = player->GetVoiceFlags();
    voiceFlags = flags;
}

VoiceFlagValue CVoiceManager::GetClientVoiceFlags(int playerid)
{
    auto playermanager = g_ifaceService.FetchInterface<IPlayerManager>(PLAYERMANAGER_INTERFACE_VERSION);
    auto player = playermanager->GetPlayer(playerid);
    if (!player) return Speak_Normal;

    auto& voiceFlags = player->GetVoiceFlags();
    return voiceFlags;
}
