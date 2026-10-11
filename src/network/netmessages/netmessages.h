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

#ifndef src_network_netmessages_netmessages_h
#define src_network_netmessages_netmessages_h

#include <api/network/netmessages/netmessages.h>

#include <api/utils/mutex.h>

#include <unordered_set>

 // ofc you need to stay here
#include <public/engine/igameeventsystem.h>

class CNetMessages : public INetMessages
{
public:
    virtual void Initialize() override;
    virtual void Shutdown() override;

    virtual void RegisterMessageHook(NetMessageHookType type, int messageid) override;
    virtual void UnregisterMessageHook(NetMessageHookType type, int messageid) override;
    virtual bool IsMessageRegistered(NetMessageHookType type, int messageid) override;

    virtual void SetServerMessageSendHandler(std::function<int(uint64_t*, int, void*)> handler) override;
    virtual void SetClientMessageSendHandler(std::function<int(int, int, void*)> handler) override;
    virtual void SetServerMessageInternalSendHandler(std::function<int(int, int, void*)> handler) override;
private:
    QueueMutex m_mtxRegisteredMessages;
    std::unordered_set<int> m_registeredMessages[NetMessageHook_Count];
};

#endif