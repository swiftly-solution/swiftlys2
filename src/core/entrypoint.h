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

#include <string>
#include <public/steam/steam_api_common.h>
#include <public/steam/isteamugc.h>

enum class BridgeKind_t
{
    Metamod = 0,
    SwiftlyLoader = 1,
};

class SwiftlyCore
{
private:
    BridgeKind_t m_iKind;
    std::string m_sCorePath;
    std::string m_sLogPath;

public:
    bool Load(BridgeKind_t kind);
    bool Unload();

    void OnMapLoad(std::string map_name);
    void OnMapUnload();

    void* GetInterface(const std::string& iface_name);
    void SendConsoleMessage(const std::string& message);

    std::string GetCurrentGame();
    int GetMaxGameClients();

    std::string& GetCorePath();
    std::string GetVersion();
};

extern SwiftlyCore g_SwiftlyCore;

#endif