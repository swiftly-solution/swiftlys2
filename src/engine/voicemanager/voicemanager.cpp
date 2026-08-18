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

#include <api/interfaces/interfaces.h>
#include <s2binlib/s2binlib.h>

IVFunctionHook* g_pSetClientListeningHook = nullptr;
IVFunctionHook* g_pClientCommandHook = nullptr;
IVFunctionHook* g_pClientVoiceHook = nullptr;

bool SetClientListeningHook(void* _this, CPlayerSlot iReceiver, CPlayerSlot iSender, bool bListen);
void ClientCommandHook(void* _this, CPlayerSlot slot, const CCommand& args);
void ClientVoiceHook(void* _this, CPlayerSlot slot);

#define CBaseEntity_m_iTeamNum 0x9DC483B8A5BFEFB3

void CVoiceManager::Initialize()
{
    g_pSetClientListeningHook = g_pHooksManager->CreateVFunctionHook();
    g_pSetClientListeningHook->SetHookFunction(INTERFACEVERSION_VENGINESERVER, g_pGameDataManager->GetOffsets()->Fetch("IVEngineServer2::SetClientListening"), (void*)SetClientListeningHook);
    g_pSetClientListeningHook->Enable();

    void* gameclientsvtable = nullptr;
    s2binlib_find_vtable("server", "CSource2GameClients", &gameclientsvtable);

    g_pClientCommandHook = g_pHooksManager->CreateVFunctionHook();
    g_pClientCommandHook->SetHookFunction(gameclientsvtable, g_pGameDataManager->GetOffsets()->Fetch("IServerGameClients::ClientCommand"), (void*)ClientCommandHook, true);
    g_pClientCommandHook->Enable();

    g_pClientVoiceHook = g_pHooksManager->CreateVFunctionHook();
    g_pClientVoiceHook->SetHookFunction(gameclientsvtable, g_pGameDataManager->GetOffsets()->Fetch("IServerGameClients::ClientVoice"), (void*)ClientVoiceHook, true);
    g_pClientVoiceHook->Enable();
}

void CVoiceManager::Shutdown()
{
    if (g_pSetClientListeningHook)
    {
        g_pSetClientListeningHook->Disable();
        g_pHooksManager->DestroyVFunctionHook(g_pSetClientListeningHook);
        g_pSetClientListeningHook = nullptr;
    }

    if (g_pClientCommandHook)
    {
        g_pClientCommandHook->Disable();
        g_pHooksManager->DestroyVFunctionHook(g_pClientCommandHook);
        g_pClientCommandHook = nullptr;
    }

    if (g_pClientVoiceHook)
    {
        g_pClientVoiceHook->Disable();
        g_pHooksManager->DestroyVFunctionHook(g_pClientVoiceHook);
        g_pClientVoiceHook = nullptr;
    }
}

bool SetClientListeningHook(void* _this, CPlayerSlot iReceiver, CPlayerSlot iSender, bool bListen)
{
    IPlayer* receiver = g_pPlayerManager->GetPlayer(iReceiver.Get());
    if (!receiver) return reinterpret_cast<decltype(&SetClientListeningHook)>(g_pSetClientListeningHook->GetOriginal())(_this, iReceiver, iSender, bListen);

    IPlayer* sender = g_pPlayerManager->GetPlayer(iSender.Get());
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

        bListen = (*(int*)(g_pSDKSchema->GetPropPtr(senderController, CBaseEntity_m_iTeamNum))) == (*(int*)(g_pSDKSchema->GetPropPtr(receiverController, CBaseEntity_m_iTeamNum)));
        return reinterpret_cast<decltype(&SetClientListeningHook)>(g_pSetClientListeningHook->GetOriginal())(_this, iReceiver, iSender, bListen);
    }

    return reinterpret_cast<decltype(&SetClientListeningHook)>(g_pSetClientListeningHook->GetOriginal())(_this, iReceiver, iSender, bListen);
}

void ClientCommandHook(void* _this, CPlayerSlot slot, const CCommand& args)
{
    IPlayer* receiver = g_pPlayerManager->GetPlayer(slot.Get());
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

extern void* g_pOnClientVoiceCallback;

void ClientVoiceHook(void* _this, CPlayerSlot slot)
{
    reinterpret_cast<decltype(&ClientVoiceHook)>(g_pClientVoiceHook->GetOriginal())(_this, slot);

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
