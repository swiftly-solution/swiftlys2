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
#include <nlohmann/json.hpp>

#include <cstdio>
#include <cstring>
#include <ctime>
#include <fstream>
#include <string>

#ifdef _WIN32
#include <DbgHelp.h>
#include <Windows.h>
#include <io.h>
#include <process.h>

#else
#include "client/linux/handler/exception_handler.h"
#include "common/linux/linux_libc_support.h"
#include "third_party/lss/linux_syscall_support.h"
#include <linux/limits.h>
#include <pthread.h>
#include <signal.h>
#include <sys/resource.h>
#include <sys/sysinfo.h>
#include <unistd.h>

#endif

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

inline void ReportCrashIncident(const std::string& basePath, void* exceptionInfo)
{
    try
    {
        nlohmann::json crashReport;

        // Capture basic crash metadata
        std::time_t timestamp = std::time(nullptr);
        char timeBuffer[100];
        std::strftime(timeBuffer, sizeof(timeBuffer), "%Y-%m-%d %H:%M:%S", std::localtime(&timestamp));
        crashReport["timestamp"] = timeBuffer;

#ifdef _WIN32
        crashReport["processId"] = GetCurrentProcessId();
        crashReport["threadId"] = GetCurrentThreadId();

        auto* pExceptionPointers = static_cast<PEXCEPTION_POINTERS>(exceptionInfo);
        if (pExceptionPointers && pExceptionPointers->ExceptionRecord)
        {
            auto* record = pExceptionPointers->ExceptionRecord;

            auto GetExceptionCodeString = [](DWORD code) -> std::string
            {
                switch (code)
                {
                case EXCEPTION_ACCESS_VIOLATION:
                    return "EXCEPTION_ACCESS_VIOLATION";
                case EXCEPTION_STACK_OVERFLOW:
                    return "EXCEPTION_STACK_OVERFLOW";
                case EXCEPTION_ILLEGAL_INSTRUCTION:
                    return "EXCEPTION_ILLEGAL_INSTRUCTION";
                case EXCEPTION_INT_DIVIDE_BY_ZERO:
                    return "EXCEPTION_INT_DIVIDE_BY_ZERO";
                case EXCEPTION_INT_OVERFLOW:
                    return "EXCEPTION_INT_OVERFLOW";
                case EXCEPTION_ARRAY_BOUNDS_EXCEEDED:
                    return "EXCEPTION_ARRAY_BOUNDS_EXCEEDED";
                case EXCEPTION_FLT_DENORMAL_OPERAND:
                    return "EXCEPTION_FLT_DENORMAL_OPERAND";
                case EXCEPTION_FLT_DIVIDE_BY_ZERO:
                    return "EXCEPTION_FLT_DIVIDE_BY_ZERO";
                case EXCEPTION_FLT_INEXACT_RESULT:
                    return "EXCEPTION_FLT_INEXACT_RESULT";
                case EXCEPTION_FLT_INVALID_OPERATION:
                    return "EXCEPTION_FLT_INVALID_OPERATION";
                case EXCEPTION_FLT_OVERFLOW:
                    return "EXCEPTION_FLT_OVERFLOW";
                case EXCEPTION_FLT_STACK_CHECK:
                    return "EXCEPTION_FLT_STACK_CHECK";
                case EXCEPTION_FLT_UNDERFLOW:
                    return "EXCEPTION_FLT_UNDERFLOW";
                case EXCEPTION_DATATYPE_MISALIGNMENT:
                    return "EXCEPTION_DATATYPE_MISALIGNMENT";
                case EXCEPTION_IN_PAGE_ERROR:
                    return "EXCEPTION_IN_PAGE_ERROR";
                case EXCEPTION_INVALID_DISPOSITION:
                    return "EXCEPTION_INVALID_DISPOSITION";
                case EXCEPTION_NONCONTINUABLE_EXCEPTION:
                    return "EXCEPTION_NONCONTINUABLE_EXCEPTION";
                case EXCEPTION_PRIV_INSTRUCTION:
                    return "EXCEPTION_PRIV_INSTRUCTION";
                case EXCEPTION_GUARD_PAGE:
                    return "EXCEPTION_GUARD_PAGE";
                case EXCEPTION_INVALID_HANDLE:
                    return "EXCEPTION_INVALID_HANDLE";
                case 0xC0000194:
                    return "EXCEPTION_POSSIBLE_DEADLOCK";
                default:
                    return fmt::format("UNKNOWN_EXCEPTION_0x{:08X}", code);
                }
            };

            crashReport["exception"]["code"] = fmt::format("0x{:08X}", record->ExceptionCode);
            crashReport["exception"]["codeName"] = GetExceptionCodeString(record->ExceptionCode);
            crashReport["exception"]["address"] = fmt::format("0x{:016X}", reinterpret_cast<uintptr_t>(record->ExceptionAddress));
            crashReport["exception"]["flags"] = fmt::format("0x{:08X}", record->ExceptionFlags);

            // For access violations, capture the memory address and operation type
            if (record->ExceptionCode == EXCEPTION_ACCESS_VIOLATION && record->NumberParameters >= 2)
            {
                const char* accessType = (record->ExceptionInformation[0] == 0) ? "READ" : (record->ExceptionInformation[0] == 1) ? "WRITE" : (record->ExceptionInformation[0] == 8) ? "DEP_VIOLATION" : "UNKNOWN";
                crashReport["exception"]["accessViolation"]["type"] = accessType;
                crashReport["exception"]["accessViolation"]["address"] = fmt::format("0x{:016X}", record->ExceptionInformation[1]);
            }

            if (record->ExceptionRecord)
            {
                crashReport["exception"]["hasNestedException"] = true;
            }
        }

        // Capture CPU register state (x64 only!!!)
        if (pExceptionPointers && pExceptionPointers->ContextRecord)
        {
            auto* context = pExceptionPointers->ContextRecord;
            crashReport["context"]["rip"] = fmt::format("0x{:016X}", context->Rip);
            crashReport["context"]["rsp"] = fmt::format("0x{:016X}", context->Rsp);
            crashReport["context"]["rbp"] = fmt::format("0x{:016X}", context->Rbp);
            crashReport["context"]["rax"] = fmt::format("0x{:016X}", context->Rax);
            crashReport["context"]["rbx"] = fmt::format("0x{:016X}", context->Rbx);
            crashReport["context"]["rcx"] = fmt::format("0x{:016X}", context->Rcx);
            crashReport["context"]["rdx"] = fmt::format("0x{:016X}", context->Rdx);
            crashReport["context"]["rsi"] = fmt::format("0x{:016X}", context->Rsi);
            crashReport["context"]["rdi"] = fmt::format("0x{:016X}", context->Rdi);
            crashReport["context"]["flags"] = fmt::format("0x{:08X}", context->EFlags);
        }

        SYSTEM_INFO sysInfo;
        GetSystemInfo(&sysInfo);
        crashReport["system"]["processorArchitecture"] = sysInfo.wProcessorArchitecture;
        crashReport["system"]["numberOfProcessors"] = sysInfo.dwNumberOfProcessors;
        crashReport["system"]["pageSize"] = sysInfo.dwPageSize;

        MEMORYSTATUSEX memStatus;
        memStatus.dwLength = sizeof(memStatus);
        if (GlobalMemoryStatusEx(&memStatus))
        {
            crashReport["memory"]["totalPhysical"] = memStatus.ullTotalPhys;
            crashReport["memory"]["availablePhysical"] = memStatus.ullAvailPhys;
            crashReport["memory"]["totalVirtual"] = memStatus.ullTotalVirtual;
            crashReport["memory"]["availableVirtual"] = memStatus.ullAvailVirtual;
            crashReport["memory"]["memoryLoad"] = memStatus.dwMemoryLoad;
        }
#else
        crashReport["processId"] = getpid();
        crashReport["threadId"] = static_cast<uint64_t>(pthread_self());

        // Linux: No exception details available from breakpad callback
        // But we maintain the same JSON structure for consistency
        crashReport["exception"]["code"] = "N/A";
        crashReport["exception"]["codeName"] = "Linux signal (details in minidump)";
        crashReport["exception"]["address"] = "N/A";
        crashReport["exception"]["flags"] = "N/A";

        // Linux: Context not available in callback, but maintain structure
        // These would need signal handler or ptrace to capture
        crashReport["context"]["rip"] = "N/A";
        crashReport["context"]["rsp"] = "N/A";
        crashReport["context"]["rbp"] = "N/A";
        crashReport["context"]["rax"] = "N/A";
        crashReport["context"]["rbx"] = "N/A";
        crashReport["context"]["rcx"] = "N/A";
        crashReport["context"]["rdx"] = "N/A";
        crashReport["context"]["rsi"] = "N/A";
        crashReport["context"]["rdi"] = "N/A";
        crashReport["context"]["flags"] = "N/A";

        crashReport["system"]["processorArchitecture"] = "x86_64";
        crashReport["system"]["numberOfProcessors"] = sysconf(_SC_NPROCESSORS_ONLN);
        crashReport["system"]["pageSize"] = sysconf(_SC_PAGESIZE);

        struct sysinfo si;
        if (sysinfo(&si) == 0)
        {
            crashReport["memory"]["totalPhysical"] = si.totalram * si.mem_unit;
            crashReport["memory"]["availablePhysical"] = si.freeram * si.mem_unit;
            crashReport["memory"]["totalVirtual"] = (si.totalram + si.totalswap) * si.mem_unit;
            crashReport["memory"]["availableVirtual"] = (si.freeram + si.freeswap) * si.mem_unit;
            crashReport["memory"]["memoryLoad"] = static_cast<uint32_t>((1.0 - static_cast<double>(si.freeram) / si.totalram) * 100);
        }
#endif

        std::string jsonPath = basePath + ".json";
        std::ofstream jsonFile(jsonPath);
        if (jsonFile.is_open())
        {
            jsonFile << crashReport.dump(4);
            jsonFile.close();

#ifdef _WIN32
            const char* msg = "[CrashReporter] Wrote crash report JSON to: ";
            _write(_fileno(stdout), msg, strlen(msg));
            _write(_fileno(stdout), jsonPath.c_str(), jsonPath.size());
            _write(_fileno(stdout), "\n", 1);
#else
            const char* msg = "[CrashReporter] Wrote crash report JSON to: ";
            write(STDOUT_FILENO, msg, strlen(msg));
            write(STDOUT_FILENO, jsonPath.c_str(), jsonPath.size());
            write(STDOUT_FILENO, "\n", 1);
#endif
        }
    }
    catch (...)
    {
    }
}

#ifdef _WIN32
static PVOID g_vehHandle = nullptr;

void BreakpadDumpCallback(PEXCEPTION_POINTERS exceptionInfo)
{
    if (g_dumpWritten)
    {
        return;
    }
    g_dumpWritten = true;

    std::string fileBaseName = g_dumpPath + "\\" + get_uuid();
    std::wstring dumpFile = StringWide(fileBaseName + ".dmp");

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

        ReportCrashIncident(fileBaseName, exceptionInfo);
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
        sys_write(STDOUT_FILENO, descriptor.path(), my_strlen(descriptor.path()));
        sys_write(STDOUT_FILENO, "\n", 1);

        std::string dumpPath = descriptor.path();
        std::string basePath = dumpPath.substr(0, dumpPath.rfind(".dmp"));
        ReportCrashIncident(basePath, nullptr);
    }
    else
    {
        sys_write(STDOUT_FILENO, "[CrashReporter] Failed to write minidump to: ", 45);
        sys_write(STDOUT_FILENO, descriptor.path(), my_strlen(descriptor.path()));
        sys_write(STDOUT_FILENO, "\n", 1);
    }
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