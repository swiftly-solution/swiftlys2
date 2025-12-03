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
#include <cstring>
#include <string>

static std::string g_dumpPath;
static bool g_dumpWritten = false;

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
#include <DbgHelp.h>
#include <Windows.h>
#include <io.h>

static PVOID g_vehHandle = nullptr;

void BreakpadDumpCallback(PEXCEPTION_POINTERS exceptionInfo)
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
        const char* msg = "[CrashReporter] Failed to create dump file!\n";
        _write(_fileno(stdout), msg, strlen(msg));
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
        const char* msg = "[CrashReporter] Wrote minidump to: ";
        _write(_fileno(stdout), msg, strlen(msg));
        std::string path = StringTight(dumpFile);
        _write(_fileno(stdout), path.c_str(), path.size());
        _write(_fileno(stdout), "\n", 1);
    }
    else
    {
        const char* msg = "[CrashReporter] Failed to write minidump to: ";
        _write(_fileno(stdout), msg, strlen(msg));
        std::string path = StringTight(dumpFile);
        _write(_fileno(stdout), path.c_str(), path.size());
        _write(_fileno(stdout), "\n", 1);
    }
}

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
        BreakpadDumpCallback(exceptionInfo);
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
#include "client/linux/handler/exception_handler.h"
#include "common/linux/linux_libc_support.h"
#include "third_party/lss/linux_syscall_support.h"
#include <linux/limits.h>
#include <signal.h>
#include <sys/resource.h>
#include <unistd.h>

static char g_linuxDumpPath[PATH_MAX];
static google_breakpad::ExceptionHandler* g_exceptionHandler = nullptr;

static bool BreakpadDumpCallback(const google_breakpad::MinidumpDescriptor& descriptor, void* context, bool succeeded)
{
    if (succeeded)
    {
        sys_write(STDOUT_FILENO, "[CrashReporter] Wrote minidump to: ", 35);
    }
    else
    {
        sys_write(STDOUT_FILENO, "[CrashReporter] Failed to write minidump to: ", 45);
    }
    sys_write(STDOUT_FILENO, descriptor.path(), my_strlen(descriptor.path()));
    sys_write(STDOUT_FILENO, "\n", 1);
    return succeeded;
}

void RegisterCrashHandlers()
{
    strncpy(g_linuxDumpPath, g_dumpPath.c_str(), sizeof(g_linuxDumpPath) - 1);
    g_linuxDumpPath[sizeof(g_linuxDumpPath) - 1] = '\0';
    google_breakpad::MinidumpDescriptor descriptor(g_linuxDumpPath);
    g_exceptionHandler = new google_breakpad::ExceptionHandler(descriptor, nullptr, BreakpadDumpCallback, nullptr, true, -1);
}

void UnregisterCrashHandlers()
{
    if (g_exceptionHandler)
    {
        delete g_exceptionHandler;
        g_exceptionHandler = nullptr;
    }
}
#endif