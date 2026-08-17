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

#include "crashreporter.h"
#ifdef PAGE_SIZE
#undef PAGE_SIZE
#endif

#include <api/interfaces/manager.h>
#include <api/shared/files.h>
#include <api/shared/plat.h>
#include <api/shared/string.h>
#include <api/shared/texttable.h>

#include <core/managed/host/host.h>
#include <core/managed/host/strconv.h>

#include <core/entrypoint.h>

#include <fmt/format.h>


#include <nlohmann/json.hpp>

bool setEnvVar(const std::string& key, const std::string& value);
std::string g_dumpPath;
std::string g_relativeDumpPath;
bool g_dumpWritten = false;
google_breakpad::ExceptionHandler* exceptionHandler = nullptr;

void CrashReporter::Init()
{
    auto logger = g_ifaceService.FetchInterface<ILogger>(LOGGER_INTERFACE_VERSION);

    if (!Files::ExistsPath(g_SwiftlyCore.GetCorePath() + "dumps"))
    {
        if (!Files::CreateDir(g_SwiftlyCore.GetCorePath() + "dumps"))
        {
            logger->Error("Crash Listener", "Couldn't create dumps folder.\n");
            return;
        }
    }

    if (!Files::ExistsPath(g_SwiftlyCore.GetCorePath() + "dumps/crashreport"))
    {
        if (!Files::CreateDir(g_SwiftlyCore.GetCorePath() + "dumps/crashreport"))
        {
            logger->Error("Crash Listener", "Couldn't create dumps crashreport folder.\n");
            return;
        }
    }

    if (!Files::ExistsPath(g_SwiftlyCore.GetCorePath() + "dumps/prevention"))
    {
        if (!Files::CreateDir(g_SwiftlyCore.GetCorePath() + "dumps/prevention"))
        {
            logger->Error("Crash Listener", "Couldn't create dumps prevention folder.\n");
            return;
        }
    }

    for (const auto& dir : Files::FetchDirectories(g_SwiftlyCore.GetCorePath() + "dumps/crashreport"))
    {
        if (Files::FetchFileNames(dir).empty() && Files::FetchDirectories(dir).empty())
            Files::Delete(dir);
    }

    for (const auto& dir : Files::FetchDirectories(g_SwiftlyCore.GetCorePath() + "dumps/crashreport"))
    {
        auto fileNames = Files::FetchFileNames(dir);

        bool hasCrashInfo = false;
        std::string dmpRelPath;

        for (const auto& f : fileNames)
        {
            if (ends_with(f, "crashinfo.json"))
            {
                hasCrashInfo = true;
                break;
            }
        }

        if (hasCrashInfo)
            continue;

        for (const auto& f : fileNames)
        {
            if (ends_with(f, ".dmp"))
            {
                dmpRelPath = f;
                break;
            }
        }

        if (dmpRelPath.empty())
            continue;

        logger->Info("Crash Reporter", fmt::format("Recovering crashinfo.json for: {}\n", dir));
        ParseAndWriteCrashInfo(Files::GeneratePath(dmpRelPath), dir + "/crashinfo.json");
    }

    g_relativeDumpPath = g_SwiftlyCore.GetCorePath() + "dumps/crashreport/" + get_uuid();
    g_dumpPath = Files::GeneratePath(g_relativeDumpPath);

    if (!Files::CreateDir(g_relativeDumpPath))
    {
        logger->Error("Crash Listener", "Couldn't create dump directory.\n");
        return;
    }

#ifdef _WIN32
    InitCrashReporterWindows();
#else
    InitCrashReporterLinux();
#endif
}

void CrashReporter::Shutdown()
{
#ifdef _WIN32
    ShutdownCrashReporterWindows();
#endif

    delete exceptionHandler;

    if (!g_dumpWritten && !g_relativeDumpPath.empty())
        Files::Delete(g_relativeDumpPath);
}

void CrashReporter::EnableDotnetCrashTracer(int level)
{
    auto logger = g_ifaceService.FetchInterface<ILogger>(LOGGER_INTERFACE_VERSION);
    logger->Warning("Crash Reporter", fmt::format("Dotnet crash tracer level set to: {}\n", level));

    m_tracerLevel = level;
    if (level <= 0)
    {
        return;
    }

    setEnvVar("CORECLR_ENABLE_PROFILING", "1");
    setEnvVar("CORECLR_PROFILER", "{a2648b53-a560-486c-9e56-c3922a330182}");
    auto tracerPath = Files::GeneratePath(g_SwiftlyCore.GetCorePath() + WIN_LINUX("bin\\win64\\sw2tracer.dll", "bin/linuxsteamrt64/libsw2tracer.so"));
    setEnvVar("CORECLR_PROFILER_PATH", tracerPath);
}

int CrashReporter::GetDotnetCrashTracerLevel()
{
    return m_tracerLevel;
}

void CrashReporter::ReportPreventionIncident(std::string category, std::string reason)
{
    static auto logger = g_ifaceService.FetchInterface<ILogger>(LOGGER_INTERFACE_VERSION);

    logger->Warning("Crash Prevention", "A crash has been prevented by Swiftly Core and the details will be listed below:\n");

    TextTable backtraceTable('-', '|', '+');

    backtraceTable.add(" Category ");
    backtraceTable.add(" Message ");
    backtraceTable.endOfRow();

    backtraceTable.add(fmt::format(" {} ", category));
    backtraceTable.add(fmt::format(" {} ", reason));
    backtraceTable.endOfRow();

    PrintTextTable(LogType::WARNING, "Crash Prevention", backtraceTable);

    std::string file_path = fmt::format("{}dumps/prevention/incident.{}.log", g_SwiftlyCore.GetCorePath(), get_uuid());
    if (Files::ExistsPath(file_path))
    {
        Files::Delete(file_path);
    }

    Files::Append(file_path, fmt::format("================================\nCategory: {}\nDetails: {}", category, reason), false);
    logger->Warning("Crash Prevention", fmt::format("A log file has been created at: {}\n", file_path));
}

#ifndef _WIN32
void CrashReporterOnTickLinux();
#endif

void CrashReporter::OnTick()
{
#ifndef _WIN32
    CrashReporterOnTickLinux();
#endif
}