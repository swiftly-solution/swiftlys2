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
#include <api/utils/bitvec.h>

#include <core/entrypoint.h>

#include "cs_usercmd.pb.h"
#include "usercmd.pb.h"

#include <s2binlib/s2binlib.h>

#include <api/shared/string.h>
#include <api/sdk/recipientfilter.h>
#include <public/engine/igameeventsystem.h>
#include <public/networksystem/inetworkmessages.h>
#include <public/networksystem/netmessage.h>
#include "usermessages.pb.h"

class CUserCmd
{
public:
    [[maybe_unused]] char pad0[0x10];
    CSGOUserCmdPB cmd;
    [[maybe_unused]] char pad1[0x38];
#ifdef _WIN32
    [[maybe_unused]] char pad2[0x8];
#endif
};

IVFunctionHook* g_pOnGameFramePlayerHook = nullptr;

IVFunctionHook* g_pClientConnectHook = nullptr;
IVFunctionHook* g_pOnClientConnectedHook = nullptr;
IVFunctionHook* g_pClientDisconnectHook = nullptr;
IVFunctionHook* g_pClientPutInServerHook = nullptr;

IVFunctionHook* g_pCheckTransmitHook = nullptr;

void OnGameFramePlayerHook(void* _this, bool simulate, bool first, bool last);

void OnClientPutInServerHook(void* _this, CPlayerSlot slot, char const* pszName, int type, uint64 xuid);
bool ClientConnectHook(void* _this, CPlayerSlot slot, const char* pszName, uint64 xuid, const char* pszNetworkID, bool unk1, CBufferString* pRejectReason);
void OnClientConnectedHook(void* _this, CPlayerSlot slot, const char* pszName, uint64 xuid, const char* pszNetworkID, const char* pszAddress, bool bFakePlayer);
void ClientDisconnectHook(void* _this, CPlayerSlot slot, int reason, const char* pszName, uint64 xuid, const char* pszNetworkID);
void CheckTransmitHook(void* _this, CCheckTransmitInfo** ppInfoList, int infoCount, CBitVec<16384>& unionTransmitEdicts, CBitVec<16384>& unk, const Entity2Networkable_t** pNetworkables, const uint16_t* pEntityIndicies, int nEntities);

void CPlayerManager::Initialize()
{
    for (auto& player : g_Players)
        player.reset();

    void* gameclientsvtable = nullptr;
    s2binlib_find_vtable("server", "CSource2GameClients", &gameclientsvtable);

    void* gameentitiesvtable = nullptr;
    s2binlib_find_vtable("server", "CSource2GameEntities", &gameentitiesvtable);

    g_pClientConnectHook = g_pHooksManager->CreateVFunctionHook();
    g_pClientConnectHook->SetHookFunction(gameclientsvtable, g_pGameDataManager->GetOffsets()->Fetch("IServerGameClients::ClientConnect"), reinterpret_cast<void*>(ClientConnectHook), true);
    g_pClientConnectHook->Enable();

    g_pOnClientConnectedHook = g_pHooksManager->CreateVFunctionHook();
    g_pOnClientConnectedHook->SetHookFunction(gameclientsvtable, g_pGameDataManager->GetOffsets()->Fetch("IServerGameClients::OnClientConnected"), reinterpret_cast<void*>(OnClientConnectedHook), true);
    g_pOnClientConnectedHook->Enable();

    g_pClientDisconnectHook = g_pHooksManager->CreateVFunctionHook();
    g_pClientDisconnectHook->SetHookFunction(gameclientsvtable, g_pGameDataManager->GetOffsets()->Fetch("IServerGameClients::ClientDisconnect"), reinterpret_cast<void*>(ClientDisconnectHook), true);
    g_pClientDisconnectHook->Enable();

    g_pClientPutInServerHook = g_pHooksManager->CreateVFunctionHook();
    g_pClientPutInServerHook->SetHookFunction(gameclientsvtable, g_pGameDataManager->GetOffsets()->Fetch("IServerGameClients::ClientPutInServer"), reinterpret_cast<void*>(OnClientPutInServerHook), true);
    g_pClientPutInServerHook->Enable();

    g_pCheckTransmitHook = g_pHooksManager->CreateVFunctionHook();
    g_pCheckTransmitHook->SetHookFunction(gameentitiesvtable, g_pGameDataManager->GetOffsets()->Fetch("ISource2GameEntities::CheckTransmit"), reinterpret_cast<void*>(CheckTransmitHook), true);
    g_pCheckTransmitHook->Enable();

    void* serverGameDLLVTable;
    s2binlib_find_vtable("server", "CSource2Server", &serverGameDLLVTable);

    g_pOnGameFramePlayerHook = g_pHooksManager->CreateVFunctionHook();
    g_pOnGameFramePlayerHook->SetHookFunction(serverGameDLLVTable, g_pGameDataManager->GetOffsets()->Fetch("IServerGameDLL::GameFrame"), reinterpret_cast<void*>(OnGameFramePlayerHook), true);
    g_pOnGameFramePlayerHook->Enable();
}

void CPlayerManager::Shutdown()
{
    for (auto& slot : g_Players)
        slot.reset();

    if (g_pOnGameFramePlayerHook)
    {
        g_pOnGameFramePlayerHook->Disable();
        g_pHooksManager->DestroyVFunctionHook(g_pOnGameFramePlayerHook);
        g_pOnGameFramePlayerHook = nullptr;
    }

    if (g_pClientConnectHook)
    {
        g_pClientConnectHook->Disable();
        g_pHooksManager->DestroyVFunctionHook(g_pClientConnectHook);
        g_pClientConnectHook = nullptr;
    }

    if (g_pOnClientConnectedHook)
    {
        g_pOnClientConnectedHook->Disable();
        g_pHooksManager->DestroyVFunctionHook(g_pOnClientConnectedHook);
        g_pOnClientConnectedHook = nullptr;
    }

    if (g_pClientDisconnectHook)
    {
        g_pClientDisconnectHook->Disable();
        g_pHooksManager->DestroyVFunctionHook(g_pClientDisconnectHook);
        g_pClientDisconnectHook = nullptr;
    }

    if (g_pClientPutInServerHook)
    {
        g_pClientPutInServerHook->Disable();
        g_pHooksManager->DestroyVFunctionHook(g_pClientPutInServerHook);
        g_pClientPutInServerHook = nullptr;
    }

    if (g_pCheckTransmitHook)
    {
        g_pCheckTransmitHook->Disable();
        g_pHooksManager->DestroyVFunctionHook(g_pCheckTransmitHook);
        g_pCheckTransmitHook = nullptr;
    }
}

extern void* g_pOnClientPutInServerCallback;

void OnClientPutInServerHook(void* _this, CPlayerSlot slot, char const* pszName, int type, uint64 xuid)
{
    reinterpret_cast<decltype(&OnClientPutInServerHook)>(g_pClientPutInServerHook->GetOriginal())(_this, slot, pszName, type, xuid);

    if (type == 0)
    {
        g_pConvarManager->QueryClientConvar(slot.Get(), "cl_language");
    }

    if (g_pGameEngine->IsClientFullyAuthenticated(slot))
    {
        auto player = g_pPlayerManager->GetPlayer(slot.Get());
        if (player)
            player->ChangeAuthorizationState(true);
    }

    if (g_pOnClientPutInServerCallback)
        reinterpret_cast<void (*)(int, int)>(g_pOnClientPutInServerCallback)(slot.Get(), type);
}

void CheckTransmitHook(void* _this, CCheckTransmitInfo** ppInfoList, int infoCount, CBitVec<16384>& unionTransmitEdicts, CBitVec<16384>& unk, const Entity2Networkable_t** pNetworkables, const uint16_t* pEntityIndicies, int nEntities)
{
    reinterpret_cast<decltype(&CheckTransmitHook)>(g_pCheckTransmitHook->GetOriginal())(_this, ppInfoList, infoCount, unionTransmitEdicts, unk, pNetworkables, pEntityIndicies, nEntities);

    for (int i = 0; i < infoCount; i++)
    {
        auto& pInfo = ppInfoList[i];
        int playerid = pInfo->m_nPlayerSlot.Get();

        auto player = g_pPlayerManager->GetPlayer(playerid);
        if (!player)
        {
            continue;
        }

        auto& blockedBits = player->GetBlockedTransmittingBits();

        QueueLockGuard lock(blockedBits.mutex);

        auto transmitEntity = reinterpret_cast<CBitVector<MAX_EDICTS>*>(pInfo->m_pTransmitEntity);
        transmitEntity->Filter(blockedBits.blockedTransmitBits);
    }
}

extern void* g_pOnGameTickCallback;

void OnGameFramePlayerHook(void* _this, bool simulate, bool first, bool last)
{
    reinterpret_cast<decltype(&OnGameFramePlayerHook)>(g_pOnGameFramePlayerHook->GetOriginal())(_this, simulate, first, last);

    if (g_pOnGameTickCallback)
        reinterpret_cast<void (*)(bool, bool, bool)>(g_pOnGameTickCallback)(simulate, first, last);

    for (int i = 0; i < 64; i++)
    {
        auto player = g_pPlayerManager->GetPlayer(i);
        if (player) player->Think();
    }

    g_pCrashReporter->OnTick();
}

extern void* g_pOnClientConnectCallback;

bool ClientConnectHook(void* _this, CPlayerSlot slot, const char* pszName, uint64 xuid, const char* pszNetworkID, bool unk1, CBufferString* pRejectReason)
{
    auto playerid = slot.Get();
    auto player = g_pPlayerManager->RegisterPlayer(playerid);
    if (!player)
    {
        return false;
    }

    player->SetUnauthorizedSteamID(xuid);
    player->SetFakeClient(xuid == 0);

    if (g_pOnClientConnectCallback)
    {
        if (reinterpret_cast<bool (*)(int)>(g_pOnClientConnectCallback)(playerid) == false)
        {
            g_pPlayerManager->UnregisterPlayer(playerid);
            return false;
        }
    }

    return reinterpret_cast<decltype(&ClientConnectHook)>(g_pClientConnectHook->GetOriginal())(_this, slot, pszName, xuid, pszNetworkID, unk1, pRejectReason);
}

void OnClientConnectedHook(void* _this, CPlayerSlot slot, const char* pszName, uint64 xuid, const char* pszNetworkID, const char* pszAddress, bool bFakePlayer)
{
    auto playerid = slot.Get();

    if (bFakePlayer)
    {
        auto player = g_pPlayerManager->RegisterPlayer(playerid);
        player->SetFakeClient(true);

        if (g_pOnClientConnectCallback)
        {
            if (reinterpret_cast<bool (*)(int)>(g_pOnClientConnectCallback)(playerid) == false)
            {
                player->Kick("Connection rejected by plugin.", 0);
                return;
            }
        }
    }
    else
    {
        if (g_pGameEngine->IsClientFullyAuthenticated(slot))
        {
            auto player = g_pPlayerManager->GetPlayer(playerid);
            if (player)
                player->ChangeAuthorizationState(true);
        }
    }

    reinterpret_cast<decltype(&OnClientConnectedHook)>(g_pOnClientConnectedHook->GetOriginal())(_this, slot, pszName, xuid, pszNetworkID, pszAddress, bFakePlayer);
}

extern void* g_pOnClientDisconnectCallback;

void ClientDisconnectHook(void* _this, CPlayerSlot slot, int reason, const char* pszName, uint64 xuid, const char* pszNetworkID)
{
    reinterpret_cast<decltype(&ClientDisconnectHook)>(g_pClientDisconnectHook->GetOriginal())(_this, slot, reason, pszName, xuid, pszNetworkID);

    auto playerid = slot.Get();

    if (g_pOnClientDisconnectCallback)
        reinterpret_cast<void (*)(int, int)>(g_pOnClientDisconnectCallback)(playerid, reason);

    g_pPlayerManager->UnregisterPlayer(playerid);
}

IPlayer* CPlayerManager::RegisterPlayer(int playerid)
{
    if (playerid < 0 || playerid >= g_SwiftlyCore.GetMaxGameClients())
        return nullptr;

    if (g_Players[playerid].has_value())
        UnregisterPlayer(playerid);

    g_Players[playerid].emplace();
    g_Players[playerid]->Initialize(playerid);

    return &g_Players[playerid].value();
}

void CPlayerManager::UnregisterPlayer(int playerid)
{
    if (playerid < 0 || playerid >= g_SwiftlyCore.GetMaxGameClients())
        return;

    if (!g_Players[playerid].has_value())
        return;

    g_Players[playerid]->Shutdown();
    g_Players[playerid].reset();
}

IPlayer* CPlayerManager::GetPlayer(int playerid)
{
    if (!g_Players[playerid].has_value())
        return nullptr;

    return &g_Players[playerid].value();
}

int CPlayerManager::GetPlayerCount()
{
    int count = 0;

    for (int i = 0; i < GetPlayerCap(); i++)
        if (g_pGameEngine->GetClientSteamID(i))
            ++count;

    return count;
}

int CPlayerManager::GetPlayerCap()
{
    return g_SwiftlyCore.GetMaxGameClients();
}

extern bool bypassPostEventAbstractHook;

void CPlayerManager::SendMsg(MessageType type, const std::string& message, int duration)
{
    if (type == MessageType::CenterHTML)
    {
        for (int i = 0; i < g_SwiftlyCore.GetMaxGameClients(); i++)
        {
            IPlayer* player = GetPlayer(i);
            if (player) player->SendMsg(type, message, duration);
        }
    }
    else {
        auto msg = RemoveHtmlTags(message);
        if (type == MessageType::Console)
        {
            msg = ClearColors(msg);
            msg += "\n";
        }

        if (type == MessageType::Chat || type == MessageType::ChatEOT)
        {
            if (msg.size() > 0)
            {
                msg += "\x01";

                bool startsWithColor = (msg.at(0) == '[');
                msg = ProcessColor(message, 0);

                if (startsWithColor)
                    msg = " " + msg;
            }

            auto splitMessage = explode(msg, "[newline]");

            auto netmsg = g_pGameNetworkMessages->FindNetworkMessagePartial("TextMsg");

            for (auto& part : splitMessage)
            {
                auto pmsg = netmsg->AllocateMessage()->ToPB<CUserMessageTextMsg>();

                pmsg->set_dest((int)type);
                pmsg->add_param(part);

                bypassPostEventAbstractHook = true;

                CBroadcastRecipientFilter filter;
                g_pGameEventSystem->PostEventAbstract(-1, false, &filter, netmsg, pmsg, 0);

                bypassPostEventAbstractHook = false;

                // see in src/engine/convars/convars.cpp at the end of the file why i "love" this now
                delete pmsg;
            }
        }
        else {
            auto netmsg = g_pGameNetworkMessages->FindNetworkMessagePartial("TextMsg");
            auto pmsg = netmsg->AllocateMessage()->ToPB<CUserMessageTextMsg>();

            pmsg->set_dest((int)type);
            pmsg->add_param(msg);

            bypassPostEventAbstractHook = true;

            CBroadcastRecipientFilter filter;
            g_pGameEventSystem->PostEventAbstract(-1, false, &filter, netmsg, pmsg, 0);

            bypassPostEventAbstractHook = false;

            // see in src/engine/convars/convars.cpp at the end of the file why i "love" this now
            delete pmsg;
        }
    }
}

void CPlayerManager::SteamAPIServerActivated()
{
    m_CallbackValidateAuthTicketResponse.Register(this, &CPlayerManager::OnValidateAuthTicket);
}

void CPlayerManager::OnValidateAuthTicket(ValidateAuthTicketResponse_t* response)
{
    uint64_t steamid = response->m_SteamID.ConvertToUint64();

    for (int i = 0; i < GetPlayerCap(); i++)
    {
        auto player = GetPlayer(i);
        if (!player)
            continue;
        if (player->GetUnauthorizedSteamID() != steamid)
            continue;

        player->ChangeAuthorizationState(response->m_eAuthSessionResponse == k_EAuthSessionResponseOK);
        break;
    }
}
