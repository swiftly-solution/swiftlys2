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

#include <public/igameevents.h>

#include <chrono>

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
    virtual int GetSlot() override;

    virtual void SetUnauthorizedSteamID(uint64_t steamID) override;

    virtual uint64_t GetUnauthorizedSteamID() override;
    virtual uint64_t GetSteamID() override;

    virtual void ChangeAuthorizationState(bool bAuthorized) override;

    virtual std::string& GetLanguage() override;

    virtual void* GetController() override;
    virtual void* GetPawn() override;
    virtual void* GetPlayerPawn() override;

    virtual ListenOverride& GetListenOverride(int targetid) override;
    virtual VoiceFlagValue& GetVoiceFlags() override;
    virtual CPlayerBitVec& GetSelfMutes() override;

    virtual uint64_t& GetPressedButtons() override;
    virtual void PerformCommand(const std::string& command) override;
    virtual std::string GetIPAddress() override;
    virtual void Kick(const std::string& sReason, int uReason) override;

    virtual BlockedTransmitInfo& GetBlockedTransmittingBits() override;

    virtual void Think() override;

    /** Menus! **/
    virtual void RenderMenuCenterText(const std::string& text) override;
    virtual void ClearRenderMenuCenterText() override;
    virtual bool HasMenuShown() override;
    virtual bool IsFirstSpawn() override;
    virtual void SetFirstSpawn(bool state) override;

private:
    int m_iPlayerId;
    bool m_bAuthorized;
    bool m_bFirstSpawn = true;
    bool m_bIsFakeClient = false;

    ListenOverride m_uListenMap[66] = {};
    VoiceFlagValue m_uVoiceFlags = VoiceFlagValue::Speak_Normal;
    CPlayerBitVec m_bvSelfMutes = {};

    BlockedTransmitInfo m_bvBlockedTransmittingEntities = {};

    uint64_t m_uPressedButtons = 0;

    std::chrono::high_resolution_clock::time_point m_uConnectedTimeStart;

    uint64_t m_uUnauthorizedSteamID = 0;

    std::string m_sLanguage = "en";

    uint64_t centerMessageEndTime = 0;
    std::string centerMessageText = "";
    IGameEvent* centerMessageEvent = nullptr;

    std::string centerMenuText = "";
};

#endif