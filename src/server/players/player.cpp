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

#include <api/sdk/recipientfilter.h>
#include <api/shared/string.h>

#include <public/engine/igameeventsystem.h>
#include <public/networksystem/inetworkmessages.h>

#include <game/shared/ehandle.h>
#include <public/const.h>

#include "usermessages.pb.h"

#define CBaseEntity_m_iTeamNum 0x9DC483B8A5BFEFB3
#define CBaseEntity_m_fFlags 0x9DC483B8A4A37590

#define CBasePlayerController_m_hPawn 0x3979FF6E7C628C1D
#define CCSPlayerController_m_hPlayerPawn 0x28ECD7A1D6C93E7C

#define CBasePlayerPawn_m_pMovementServices 0xCA2EED04CF73E28A
#define CPlayer_MovementServices_m_nButtons 0xD5BDF28998CCEF82
#define CInButtonState_m_pButtonStates 0x6C8AF06A00121DF9

static const std::vector<std::string> g_vButtons = {
    "mouse1",
    "space",
    "ctrl",
    "w",
    "s",
    "e",
    "esc",
    "a",
    "d",
    "a",
    "d",
    "mouse2",
    "unknown_key_run",
    "r",
    "alt",
    "alt",
    "shift",
    "unknown_key_speed",
    "shift",
    "unknown_key_hudzoom",
    "unknown_key_weapon1",
    "unknown_key_weapon2",
    "unknown_key_bullrush",
    "unknown_key_grenade1",
    "unknown_key_grenade2",
    "unknown_key_lookspin",
    "unknown_key_26",
    "unknown_key_27",
    "unknown_key_28",
    "unknown_key_29",
    "unknown_key_30",
    "unknown_key_31",
    "unknown_key_32",
    "tab",
    "unknown_key_34",
    "f",
    "unknown_key_36",
    "unknown_key_37",
    "unknown_key_38",
    "unknown_key_39",
    "unknown_key_40",
    "unknown_key_41",
    "unknown_key_42",
    "unknown_key_43",
    "unknown_key_44",
    "unknown_key_45",
    "unknown_key_46",
    "unknown_key_47",
    "unknown_key_48",
    "unknown_key_49",
    "unknown_key_50",
    "unknown_key_51",
    "unknown_key_52",
    "unknown_key_53",
    "unknown_key_54",
    "unknown_key_55",
    "unknown_key_56",
    "unknown_key_57",
    "unknown_key_58",
    "unknown_key_59",
    "unknown_key_60",
    "unknown_key_61",
    "unknown_key_62",
    "unknown_key_63",
};

void CPlayer::Initialize(int playerid)
{
    m_iPlayerId = playerid;
    m_bAuthorized = false;

    m_uConnectedTimeStart = std::chrono::high_resolution_clock::now();
}

void CPlayer::Shutdown()
{
    m_iPlayerId = -1;
    m_bAuthorized = false;

    if (centerMessageEvent)
    {
        static auto eventmanager = g_ifaceService.FetchInterface<IEventManager>(GAMEEVENTMANAGER_INTERFACE_VERSION);
        eventmanager->GetGameEventManager()->FreeEvent(centerMessageEvent);
        centerMessageEvent = nullptr;
    }
}

extern INetworkMessages* networkMessages;
extern bool bypassPostEventAbstractHook;

void CPlayer::SendMsg(MessageType type, const std::string& message, int duration = 5000)
{
    if (IsFakeClient())
        return;

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
        if (msg.size() > 0)
        {
            if (msg.ends_with("\n"))
                msg.pop_back();

            msg += "\x01";

            bool startsWithColor = (msg.at(0) == '[');
            auto schema = g_ifaceService.FetchInterface<ISDKSchema>(SDKSCHEMA_INTERFACE_VERSION);
            if (!schema)
                return;

            msg = ProcessColor(message, *(int*)(schema->GetPropPtr(GetController(), CBaseEntity_m_iTeamNum)));

            if (startsWithColor)
                msg = " " + msg;
        }

        auto gameEventSystem = g_ifaceService.FetchInterface<IGameEventSystem>(GAMEEVENTSYSTEM_INTERFACE_VERSION);
        if (!gameEventSystem)
            return;

        auto netmsg = networkMessages->FindNetworkMessagePartial("TextMsg");
        auto pmsg = netmsg->AllocateMessage()->ToPB<CUserMessageTextMsg>();

        pmsg->set_dest((int)type);
        pmsg->add_param(msg);

        bypassPostEventAbstractHook = true;

        CSingleRecipientFilter filter(m_iPlayerId);
        gameEventSystem->PostEventAbstract(-1, false, &filter, netmsg, pmsg, 0);

        bypassPostEventAbstractHook = false;

        // see in src/engine/convars/convars.cpp at the end of the file why i "love" this now
        delete pmsg;
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

    auto engine = g_ifaceService.FetchInterface<IVEngineServer2>(INTERFACEVERSION_VENGINESERVER);
    if (!engine)
        return m_uUnauthorizedSteamID;

    auto steamid = engine->GetClientSteamID(m_iPlayerId);
    if (!steamid)
        return m_uUnauthorizedSteamID;

    return steamid->ConvertToUint64();
}

uint64_t CPlayer::GetSteamID()
{
    if (IsFakeClient())
        return 0;

    auto config = g_ifaceService.FetchInterface<IConfiguration>(CONFIGURATION_INTERFACE_VERSION);
    if (!config)
        return 0;

    auto s = std::get_if<std::string>(&config->GetValue("core.SteamAuth.Mode"));
    if (m_bAuthorized)
    {
        auto engine = g_ifaceService.FetchInterface<IVEngineServer2>(INTERFACEVERSION_VENGINESERVER);
        if (!engine)
            return 0;

        auto steamid = engine->GetClientSteamID(m_iPlayerId);
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
    return m_sLanguage;
}

void* CPlayer::GetController()
{
    static auto entsystem = g_ifaceService.FetchInterface<IEntitySystem>(ENTITYSYSTEM_INTERFACE_VERSION);

    auto entitySystem = entsystem->GetEntitySystem();
    if (!entitySystem)
        return nullptr;

    CEntityInstance* controller = entitySystem->GetEntityInstance(CEntityIndex(m_iPlayerId + 1));
    return controller;
}

void* CPlayer::GetPawn()
{
    static auto schema = g_ifaceService.FetchInterface<ISDKSchema>(SDKSCHEMA_INTERFACE_VERSION);

    auto controller = GetController();
    if (!controller)
        return nullptr;

    auto pawn = schema->GetPropPtr(controller, CBasePlayerController_m_hPawn);
    if (!pawn)
        return nullptr;

    CHandle<CEntityInstance>& pawnHandle = *(CHandle<CEntityInstance>*)pawn;
    return pawnHandle.Get();
}

void* CPlayer::GetPlayerPawn()
{
    static auto schema = g_ifaceService.FetchInterface<ISDKSchema>(SDKSCHEMA_INTERFACE_VERSION);

    auto controller = GetController();
    if (!controller)
        return nullptr;

    auto playerPawn = schema->GetPropPtr(controller, CCSPlayerController_m_hPlayerPawn);
    if (!playerPawn)
        return nullptr;

    CHandle<CEntityInstance>& playerPawnHandle = *(CHandle<CEntityInstance>*)playerPawn;
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

uint64_t& CPlayer::GetPressedButtons()
{
    return m_uPressedButtons;
}

void CPlayer::PerformCommand(const std::string& command)
{
    auto engine = g_ifaceService.FetchInterface<IVEngineServer2>(INTERFACEVERSION_VENGINESERVER);
    if (!engine)
        return;
    engine->ClientCommand(CPlayerSlot(m_iPlayerId), command.c_str());
}

std::string CPlayer::GetIPAddress()
{
    auto engine = g_ifaceService.FetchInterface<IVEngineServer2>(INTERFACEVERSION_VENGINESERVER);
    if (!engine)
        return "";
    auto pNetChan = engine->GetPlayerNetInfo(m_iPlayerId);
    if (!pNetChan)
        return "";

    return explode(pNetChan->GetAddress(), ":")[0];
}

void CPlayer::Kick(const std::string& sReason, int uReason)
{
    auto engine = g_ifaceService.FetchInterface<IVEngineServer2>(INTERFACEVERSION_VENGINESERVER);
    if (!engine)
        return;
    engine->DisconnectClient(m_iPlayerId, uReason, sReason.c_str());
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
typedef IGameEventListener2* (*GetLegacyGameEventListener)(CPlayerSlot slot);

void CPlayer::Think()
{
    static auto gamedata = g_ifaceService.FetchInterface<IGameDataManager>(GAMEDATA_INTERFACE_VERSION);
    static auto eventmanager = g_ifaceService.FetchInterface<IEventManager>(GAMEEVENTMANAGER_INTERFACE_VERSION);
    static auto pListenerSig = gamedata->GetSignatures()->Fetch("LegacyGameEventListener");
    if (pListenerSig)
    {
        auto listener = reinterpret_cast<GetLegacyGameEventListener>(pListenerSig)(m_iPlayerId);
        if (listener)
        {
            if (!centerMessageEvent)
                centerMessageEvent = eventmanager->GetGameEventManager()->CreateEvent("show_survival_respawn_status");

            if (centerMessageEvent)
            {
                if (centerMenuText != "")
                {
                    centerMessageEvent->SetString("loc_token", centerMenuText.c_str());
                    centerMessageEvent->SetInt("duration", 1);
                    centerMessageEvent->SetInt("userid", m_iPlayerId);

                    listener->FireGameEvent(centerMessageEvent);
                }
                else
                {
                    if (centerMessageEndTime >= GetTime())
                    {
                        centerMessageEvent->SetString("loc_token", centerMessageText.c_str());
                        centerMessageEvent->SetInt("duration", 1);
                        centerMessageEvent->SetInt("userid", m_iPlayerId);

                        listener->FireGameEvent(centerMessageEvent);
                    }
                    else
                    {
                        centerMessageEndTime = 0;
                    }
                }
            }
        }
    }

    auto pawn = GetPawn();

    static auto sdkschema = g_ifaceService.FetchInterface<ISDKSchema>(SDKSCHEMA_INTERFACE_VERSION);
    static auto vgui = g_ifaceService.FetchInterface<IVGUI>(VGUI_INTERFACE_VERSION);

    if (pawn)
    {
        auto& movementServices = *(void**)sdkschema->GetPropPtr(pawn, CBasePlayerPawn_m_pMovementServices);
        if (movementServices)
        {
            void* buttons = sdkschema->GetPropPtr(movementServices, CPlayer_MovementServices_m_nButtons);
            if (buttons)
            {
                uint64_t* states = (uint64_t*)sdkschema->GetPropPtr(buttons, CInButtonState_m_pButtonStates);
                uint64_t& newButtons = states[0];
                if (newButtons != m_uPressedButtons)
                {
                    for (int i = 0; i < 64; i++)
                    {
                        if ((m_uPressedButtons & (1ULL << i)) == 0 && (newButtons & (1ULL << i)) != 0)
                        {
                            if (g_pOnClientKeyStateChangedCallback)
                                reinterpret_cast<void (*)(int, uint32_t, bool)>(g_pOnClientKeyStateChangedCallback)(m_iPlayerId, i, true);
                        }
                        else if ((m_uPressedButtons & (1ULL << i)) != 0 && (newButtons & (1ULL << i)) == 0)
                        {
                            if (g_pOnClientKeyStateChangedCallback)
                                reinterpret_cast<void (*)(int, uint32_t, bool)>(g_pOnClientKeyStateChangedCallback)(m_iPlayerId, i, false);
                        }
                    }

                    m_uPressedButtons = newButtons;
                }
            }
        }

        auto& observerServices = *(void**)sdkschema->GetPropPtr(pawn, 14568842447348147577); // CBasePlayerPawn::m_pObserverServices
        if (observerServices)
        {
            CHandle<CEntityInstance>& observerTarget = *(CHandle<CEntityInstance>*)sdkschema->GetPropPtr(observerServices, 1590106406667131980); // CPlayer_ObserverServices::m_hObserverTarget
            vgui->CheckRenderForPlayer(this, observerTarget);
        }
    }
}

void CPlayer::RenderMenuCenterText(const std::string& text)
{
    centerMenuText = text;
}

void CPlayer::ClearRenderMenuCenterText()
{
    centerMenuText = "";
}

bool CPlayer::HasMenuShown()
{
    return !centerMenuText.empty();
}