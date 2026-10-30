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

#include "netmessages.h"
#include <memory/hooks/vfunction.h>
#include <memory/hooks/function.h>
#include "cstrike15_usermessages.pb.h"
#include "gameevents.pb.h"

#include <cstdint>
#include <functional>
#include <string>
#include <utility>

#include <api/interfaces/interfaces.h>
#include <public/engine2/serversideclient.h>

#include <api/shared/hookresult.h>
#include <api/shared/plat.h>
#include <api/shared/profiler.h>

constexpr uintptr_t kProtoToNetMessageOffset = 48;

bool bypassPostEventAbstractHook = false;

static std::function<int(uint64_t*, int, void*)> g_fnServerMessageSendHandler;
static std::function<int(int, int, void*)> g_fnClientMessageSendHandler;
static std::function<int(int, int, void*)> g_fnServerMessageInternalSendHandler;

bool FilterMessage(void* client, CNetMessage* cMsg, INetChannel* netchan);
void PostEventAbstractHook(void* _this, CSplitScreenSlot nSlot, bool bLocalOnly, int nClientCount, const uint64* clients,
    INetworkMessageInternal* pEvent, const CNetMessage* pData, unsigned long nSize, NetChannelBufType_t bufType);

bool SendNetMessage(CServerSideClient* client, CNetMessage* pData, NetChannelBufType_t bufType);

FunctionHook g_FilterMessageHook(reinterpret_cast<void*>(FilterMessage), "INetworkMessageProcessingPreFilter::FilterMessage");
VFunctionHook g_PostEventAbstractHook(reinterpret_cast<void*>(PostEventAbstractHook), "engine2", "CGameEventSystem", "IGameEventSystem::PostEventAbstract");
VFunctionHook g_SendNetMessageHook(reinterpret_cast<void*>(SendNetMessage), "engine2", "CServerSideClient", "CServerSideClient::SendNetMessage");

template <typename T>
int DispatchClientUserMessage(int playerid, int messageid, const std::string& payload)
{
    PROFILE_FUNCTION();

    if (!g_pNetMessages->IsMessageRegistered(NetMessageHook_Client, messageid)) return 0;

    T innerMessage;
    if (!innerMessage.ParseFromString(payload)) return 0;

    auto innerHandle = reinterpret_cast<void*>(reinterpret_cast<uintptr_t>(&innerMessage) - kProtoToNetMessageOffset);
    return g_fnClientMessageSendHandler(playerid, messageid, innerHandle);
}

int DispatchClientUserMessage(int playerid, const CSVCMsg_UserMessage& userMessage)
{
    PROFILE_FUNCTION();

    const auto& payload = userMessage.msg_data();

    switch (userMessage.msg_type())
    {
        case CS_UM_DisconnectToLobby:
            return DispatchClientUserMessage<CCSUsrMsg_DisconnectToLobby>(playerid, CS_UM_DisconnectToLobby, payload);
        case CS_UM_PlayerDecalDigitalSignature:
            return DispatchClientUserMessage<CCSUsrMsg_PlayerDecalDigitalSignature>(playerid, CS_UM_PlayerDecalDigitalSignature, payload);
        case CS_UM_CounterStrafe:
            return DispatchClientUserMessage<CCSUsrMsg_CounterStrafe>(playerid, CS_UM_CounterStrafe, payload);
        case CS_UM_CustomHudClicked:
            return DispatchClientUserMessage<CCSUsrMsg_CustomHudClicked>(playerid, CS_UM_CustomHudClicked, payload);
        default:
            return 0;
    }
}

bool SendNetMessage(CServerSideClient* client, CNetMessage* pData, NetChannelBufType_t bufType)
{
    PROFILE_FUNCTION();

    if (client && pData)
    {
        int msgid = pData->GetNetMessage()->GetNetMessageInfo()->m_MessageId;
        if (g_fnServerMessageInternalSendHandler && g_pNetMessages->IsMessageRegistered(NetMessageHook_ServerInternal, msgid))
        {
            auto playerid = client->GetPlayerSlot().Get();

            auto res = g_fnServerMessageInternalSendHandler(playerid, msgid, pData);
            if (res == HookResult_Stop || res == HookResult_CancelOriginal) return true;
        }
    }

    PROFILE_PAUSE();

    return reinterpret_cast<decltype(&SendNetMessage)>(g_SendNetMessageHook.GetOriginal())(client, pData, bufType);
}

bool FilterMessage(void* client, CNetMessage* cMsg, INetChannel* netchan)
{
    PROFILE_FUNCTION();

    if (client && cMsg && g_fnClientMessageSendHandler)
    {
        static auto playerIndex = g_pGameDataManager->GetOffsets()->Fetch("CServerSideClientBase::m_nClientSlot") - WIN_LINUX(8, 48);

        auto playerid = *reinterpret_cast<int*>(reinterpret_cast<uintptr_t>(client) + playerIndex);
        auto msgid = cMsg->GetNetMessage()->GetNetMessageInfo()->m_MessageId;

        if (msgid == GE_Source1LegacyListenEvents)
        {
            auto msg = static_cast<CMsgSource1LegacyListenEvents*>(cMsg->AsProto());
            CBitVector<8192> eventIDs = {};
            auto eventArrayBits = msg->mutable_eventarraybits();
            for(int i = 0; i < eventArrayBits->size(); i++)
                eventIDs.SetDWord(i, eventArrayBits->Get(i));

            auto player = g_pPlayerManager->GetPlayer(playerid);
            if (player) player->SetListeningGameEvents(eventIDs);
        }

        bool stopOriginal = false;

        if (g_pNetMessages->IsMessageRegistered(NetMessageHook_Client, msgid))
        {
            auto res = g_fnClientMessageSendHandler(playerid, msgid, cMsg);
            if (res == HookResult_Stop) return true;
            if (res == HookResult_CancelOriginal) stopOriginal = true;
        }

        if (msgid == svc_UserMessage)
        {
            auto userMessage = static_cast<CSVCMsg_UserMessage*>(cMsg->AsProto());
            if (userMessage)
            {
                auto innerResult = DispatchClientUserMessage(playerid, *userMessage);
                if (innerResult == HookResult_Stop) return true;
                if (innerResult == HookResult_CancelOriginal) stopOriginal = true;
            }
        }

        if (stopOriginal) return true;
    }

    PROFILE_PAUSE();

    return reinterpret_cast<decltype(&FilterMessage)>(g_FilterMessageHook.GetOriginal())(client, cMsg, netchan);
}

void PostEventAbstractHook(void* _this, CSplitScreenSlot nSlot, bool bLocalOnly, int nClientCount, const uint64* clients, INetworkMessageInternal* pEvent, const CNetMessage* pData, unsigned long nSize, NetChannelBufType_t bufType)
{
    PROFILE_FUNCTION();

    if (!bypassPostEventAbstractHook && g_fnServerMessageSendHandler)
    {
        int msgid = pEvent->GetNetMessageInfo()->m_MessageId;
        if (g_pNetMessages->IsMessageRegistered(NetMessageHook_Server, msgid))
        {
            auto playermask = reinterpret_cast<uint64_t*>(const_cast<uint64*>(clients));
            auto msg = const_cast<CNetMessage*>(pData);

            auto res = g_fnServerMessageSendHandler(playermask, msgid, msg);
            if (res == HookResult_Stop || res == HookResult_CancelOriginal) return;
        }
    }

    PROFILE_PAUSE();

    reinterpret_cast<decltype(&PostEventAbstractHook)>(g_PostEventAbstractHook.GetOriginal())(_this, nSlot, bLocalOnly, nClientCount, clients, pEvent, pData, nSize, bufType);
}

bool CNetMessages::IsValidHook(NetMessageHookType type, int messageid)
{
    PROFILE_FUNCTION();

    return type >= 0 && type < NetMessageHook_Count && messageid >= 0 && messageid < MESSAGE_COUNT;
}

void CNetMessages::RegisterMessageHook(NetMessageHookType type, int messageid)
{
    PROFILE_FUNCTION();

    if (!IsValidHook(type, messageid)) return;

    m_registeredMessages[type][messageid >> 6].fetch_or(1ULL << (messageid & 63), std::memory_order_release);
}

void CNetMessages::UnregisterMessageHook(NetMessageHookType type, int messageid)
{
    PROFILE_FUNCTION();

    if (!IsValidHook(type, messageid)) return;

    m_registeredMessages[type][messageid >> 6].fetch_and(~(1ULL << (messageid & 63)), std::memory_order_release);
}

bool CNetMessages::IsMessageRegistered(NetMessageHookType type, int messageid)
{
    PROFILE_FUNCTION();

    if (!IsValidHook(type, messageid)) return false;

    return (m_registeredMessages[type][messageid >> 6].load(std::memory_order_acquire) & (1ULL << (messageid & 63))) != 0;
}

void CNetMessages::SetServerMessageSendHandler(std::function<int(uint64_t*, int, void*)> handler)
{
    PROFILE_FUNCTION();

    g_fnServerMessageSendHandler = std::move(handler);
}

void CNetMessages::SetClientMessageSendHandler(std::function<int(int, int, void*)> handler)
{
    PROFILE_FUNCTION();

    g_fnClientMessageSendHandler = std::move(handler);
}

void CNetMessages::SetServerMessageInternalSendHandler(std::function<int(int, int, void*)> handler)
{
    PROFILE_FUNCTION();

    g_fnServerMessageInternalSendHandler = std::move(handler);
}
