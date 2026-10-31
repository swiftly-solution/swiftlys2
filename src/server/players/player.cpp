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

#include <public/tier0/recipientfilter.h>
#include <api/shared/profiler.h>
#include <api/shared/string.h>

#include <public/engine2/igameeventsystem.h>
#include <public/networksystem/inetworkmessages.h>

#include <public/game/ehandle.h>

#include "usermessages.pb.h"
#include "gameevents.pb.h"
#include <public/engine2/serversideclient.h>
#include <public/engine2/networkgameserverbase.h>
#include <scripting/engine/events.h>

#include <vector>

constexpr uint64_t CBaseEntity_m_iTeamNum = SchemaFieldHash("CBaseEntity", "m_iTeamNum");

constexpr uint64_t CBasePlayerController_m_iszPlayerName = SchemaFieldHash("CBasePlayerController", "m_iszPlayerName");
constexpr uint64_t CCSPlayerController_m_pActionTrackingServices = SchemaFieldHash("CCSPlayerController", "m_pActionTrackingServices");
constexpr uint64_t CCSPlayer_ActionTrackingServices_m_matchStats = SchemaFieldHash("CCSPlayerController_ActionTrackingServices", "m_matchStats");
constexpr uint64_t CSPerRoundStats_t_m_iKills = SchemaFieldHash("CSPerRoundStats_t", "m_iKills");

constexpr uint64_t CBasePlayerController_m_hPawn = SchemaFieldHash("CBasePlayerController", "m_hPawn");
constexpr uint64_t CCSPlayerController_m_hPlayerPawn = SchemaFieldHash("CCSPlayerController", "m_hPlayerPawn");

constexpr uint64_t CBasePlayerPawn_m_pMovementServices = SchemaFieldHash("CBasePlayerPawn", "m_pMovementServices");
constexpr uint64_t CPlayer_MovementServices_m_nButtons = SchemaFieldHash("CPlayer_MovementServices", "m_nButtons");
constexpr uint64_t CInButtonState_m_pButtonStates = SchemaFieldHash("CInButtonState", "m_pButtonStates");

extern bool bypassPostEventAbstractHook;

static uint64_t s_nextSessionId = 1000;

static void* GetHandleEntity(void* owner, uint64_t hash)
{
    PROFILE_FUNCTION();

    auto handle = static_cast<CHandle<CEntityInstance>*>(g_pSDKSchema->GetPropPtr(owner, hash));
    return handle ? handle->Get() : nullptr;
}

CServerSideClientBase* GetServerSideClient(int playerid)
{
    PROFILE_FUNCTION();

    auto gameserver = g_pGameNetworkServerService->GetIGameServer();
    return gameserver->m_Clients.Element(playerid);
}

void PostTextMsg(IRecipientFilter* filter, MessageType type, const std::string& message, std::optional<int> team)
{
    PROFILE_FUNCTION();

    auto msg = RemoveHtmlTags(message);
    if (type == MessageType::Console)
    {
        msg = ClearColors(std::move(msg));
        msg += '\n';
    }

    std::vector<std::string> parts;
    if (type == MessageType::Chat || type == MessageType::ChatEOT)
    {
        if (!msg.empty())
        {
            if (!team)
                return;

            const bool startsWithColor = msg.front() == '[';
            msg = ProcessColor(message, *team);
            if (startsWithColor)
                msg.insert(0, " ");
        }

        parts = explode(msg, "[newline]");
    }
    else
    {
        parts.push_back(std::move(msg));
    }

    auto netmsg = g_pGameNetworkMessages->FindNetworkMessagePartial("TextMsg");
    if (!netmsg)
        return;

    for (const auto& part : parts)
    {
        CNetMessagePB<CUserMessageTextMsg>* pmsg = netmsg->AllocateMessage()->ToPB<CUserMessageTextMsg>();
        pmsg->set_dest(static_cast<int>(type));
        pmsg->add_param(part);

        bypassPostEventAbstractHook = true;
        g_pGameEventSystem->PostEventAbstract(-1, false, filter, netmsg, pmsg, 0);
        bypassPostEventAbstractHook = false;
        delete pmsg;
    }
}

void CPlayer::EnsureCenterMessageEvent()
{
    PROFILE_FUNCTION();

    if (!m_pCenterMessageEvent)
        m_pCenterMessageEvent = g_pGameEventManager->GetGameEventManager()->CreateEvent("show_survival_respawn_status", true);
}

void CPlayer::Initialize(int playerid)
{
    PROFILE_FUNCTION();

    m_iPlayerId = playerid;

    m_uConnectedTimeStart = std::chrono::steady_clock::now();
    m_uSessionId = s_nextSessionId++;
    EnsureCenterMessageEvent();
}

void CPlayer::Shutdown()
{
    PROFILE_FUNCTION();

    m_iPlayerId = -1;
    m_bAuthorized = false;

    g_pGameEventManager->GetGameEventManager()->FreeEvent(m_pCenterMessageEvent);
    m_pCenterMessageEvent = nullptr;
}

void CPlayer::SendMsg(MessageType type, const std::string& message, int duration)
{
    PROFILE_FUNCTION();

    if (type == MessageType::CenterHTML)
    {
        if (message.empty())
        {
            m_centerMessageEndTime = {};
        }
        else
        {
            m_centerMessageEndTime = std::chrono::steady_clock::now() + std::chrono::milliseconds(duration);
            m_sCenterMessageText = message;
        }
        return;
    }

    CSingleUserRecipientFilter filter(m_iPlayerId);
    PostTextMsg(&filter, type, message, GetTeam());
}

bool CPlayer::IsAuthorized()
{
    PROFILE_FUNCTION();

    return m_bAuthorized;
}

void CPlayer::SetFakeClient(bool state)
{
    PROFILE_FUNCTION();

    m_bIsFakeClient = state;
}

bool CPlayer::IsFakeClient()
{
    PROFILE_FUNCTION();

    return m_bIsFakeClient;
}

uint32_t CPlayer::GetConnectedTime()
{
    PROFILE_FUNCTION();

    return static_cast<uint32_t>(std::chrono::duration_cast<std::chrono::seconds>(std::chrono::steady_clock::now() - m_uConnectedTimeStart).count());
}

void CPlayer::SetUnauthorizedSteamID(uint64_t steamID)
{
    PROFILE_FUNCTION();

    m_uUnauthorizedSteamID = steamID;
}

uint64_t CPlayer::GetUnauthorizedSteamID()
{
    PROFILE_FUNCTION();

    if (IsFakeClient())
        return 0;

    auto steamid = g_pGameEngine->GetClientSteamID(m_iPlayerId);
    return steamid ? steamid->ConvertToUint64() : m_uUnauthorizedSteamID;
}

uint64_t CPlayer::GetSteamID()
{
    PROFILE_FUNCTION();

    if (IsFakeClient())
        return 0;

    if (m_bAuthorized)
    {
        auto steamid = g_pGameEngine->GetClientSteamID(m_iPlayerId);
        return steamid ? steamid->ConvertToUint64() : 0;
    }

    const auto* mode = std::get_if<std::string>(&g_pConfiguration->GetValue("core.SteamAuth.Mode"));
    return mode && *mode == "flexible" ? GetUnauthorizedSteamID() : 0;
}

void CPlayer::ChangeAuthorizationState(bool bAuthorized)
{
    PROFILE_FUNCTION();

    if (m_bAuthorized == bAuthorized)
        return;

    m_bAuthorized = bAuthorized;

    if (bAuthorized)
    {
        if (g_pOnClientSteamAuthorizeCallback)
            g_pOnClientSteamAuthorizeCallback(m_iPlayerId);
    }
    else
    {
        if (g_pOnClientSteamAuthorizeFailCallback)
            g_pOnClientSteamAuthorizeFailCallback(m_iPlayerId);
    }
}

void* CPlayer::GetController()
{
    PROFILE_FUNCTION();

    auto entitySystem = g_pEntSystem->GetEntitySystem();
    if (!entitySystem)
        return nullptr;

    return entitySystem->GetEntityInstance(CEntityIndex(m_iPlayerId + 1));
}

std::optional<int> CPlayer::GetTeam()
{
    PROFILE_FUNCTION();

    void* controller = GetController();
    if (!controller)
        return std::nullopt;

    auto team = static_cast<int*>(g_pSDKSchema->GetPropPtr(controller, CBaseEntity_m_iTeamNum));
    if (!team)
        return std::nullopt;

    return *team;
}

int32_t CPlayer::GetKills()
{
    PROFILE_FUNCTION();

    void* controller = GetController();
    if (!controller)
        return 0;

    auto servicesPtr = static_cast<void**>(g_pSDKSchema->GetPropPtr(controller, CCSPlayerController_m_pActionTrackingServices));
    if (!servicesPtr || !*servicesPtr)
        return 0;

    auto matchStats = g_pSDKSchema->GetPropPtr(*servicesPtr, CCSPlayer_ActionTrackingServices_m_matchStats);
    if (!matchStats)
        return 0;

    auto kills = static_cast<int32_t*>(g_pSDKSchema->GetPropPtr(matchStats, CSPerRoundStats_t_m_iKills));
    return kills ? *kills : 0;
}

const char* CPlayer::GetName()
{
    PROFILE_FUNCTION();

    void* controller = GetController();
    if (!controller)
        return "";

    auto name = static_cast<const char*>(g_pSDKSchema->GetPropPtr(controller, CBasePlayerController_m_iszPlayerName));
    return name ? name : "";
}

void* CPlayer::GetPawn()
{
    PROFILE_FUNCTION();

    return GetHandleEntity(GetController(), CBasePlayerController_m_hPawn);
}

void* CPlayer::GetPlayerPawn()
{
    PROFILE_FUNCTION();

    return GetHandleEntity(GetController(), CCSPlayerController_m_hPlayerPawn);
}

std::string CPlayer::GetIPAddress()
{
    PROFILE_FUNCTION();

    if (m_iPlayerId < 0 || m_iPlayerId >= MAX_PLAYERS)
        return "";

    auto pNetChan = g_pGameEngine->GetPlayerNetInfo(m_iPlayerId);
    if (!pNetChan)
        return "";

    return pNetChan->GetRemoteAddress().ToString(true);
}

void CPlayer::Kick(const std::string& sReason, int uReason)
{
    PROFILE_FUNCTION();

    g_pGameEngine->KickClient(m_iPlayerId, sReason.c_str(), (ENetworkDisconnectionReason)uReason);
}

BlockedTransmitInfo& CPlayer::GetBlockedTransmittingBits()
{
    PROFILE_FUNCTION();

    return m_bvBlockedTransmittingEntities;
}

bool CPlayer::IsFirstSpawn()
{
    PROFILE_FUNCTION();

    return m_bFirstSpawn;
}

void CPlayer::SetFirstSpawn(bool state)
{
    PROFILE_FUNCTION();

    m_bFirstSpawn = state;
}

void CPlayer::UpdateCenterMessage()
{
    PROFILE_FUNCTION();

    std::string menuText;
    {
        std::lock_guard lock(m_centerMenuMutex);
        menuText = m_sCenterMenuText;
    }

    const bool showMenu = !menuText.empty();
    const bool showMessage = !showMenu && m_centerMessageEndTime >= std::chrono::steady_clock::now();
    if (!showMenu && !showMessage)
    {
        m_centerMessageEndTime = {};
        return;
    }

    EnsureCenterMessageEvent();

    m_pCenterMessageEvent->SetString("loc_token", (showMenu ? menuText : m_sCenterMessageText).c_str());
    m_pCenterMessageEvent->SetInt("duration", 1);
    m_pCenterMessageEvent->SetInt("userid", m_iPlayerId);
    SendGameEvent(m_pCenterMessageEvent);
}

void CPlayer::UpdateButtons()
{
    PROFILE_FUNCTION();

    auto pawn = GetPawn();
    if (!pawn)
        return;

    auto movementServices = static_cast<void**>(g_pSDKSchema->GetPropPtr(pawn, CBasePlayerPawn_m_pMovementServices));
    if (!movementServices)
        return;

    auto buttons = g_pSDKSchema->GetPropPtr(*movementServices, CPlayer_MovementServices_m_nButtons);
    if(!buttons) 
        return;
        
    auto states = static_cast<uint64_t*>(g_pSDKSchema->GetPropPtr(buttons, CInButtonState_m_pButtonStates));
    if (!states)
        return;

    const uint64_t newButtons = *states;
    if (newButtons == m_uPressedButtons)
        return;

    if (g_pOnClientKeyStateChangedCallback)
    {
        for (int i = 0; i < 64; i++)
        {
            bool wasPressed = (m_uPressedButtons >> i) & 1;
            bool isPressed = (newButtons >> i) & 1;

            if (wasPressed != isPressed)
                g_pOnClientKeyStateChangedCallback(m_iPlayerId, i, isPressed);
        }
    }

    m_uPressedButtons = newButtons;
}

void CPlayer::Think()
{
    PROFILE_FUNCTION();

    UpdateCenterMessage();
    UpdateButtons();
}

void CPlayer::RenderMenuCenterText(const std::string& text)
{
    PROFILE_FUNCTION();

    std::lock_guard lock(m_centerMenuMutex);
    m_sCenterMenuText = text;
}

void CPlayer::ClearRenderMenuCenterText()
{
    PROFILE_FUNCTION();

    std::lock_guard lock(m_centerMenuMutex);
    m_sCenterMenuText.clear();
}

bool CPlayer::HasMenuShown()
{
    PROFILE_FUNCTION();

    std::lock_guard lock(m_centerMenuMutex);
    return !m_sCenterMenuText.empty();
}

uint64_t CPlayer::GetSessionID()
{
    PROFILE_FUNCTION();

    return m_uSessionId;
}

bool CPlayer::IsListeningGameEvent(int eventID)
{
    PROFILE_FUNCTION();

    std::lock_guard lock(m_listeningGameEventsMutex);
    return m_listeningGameEvents.IsBitSet(eventID);
}

void CPlayer::SetListeningGameEvents(CBitVector<8192>& eventIDs)
{
    PROFILE_FUNCTION();

    std::lock_guard lock(m_listeningGameEventsMutex);
    m_listeningGameEvents.CopyFrom(eventIDs);
}

void CPlayer::SendGameEvent(IGameEvent* event)
{
    PROFILE_FUNCTION();

    if(!event) return;

    auto msg = g_pGameNetworkMessages->FindNetworkMessageById(GE_Source1LegacyGameEvent);
    if(!msg) return;

    auto data = msg->AllocateMessage()->ToPB<CMsgSource1LegacyGameEvent>();
    CSingleUserRecipientFilter filter(m_iPlayerId);

    bypassPostEventAbstractHook = true;
    g_pGameEventManager->GetGameEventManager()->SerializeEvent(event, data);
    g_pGameEventSystem->PostEventAbstract(-1, false, &filter, msg, data, 0);
    delete data;
    bypassPostEventAbstractHook = false;
}
