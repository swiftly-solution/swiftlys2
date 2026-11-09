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

#include <atomic>

 // ofc you need to stay here
#include <public/engine2/igameeventsystem.h>

class CNetMessages : public INetMessages
{
public:
    void RegisterMessageHook(NetMessageHookType type, int messageid) override;
    void UnregisterMessageHook(NetMessageHookType type, int messageid) override;
    [[nodiscard]] bool IsMessageRegistered(NetMessageHookType type, int messageid) override;

    void SetServerMessageSendHandler(std::function<int(uint64_t*, int, void*)> handler) override;
    void SetClientMessageSendHandler(std::function<int(int, int, void*)> handler) override;
    void SetServerMessageInternalSendHandler(std::function<int(int, int, void*)> handler) override;

private:
    [[nodiscard]] static bool IsValidHook(NetMessageHookType type, int messageid);

    static constexpr int MESSAGE_COUNT = 16385;
    std::atomic<uint64_t> m_registeredMessages[NetMessageHook_Count][(MESSAGE_COUNT + 63) / 64]{};
};

#endif
