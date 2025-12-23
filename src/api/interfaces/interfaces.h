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
#include <api/engine/vgui/vgui.h>
#include <api/engine/voicemanager/voicemanager.h>

#include <api/memory/allocator/allocator.h>
#include <api/memory/gamedata/manager.h>
#include <api/memory/hooks/manager.h>

#include <api/monitor/logger/logger.h>
#include <api/monitor/crashreporter/crashreporter.h>

#include <api/network/sounds/soundevents.h>
#include <api/network/database/manager.h>
#include <api/network/netmessages/netmessages.h>

#include <api/sdk/schema.h>

#include <api/server/commands/manager.h>
#include <api/server/configuration/configuration.h>
#include <api/server/players/manager.h>
#include <api/server/translations/translations.h>

SW_API void* GetPureInterface(const char* iface_name);

#define EXTENSIONMANAGER_INTERFACE_VERSION                  "ExtensionManagerAPI"
#define LOGGER_INTERFACE_VERSION                            "LoggerAPI"
#define MEMORYALLOCATOR_INTERFACE_VERSION                   "MemoryAllocatorAPI"
#define CRASHREPORTER_INTERFACE_VERSION                     "CrashReporterAPI"
#define HOOKSMANAGER_INTERFACE_VERSION                      "HooksManagerAPI"
#define GAMEDATA_INTERFACE_VERSION                          "GameDataAPI"
#define CONFIGURATION_INTERFACE_VERSION                     "ConfigurationAPI"
#define ENTITYSYSTEM_INTERFACE_VERSION                      "EntitySystemAPI"
#define SDKSCHEMA_INTERFACE_VERSION                         "SDKSchemaAPI"
#define CONVARMANAGER_INTERFACE_VERSION                     "ConVarManagerAPI"
#define GAMEEVENTMANAGER_INTERFACE_VERSION                  "GameEventManagerAPI"
#define VOICEMANAGER_INTERFACE_VERSION                      "VoiceManagerAPI"
#define SCRIPTING_INTERFACE_VERSION                         "ScriptingAPI"
#define PLAYERMANAGER_INTERFACE_VERSION                     "PlayerManagerAPI"
#define SOUNDEVENTMANAGER_INTERFACE_VERSION                 "SoundEventManagerAPI"
#define DATABASEMANAGER_INTERFACE_VERSION                   "DatabaseManagerAPI"
#define TRANSLATIONS_INTERFACE_VERSION                      "TranslationsAPI"
#define SERVERCOMMANDS_INTERFACE_VERSION                    "ServerCommandsAPI"
#define NETMESSAGES_INTERFACE_VERSION                       "NetMessagesAPI"
#define VGUI_INTERFACE_VERSION                              "VGUIAPI"
#define CONSOLEOUTPUT_INTERFACE_VERSION                     "ConsoleOutputAPI"

#endif