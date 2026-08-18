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

#include <api/interfaces/interfaces.h>
#include <api/sdk/serversideclient.h>
#include <memory/gamedata/manager.h>

#include <api/shared/plat.h>
#include <s2binlib/s2binlib.h>

std::function<int(uint64_t*, int, void*)> g_fnServerMessageSendHandler;
std::function<int(int, int, void*)> g_fnClientMessageSendHandler;
std::function<int(int, int, void*)> g_fnServerMessageInternalSendHandler;

IFunctionHook* g_pFilterMessageHook = nullptr;
IVFunctionHook* g_pPostEventAbstractHook = nullptr;
IVFunctionHook* g_pSendNetMessageHook = nullptr;

bool bypassPostEventAbstractHook = false;

bool FilterMessage(void* client, CNetMessage* cMsg, INetChannel* netchan);
void PostEventAbstractHook(void* _this, CSplitScreenSlot nSlot, bool bLocalOnly, int nClientCount, const uint64* clients,
    INetworkMessageInternal* pEvent, const CNetMessage* pData, unsigned long nSize, NetChannelBufType_t bufType);

bool SendNetMessage(CServerSideClient* client, CNetMessage* pData, NetChannelBufType_t bufType);

void CNetMessages::Initialize()
{
    g_pFilterMessageHook = g_pHooksManager->CreateFunctionHook();
    g_pFilterMessageHook->SetHookFunction(g_pGameDataManager->GetSignatures()->Fetch("INetworkMessageProcessingPreFilter::FilterMessage"), (void*)FilterMessage);
    g_pFilterMessageHook->Enable();

    void* gameEventSystem = nullptr;
    s2binlib_find_vtable("engine2", "CGameEventSystem", &gameEventSystem);

    g_pPostEventAbstractHook = g_pHooksManager->CreateVFunctionHook();
    g_pPostEventAbstractHook->SetHookFunction(gameEventSystem, g_pGameDataManager->GetOffsets()->Fetch("IGameEventSystem::PostEventAbstract"), (void*)PostEventAbstractHook, true);
    g_pPostEventAbstractHook->Enable();

    void* serverSideClientVTable = nullptr;
    s2binlib_find_vtable("engine2", "CServerSideClient", &serverSideClientVTable);

    g_pSendNetMessageHook = g_pHooksManager->CreateVFunctionHook();
    g_pSendNetMessageHook->SetHookFunction(serverSideClientVTable, g_pGameDataManager->GetOffsets()->Fetch("CServerSideClient::SendNetMessage"), (void*)SendNetMessage, true);
    g_pSendNetMessageHook->Enable();
}

void CNetMessages::Shutdown()
{
    g_pFilterMessageHook->Disable();
    g_pPostEventAbstractHook->Disable();

    g_pHooksManager->DestroyFunctionHook(g_pFilterMessageHook);
    g_pHooksManager->DestroyVFunctionHook(g_pPostEventAbstractHook);
    g_pHooksManager->DestroyVFunctionHook(g_pSendNetMessageHook);
}

bool SendNetMessage(CServerSideClient* client, CNetMessage* pData, NetChannelBufType_t bufType)
{
    if (!client) return reinterpret_cast<decltype(&SendNetMessage)>(g_pSendNetMessageHook->GetOriginal())(client, pData, bufType);
    if (!pData) return reinterpret_cast<decltype(&SendNetMessage)>(g_pSendNetMessageHook->GetOriginal())(client, pData, bufType);

    auto playerid = client->GetPlayerSlot().Get();
    int msgid = pData->GetNetMessage()->GetNetMessageInfo()->m_MessageId;

    bool stopOriginal = false;
    if (g_fnServerMessageInternalSendHandler)
    {
        auto res = g_fnServerMessageInternalSendHandler(playerid, msgid, pData);
        if (res == 1) return true;
        else if (res == 3) stopOriginal = true;
    }

    if (stopOriginal) return true;
    return reinterpret_cast<decltype(&SendNetMessage)>(g_pSendNetMessageHook->GetOriginal())(client, pData, bufType);
}

bool FilterMessage(void* client, CNetMessage* cMsg, INetChannel* netchan)
{
    if (!client) return reinterpret_cast<decltype(&FilterMessage)>(g_pFilterMessageHook->GetOriginal())(client, cMsg, netchan);
    if (!cMsg) return reinterpret_cast<decltype(&FilterMessage)>(g_pFilterMessageHook->GetOriginal())(client, cMsg, netchan);

    static auto playerIndex = g_pGameDataManager->GetOffsets()->Fetch("CServerSideClientBase::m_nClientSlot") - WIN_LINUX(8, 48);

    auto playerid = *(int*)((uintptr_t)client + playerIndex);
    int msgid = cMsg->GetNetMessage()->GetNetMessageInfo()->m_MessageId;

    bool stopOriginal = false;
    if (g_fnClientMessageSendHandler)
    {
        auto res = g_fnClientMessageSendHandler(playerid, msgid, cMsg);
        if (res == 1) return true;
        else if (res == 3) stopOriginal = true;
    }

    if (stopOriginal) return true;
    return reinterpret_cast<decltype(&FilterMessage)>(g_pFilterMessageHook->GetOriginal())(client, cMsg, netchan);
}

void PostEventAbstractHook(void* _this, CSplitScreenSlot nSlot, bool bLocalOnly, int nClientCount, const uint64* clients, INetworkMessageInternal* pEvent, const CNetMessage* pData, unsigned long nSize, NetChannelBufType_t bufType)
{
    if (bypassPostEventAbstractHook) return reinterpret_cast<decltype(&PostEventAbstractHook)>(g_pPostEventAbstractHook->GetOriginal())(_this, nSlot, bLocalOnly, nClientCount, clients, pEvent, pData, nSize, bufType);

    int msgid = pEvent->GetNetMessageInfo()->m_MessageId;
    CNetMessage* msg = const_cast<CNetMessage*>(pData);
    uint64_t* playermask = (uint64_t*)(clients);

    bool stopOriginal = false;
    if (g_fnServerMessageSendHandler)
    {
        auto res = g_fnServerMessageSendHandler(playermask, msgid, msg);
        if (res == 1) return;
        else if (res == 3) stopOriginal = true;
    }

    if (stopOriginal) return;

    reinterpret_cast<decltype(&PostEventAbstractHook)>(g_pPostEventAbstractHook->GetOriginal())(_this, nSlot, bLocalOnly, nClientCount, clients, pEvent, pData, nSize, bufType);
}

void CNetMessages::SetServerMessageSendHandler(std::function<int(uint64_t*, int, void*)> handler)
{
    g_fnServerMessageSendHandler = handler;
}

void CNetMessages::SetClientMessageSendHandler(std::function<int(int, int, void*)> handler)
{
    g_fnClientMessageSendHandler = handler;
}

void CNetMessages::SetServerMessageInternalSendHandler(std::function<int(int, int, void*)> handler)
{
    g_fnServerMessageInternalSendHandler = handler;
}