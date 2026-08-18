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

#ifndef _api_interfaces_interfaces_h
#define _api_interfaces_interfaces_h

#include <string>
#include <api/dll/extern.h>

#include <api/engine/consoleoutput/consoleoutput.h>
#include <api/engine/convars/convars.h>
#include <api/engine/entities/entitysystem.h>
#include <api/engine/gameevents/gameevents.h>
#include <api/engine/voicemanager/voicemanager.h>

#include <api/memory/allocator/allocator.h>
#include <api/memory/gamedata/manager.h>
#include <api/memory/hooks/manager.h>

#include <api/monitor/logger/logger.h>
#include <api/monitor/crashreporter/crashreporter.h>

#include <api/network/sounds/soundevents.h>
#include <api/network/database/manager.h>
#include <api/network/netmessages/netmessages.h>

#include <api/scripting/scripting.h>

#include <api/sdk/schema.h>

#include <api/server/commands/manager.h>
#include <api/server/configuration/configuration.h>
#include <api/server/players/manager.h>
#include <api/server/translations/translations.h>

#include <public/filesystem.h>
#include <public/eiface.h>
#include <public/engine/igameeventsystem.h>
#include <public/networksystem/inetworkmessages.h>
#include <public/networksystem/inetworksystem.h>
#include <public/iserver.h>
#include <public/icvar.h>
#include <public/schemasystem/schemasystem.h>
#include <public/networkstringtabledefs.h>

 /**
  * Project Interfaces
  */

extern ILogger* g_pLogger;
extern IMemoryAllocator* g_pMemoryAllocator;
extern ICrashReporter* g_pCrashReporter;
extern IHooksManager* g_pHooksManager;
extern IGameDataManager* g_pGameDataManager;
extern IConfiguration* g_pConfiguration;
extern IEntitySystem* g_pEntSystem;
extern ISDKSchema* g_pSDKSchema;
extern IConvarManager* g_pConvarManager;
extern IEventManager* g_pGameEventManager;
extern IScriptingAPI* g_pScriptingAPI;
extern IPlayerManager* g_pPlayerManager;
extern IVoiceManager* g_pVoiceManager;
extern ISoundEventManager* g_pSoundEventManager;
extern IDatabaseManager* g_pDatabaseManager;
extern ITranslations* g_pTranslations;
extern IServerCommands* g_pServerCommands;
extern INetMessages* g_pNetMessages;
extern IConsoleOutput* g_pConsoleOutput;

/**
 * Game Interfaces
 */

extern IFileSystem* g_pGameFileSystem;
extern IVEngineServer2* g_pGameEngine;
extern IGameEventSystem* g_pGameEventSystem;
extern void* g_pGameSoundSystem;
extern INetworkMessages* g_pGameNetworkMessages;
extern INetworkSystem* g_pGameNetworkSystem;
extern INetworkServerService* g_pGameNetworkServerService;
extern ICvar* g_pGameCvar;
extern CSchemaSystem* g_pGameSchemaSystem;
extern INetworkStringTableContainer* g_pGameNetworkStringTableContainer;
extern ISource2GameClients* g_pGameClientsService;
extern void* g_pGameResources;

#endif