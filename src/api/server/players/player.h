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

#ifndef src_api_server_players_player_h
#define src_api_server_players_player_h

#include <atomic>
#include <cstdint>
#include <string>

#include <api/engine/voicemanager/voicemanager.h>
#include <mutex>

#include <api/utils/bitvec.h>
#include <public/bitvec.h>
#include <public/const.h>
#include <public/igameevents.h>

#include "networkbasetypes.pb.h"

enum MessageType : uint8_t
{
    Notify = 1,
    Console = 2,
    Chat = 3,
    Center = 4,
    ChatEOT = 5,
    Alert = 6,
    CenterHTML = 100,
};

struct BlockedTransmitInfo
{
    CBitVector<MAX_EDICTS> blockedTransmitBits;
    std::mutex mutex;
    std::atomic<int> blockedCount{0};

    void Block(int entityidx)
    {
        if (blockedTransmitBits.IsBitSet(entityidx)) return;
        blockedTransmitBits.Set(entityidx);
        blockedCount.fetch_add(1, std::memory_order_relaxed);
    }

    void Unblock(int entityidx)
    {
        if (!blockedTransmitBits.IsBitSet(entityidx)) return;
        blockedTransmitBits.Clear(entityidx);
        blockedCount.fetch_sub(1, std::memory_order_relaxed);
    }

    void UnblockAll()
    {
        blockedTransmitBits.ClearAll();
        blockedCount.store(0, std::memory_order_relaxed);
    }
};

class IPlayer
{
public:
    virtual void Initialize(int playerid) = 0;
    virtual void Shutdown() = 0;

    virtual void SendMsg(MessageType type, const std::string& message, int duration) = 0;

    virtual bool IsFakeClient() = 0;
    virtual void SetFakeClient(bool state) = 0;
    virtual bool IsAuthorized() = 0;

    virtual uint32_t GetConnectedTime() = 0;
    virtual int GetSlot() = 0;

    virtual void SetUnauthorizedSteamID(uint64_t steamID) = 0;

    virtual uint64_t GetUnauthorizedSteamID() = 0;
    virtual uint64_t GetSteamID() = 0;

    virtual void ChangeAuthorizationState(bool bAuthorized) = 0;

    virtual std::string& GetLanguage() = 0;
    virtual void SetLanguage(const std::string& language) = 0;

    /** Player SDK Classes **/
    virtual void* GetController() = 0;
    virtual void* GetPawn() = 0;
    virtual void* GetPlayerPawn() = 0;

    /** Voice Manager **/
    virtual ListenOverride& GetListenOverride(int targetid) = 0;
    virtual VoiceFlagValue& GetVoiceFlags() = 0;
    virtual CPlayerBitVec& GetSelfMutes() = 0;

    /** Engine Stuff **/
    virtual std::string GetIPAddress() = 0;
    virtual void Kick(const std::string& sReason, int uReason) = 0;
    virtual void SetListeningGameEvents(CBitVector<8192>& eventIDs) = 0;
    virtual bool IsListeningGameEvent(int eventID) = 0;
    virtual void SendGameEvent(IGameEvent* event) = 0;

    /** Transmit Stuff **/
    virtual BlockedTransmitInfo& GetBlockedTransmittingBits() = 0;

    virtual void Think() = 0;

    /** Menus! **/
    virtual void RenderMenuCenterText(const std::string& text) = 0;
    virtual void ClearRenderMenuCenterText() = 0;
    virtual bool HasMenuShown() = 0;

    virtual bool IsFirstSpawn() = 0;
    virtual void SetFirstSpawn(bool state) = 0;

    virtual uint64_t GetSessionID() = 0;
};

#endif