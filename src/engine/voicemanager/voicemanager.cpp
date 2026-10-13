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
#include <memory/hooks/vfunction.h>

#include <api/interfaces/interfaces.h>

bool SetClientListeningHook(void* _this, CPlayerSlot iReceiver, CPlayerSlot iSender, bool bListen);
void ClientCommandHook(void* _this, CPlayerSlot slot, const CCommand& args);
void ClientVoiceHook(void* _this, CPlayerSlot slot);

VFunctionHook g_SetClientListeningHook(reinterpret_cast<void*>(SetClientListeningHook), INTERFACEVERSION_VENGINESERVER, "IVEngineServer2::SetClientListening");
VFunctionHook g_ClientCommandHook(reinterpret_cast<void*>(ClientCommandHook), "server", "CSource2GameClients", "IServerGameClients::ClientCommand");
VFunctionHook g_ClientVoiceHook(reinterpret_cast<void*>(ClientVoiceHook), "server", "CSource2GameClients", "IServerGameClients::ClientVoice");

#define CBaseEntity_m_iTeamNum 0x9DC483B8A5BFEFB3

bool SetClientListeningHook(void* _this, CPlayerSlot iReceiver, CPlayerSlot iSender, bool bListen)
{
    IPlayer* receiver = g_pPlayerManager->GetPlayer(iReceiver.Get());
    if (!receiver) return reinterpret_cast<decltype(&SetClientListeningHook)>(g_SetClientListeningHook.GetOriginal())(_this, iReceiver, iSender, bListen);

    IPlayer* sender = g_pPlayerManager->GetPlayer(iSender.Get());
    if (!sender) return reinterpret_cast<decltype(&SetClientListeningHook)>(g_SetClientListeningHook.GetOriginal())(_this, iReceiver, iSender, bListen);

    auto& listenOverride = receiver->GetListenOverride(iSender.Get());
    auto& senderFlags = sender->GetVoiceFlags();
    auto& receiverFlags = receiver->GetVoiceFlags();
    auto& selfmutes = receiver->GetSelfMutes();

    if (selfmutes.Get(iSender.Get()))
    {
        return reinterpret_cast<decltype(&SetClientListeningHook)>(g_SetClientListeningHook.GetOriginal())(_this, iReceiver, iSender, false);
    }

    if (senderFlags & VoiceFlagValue::Speak_Muted)
    {
        return reinterpret_cast<decltype(&SetClientListeningHook)>(g_SetClientListeningHook.GetOriginal())(_this, iReceiver, iSender, false);
    }

    if (listenOverride == ListenOverride::Listen_Mute)
    {
        return reinterpret_cast<decltype(&SetClientListeningHook)>(g_SetClientListeningHook.GetOriginal())(_this, iReceiver, iSender, false);
    }
    else if (listenOverride == ListenOverride::Listen_Hear)
    {
        return reinterpret_cast<decltype(&SetClientListeningHook)>(g_SetClientListeningHook.GetOriginal())(_this, iReceiver, iSender, true);
    }

    if ((senderFlags & VoiceFlagValue::Speak_All) || (receiverFlags & VoiceFlagValue::Speak_ListenAll))
    {
        return reinterpret_cast<decltype(&SetClientListeningHook)>(g_SetClientListeningHook.GetOriginal())(_this, iReceiver, iSender, true);
    }

    if ((senderFlags & VoiceFlagValue::Speak_Team) || (receiverFlags & VoiceFlagValue::Speak_ListenTeam))
    {
        auto senderController = sender->GetController();
        auto receiverController = receiver->GetController();
        if (!senderController || !receiverController)
            return reinterpret_cast<decltype(&SetClientListeningHook)>(g_SetClientListeningHook.GetOriginal())(_this, iReceiver, iSender, bListen);

        bListen = (*(int*)(g_pSDKSchema->GetPropPtr(senderController, CBaseEntity_m_iTeamNum))) == (*(int*)(g_pSDKSchema->GetPropPtr(receiverController, CBaseEntity_m_iTeamNum)));
        return reinterpret_cast<decltype(&SetClientListeningHook)>(g_SetClientListeningHook.GetOriginal())(_this, iReceiver, iSender, bListen);
    }

    return reinterpret_cast<decltype(&SetClientListeningHook)>(g_SetClientListeningHook.GetOriginal())(_this, iReceiver, iSender, bListen);
}

void ClientCommandHook(void* _this, CPlayerSlot slot, const CCommand& args)
{
    IPlayer* receiver = g_pPlayerManager->GetPlayer(slot.Get());
    if (!receiver) return reinterpret_cast<decltype(&ClientCommandHook)>(g_ClientCommandHook.GetOriginal())(_this, slot, args);

    if (args.ArgC() > 1 && std::string(args.Arg(0)) == "vban")
    {
        uint32_t mask = 0;
        sscanf(args.Arg(1), "%x", &mask);
        auto& selfmutes = receiver->GetSelfMutes();
        selfmutes.SetDWord(0, mask);
    }

    return reinterpret_cast<decltype(&ClientCommandHook)>(g_ClientCommandHook.GetOriginal())(_this, slot, args);
}

extern void* g_pOnClientVoiceCallback;

void ClientVoiceHook(void* _this, CPlayerSlot slot)
{
    reinterpret_cast<decltype(&ClientVoiceHook)>(g_ClientVoiceHook.GetOriginal())(_this, slot);

    if (g_pOnClientVoiceCallback != nullptr)
        reinterpret_cast<void(*)(int)>(g_pOnClientVoiceCallback)(slot.Get());
}

void CVoiceManager::SetClientListenOverride(int playerid, int targetid, ListenOverride override)
{
    auto player = g_pPlayerManager->GetPlayer(playerid);
    if (!player) return;

    auto& listenOverrider = player->GetListenOverride(targetid);
    listenOverrider = override;
}

ListenOverride CVoiceManager::GetClientListenOverride(int playerid, int targetid)
{
    auto player = g_pPlayerManager->GetPlayer(playerid);
    if (!player) return Listen_Default;

    auto& listenOverrider = player->GetListenOverride(targetid);
    return listenOverrider;
}

void CVoiceManager::SetClientVoiceFlags(int playerid, VoiceFlagValue flags)
{
    auto player = g_pPlayerManager->GetPlayer(playerid);
    if (!player) return;

    auto& voiceFlags = player->GetVoiceFlags();
    voiceFlags = flags;
}

VoiceFlagValue CVoiceManager::GetClientVoiceFlags(int playerid)
{
    auto player = g_pPlayerManager->GetPlayer(playerid);
    if (!player) return Speak_Normal;

    auto& voiceFlags = player->GetVoiceFlags();
    return voiceFlags;
}
