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

#include "player.h"

#include <mutex>

#include <api/sdk/recipientfilter.h>
#include <api/shared/string.h>

#include <public/engine/igameeventsystem.h>
#include <public/networksystem/inetworkmessages.h>

#include <game/shared/ehandle.h>
#include <public/const.h>

#include "usermessages.pb.h"
#include "gameevents.pb.h"
#include <api/sdk/serversideclient.h>
#include <public/iserver.h>

#define CBaseEntity_m_iTeamNum 0x9DC483B8A5BFEFB3

#define CBasePlayerController_m_hPawn 0x3979FF6E7C628C1D
#define CCSPlayerController_m_hPlayerPawn 0x28ECD7A1D6C93E7C

#define CBasePlayerPawn_m_pMovementServices 0xCA2EED04CF73E28A
#define CPlayer_MovementServices_m_nButtons 0xD5BDF28998CCEF82
#define CInButtonState_m_pButtonStates 0x6C8AF06A00121DF9

static int32_t CachedSchemaOffset(uint64_t hash, int32_t& cache)
{
    if (cache == -1) cache = g_pSDKSchema->GetOffset(hash);
    return cache;
}

uint64_t sessionId = 1000;

CServerSideClient* GetServerSideClient(int playerid)
{
    static auto offset = g_pGameDataManager->GetOffsets()->Fetch("CNetworkGameServer::m_Clients");

    auto gameserver = g_pGameNetworkServerService->GetIGameServer();

    auto clients = ((CUtlVector<CServerSideClient*>*)((uintptr_t)gameserver + offset));
    return clients->Element(playerid);
}

void CPlayer::Initialize(int playerid)
{
    m_iPlayerId = playerid;
    m_bAuthorized = false;

    m_uConnectedTimeStart = std::chrono::high_resolution_clock::now();
    m_uSessionId = sessionId++;
    centerMessageEvent = g_pGameEventManager->GetGameEventManager()->CreateEvent("show_survival_respawn_status");
}

void CPlayer::Shutdown()
{
    m_iPlayerId = -1;
    m_bAuthorized = false;

    g_pGameEventManager->GetGameEventManager()->FreeEvent(centerMessageEvent);
    centerMessageEvent = nullptr;
}

extern bool bypassPostEventAbstractHook;

void CPlayer::SendMsg(MessageType type, const std::string& message, int duration = 5000)
{
    if (type == MessageType::CenterHTML)
    {
        if (message == "")
            centerMessageEndTime = 0;
        else
        {
            centerMessageEndTime = GetTime() + duration;
            centerMessageText = message;
        }
    }
    else
    {
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

                auto controller = GetController();
                if (!controller)
                    return;

                msg = ProcessColor(message, *(int*)(g_pSDKSchema->GetPropPtr(controller, CBaseEntity_m_iTeamNum)));

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

                CSingleRecipientFilter filter(m_iPlayerId);
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

            CSingleRecipientFilter filter(m_iPlayerId);
            g_pGameEventSystem->PostEventAbstract(-1, false, &filter, netmsg, pmsg, 0);

            bypassPostEventAbstractHook = false;

            // see in src/engine/convars/convars.cpp at the end of the file why i "love" this now
            delete pmsg;
        }
    }
}

bool CPlayer::IsAuthorized()
{
    return m_bAuthorized;
}

void CPlayer::SetFakeClient(bool state)
{
    m_bIsFakeClient = state;
}

bool CPlayer::IsFakeClient()
{
    return m_bIsFakeClient;
}

uint32_t CPlayer::GetConnectedTime()
{
    return std::chrono::duration_cast<std::chrono::seconds>(std::chrono::high_resolution_clock::now() - m_uConnectedTimeStart).count();
}

int CPlayer::GetSlot()
{
    return m_iPlayerId;
}

void CPlayer::SetUnauthorizedSteamID(uint64_t steamID)
{
    m_uUnauthorizedSteamID = steamID;
}

uint64_t CPlayer::GetUnauthorizedSteamID()
{
    if (IsFakeClient())
        return 0;

    auto steamid = g_pGameEngine->GetClientSteamID(m_iPlayerId);
    if (!steamid)
        return m_uUnauthorizedSteamID;

    return steamid->ConvertToUint64();
}

uint64_t CPlayer::GetSteamID()
{
    if (IsFakeClient())
        return 0;

    auto s = std::get_if<std::string>(&g_pConfiguration->GetValue("core.SteamAuth.Mode"));
    if (m_bAuthorized)
    {
        auto steamid = g_pGameEngine->GetClientSteamID(m_iPlayerId);
        if (!steamid)
            return 0;

        return steamid->ConvertToUint64();
    }
    else if (*s == "flexible")
    {
        return GetUnauthorizedSteamID();
    }
    else
        return 0;
}

extern void* g_pOnClientSteamAuthorizeCallback;
extern void* g_pOnClientSteamAuthorizeFailCallback;

void CPlayer::ChangeAuthorizationState(bool bAuthorized)
{
    if (m_bAuthorized == bAuthorized)
        return;

    m_bAuthorized = bAuthorized;

    if (bAuthorized)
    {
        if (g_pOnClientSteamAuthorizeCallback)
            reinterpret_cast<void (*)(int)>(g_pOnClientSteamAuthorizeCallback)(m_iPlayerId);
    }
    else
    {
        if (g_pOnClientSteamAuthorizeFailCallback)
            reinterpret_cast<void (*)(int)>(g_pOnClientSteamAuthorizeFailCallback)(m_iPlayerId);
    }
}

std::string& CPlayer::GetLanguage()
{
    if (m_bLanguageRetrieved) {
        return m_sLanguage;
    }
    else {
        return std::get<std::string>(g_pConfiguration->GetValue("core.Language"));
    }
}

void CPlayer::SetLanguage(const std::string& language)
{
    m_sLanguage = language;
    m_bLanguageRetrieved = true;
}

void* CPlayer::GetController()
{
    auto entitySystem = g_pEntSystem->GetEntitySystem();
    if (!entitySystem)
        return nullptr;

    CEntityInstance* controller = entitySystem->GetEntityInstance(CEntityIndex(m_iPlayerId + 1));
    return controller;
}

void* CPlayer::GetPawn()
{
    auto controller = GetController();
    if (!controller)
        return nullptr;

    static int32_t s_offset = -1;
    auto offset = CachedSchemaOffset(CBasePlayerController_m_hPawn, s_offset);
    if (offset == -1)
        return nullptr;

    CHandle<CEntityInstance>& pawnHandle = *(CHandle<CEntityInstance>*)((uintptr_t)controller + offset);
    return pawnHandle.Get();
}

void* CPlayer::GetPlayerPawn()
{
    auto controller = GetController();
    if (!controller)
        return nullptr;

    static int32_t s_offset = -1;
    auto offset = CachedSchemaOffset(CCSPlayerController_m_hPlayerPawn, s_offset);
    if (offset == -1)
        return nullptr;

    CHandle<CEntityInstance>& playerPawnHandle = *(CHandle<CEntityInstance>*)((uintptr_t)controller + offset);
    return playerPawnHandle.Get();
}

ListenOverride& CPlayer::GetListenOverride(int targetid)
{
    return m_uListenMap[targetid];
}

VoiceFlagValue& CPlayer::GetVoiceFlags()
{
    return m_uVoiceFlags;
}

CPlayerBitVec& CPlayer::GetSelfMutes()
{
    return m_bvSelfMutes;
}

std::string CPlayer::GetIPAddress()
{
    if (m_iPlayerId < 0 || m_iPlayerId > 63)
        return "";

    auto pNetChan = g_pGameEngine->GetPlayerNetInfo(m_iPlayerId);
    if (!pNetChan)
        return "";

    return pNetChan->GetRemoteAddress().ToString(true);
}

void CPlayer::Kick(const std::string& sReason, int uReason)
{
    g_pGameEngine->KickClient(m_iPlayerId, sReason.c_str(), uReason);
}

BlockedTransmitInfo& CPlayer::GetBlockedTransmittingBits()
{
    return m_bvBlockedTransmittingEntities;
}

bool CPlayer::IsFirstSpawn()
{
    return m_bFirstSpawn;
}

void CPlayer::SetFirstSpawn(bool state)
{
    m_bFirstSpawn = state;
}

extern void* g_pOnClientKeyStateChangedCallback;

void CPlayer::Think()
{
    std::string menuText;
    {
        std::lock_guard<std::mutex> lock(centerMenuMutex);
        menuText = centerMenuText;
    }
    const bool showMenu = !menuText.empty();
    const bool showMessage = !showMenu && centerMessageEndTime >= GetTime();

    if (!showMenu && !showMessage)
    {
        centerMessageEndTime = 0;
    }
    else
    {
        if(!centerMessageEvent)
            centerMessageEvent = g_pGameEventManager->GetGameEventManager()->CreateEvent("show_survival_respawn_status");
            
        centerMessageEvent->SetString("loc_token", (showMenu ? menuText : centerMessageText).c_str());
        centerMessageEvent->SetInt("duration", 1);
        centerMessageEvent->SetInt("userid", m_iPlayerId);
        SendGameEvent(centerMessageEvent);
    }

    auto pawn = GetPawn();
    if (!pawn)
        return;

    static int32_t s_movementServicesOffset = -1;
    static int32_t s_buttonsOffset = -1;
    static int32_t s_buttonStatesOffset = -1;

    auto movementServicesOffset = CachedSchemaOffset(CBasePlayerPawn_m_pMovementServices, s_movementServicesOffset);
    auto buttonsOffset = CachedSchemaOffset(CPlayer_MovementServices_m_nButtons, s_buttonsOffset);
    auto buttonStatesOffset = CachedSchemaOffset(CInButtonState_m_pButtonStates, s_buttonStatesOffset);
    if (movementServicesOffset == -1 || buttonsOffset == -1 || buttonStatesOffset == -1)
        return;

    auto movementServices = *(uintptr_t*)((uintptr_t)pawn + movementServicesOffset);
    if (!movementServices)
        return;

    auto states = (uint64_t*)(movementServices + buttonsOffset + buttonStatesOffset);
    const uint64_t newButtons = states[0];
    if (newButtons == m_uPressedButtons)
        return;

    if (g_pOnClientKeyStateChangedCallback)
    {
        auto callback = reinterpret_cast<void (*)(int, uint32_t, bool)>(g_pOnClientKeyStateChangedCallback);

        for (int i = 0; i < 64; i++)
        {
            uint64_t mask = (1ULL << i);
            uint64_t oldState = m_uPressedButtons & mask;
            uint64_t newState = newButtons & mask;

            if (oldState == 0 && newState != 0)
            {
                callback(m_iPlayerId, i, true);
            }
            else if (oldState != 0 && newState == 0)
            {
                callback(m_iPlayerId, i, false);
            }
        }
    }

    m_uPressedButtons = newButtons;
}

void CPlayer::RenderMenuCenterText(const std::string& text)
{
    std::lock_guard<std::mutex> lock(centerMenuMutex);
    centerMenuText = text;
}

void CPlayer::ClearRenderMenuCenterText()
{
    std::lock_guard<std::mutex> lock(centerMenuMutex);
    centerMenuText = "";
}

bool CPlayer::HasMenuShown()
{
    std::lock_guard<std::mutex> lock(centerMenuMutex);
    return !centerMenuText.empty();
}

uint64_t CPlayer::GetSessionID()
{
    return m_uSessionId;
}

bool CPlayer::IsListeningGameEvent(int eventID)
{
    std::lock_guard<std::mutex> lock(m_listeningGameEventsMutex);
    return m_listeningGameEvents.IsBitSet(eventID);
}

void CPlayer::SetListeningGameEvents(CBitVector<8192>& eventIDs)
{
    std::lock_guard<std::mutex> lock(m_listeningGameEventsMutex);
    m_listeningGameEvents.CopyFrom(eventIDs);
}

void CPlayer::SendGameEvent(IGameEvent* event)
{
    if(!event) return;

    auto msg = g_pGameNetworkMessages->FindNetworkMessageById(GE_Source1LegacyGameEvent);
    if(!msg) return;

    auto data = msg->AllocateMessage()->ToPB<CMsgSource1LegacyGameEvent>();
    CSingleRecipientFilter filter(m_iPlayerId);

    bypassPostEventAbstractHook = true;
    g_pGameEventManager->GetGameEventManager()->SerializeEvent(event, data);
    g_pGameEventSystem->PostEventAbstract(-1, false, &filter, msg, data, 0);
    delete data;
    bypassPostEventAbstractHook = false;
}
