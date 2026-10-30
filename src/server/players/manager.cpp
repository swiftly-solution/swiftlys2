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
#include <memory/hooks/vfunction.h>

#include <api/interfaces/interfaces.h>
#include <api/shared/profiler.h>
#include <api/utils/bitvec.h>

#include <core/entrypoint.h>

#include <public/tier0/recipientfilter.h>
#include <public/entity2/entitynetworkable.h>
#include <public/engine2/isource2server.h>
#include <scripting/engine/events.h>

void OnGameFramePlayerHook(void* _this, bool simulate, bool first, bool last);

void OnClientPutInServerHook(void* _this, CPlayerSlot slot, char const* pszName, int type, uint64 xuid);
bool ClientConnectHook(void* _this, CPlayerSlot slot, const char* pszName, uint64 xuid, const char* pszNetworkID, bool unk1, CBufferString* pRejectReason);
void OnClientConnectedHook(void* _this, CPlayerSlot slot, const char* pszName, uint64 xuid, const char* pszNetworkID, const char* pszAddress, bool bFakePlayer);
void ClientDisconnectHook(void* _this, CPlayerSlot slot, int reason, const char* pszName, uint64 xuid, const char* pszNetworkID);
void CheckTransmitHook(void* _this, CCheckTransmitInfo** ppInfoList, int infoCount, CBitVec<16384>& unionTransmitEdicts, CBitVec<16384>& unk, const Entity2Networkable_t** pNetworkables, const uint16_t* pEntityIndicies, int nEntities);

VFunctionHook g_ClientConnectHook(reinterpret_cast<void*>(ClientConnectHook), "server", "CSource2GameClients", "IServerGameClients::ClientConnect");
VFunctionHook g_OnClientConnectedHook(reinterpret_cast<void*>(OnClientConnectedHook), "server", "CSource2GameClients", "IServerGameClients::OnClientConnected");
VFunctionHook g_ClientDisconnectHook(reinterpret_cast<void*>(ClientDisconnectHook), "server", "CSource2GameClients", "IServerGameClients::ClientDisconnect");
VFunctionHook g_ClientPutInServerHook(reinterpret_cast<void*>(OnClientPutInServerHook), "server", "CSource2GameClients", "IServerGameClients::ClientPutInServer");
VFunctionHook g_CheckTransmitHook(reinterpret_cast<void*>(CheckTransmitHook), "server", "CSource2GameEntities", "ISource2GameEntities::CheckTransmit");
VFunctionHook g_OnGameFramePlayerHook(reinterpret_cast<void*>(OnGameFramePlayerHook), "server", "CSource2Server", "IServerGameDLL::GameFrame");

void CPlayerManager::Initialize()
{
    PROFILE_FUNCTION();

    for (auto& player : m_players)
        player.reset();
}

void CPlayerManager::Shutdown()
{
    PROFILE_FUNCTION();

    for (auto& player : m_players)
        player.reset();
}

void OnClientPutInServerHook(void* _this, CPlayerSlot slot, char const* pszName, int type, uint64 xuid)
{
    reinterpret_cast<decltype(&OnClientPutInServerHook)>(g_ClientPutInServerHook.GetOriginal())(_this, slot, pszName, type, xuid);

    PROFILE_FUNCTION();

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
        g_pOnClientPutInServerCallback(slot.Get(), type);
}

void CheckTransmitHook(void* _this, CCheckTransmitInfo** ppInfoList, int infoCount, CBitVec<16384>& unionTransmitEdicts, CBitVec<16384>& unk, const Entity2Networkable_t** pNetworkables, const uint16_t* pEntityIndicies, int nEntities)
{
    reinterpret_cast<decltype(&CheckTransmitHook)>(g_CheckTransmitHook.GetOriginal())(_this, ppInfoList, infoCount, unionTransmitEdicts, unk, pNetworkables, pEntityIndicies, nEntities);

    PROFILE_FUNCTION();

    for (int i = 0; i < infoCount; i++)
    {
        auto* pInfo = ppInfoList[i];

        auto player = g_pPlayerManager->GetPlayer(pInfo->m_nPlayerSlot.Get());
        if (!player)
            continue;

        auto& blockedBits = player->GetBlockedTransmittingBits();
        if (blockedBits.blockedCount.load(std::memory_order_relaxed) == 0)
            continue;

        std::lock_guard lock(blockedBits.mutex);

        auto transmitEntity = reinterpret_cast<CBitVector<MAX_EDICTS>*>(pInfo->m_pTransmitEntity);
        transmitEntity->Filter(blockedBits.blockedTransmitBits);
    }
}

void OnGameFramePlayerHook(void* _this, bool simulate, bool first, bool last)
{
    reinterpret_cast<decltype(&OnGameFramePlayerHook)>(g_OnGameFramePlayerHook.GetOriginal())(_this, simulate, first, last);

    PROFILE_FUNCTION();

    if (g_pOnGameTickCallback)
        g_pOnGameTickCallback(simulate, first, last);

    for (int i = 0; i < g_pPlayerManager->GetPlayerCap(); i++)
    {
        auto player = g_pPlayerManager->GetPlayer(i);
        if (player) player->Think();
    }

    g_pCrashReporter->OnTick();
}

bool ClientConnectHook(void* _this, CPlayerSlot slot, const char* pszName, uint64 xuid, const char* pszNetworkID, bool unk1, CBufferString* pRejectReason)
{
    PROFILE_FUNCTION();

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
        if (!g_pOnClientConnectCallback(playerid))
        {
            g_pPlayerManager->UnregisterPlayer(playerid);
            return false;
        }
    }

    PROFILE_PAUSE();

    return reinterpret_cast<decltype(&ClientConnectHook)>(g_ClientConnectHook.GetOriginal())(_this, slot, pszName, xuid, pszNetworkID, unk1, pRejectReason);
}

void OnClientConnectedHook(void* _this, CPlayerSlot slot, const char* pszName, uint64 xuid, const char* pszNetworkID, const char* pszAddress, bool bFakePlayer)
{
    PROFILE_FUNCTION();

    auto playerid = slot.Get();

    if (bFakePlayer)
    {
        auto player = g_pPlayerManager->RegisterPlayer(playerid);
        if (player)
        {
            player->SetFakeClient(true);

            if (g_pOnClientConnectCallback && !g_pOnClientConnectCallback(playerid))
            {
                player->Kick("Connection rejected by plugin.", 0);
                return;
            }
        }
    }
    else if (g_pGameEngine->IsClientFullyAuthenticated(slot))
    {
        auto player = g_pPlayerManager->GetPlayer(playerid);
        if (player) player->ChangeAuthorizationState(true);
    }

    reinterpret_cast<decltype(&OnClientConnectedHook)>(g_OnClientConnectedHook.GetOriginal())(_this, slot, pszName, xuid, pszNetworkID, pszAddress, bFakePlayer);
}

void ClientDisconnectHook(void* _this, CPlayerSlot slot, int reason, const char* pszName, uint64 xuid, const char* pszNetworkID)
{
    reinterpret_cast<decltype(&ClientDisconnectHook)>(g_ClientDisconnectHook.GetOriginal())(_this, slot, reason, pszName, xuid, pszNetworkID);

    PROFILE_FUNCTION();

    auto playerid = slot.Get();

    if (g_pOnClientDisconnectCallback)
        g_pOnClientDisconnectCallback(playerid, reason);

    g_pPlayerManager->UnregisterPlayer(playerid);
}

IPlayer* CPlayerManager::RegisterPlayer(int playerid)
{
    PROFILE_FUNCTION();

    if (playerid < 0 || playerid >= g_SwiftlyCore.GetMaxGameClients())
        return nullptr;

    auto& slot = m_players[playerid];
    if (slot)
        UnregisterPlayer(playerid);

    slot.emplace();
    slot->Initialize(playerid);

    return &*slot;
}

void CPlayerManager::UnregisterPlayer(int playerid)
{
    PROFILE_FUNCTION();

    if (playerid < 0 || playerid >= g_SwiftlyCore.GetMaxGameClients())
        return;

    auto& slot = m_players[playerid];
    if (!slot)
        return;

    slot->Shutdown();
    slot.reset();
}

IPlayer* CPlayerManager::GetPlayer(int playerid)
{
    PROFILE_FUNCTION();

    if (playerid < 0 || playerid >= MAX_PLAYERS || !m_players[playerid])
        return nullptr;

    return &*m_players[playerid];
}

int CPlayerManager::GetPlayerCount()
{
    PROFILE_FUNCTION();

    int count = 0;

    for (int i = 0; i < GetPlayerCap(); i++)
        if (g_pGameEngine->GetClientSteamID(i))
            ++count;

    return count;
}

int CPlayerManager::GetPlayerCap()
{
    PROFILE_FUNCTION();

    return g_SwiftlyCore.GetMaxGameClients();
}

void CPlayerManager::SendMsg(MessageType type, const std::string& message, int duration)
{
    PROFILE_FUNCTION();

    if (type == MessageType::CenterHTML)
    {
        for (int i = 0; i < GetPlayerCap(); i++)
        {
            auto player = GetPlayer(i);
            if (player) player->SendMsg(type, message, duration);
        }
        return;
    }

    CBroadcastRecipientFilter filter;
    PostTextMsg(&filter, type, message, 0);
}

void CPlayerManager::SteamAPIServerActivated()
{
    PROFILE_FUNCTION();

    m_CallbackValidateAuthTicketResponse.Register(this, &CPlayerManager::OnValidateAuthTicket);
}

void CPlayerManager::OnValidateAuthTicket(ValidateAuthTicketResponse_t* response)
{
    PROFILE_FUNCTION();

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
