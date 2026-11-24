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
#include <api/shared/profiler.h>

#include <cstdio>
#include <cstring>

bool SetClientListeningHook(void* _this, CPlayerSlot iReceiver, CPlayerSlot iSender, bool bListen);
void ClientCommandHook(void* _this, CPlayerSlot slot, const CCommand& args);

VFunctionHook g_SetClientListeningHook(reinterpret_cast<void*>(SetClientListeningHook), "engine2", "CEngineServer", "CEngineServer::SetClientListening");
VFunctionHook g_ClientCommandHook(reinterpret_cast<void*>(ClientCommandHook), "server", "CSource2GameClients", "CSource2GameClients::ClientCommand");

bool ResolveListen(IPlayer* receiver, IPlayer* sender, int senderSlot, bool bListen)
{
    PROFILE_FUNCTION();

    const VoiceFlagValue senderFlags = sender->GetVoiceFlags();
    const VoiceFlagValue receiverFlags = receiver->GetVoiceFlags();
    const ListenOverride listenOverride = receiver->GetListenOverride(senderSlot);

    if (receiver->GetSelfMutes().Get(senderSlot) || (senderFlags & Speak_Muted) || listenOverride == Listen_Mute)
        return false;

    if (listenOverride == Listen_Hear || (senderFlags & Speak_All) || (receiverFlags & Speak_ListenAll))
        return true;

    if ((senderFlags & Speak_Team) || (receiverFlags & Speak_ListenTeam))
    {
        auto senderTeam = sender->GetTeam();
        auto receiverTeam = receiver->GetTeam();
        if (senderTeam && receiverTeam)
            return *senderTeam == *receiverTeam;
    }

    return bListen;
}

bool SetClientListeningHook(void* _this, CPlayerSlot iReceiver, CPlayerSlot iSender, bool bListen)
{
    PROFILE_FUNCTION();

    IPlayer* receiver = g_pPlayerManager->GetPlayer(iReceiver.Get());
    IPlayer* sender = g_pPlayerManager->GetPlayer(iSender.Get());
    if (receiver && sender)
        bListen = ResolveListen(receiver, sender, iSender.Get(), bListen);

    PROFILE_PAUSE();

    return reinterpret_cast<decltype(&SetClientListeningHook)>(g_SetClientListeningHook.GetOriginal())(_this, iReceiver, iSender, bListen);
}

void ClientCommandHook(void* _this, CPlayerSlot slot, const CCommand& args)
{
    PROFILE_FUNCTION();

    IPlayer* receiver = g_pPlayerManager->GetPlayer(slot.Get());
    if (receiver && args.ArgC() > 1 && std::strcmp(args.Arg(0), "vban") == 0)
    {
        uint32_t mask = 0;
        sscanf(args.Arg(1), "%x", &mask);
        receiver->GetSelfMutes().SetDWord(0, mask);
    }

    PROFILE_PAUSE();

    reinterpret_cast<decltype(&ClientCommandHook)>(g_ClientCommandHook.GetOriginal())(_this, slot, args);
}

void CVoiceManager::SetClientListenOverride(int playerid, int targetid, ListenOverride value)
{
    PROFILE_FUNCTION();

    auto player = g_pPlayerManager->GetPlayer(playerid);
    if (!player) return;
    player->GetListenOverride(targetid) = value;
}

ListenOverride CVoiceManager::GetClientListenOverride(int playerid, int targetid)
{
    PROFILE_FUNCTION();

    auto player = g_pPlayerManager->GetPlayer(playerid);
    if (!player) return Listen_Default;
    return player->GetListenOverride(targetid);
}

void CVoiceManager::SetClientVoiceFlags(int playerid, VoiceFlagValue flags)
{
    PROFILE_FUNCTION();

    auto player = g_pPlayerManager->GetPlayer(playerid);
    if (!player) return;

    player->GetVoiceFlags() = flags;
}

VoiceFlagValue CVoiceManager::GetClientVoiceFlags(int playerid)
{
    PROFILE_FUNCTION();

    auto player = g_pPlayerManager->GetPlayer(playerid);
    if (!player) return Speak_Normal;
    return player->GetVoiceFlags();
}
