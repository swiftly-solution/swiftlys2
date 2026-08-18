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

#include <api/interfaces/interfaces.h>
#include <core/entrypoint.h>

#include <engine/consoleoutput/consoleoutput.h>
#include <engine/convars/convars.h>
#include <engine/entities/entitysystem.h>
#include <engine/gameevents/gameevents.h>
#include <engine/voicemanager/voicemanager.h>

#include <memory/allocator/allocator.h>
#include <memory/hooks/manager.h>
#include <memory/gamedata/manager.h>

#include <monitor/logger/logger.h>
#include <monitor/crashreporter/crashreporter.h>

#include <network/sounds/soundevents.h>
#include <network/database/manager.h>
#include <network/netmessages/netmessages.h>

#include <scripting/scripting.h>

#include <sdk/schema.h>

#include <server/commands/manager.h>
#include <server/configuration/configuration.h>
#include <server/players/manager.h>
#include <server/translations/translations.h>

Logger g_Logger;
MemoryAllocator g_MemoryAllocator;
CrashReporter g_CrashReporter;
HooksManager g_HooksManager;
GameDataManager g_GameDataManager;
Configuration g_Configuration;
CEntSystem g_EntSystem;
CSDKSchema g_SDKSchema;
CConvarManager g_ConvarManager;
CEventManager g_GameEventManager;
CScriptingAPI g_ScriptingAPI;
CPlayerManager g_PlayerManager;
CVoiceManager g_VoiceManager;
CSoundEventManager g_SoundEventManager;
CDatabaseManager g_DatabaseManager;
CTranslations g_Translations;
CServerCommands g_ServerCommands;
CNetMessages g_NetMessages;
CConsoleOutput g_ConsoleOutput;

ILogger* g_pLogger = (ILogger*)&g_Logger;
IMemoryAllocator* g_pMemoryAllocator = (IMemoryAllocator*)&g_MemoryAllocator;
ICrashReporter* g_pCrashReporter = (ICrashReporter*)&g_CrashReporter;
IHooksManager* g_pHooksManager = (IHooksManager*)&g_HooksManager;
IGameDataManager* g_pGameDataManager = (IGameDataManager*)&g_GameDataManager;
IConfiguration* g_pConfiguration = (IConfiguration*)&g_Configuration;
IEntitySystem* g_pEntSystem = (IEntitySystem*)&g_EntSystem;
ISDKSchema* g_pSDKSchema = (ISDKSchema*)&g_SDKSchema;
IConvarManager* g_pConvarManager = (IConvarManager*)&g_ConvarManager;
IEventManager* g_pGameEventManager = (IEventManager*)&g_GameEventManager;
IScriptingAPI* g_pScriptingAPI = (IScriptingAPI*)&g_ScriptingAPI;
IPlayerManager* g_pPlayerManager = (IPlayerManager*)&g_PlayerManager;
IVoiceManager* g_pVoiceManager = (IVoiceManager*)&g_VoiceManager;
ISoundEventManager* g_pSoundEventManager = (ISoundEventManager*)&g_SoundEventManager;
IDatabaseManager* g_pDatabaseManager = (IDatabaseManager*)&g_DatabaseManager;
ITranslations* g_pTranslations = (ITranslations*)&g_Translations;
IServerCommands* g_pServerCommands = (IServerCommands*)&g_ServerCommands;
INetMessages* g_pNetMessages = (INetMessages*)&g_NetMessages;
IConsoleOutput* g_pConsoleOutput = (IConsoleOutput*)&g_ConsoleOutput;

IFileSystem* g_pGameFileSystem = nullptr;
IVEngineServer2* g_pGameEngine = nullptr;
IGameEventSystem* g_pGameEventSystem = nullptr;
void* g_pGameSoundSystem = nullptr;
INetworkMessages* g_pGameNetworkMessages = nullptr;
INetworkSystem* g_pGameNetworkSystem = nullptr;
INetworkServerService* g_pGameNetworkServerService = nullptr;
ICvar* g_pGameCvar = nullptr;
CSchemaSystem* g_pGameSchemaSystem = nullptr;
INetworkStringTableContainer* g_pGameNetworkStringTableContainer = nullptr;
ISource2GameClients* g_pGameClientsService = nullptr;
void* g_pGameResources = nullptr;