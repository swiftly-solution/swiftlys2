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

#ifndef _core_entrypoint_h
#define _core_entrypoint_h

#include <public/steamworks/isteamugc.h>
#include <public/steamworks/steam_api_common.h>

#include <string>
#include <string_view>

using CreateIFaceFn = void* (*)(const char* name, int* returnCode);

enum class BridgeKind_t
{
    SwiftlyLoader = 0,
};

class SwiftlyCore
{
private:
    BridgeKind_t m_iKind = BridgeKind_t::SwiftlyLoader;
    std::string m_sCorePath;
    std::string m_sLogPath;
    int m_iMaxClients = -1;

public:
    [[nodiscard]] bool Load(BridgeKind_t kind, CreateIFaceFn serverFactory, CreateIFaceFn engineFactory);
    bool Unload();

    [[nodiscard]] void* GetInterface(const std::string& iface_name);

    template <class T>
    [[nodiscard]] T* GetInterfaceAs(const std::string& iface_name)
    {
        return static_cast<T*>(GetInterface(iface_name));
    }

    void SendConsoleMessage(const std::string& message);

    [[nodiscard]] std::string GetCurrentGame();
    [[nodiscard]] int GetMaxGameClients();

    [[nodiscard]] std::string& GetCorePath();
    [[nodiscard]] std::string GetVersion();
};

extern SwiftlyCore g_SwiftlyCore;

#endif