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

#include "metamod.h"
#include "../entrypoint.h"

#include <api/interfaces/manager.h>
#include <memory/gamedata/manager.h>

#include <public/iserver.h>
#include <public/tier0/icommandline.h>
#include <steam/steam_gameserver.h>
#include <public/engine/igameeventsystem.h>

#include <s2binlib/s2binlib.h>

SwiftlyMMBridge g_MMPluginBridge;

class GameSessionConfiguration_t
{
};

PLUGIN_EXPOSE(SwiftlyMMBridge, g_MMPluginBridge);
bool SwiftlyMMBridge::Load(PluginId id, ISmmAPI* ismm, char* error, size_t maxlen, bool late)
{
    PLUGIN_SAVEVARS();
    g_SMAPI->AddListener(this, this);

    bool result = g_SwiftlyCore.Load(BridgeKind_t::Metamod);

    if (late)
    {
        static auto playermanager = g_ifaceService.FetchInterface<IPlayerManager>(PLAYERMANAGER_INTERFACE_VERSION);
        playermanager->SteamAPIServerActivated();
    }

    return result;
}

bool SwiftlyMMBridge::Unload(char* error, size_t maxlen)
{
    return g_SwiftlyCore.Unload();
}

void SwiftlyMMBridge::AllPluginsLoaded()
{

}

void SwiftlyMMBridge::OnLevelInit(char const* pMapName, char const* pMapEntities, char const* pOldLevel, char const* pLandmarkName, bool loadGame, bool background)
{
}

void SwiftlyMMBridge::OnLevelShutdown()
{
}

const char* SwiftlyMMBridge::GetAuthor()
{
    return "Swiftly Development Team";
}

const char* SwiftlyMMBridge::GetName()
{
    return "SwiftlyS2";
}

const char* SwiftlyMMBridge::GetDescription()
{
    return "C# Framework for Source2-based games";
}

const char* SwiftlyMMBridge::GetURL()
{
    return "https://github.com/swiftly-solution/swiftly";
}

const char* SwiftlyMMBridge::GetLicense()
{
    return "GNU GPLv3";
}

const char* SwiftlyMMBridge::GetVersion()
{
#ifndef SWIFTLY_VERSION
    return "Local";
#else
    return SWIFTLY_VERSION;
#endif
}

const char* SwiftlyMMBridge::GetDate()
{
    return __DATE__;
}

const char* SwiftlyMMBridge::GetLogTag()
{
    return "SwiftlyS2";
}