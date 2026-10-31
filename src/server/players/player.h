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

#ifndef src_server_players_player_h
#define src_server_players_player_h

#include <api/server/players/player.h>

#include <public/game/igameevents.h>
#include <public/tier0/recipientfilter.h>

#include <chrono>
#include <mutex>
#include <optional>
#include <string>

void PostTextMsg(IRecipientFilter* filter, MessageType type, const std::string& message, std::optional<int> team);

class CPlayer : public IPlayer
{
public:
    virtual void Initialize(int playerid) override;
    virtual void Shutdown() override;

    virtual void SendMsg(MessageType type, const std::string& message, int duration) override;

    virtual bool IsFakeClient() override;
    virtual void SetFakeClient(bool state) override;
    virtual bool IsAuthorized() override;

    virtual uint32_t GetConnectedTime() override;

    virtual void SetUnauthorizedSteamID(uint64_t steamID) override;

    virtual uint64_t GetUnauthorizedSteamID() override;
    virtual uint64_t GetSteamID() override;

    virtual void ChangeAuthorizationState(bool bAuthorized) override;

    virtual void* GetController() override;
    virtual void* GetPawn() override;
    virtual void* GetPlayerPawn() override;
    virtual std::optional<int> GetTeam() override;
    virtual int32_t GetKills() override;
    virtual const char* GetName() override;


    virtual std::string GetIPAddress() override;
    virtual void Kick(const std::string& sReason, int uReason) override;
    virtual void SetListeningGameEvents(CBitVector<8192>& eventIDs) override;
    virtual bool IsListeningGameEvent(int eventID) override;
    virtual void SendGameEvent(IGameEvent* event) override;

    virtual BlockedTransmitInfo& GetBlockedTransmittingBits() override;

    virtual void Think() override;

    /** Menus! **/
    virtual void RenderMenuCenterText(const std::string& text) override;
    virtual void ClearRenderMenuCenterText() override;
    virtual bool HasMenuShown() override;
    virtual bool IsFirstSpawn() override;
    virtual void SetFirstSpawn(bool state) override;
    virtual uint64_t GetSessionID() override;

private:
    void UpdateCenterMessage();
    void UpdateButtons();
    void EnsureCenterMessageEvent();

    int m_iPlayerId = -1;
    bool m_bAuthorized = false;
    bool m_bFirstSpawn = true;
    bool m_bIsFakeClient = false;


    BlockedTransmitInfo m_bvBlockedTransmittingEntities = {};

    uint64_t m_uPressedButtons = 0;

    std::chrono::steady_clock::time_point m_uConnectedTimeStart;

    uint64_t m_uUnauthorizedSteamID = 0;

    std::chrono::steady_clock::time_point m_centerMessageEndTime;
    std::string m_sCenterMessageText;
    IGameEvent* m_pCenterMessageEvent = nullptr;

    std::mutex m_centerMenuMutex;
    std::string m_sCenterMenuText;
    uint64_t m_uSessionId = 0;

    std::mutex m_listeningGameEventsMutex;
    CBitVector<8192> m_listeningGameEvents = {};
};

#endif
