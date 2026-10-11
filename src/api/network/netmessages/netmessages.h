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

#ifndef src_api_network_usermessages_usermessages_h
#define src_api_network_usermessages_usermessages_h

#include <cstdint>
#include <functional>

enum NetMessageHookType : int
{
    NetMessageHook_Server = 0,
    NetMessageHook_Client = 1,
    NetMessageHook_ServerInternal = 2,
    NetMessageHook_Count
};

class INetMessages
{
public:
    virtual void Initialize() = 0;
    virtual void Shutdown() = 0;

    virtual void RegisterMessageHook(NetMessageHookType type, int messageid) = 0;
    virtual void UnregisterMessageHook(NetMessageHookType type, int messageid) = 0;
    virtual bool IsMessageRegistered(NetMessageHookType type, int messageid) = 0;

    // playermask_ptr, netmessageid, pmsg, return HookResult int
    virtual void SetServerMessageSendHandler(std::function<int(uint64_t*, int, void*)> handler) = 0;

    // playerid, netmessageid, pmsg, return HookResult int
    virtual void SetClientMessageSendHandler(std::function<int(int, int, void*)> handler) = 0;

    // playerid, netmessageid, pmsg, return HookResult int
    virtual void SetServerMessageInternalSendHandler(std::function<int(int, int, void*)> handler) = 0;
};

#endif