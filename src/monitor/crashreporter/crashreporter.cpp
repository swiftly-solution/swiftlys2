/************************************************************************************************
 *  SwiftlyS2 is a scripting framework for Source2-based games.
 *  Copyright (C) 2025 Swiftly Solution SRL via Sava Andrei-Sebastian and it's contributors
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

#include <api/interfaces/manager.h>
#include <api/shared/files.h>
#include <api/shared/plat.h>
#include <api/shared/string.h>
#include <api/shared/texttable.h>

#include <public/eiface.h>

#include <core/entrypoint.h>
#include <core/managed/host/strconv.h>

#include <fmt/format.h>

#include <cstdio>
#include <string>

#ifdef _WIN32
#include <DbgHelp.h>
#include <Windows.h>
static PVOID g_vehHandle = nullptr;
#else
#include <signal.h>
#include <sys/resource.h>
#include <unistd.h>
#endif

static std::string g_dumpPath;
static bool g_dumpWritten = false;

#ifdef _WIN32
void OnCrash(PEXCEPTION_POINTERS exceptionInfo)
{
    if (g_dumpWritten)
    {
        return;
    }
    g_dumpWritten = true;

    std::wstring dumpFile = StringWide(g_dumpPath + "\\" + get_uuid() + ".dmp");

    HANDLE hFile = CreateFileW(dumpFile.c_str(), GENERIC_WRITE, 0, nullptr, CREATE_ALWAYS, FILE_ATTRIBUTE_NORMAL, nullptr);
    if (hFile == INVALID_HANDLE_VALUE)
    {
        g_ifaceService.FetchInterface<ILogger>(LOGGER_INTERFACE_VERSION)->Error("CrashReporter", "Failed to create dump file!\n");
        return;
    }

    MINIDUMP_EXCEPTION_INFORMATION mei;
    mei.ThreadId = GetCurrentThreadId();
    mei.ExceptionPointers = exceptionInfo;
    mei.ClientPointers = FALSE;

    BOOL result = MiniDumpWriteDump(GetCurrentProcess(), GetCurrentProcessId(), hFile, static_cast<MINIDUMP_TYPE>(MiniDumpWithDataSegs | MiniDumpWithHandleData | MiniDumpWithThreadInfo | MiniDumpWithUnloadedModules), &mei, nullptr, nullptr);

    CloseHandle(hFile);

    if (result)
    {
        g_ifaceService.FetchInterface<ILogger>(LOGGER_INTERFACE_VERSION)->Info("CrashReporter", std::format("Crash dump written: {}\n", StringTight(dumpFile)));
    }
    else
    {
        g_ifaceService.FetchInterface<ILogger>(LOGGER_INTERFACE_VERSION)->Error("CrashReporter", "Failed to write crash dump!\n");
    }
}
#else
void OnCrash(int sig)
{
    if (g_dumpWritten)
    {
        return;
    }
    g_dumpWritten = true;

    const char* sigName = "Unknown";
    switch (sig)
    {
    case SIGSEGV:
        sigName = "SIGSEGV";
        break;
    case SIGABRT:
        sigName = "SIGABRT";
        break;
    case SIGFPE:
        sigName = "SIGFPE";
        break;
    case SIGILL:
        sigName = "SIGILL";
        break;
    case SIGBUS:
        sigName = "SIGBUS";
        break;
    case SIGSYS:
        sigName = "SIGSYS";
        break;
    case SIGXCPU:
        sigName = "SIGXCPU";
        break;
    case SIGXFSZ:
        sigName = "SIGXFSZ";
        break;
    case SIGIOT:
        sigName = "SIGIOT";
        break;
    case SIGPWR:
        sigName = "SIGPWR";
        break;
    case SIGSTKFLT:
        sigName = "SIGSTKFLT";
        break;
    }

    g_ifaceService.FetchInterface<ILogger>(LOGGER_INTERFACE_VERSION)->Info("CrashReporter", std::format("Caught signal {} ({}), core dump will be generated in: {}\n", sigName, sig, g_dumpPath));

    // Limit core dump size to 50MB
    struct rlimit rl;
    rl.rlim_cur = 20 * 1024 * 1024;
    rl.rlim_max = 20 * 1024 * 1024;
    setrlimit(RLIMIT_CORE, &rl);

    // Set coredump_filter to dump useful memory:
    // bit 0 (1): anonymous private (stack, heap)
    // bit 1 (2): anonymous shared
    // bit 2 (4): file-backed private (code segments)
    // bit 4 (16): ELF headers
    // Total: 0x17 = 23
    FILE* filterFile = fopen("/proc/self/coredump_filter", "w");
    if (filterFile)
    {
        fprintf(filterFile, "0x17");
        fclose(filterFile);
    }

    chdir(g_dumpPath.c_str());
}
#endif

void RegisterCrashHandlers();
void UnregisterCrashHandlers();

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

    if (!Files::ExistsPath(g_SwiftlyCore.GetCorePath() + "dumps/prevention"))
    {
        if (!Files::CreateDir(g_SwiftlyCore.GetCorePath() + "dumps/prevention"))
        {
            logger->Error("Crash Listener", "Couldn't create dumps prevention folder.\n");
            return;
        }
    }

    g_dumpPath = Files::GeneratePath(g_SwiftlyCore.GetCorePath() + "dumps");

    RegisterCrashHandlers();
}

void CrashReporter::Shutdown()
{
    UnregisterCrashHandlers();
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

#ifdef _WIN32
LONG CALLBACK VectoredExceptionHandler(PEXCEPTION_POINTERS exceptionInfo)
{
    switch (exceptionInfo->ExceptionRecord->ExceptionCode)
    {
    case EXCEPTION_ACCESS_VIOLATION:
    case EXCEPTION_STACK_OVERFLOW:
    case EXCEPTION_ILLEGAL_INSTRUCTION:
    case EXCEPTION_INT_DIVIDE_BY_ZERO:
    case EXCEPTION_INT_OVERFLOW:
    case EXCEPTION_ARRAY_BOUNDS_EXCEEDED:
    case EXCEPTION_FLT_DENORMAL_OPERAND:
    case EXCEPTION_FLT_DIVIDE_BY_ZERO:
    case EXCEPTION_FLT_INEXACT_RESULT:
    case EXCEPTION_FLT_INVALID_OPERATION:
    case EXCEPTION_FLT_OVERFLOW:
    case EXCEPTION_FLT_STACK_CHECK:
    case EXCEPTION_FLT_UNDERFLOW:
    case EXCEPTION_DATATYPE_MISALIGNMENT:
    case EXCEPTION_IN_PAGE_ERROR:
    case EXCEPTION_INVALID_DISPOSITION:
    case EXCEPTION_NONCONTINUABLE_EXCEPTION:
    case EXCEPTION_PRIV_INSTRUCTION:
    case EXCEPTION_GUARD_PAGE:
    case EXCEPTION_INVALID_HANDLE:
    case 0xC0000194:
        OnCrash(exceptionInfo);
        break;
    default:
        break;
    }
    return EXCEPTION_CONTINUE_SEARCH;
}

void RegisterCrashHandlers()
{
    g_vehHandle = AddVectoredExceptionHandler(1, VectoredExceptionHandler);
}

void UnregisterCrashHandlers()
{
    if (g_vehHandle)
    {
        RemoveVectoredExceptionHandler(g_vehHandle);
        g_vehHandle = nullptr;
    }
}
#else
void SignalHandler(int sig)
{
    OnCrash(sig);
    signal(sig, SIG_DFL);
    raise(sig);
}

void RegisterCrashHandlers()
{
    signal(SIGSEGV, SignalHandler);
    signal(SIGABRT, SignalHandler);
    signal(SIGFPE, SignalHandler);
    signal(SIGILL, SignalHandler);
    signal(SIGBUS, SignalHandler);
    signal(SIGSYS, SignalHandler);
    signal(SIGXCPU, SignalHandler);
    signal(SIGXFSZ, SignalHandler);
    signal(SIGIOT, SignalHandler);
    signal(SIGPWR, SignalHandler);
    signal(SIGSTKFLT, SignalHandler);
}

void UnregisterCrashHandlers()
{
    signal(SIGSEGV, SIG_DFL);
    signal(SIGABRT, SIG_DFL);
    signal(SIGFPE, SIG_DFL);
    signal(SIGILL, SIG_DFL);
    signal(SIGBUS, SIG_DFL);
    signal(SIGSYS, SIG_DFL);
    signal(SIGXCPU, SIG_DFL);
    signal(SIGXFSZ, SIG_DFL);
    signal(SIGIOT, SIG_DFL);
    signal(SIGPWR, SIG_DFL);
    signal(SIGSTKFLT, SIG_DFL);
}
#endif