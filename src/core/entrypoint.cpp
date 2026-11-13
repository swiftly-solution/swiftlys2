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
#include "managed/host/dynlib.h"
#include "managed/host/strconv.h"

#include <api/extension/host.h>
#include <api/interfaces/interfaces.h>
#include <api/memory/hooks/manager.h>
#include <api/scripting/scripting.h>
#include <api/shared/env.h>
#include <api/shared/plat.h>
#include <api/shared/profiler.h>
#include <api/shared/string.h>

#include <core/managed/host/host.h>

#include <engine/fixes/entrypoint.h>
#include <engine/gamesystem/gamesystem.h>

#include <memory/hooks/vfunction.h>

#include <monitor/consolelogger/consolelogger.h>

#include <public/engine2/igameeventsystem.h>
#include <public/engine2/ivengineserver2.h>
#include <public/tier0/convar.h>
#include <public/tier0/icommandline.h>
#include <public/tier0/icvar.h>
#include <public/tier0/keyvalues.h>
#include <public/tier0/utlstringtoken.h>

#include <scripting/engine/events.h>

#include <format>
#include <mutex>
#include <thread>

SwiftlyCore g_SwiftlyCore;
std::thread::id g_mainThreadId;

static CreateIFaceFn g_pServerFactory = nullptr;
static CreateIFaceFn g_pEngineFactory = nullptr;

static void GameServerSteamAPIActivatedHook(void* _this);

static VFunctionHook g_GameServerSteamAPIActivated(reinterpret_cast<void*>(GameServerSteamAPIActivatedHook), "server", "CSource2Server", "IServerGameDLL::GameServerSteamAPIActivated");

static constexpr std::string_view kPathSeparator = WIN_LINUX("\\", "/");
static constexpr std::string_view kDefaultCorePath = WIN_LINUX("addons\\swiftlys2", "addons/swiftlys2");
static constexpr std::string_view kDefaultLogPath = WIN_LINUX("addons\\swiftlys2\\logs", "addons/swiftlys2/logs");

static void EnsureTrailingSeparator(std::string& path)
{
    if (!path.ends_with(kPathSeparator))
        path += kPathSeparator;
}

[[nodiscard]] static std::string GameRelativePath(const std::string& relative)
{
    return std::format("{}/{}/{}", Plat_GetGameDirectory(), g_sGameFolder, relative);
}

bool SwiftlyCore::Load(BridgeKind_t kind, CreateIFaceFn serverFactory, CreateIFaceFn engineFactory)
{
    PROFILE_FUNCTION();

    g_pServerFactory = serverFactory;
    g_pEngineFactory = engineFactory;
    g_mainThreadId = std::this_thread::get_id();
    m_iKind = kind;

    SetupConsoleColors();

    m_sCorePath = CommandLine()->ParmValue(CUtlStringToken("-sw_path"), kDefaultCorePath.data());
    EnsureTrailingSeparator(m_sCorePath);

    m_sLogPath = CommandLine()->ParmValue(CUtlStringToken("-sw_logpath"), kDefaultLogPath.data());
    EnsureTrailingSeparator(m_sLogPath);
    m_sLogPath = replace(m_sLogPath, kDefaultCorePath.data(), m_sCorePath);

    g_pCrashReporter->Init();

    g_pGameFileSystem = GetInterfaceAs<IFileSystem>(FILESYSTEM_INTERFACE_VERSION);
    g_pGameEngine = GetInterfaceAs<IVEngineServer2>(INTERFACEVERSION_VENGINESERVER);
    g_pGameEventSystem = GetInterfaceAs<IGameEventSystem>(GAMEEVENTSYSTEM_INTERFACE_VERSION);
    g_pGameNetworkMessages = GetInterfaceAs<INetworkMessages>(NETWORKMESSAGES_INTERFACE_VERSION);
    g_pGameNetworkSystem = GetInterfaceAs<INetworkSystem>(NETWORKSYSTEM_INTERFACE_VERSION);
    g_pGameNetworkServerService = GetInterfaceAs<INetworkServerService>(NETWORKSERVERSERVICE_INTERFACE_VERSION);
    g_pGameCvar = GetInterfaceAs<ICvar>(CVAR_INTERFACE_VERSION);
    g_pGameSchemaSystem = GetInterfaceAs<CSchemaSystem>(SCHEMASYSTEM_INTERFACE_VERSION);
    g_pGameNetworkStringTableContainer = GetInterfaceAs<INetworkStringTableContainer>(SOURCE2ENGINETOSERVERSTRINGTABLE_INTERFACE_VERSION);
    g_pGameResources = GetInterfaceAs<CGameResourceService>(GAMERESOURCESERVICESERVER_INTERFACE_VERSION);

#ifdef _WIN32
    const std::wstring libServerPath = StringWide(GameRelativePath("bin/win64/server.dll"));
    const std::wstring libEnginePath = StringWide(std::format("{}\\bin\\win64\\engine2.dll", Plat_GetGameDirectory()));
    g_pS2BinLib->SetModuleBaseFromPointer("server", load_library(libServerPath.c_str()));
    g_pS2BinLib->SetModuleBaseFromPointer("engine2", load_library(libEnginePath.c_str()));
#endif

    SetGlobalCvarObject(g_pGameCvar);
    ConVar_Register(CVarFlags::RELEASE | CVarFlags::SERVER_CAN_EXECUTE | CVarFlags::CLIENT_CAN_EXECUTE | CVarFlags::GAMEDLL, nullptr, nullptr);

    if (const char* logLevel = CommandLine()->ParmValue(CUtlStringToken("-sw_loglevel")))
        putenv("SWIFTLY_LOG_LEVEL", logLevel, 1);

    if (const char* hideLogInConsole = CommandLine()->ParmValue(CUtlStringToken("-sw_hide_logs_in_console")))
        putenv("SWIFTLY_HIDE_LOG_IN_CONSOLE", hideLogInConsole, 1);

    if (GetCurrentGame() == "unknown")
    {
        if (g_pGameEngine)
            g_pLogger->Error("Entrypoint", std::format("Unknown game detected. App ID: {}", g_pGameEngine->GetAppID()));
        else
            g_pLogger->Error("Entrypoint", "Unknown game detected. No engine interface available.");

        return false;
    }

    g_pConfiguration->InitializeExamples();
    if (!g_pConfiguration->Load())
    {
        g_pLogger->Error("Entrypoint", "Couldn't load the core configuration.");
        return false;
    }

    bool managedEnabled = true;
    if (const bool* enabled = std::get_if<bool>(&g_pConfiguration->GetValue("core.Managed.Enable")))
        managedEnabled = *enabled;

    if (managedEnabled)
    {
        if (const int* level = std::get_if<int>(&g_pConfiguration->GetValue("core.DotnetCrashTracerLevel")); level && *level > 0)
            g_pCrashReporter->EnableDotnetCrashTracer(*level);
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
                g_pLogger->Info("Patching", std::format("Applied patch: {}\n", patch));
            }
            else
            {
                g_pLogger->Warning("Patching", std::format("Couldn't find patch: {}\n", patch));
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
!
    g_pHooksManager->Initialize();

    g_pConsoleOutput->Initialize();
    g_ConsoleLogger.Initialize();

    if (const bool* filter = std::get_if<bool>(&g_pConfiguration->GetValue("core.ConsoleFilter")); filter && *filter)
        g_pConsoleOutput->ToggleFilter();

    g_pConvarManager->Initialize();

    g_pGameEventManager->Initialize();

    if (!InitGameSystem())
    {
        g_pLogger->Error("Game System", "Couldn't initialize the Game System.\n");
        return false;
    }

    g_pPlayerManager->Initialize();

    StartFixes();

    const std::string coreRoot = GameRelativePath(m_sCorePath);
    g_pExtensionHost->LoadExtensions(coreRoot);

    if (!managedEnabled)
    {
        for (int warningIndex = 0; warningIndex < 3; ++warningIndex)
            g_pLogger->Warning("Managed", "Managed runtime is disabled by configuration.\n");
        return true;
    }

    if (!InitializeHostFXR(coreRoot))
    {
        g_pCrashReporter->ReportPreventionIncident("Managed", std::format("Couldn't initialize the .NET runtime. Make sure you installed `swiftlys2-{}-{}-with-runtimes.zip`.", WIN_LINUX("windows", "linux"), GetVersion()));
        return true;
    }

    bool managedLogEnabled = true;
    int managedLogInterval = 2000;
    if (const bool* enabled = std::get_if<bool>(&g_pConfiguration->GetValue("core.ConsoleLogger.ManagedEnable")))
        managedLogEnabled = *enabled;
    if (const int* interval = std::get_if<int>(&g_pConfiguration->GetValue("core.ConsoleLogger.WriteIntervalMs")))
        managedLogInterval = (*interval > 0) ? *interval : 2000;

    putenv("SWIFTLY_MANAGED_LOG_ENABLE", managedLogEnabled ? "1" : "0", 1);
    putenv("SWIFTLY_MANAGED_LOG_INTERVAL_MS", std::to_string(managedLogInterval).c_str(), 1);

    if (!InitializeDotNetAPI(g_pScriptingAPI->GetNativeFunctions(), g_pScriptingAPI->GetNativeFunctionsCount(), GameRelativePath(m_sLogPath)))
    {
        g_pCrashReporter->ReportPreventionIncident("Managed", "Couldn't initialize the .NET scripting API.");
        return true;
    }

    return true;
}

bool SwiftlyCore::Unload()
{
    PROFILE_FUNCTION();

    g_pExtensionHost->UnloadExtensions();
    g_pHooksManager->Shutdown();
    g_pPlayerManager->Shutdown();
    g_pEntSystem->Shutdown();
    g_pConvarManager->Shutdown();

    g_ConsoleLogger.Shutdown();
    g_pConsoleOutput->Shutdown();
    g_pCrashReporter->Shutdown();

    Profiler::Save(m_sLogPath + "profiler.txt");

    return true;
}

static void GameServerSteamAPIActivatedHook(void* _this)
{
    PROFILE_FUNCTION();

    if (g_pGameEngine->IsDedicatedServer())
        g_pPlayerManager->SteamAPIServerActivated();

    PROFILE_PAUSE();

    return reinterpret_cast<decltype(&GameServerSteamAPIActivatedHook)>(g_GameServerSteamAPIActivated.GetOriginal())(_this);
}

void* SwiftlyCore::GetInterface(const std::string& interface_name)
{
    PROFILE_FUNCTION();

    int returnCode = 0;

    if (void* iface = g_pServerFactory(interface_name.c_str(), &returnCode))
        return iface;

    return g_pEngineFactory(interface_name.c_str(), &returnCode);
}

void SwiftlyCore::SendConsoleMessage(const std::string& message)
{
    PROFILE_FUNCTION();

    Msg("%s", message.c_str());
}

std::string SwiftlyCore::GetCurrentGame()
{
    PROFILE_FUNCTION();

    return g_sGameFolder == "csgo" ? "cs2" : "unknown";
}

int SwiftlyCore::GetMaxGameClients()
{
    PROFILE_FUNCTION();

    if (m_iMaxClients == -1 && g_sGameFolder == "csgo")
        m_iMaxClients = 64;

    return m_iMaxClients;
}

std::string& SwiftlyCore::GetCorePath()
{
    PROFILE_FUNCTION();

    return m_sCorePath;
}

std::string SwiftlyCore::GetVersion()
{
    PROFILE_FUNCTION();

#ifndef SWIFTLY_VERSION
    return "Local";
#else
    return SWIFTLY_VERSION;
#endif
}
