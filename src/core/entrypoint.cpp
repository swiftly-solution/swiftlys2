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

#include "entrypoint.h"
#include "console/colors.h"
#include <api/interfaces/interfaces.h>
#include <api/interfaces/interfaces.h>
#include <api/memory/hooks/manager.h>
#include <api/scripting/scripting.h>
#include <api/shared/env.h>

#include <core/managed/host/host.h>

#include "managed/host/dynlib.h"
#include "managed/host/strconv.h"

#include <engine/entities/entitysystem.h>
#include <engine/entities/listener.h>
#include <engine/fixes/entrypoint.h>
#include <engine/gamesystem/gamesystem.h>

#include <public/tier0/icommandline.h>
#include <public/tier1/utlstringtoken.h>

#include <api/shared/files.h>
#include <api/shared/plat.h>
#include <api/shared/string.h>
#include <api/utils/mutex.h>

#include <public/icvar.h>
#include <public/tier1/KeyValues.h>

#include <fmt/format.h>

#include <public/engine/igameeventsystem.h>
#include <s2binlib/s2binlib.h>

#include <public/steam/steam_gameserver.h>

#include <public/tier1/convar.h>
#include <thread>
#include <cstdlib>

#include <monitor/consolelogger/consolelogger.h>

SwiftlyCore g_SwiftlyCore;
std::thread::id g_mainThreadId;

CreateIFaceFn g_pServerFactory = nullptr;
CreateIFaceFn g_pEngineFactory = nullptr;

IVFunctionHook* g_pGameServerSteamAPIActivated = nullptr;
IVFunctionHook* g_pGameServerSteamAPIDeactivated = nullptr;

IVFunctionHook* g_pLoopInitHook = nullptr;

void GameServerSteamAPIActivatedHook(void* _this);
void GameServerSteamAPIDeactivatedHook(void* _this);

bool LoopInitHook(void* _this, KeyValues* pKeyValues, void* pRegistry);

extern ICvar* g_pCVar;

bool SwiftlyCore::Load(BridgeKind_t kind, CreateIFaceFn serverFactory, CreateIFaceFn engineFactory)
{
    g_pServerFactory = serverFactory;
    g_pEngineFactory = engineFactory;
    g_mainThreadId = std::this_thread::get_id();
    m_iKind = kind;

    SetupConsoleColors();

    m_sCorePath = CommandLine()->ParmValue(CUtlStringToken("-sw_path"), WIN_LINUX("addons\\swiftlys2", "addons/swiftlys2"));
    if (!ends_with(m_sCorePath, WIN_LINUX("\\", "/")))
    {
        m_sCorePath += WIN_LINUX("\\", "/");
    }
    m_sLogPath = CommandLine()->ParmValue(CUtlStringToken("-sw_logpath"), WIN_LINUX("addons\\swiftlys2\\logs", "addons/swiftlys2/logs"));
    if (!ends_with(m_sLogPath, WIN_LINUX("\\", "/")))
    {
        m_sLogPath += WIN_LINUX("\\", "/");
    }
    m_sLogPath = replace(m_sLogPath, WIN_LINUX("addons\\swiftlys2", "addons/swiftlys2"), m_sCorePath);

    g_pCrashReporter->Init();

    g_pGameFileSystem = (IFileSystem*)GetInterface(FILESYSTEM_INTERFACE_VERSION);
    g_pGameEngine = (IVEngineServer2*)GetInterface(INTERFACEVERSION_VENGINESERVER);
    g_pGameEventSystem = (IGameEventSystem*)GetInterface(GAMEEVENTSYSTEM_INTERFACE_VERSION);
    g_pGameSoundSystem = GetInterface(SOUNDSYSTEM_INTERFACE_VERSION);
    g_pGameNetworkMessages = (INetworkMessages*)GetInterface(NETWORKMESSAGES_INTERFACE_VERSION);
    g_pGameNetworkSystem = (INetworkSystem*)GetInterface(NETWORKSYSTEM_INTERFACE_VERSION);
    g_pGameNetworkServerService = (INetworkServerService*)GetInterface(NETWORKSERVERSERVICE_INTERFACE_VERSION);
    g_pGameCvar = (ICvar*)GetInterface(CVAR_INTERFACE_VERSION);
    g_pGameSchemaSystem = (CSchemaSystem*)GetInterface(SCHEMASYSTEM_INTERFACE_VERSION);
    g_pGameNetworkStringTableContainer = (INetworkStringTableContainer*)GetInterface(SOURCE2ENGINETOSERVERSTRINGTABLE_INTERFACE_VERSION);
    g_pGameClientsService = (ISource2GameClients*)GetInterface(INTERFACEVERSION_SERVERGAMECLIENTS);
    g_pGameResources = GetInterface(GAMERESOURCESERVICESERVER_INTERFACE_VERSION);

    s2binlib_initialize(Plat_GetGameDirectory(), "csgo");

#ifdef _WIN32
    void* libServer = load_library(StringWide(Plat_GetGameDirectory() + std::string("\\csgo\\bin\\win64\\server.dll")).c_str());
    void* libEngine = load_library(StringWide(Plat_GetGameDirectory() + std::string("\\bin\\win64\\engine2.dll")).c_str());
    s2binlib_set_module_base_from_pointer("server", libServer);
    s2binlib_set_module_base_from_pointer("engine2", libEngine);
#endif

    g_pCVar = g_pGameCvar;
    ConVar_Register(FCVAR_RELEASE | FCVAR_SERVER_CAN_EXECUTE | FCVAR_CLIENT_CAN_EXECUTE | FCVAR_GAMEDLL, nullptr, nullptr);

    const char* logLevel = CommandLine()->ParmValue(CUtlStringToken("-sw_loglevel"));
    if (logLevel)
    {
        putenv("SWIFTLY_LOG_LEVEL", logLevel, 1);
    }

    const char* hideLogInConsole = CommandLine()->ParmValue(CUtlStringToken("-sw_hide_logs_in_console"));
    if (hideLogInConsole)
    {
        putenv("SWIFTLY_HIDE_LOG_IN_CONSOLE", hideLogInConsole, 1);
    }

    if (GetCurrentGame() == "unknown")
    {
        if (g_pGameEngine)
        {
            g_pLogger->Error("Entrypoint", fmt::format("Unknown game detected. App ID: {}", g_pGameEngine->GetAppID()));
        }
        else
        {
            g_pLogger->Error("Entrypoint", "Unknown game detected. No engine interface available.");
        }
        return false;
    }

    g_pConfiguration->InitializeExamples();
    if (!g_pConfiguration->Load())
    {
        g_pLogger->Error("Entrypoint", "Couldn't load the core configuration.");
        return false;
    }

    if (int* level = std::get_if<int>(&g_pConfiguration->GetValue("core.DotnetCrashTracerLevel")))
    {
        if (*level > 0)
        {
            g_pCrashReporter->EnableDotnetCrashTracer(*level);
        }
    }

    g_pGameDataManager->GetOffsets()->Load(GetCurrentGame());
    g_pGameDataManager->GetSignatures()->Load(GetCurrentGame());
    g_pGameDataManager->GetPatches()->Load(GetCurrentGame());

    g_pSDKSchema->Load();

    if (std::string* s = std::get_if<std::string>(&g_pConfiguration->GetValue("core.PatchesToPerform")))
    {
        auto patches = explodeToSet(*s, " ");
        for (const auto& patch : patches)
        {
            if (g_pGameDataManager->GetPatches()->Exists(patch))
            {
                g_pGameDataManager->GetPatches()->Apply(patch);
                g_pLogger->Info("Patching", fmt::format("Applied patch: {}\n", patch));
            }
            else
            {
                g_pLogger->Warning("Patching", fmt::format("Couldn't find patch: {}\n", patch));
            }
        }

        if (g_pGameDataManager->GetPatches()->Exists("SetSchemaHammerUniqueId")) {
            g_pGameDataManager->GetPatches()->Apply("SetSchemaHammerUniqueId");
            g_pLogger->Info("Patching", "Applied patch: SetSchemaHammerUniqueId\n");
        }
        else {
            g_pLogger->Warning("Patching", "Couldn't find patch: SetSchemaHammerUniqueId\n");
        }
    }

    g_pConsoleOutput->Initialize();
    g_ConsoleLogger.Initialize();

    if (bool* b = std::get_if<bool>(&g_pConfiguration->GetValue("core.ConsoleFilter")))
    {
        if (*b)
        {
            g_pConsoleOutput->ToggleFilter();
        }
    }

    g_pEntSystem->Initialize();

    g_pConvarManager->Initialize();

    g_pGameEventManager->Initialize();

    if (!InitGameSystem())
    {
        g_pLogger->Error("Game System", "Couldn't initialize the Game System.\n");
        return false;
    }

    g_pPlayerManager->Initialize();
    g_pDatabaseManager->Initialize();
    g_pTranslations->Initialize();
    g_pNetMessages->Initialize();
    g_pVoiceManager->Initialize();
    g_pServerCommands->Initialize();

    g_pHooksManager->Initialize();

    void* loopmodeLevelLoad = nullptr;
    s2binlib_find_vtable("engine2", "CLoopModeLevelLoad", &loopmodeLevelLoad);

    g_pLoopInitHook = g_pHooksManager->CreateVFunctionHook();
    g_pLoopInitHook->SetHookFunction(loopmodeLevelLoad, g_pGameDataManager->GetOffsets()->Fetch("ILoopMode::LoopInit"), (void*)LoopInitHook, true);
    g_pLoopInitHook->Enable();

    void* servervtable = nullptr;
    s2binlib_find_vtable("server", "CSource2Server", &servervtable);

    g_pGameServerSteamAPIActivated = g_pHooksManager->CreateVFunctionHook();
    g_pGameServerSteamAPIActivated->SetHookFunction(servervtable, g_pGameDataManager->GetOffsets()->Fetch("IServerGameDLL::GameServerSteamAPIActivated"), (void*)GameServerSteamAPIActivatedHook, true);
    g_pGameServerSteamAPIActivated->Enable();

    g_pGameServerSteamAPIDeactivated = g_pHooksManager->CreateVFunctionHook();
    g_pGameServerSteamAPIDeactivated->SetHookFunction(servervtable, g_pGameDataManager->GetOffsets()->Fetch("IServerGameDLL::GameServerSteamAPIDeactivated"), (void*)GameServerSteamAPIDeactivatedHook, true);
    g_pGameServerSteamAPIDeactivated->Enable();

    StartFixes();

    if (!InitializeHostFXR(std::string(Plat_GetGameDirectory()) + "/csgo/" + m_sCorePath))
    {
        g_pCrashReporter->ReportPreventionIncident("Managed", fmt::format("Couldn't initialize the .NET runtime. Make sure you installed `swiftlys2-{}-{}-with-runtimes.zip`.", WIN_LINUX("windows", "linux"), GetVersion()));
        return true;
    }

    bool managedLogEnabled = true;
    int managedLogInterval = 2000;
    if (bool* b = std::get_if<bool>(&g_pConfiguration->GetValue("core.ConsoleLogger.ManagedEnable")))
        managedLogEnabled = *b;
    if (int* i = std::get_if<int>(&g_pConfiguration->GetValue("core.ConsoleLogger.WriteIntervalMs")))
        managedLogInterval = (*i > 0) ? *i : 2000;

    putenv("SWIFTLY_MANAGED_LOG_ENABLE", managedLogEnabled ? "1" : "0", 1);
    putenv("SWIFTLY_MANAGED_LOG_INTERVAL_MS", std::to_string(managedLogInterval).c_str(), 1);

    if (!InitializeDotNetAPI(g_pScriptingAPI->GetNativeFunctions(), g_pScriptingAPI->GetNativeFunctionsCount(), std::string(Plat_GetGameDirectory()) + "/csgo/" + m_sLogPath))
    {
        g_pCrashReporter->ReportPreventionIncident("Managed", "Couldn't initialize the .NET scripting API.");
        return true;
    }

    return true;
}

bool SwiftlyCore::Unload()
{
    g_pHooksManager->Shutdown();
    g_pPlayerManager->Shutdown();
    g_pEntSystem->Shutdown();
    g_pConvarManager->Shutdown();
    g_pGameEventManager->Shutdown();
    g_pNetMessages->Shutdown();
    g_pServerCommands->Shutdown();
    g_pVoiceManager->Shutdown();

    if (g_pGameServerSteamAPIActivated != nullptr)
    {
        g_pGameServerSteamAPIActivated->Disable();
        g_pHooksManager->DestroyVFunctionHook(g_pGameServerSteamAPIActivated);
        g_pGameServerSteamAPIActivated = nullptr;
    }

    if (g_pGameServerSteamAPIDeactivated != nullptr)
    {
        g_pGameServerSteamAPIDeactivated->Disable();
        g_pHooksManager->DestroyVFunctionHook(g_pGameServerSteamAPIDeactivated);
        g_pGameServerSteamAPIDeactivated = nullptr;
    }

    StopFixes();

    ShutdownGameSystem();

    g_ConsoleLogger.Shutdown();
    g_pCrashReporter->Shutdown();

    return true;
}

void GameServerSteamAPIActivatedHook(void* _this)
{
    if (!g_pGameEngine->IsDedicatedServer())
    {
        return reinterpret_cast<decltype(&GameServerSteamAPIActivatedHook)>(g_pGameServerSteamAPIActivated->GetOriginal())(_this);
    }

    g_pPlayerManager->SteamAPIServerActivated();

    return reinterpret_cast<decltype(&GameServerSteamAPIActivatedHook)>(g_pGameServerSteamAPIActivated->GetOriginal())(_this);
}

void GameServerSteamAPIDeactivatedHook(void* _this)
{
    return reinterpret_cast<decltype(&GameServerSteamAPIDeactivatedHook)>(g_pGameServerSteamAPIDeactivated->GetOriginal())(_this);
}

std::string workshop_map = "";
QueueMutex g_qmMapLoadQueue;
std::string current_map = "";

bool LoopInitHook(void* _this, KeyValues* pKeyValues, void* pRegistry)
{
    QueueLockGuard lock(g_qmMapLoadQueue);
    if (current_map != "")
    {
        g_SwiftlyCore.OnMapUnload();
    }

    bool ret = reinterpret_cast<decltype(&LoopInitHook)>(g_pLoopInitHook->GetOriginal())(_this, pKeyValues, pRegistry);

    g_SwiftlyCore.OnMapLoad(pKeyValues->GetString("levelname"));

    if (pKeyValues->FindKey("customgamemode"))
    {
        workshop_map = pKeyValues->GetString("customgamemode");
    }
    else
    {
        workshop_map = "";
    }

    return ret;
}

extern void* g_pOnMapLoadCallback;
extern void* g_pOnMapUnloadCallback;

void SwiftlyCore::OnMapLoad(std::string map_name)
{
    current_map = map_name;

    if (g_pOnMapLoadCallback)
    {
        reinterpret_cast<void (*)(const char*)>(g_pOnMapLoadCallback)(map_name.c_str());
    }
}

void SwiftlyCore::OnMapUnload()
{
    if (g_pOnMapUnloadCallback)
    {
        reinterpret_cast<void (*)(const char*)>(g_pOnMapUnloadCallback)(current_map.c_str());
    }

    current_map = "";
}

void* SwiftlyCore::GetInterface(const std::string& interface_name)
{
    int returnCode = 0;

    void* iface = g_pServerFactory(interface_name.c_str(), &returnCode);
    if (iface != nullptr) return iface;

    iface = g_pEngineFactory(interface_name.c_str(), &returnCode);
    return iface;
}

void SwiftlyCore::SendConsoleMessage(const std::string& message)
{
    Msg("%s", message.c_str());
}

std::string SwiftlyCore::GetCurrentGame()
{
    if (!g_pGameEngine)
    {
        return "unknown";
    }

    switch (g_pGameEngine->GetAppID())
    {
    case 730:
        return "cs2";
    default:
        return "unknown";
    }
}

int SwiftlyCore::GetMaxGameClients()
{
    if (!g_pGameEngine) return 0;

    switch (g_pGameEngine->GetAppID())
    {
    case 730:
        return 64;
    default:
        return 0;
    }
}

std::string& SwiftlyCore::GetCorePath()
{
    return m_sCorePath;
}

std::string SwiftlyCore::GetVersion()
{
#ifndef SWIFTLY_VERSION
    return "Local";
#else
    return SWIFTLY_VERSION;
#endif
}
