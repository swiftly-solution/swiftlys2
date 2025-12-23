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

#include <api/interfaces/manager.h>
#include <api/shared/files.h>
#include <api/shared/plat.h>
#include <api/shared/string.h>
#include <api/shared/texttable.h>

#include <public/eiface.h>

#include <core/entrypoint.h>
#include <core/managed/host/host.h>
#include <core/managed/host/strconv.h>

#include <fmt/format.h>
#include <nlohmann/json.hpp>

#include <cstdio>
#include <cstdlib>
#include <cstring>
#include <ctime>
#include <filesystem>
#include <fstream>
#include <string>
#include <thread>

#include "tracer.h"

#ifdef _WIN32
#include <DbgHelp.h>
#include <Windows.h>
#include <io.h>
#include <process.h>
#else
#include "client/linux/handler/exception_handler.h"
#include "common/linux/linux_libc_support.h"
#include "third_party/lss/linux_syscall_support.h"
#include <cxxabi.h>
#include <dlfcn.h>
#include <execinfo.h>
#include <linux/limits.h>
#include <pthread.h>
#include <signal.h>
#include <sys/resource.h>
#include <sys/sysinfo.h>
#include <ucontext.h>
#include <unistd.h>
static siginfo_t* g_linuxSigInfo = nullptr;
static ucontext_t* g_linuxContext = nullptr;

static void* g_linuxBacktraceAddrs[128];
static volatile sig_atomic_t g_linuxBacktraceCount = 0;
static volatile sig_atomic_t g_linuxBacktraceCaptured = 0;
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

    if (!Files::ExistsPath(g_SwiftlyCore.GetCorePath() + std::string("dumps") + WIN_LINUX("\\", "/") + "crashreport"))
    {
        if (!Files::CreateDir(g_SwiftlyCore.GetCorePath() + std::string("dumps") + WIN_LINUX("\\", "/") + "crashreport"))
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

    g_dumpPath = Files::GeneratePath(g_SwiftlyCore.GetCorePath() + std::string("dumps") + WIN_LINUX("\\", "/") + "crashreport");

    RegisterCrashHandlers();
}

void CrashReporter::Shutdown()
{
    UnregisterCrashHandlers();
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
    printf("%s\n", tracerPath.c_str());
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

inline void ReportCrashIncident(const std::string& crashDir, void* exceptionInfo, uint64_t processIdOverride = 0, uint64_t threadIdOverride = 0)
{
    auto logger = g_ifaceService.FetchInterface<ILogger>(LOGGER_INTERFACE_VERSION);
    auto tracerLevel = g_ifaceService.FetchInterface<ICrashReporter>(CRASHREPORTER_INTERFACE_VERSION)->GetDotnetCrashTracerLevel();
    if (tracerLevel > 0)
    {
        std::string tracerPath = crashDir + WIN_LINUX("\\", "/") + "managedtrace.txt";
        logger->Warning("Crash Reporter", fmt::format("Dumping managed trace to: {}\n", tracerPath));
        TracerDump(g_SwiftlyCore.GetCorePath(), tracerPath.c_str());
    }

    try
    {
        nlohmann::json crashReport;
        std::time_t timestamp = std::time(nullptr);
        char timeBuffer[100];
        std::strftime(timeBuffer, sizeof(timeBuffer), "%Y-%m-%d %H:%M:%S", std::localtime(&timestamp));

        struct tm localtime;
        struct tm utctime;
#ifdef _WIN32
        localtime_s(&localtime, &timestamp);
        gmtime_s(&utctime, &timestamp);
#else
        localtime_r(&timestamp, &localtime);
        gmtime_r(&timestamp, &utctime);
#endif
        // Calculate offset in hours and minutes
        int offset_hours = localtime.tm_hour - utctime.tm_hour;
        int offset_mins = localtime.tm_min - utctime.tm_min;
        if (localtime.tm_mday != utctime.tm_mday)
        {
            if (localtime.tm_mday > utctime.tm_mday || localtime.tm_mon > utctime.tm_mon || localtime.tm_year > utctime.tm_year)
            {
                offset_hours += 24;
            }
            else
            {
                offset_hours -= 24;
            }
        }
        crashReport["timestamp"] = fmt::format("{} UTC{:+03d}:{:02d}", timeBuffer, offset_hours, abs(offset_mins));
        crashReport["timestampUTC"] = static_cast<uint64_t>(timestamp);

#ifdef _WIN32
        crashReport["processId"] = processIdOverride ? processIdOverride : static_cast<uint64_t>(GetCurrentProcessId());
        crashReport["threadId"] = threadIdOverride ? threadIdOverride : static_cast<uint64_t>(GetCurrentThreadId());

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

        // Capture CPU register state
        if (pExceptionPointers && pExceptionPointers->ContextRecord)
        {
            auto* context = pExceptionPointers->ContextRecord;

            // General Purpose Registers (64-bit)
            auto& gpr = crashReport["registers"]["general"];
            gpr["rax"] = fmt::format("0x{:016X}", context->Rax);
            gpr["rbx"] = fmt::format("0x{:016X}", context->Rbx);
            gpr["rcx"] = fmt::format("0x{:016X}", context->Rcx);
            gpr["rdx"] = fmt::format("0x{:016X}", context->Rdx);
            gpr["rsi"] = fmt::format("0x{:016X}", context->Rsi);
            gpr["rdi"] = fmt::format("0x{:016X}", context->Rdi);
            gpr["rbp"] = fmt::format("0x{:016X}", context->Rbp);
            gpr["rsp"] = fmt::format("0x{:016X}", context->Rsp);
            gpr["r8"] = fmt::format("0x{:016X}", context->R8);
            gpr["r9"] = fmt::format("0x{:016X}", context->R9);
            gpr["r10"] = fmt::format("0x{:016X}", context->R10);
            gpr["r11"] = fmt::format("0x{:016X}", context->R11);
            gpr["r12"] = fmt::format("0x{:016X}", context->R12);
            gpr["r13"] = fmt::format("0x{:016X}", context->R13);
            gpr["r14"] = fmt::format("0x{:016X}", context->R14);
            gpr["r15"] = fmt::format("0x{:016X}", context->R15);

            // Instruction Pointer
            crashReport["registers"]["rip"] = fmt::format("0x{:016X}", context->Rip);

            // Lower 32-bit portions
            auto& gpr32 = crashReport["registers"]["general32"];
            gpr32["eax"] = fmt::format("0x{:08X}", static_cast<uint32_t>(context->Rax));
            gpr32["ebx"] = fmt::format("0x{:08X}", static_cast<uint32_t>(context->Rbx));
            gpr32["ecx"] = fmt::format("0x{:08X}", static_cast<uint32_t>(context->Rcx));
            gpr32["edx"] = fmt::format("0x{:08X}", static_cast<uint32_t>(context->Rdx));
            gpr32["esi"] = fmt::format("0x{:08X}", static_cast<uint32_t>(context->Rsi));
            gpr32["edi"] = fmt::format("0x{:08X}", static_cast<uint32_t>(context->Rdi));
            gpr32["ebp"] = fmt::format("0x{:08X}", static_cast<uint32_t>(context->Rbp));
            gpr32["esp"] = fmt::format("0x{:08X}", static_cast<uint32_t>(context->Rsp));

            // Lower 16-bit and 8-bit portions for RAX-RDX
            auto& gprLow = crashReport["registers"]["legacy"];
            gprLow["ax"] = fmt::format("0x{:04X}", static_cast<uint16_t>(context->Rax));
            gprLow["bx"] = fmt::format("0x{:04X}", static_cast<uint16_t>(context->Rbx));
            gprLow["cx"] = fmt::format("0x{:04X}", static_cast<uint16_t>(context->Rcx));
            gprLow["dx"] = fmt::format("0x{:04X}", static_cast<uint16_t>(context->Rdx));
            gprLow["al"] = fmt::format("0x{:02X}", static_cast<uint8_t>(context->Rax));
            gprLow["bl"] = fmt::format("0x{:02X}", static_cast<uint8_t>(context->Rbx));
            gprLow["cl"] = fmt::format("0x{:02X}", static_cast<uint8_t>(context->Rcx));
            gprLow["dl"] = fmt::format("0x{:02X}", static_cast<uint8_t>(context->Rdx));
            gprLow["ah"] = fmt::format("0x{:02X}", static_cast<uint8_t>(context->Rax >> 8));
            gprLow["bh"] = fmt::format("0x{:02X}", static_cast<uint8_t>(context->Rbx >> 8));
            gprLow["ch"] = fmt::format("0x{:02X}", static_cast<uint8_t>(context->Rcx >> 8));
            gprLow["dh"] = fmt::format("0x{:02X}", static_cast<uint8_t>(context->Rdx >> 8));

            // Segment Registers
            auto& segments = crashReport["registers"]["segments"];
            segments["cs"] = fmt::format("0x{:04X}", context->SegCs);
            segments["ds"] = fmt::format("0x{:04X}", context->SegDs);
            segments["es"] = fmt::format("0x{:04X}", context->SegEs);
            segments["fs"] = fmt::format("0x{:04X}", context->SegFs);
            segments["gs"] = fmt::format("0x{:04X}", context->SegGs);
            segments["ss"] = fmt::format("0x{:04X}", context->SegSs);

            // Flags Register (RFLAGS/EFLAGS) with detailed breakdown
            crashReport["registers"]["rflags"]["raw"] = fmt::format("0x{:08X}", context->EFlags);
            auto& flags = crashReport["registers"]["rflags"]["bits"];
            flags["CF"] = (context->EFlags & 0x0001) ? 1 : 0;    // Carry Flag
            flags["PF"] = (context->EFlags & 0x0004) ? 1 : 0;    // Parity Flag
            flags["AF"] = (context->EFlags & 0x0010) ? 1 : 0;    // Auxiliary Carry Flag
            flags["ZF"] = (context->EFlags & 0x0040) ? 1 : 0;    // Zero Flag
            flags["SF"] = (context->EFlags & 0x0080) ? 1 : 0;    // Sign Flag
            flags["TF"] = (context->EFlags & 0x0100) ? 1 : 0;    // Trap Flag
            flags["IF"] = (context->EFlags & 0x0200) ? 1 : 0;    // Interrupt Enable Flag
            flags["DF"] = (context->EFlags & 0x0400) ? 1 : 0;    // Direction Flag
            flags["OF"] = (context->EFlags & 0x0800) ? 1 : 0;    // Overflow Flag
            flags["IOPL"] = (context->EFlags >> 12) & 0x3;       // I/O Privilege Level
            flags["NT"] = (context->EFlags & 0x4000) ? 1 : 0;    // Nested Task
            flags["RF"] = (context->EFlags & 0x10000) ? 1 : 0;   // Resume Flag
            flags["VM"] = (context->EFlags & 0x20000) ? 1 : 0;   // Virtual-8086 Mode
            flags["AC"] = (context->EFlags & 0x40000) ? 1 : 0;   // Alignment Check
            flags["VIF"] = (context->EFlags & 0x80000) ? 1 : 0;  // Virtual Interrupt Flag
            flags["VIP"] = (context->EFlags & 0x100000) ? 1 : 0; // Virtual Interrupt Pending
            flags["ID"] = (context->EFlags & 0x200000) ? 1 : 0;  // ID Flag

            // SSE/AVX State (XMM Registers)
            if (context->ContextFlags & CONTEXT_FLOATING_POINT)
            {
                auto& xmm = crashReport["registers"]["xmm"];
                xmm["mxcsr"] = fmt::format("0x{:08X}", context->MxCsr); // MXCSR control/status register

                for (int i = 0; i < 16; i++)
                {
                    auto& xmmReg = xmm[fmt::format("xmm{}", i)];
                    M128A* xmmData = &context->Xmm0 + i;
                    xmmReg["low"] = fmt::format("0x{:016X}", xmmData->Low);
                    xmmReg["high"] = fmt::format("0x{:016X}", xmmData->High);
                    xmmReg["full"] = fmt::format("{:016X}{:016X}", xmmData->High, xmmData->Low);
                }

                xmm["fpuControlWord"] = fmt::format("0x{:04X}", context->FltSave.ControlWord);
                xmm["fpuStatusWord"] = fmt::format("0x{:04X}", context->FltSave.StatusWord);
                xmm["fpuTagWord"] = fmt::format("0x{:02X}", context->FltSave.TagWord);
            }

            // Debug Registers (if available and ContextFlags includes CONTEXT_DEBUG_REGISTERS)
            if (context->ContextFlags & CONTEXT_DEBUG_REGISTERS)
            {
                auto& debug = crashReport["registers"]["debug"];
                debug["dr0"] = fmt::format("0x{:016X}", context->Dr0); // Breakpoint address 0
                debug["dr1"] = fmt::format("0x{:016X}", context->Dr1); // Breakpoint address 1
                debug["dr2"] = fmt::format("0x{:016X}", context->Dr2); // Breakpoint address 2
                debug["dr3"] = fmt::format("0x{:016X}", context->Dr3); // Breakpoint address 3
                debug["dr6"] = fmt::format("0x{:016X}", context->Dr6); // Debug status
                debug["dr7"] = fmt::format("0x{:016X}", context->Dr7); // Debug control
            }

            // Control Registers info
            crashReport["registers"]["control"]["contextFlags"] = fmt::format("0x{:08X}", context->ContextFlags);

            // Stack pointer analysis
            auto& stack = crashReport["registers"]["stackInfo"];
            stack["rsp"] = fmt::format("0x{:016X}", context->Rsp);
            stack["rbp"] = fmt::format("0x{:016X}", context->Rbp);
            if (context->Rbp > context->Rsp)
            {
                stack["frameSize"] = fmt::format("0x{:X}", context->Rbp - context->Rsp);
            }

            // Capture call stack
            auto& callStack = crashReport["callstack"];

            // Native call stack
            auto& nativeStack = callStack["native"];
            nativeStack["captureMethod"] = "StackWalk64";

            HANDLE process = GetCurrentProcess();
            HANDLE thread = GetCurrentThread();

            // Initialize symbol handler for module info
            // Required for SymGetModuleBase64 to work properly
            SymSetOptions(SYMOPT_UNDNAME | SYMOPT_DEFERRED_LOADS);
            if (!SymInitialize(process, nullptr, TRUE))
            {
                nativeStack["symbolInitWarning"] = "Failed to initialize symbols, module info may be incomplete";
            }

            // Initialize stack walking
            STACKFRAME64 stackFrame = {};
            stackFrame.AddrPC.Offset = context->Rip;
            stackFrame.AddrPC.Mode = AddrModeFlat;
            stackFrame.AddrFrame.Offset = context->Rbp;
            stackFrame.AddrFrame.Mode = AddrModeFlat;
            stackFrame.AddrStack.Offset = context->Rsp;
            stackFrame.AddrStack.Mode = AddrModeFlat;

            // Capture up to 64 frames
            const int maxFrames = 64;
            int frameCount = 0;
            auto& frames = nativeStack["frames"];

            // Walk the stack
            while (frameCount < maxFrames)
            {
                if (!StackWalk64(IMAGE_FILE_MACHINE_AMD64, process, thread, &stackFrame, context, nullptr, SymFunctionTableAccess64, SymGetModuleBase64, nullptr))
                {
                    // Stack walk failed, stop here
                    break;
                }

                if (stackFrame.AddrPC.Offset == 0)
                {
                    // Check if we have a valid frame
                    break;
                }

                auto& frame = frames[frameCount];
                frame["index"] = frameCount;
                frame["pc"] = fmt::format("0x{:016X}", stackFrame.AddrPC.Offset);
                frame["sp"] = fmt::format("0x{:016X}", stackFrame.AddrStack.Offset);
                frame["fp"] = fmt::format("0x{:016X}", stackFrame.AddrFrame.Offset);

                // Get module info for this address
                DWORD64 moduleBase = SymGetModuleBase64(process, stackFrame.AddrPC.Offset);
                if (moduleBase != 0)
                {
                    char moduleName[MAX_PATH];
                    if (GetModuleFileNameA((HMODULE)moduleBase, moduleName, MAX_PATH))
                    {
                        // Extract just the filename from the full path
                        const char* fileName = strrchr(moduleName, '\\');
                        frame["module"] = fileName ? (fileName + 1) : moduleName;
                        frame["moduleBase"] = fmt::format("0x{:016X}", moduleBase);
                        frame["offsetInModule"] = fmt::format("0x{:X}", stackFrame.AddrPC.Offset - moduleBase);
                    }
                }

                // Skip symbol resolution to avoid allocating memory in a potentially corrupted heap
                // Recorded addresses are sufficient for post-mortem analysis with external debuggers

                frameCount++;
            }

            nativeStack["frameCount"] = frameCount;

            auto& stackMemory = crashReport["stackMemory"];
            try
            {
                // Try to read 256 bytes of stack (before and after RSP)
                const size_t dumpSize = 256;
                const size_t beforeSize = 64;
                uint64_t stackStart = context->Rsp >= beforeSize ? context->Rsp - beforeSize : 0;

                stackMemory["dumpStart"] = fmt::format("0x{:016X}", stackStart);
                stackMemory["dumpSize"] = dumpSize;

                // Read stack memory carefully (may fail if stack is corrupted)
                auto& stackData = stackMemory["data"];
                uint8_t* stackPtr = reinterpret_cast<uint8_t*>(stackStart);
                for (size_t i = 0; i < dumpSize && i < 32; i += 8) // Limit to first 32 quadwords
                {
                    if (IsBadReadPtr(stackPtr + i, 8))
                    {
                        stackData[fmt::format("0x{:016X}", stackStart + i)] = "UNREADABLE";
                    }
                    else
                    {
                        uint64_t value = *reinterpret_cast<uint64_t*>(stackPtr + i);
                        stackData[fmt::format("0x{:016X}", stackStart + i)] = fmt::format("0x{:016X}", value);

                        // Mark if this is near RSP or RBP
                        if (stackStart + i == context->Rsp)
                        {
                            stackData[fmt::format("0x{:016X}Note", stackStart + i)] = "RSP";
                        }
                        else if (stackStart + i == context->Rbp)
                        {
                            stackData[fmt::format("0x{:016X}Note", stackStart + i)] = "RBP";
                        }
                    }
                }
            }
            catch (...)
            {
                stackMemory["error"] = "Failed to read stack memory";
            }
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

        // Helper to get signal name
        auto GetSignalName = [](int sig) -> std::string
            {
                switch (sig)
                {
                case SIGSEGV:
                    return "SIGSEGV";
                case SIGBUS:
                    return "SIGBUS";
                case SIGFPE:
                    return "SIGFPE";
                case SIGILL:
                    return "SIGILL";
                case SIGABRT:
                    return "SIGABRT";
                case SIGTRAP:
                    return "SIGTRAP";
                case SIGSYS:
                    return "SIGSYS";
                default:
                    return fmt::format("Signal {}", sig);
                }
            };

        // Exception details from saved signal info
        if (g_linuxSigInfo)
        {
            crashReport["exception"]["code"] = fmt::format("0x{:08X}", g_linuxSigInfo->si_signo);
            crashReport["exception"]["codeName"] = GetSignalName(g_linuxSigInfo->si_signo);
            crashReport["exception"]["address"] = fmt::format("0x{:016X}", reinterpret_cast<uintptr_t>(g_linuxSigInfo->si_addr));
            crashReport["exception"]["errno"] = g_linuxSigInfo->si_errno;
            crashReport["exception"]["siCode"] = g_linuxSigInfo->si_code;

            // For SIGSEGV, provide access violation details
            if (g_linuxSigInfo->si_signo == SIGSEGV)
            {
                const char* accessType = "UNKNOWN";
                switch (g_linuxSigInfo->si_code)
                {
                case SEGV_MAPERR:
                    accessType = "SEGV_MAPERR";
                    break;
                case SEGV_ACCERR:
                    accessType = "SEGV_ACCERR";
                    break;
                }
                crashReport["exception"]["accessViolation"]["type"] = accessType;
                crashReport["exception"]["accessViolation"]["address"] = fmt::format("0x{:016X}", reinterpret_cast<uintptr_t>(g_linuxSigInfo->si_addr));
            }
        }
        else
        {
            crashReport["exception"]["code"] = "N/A";
            crashReport["exception"]["codeName"] = "Linux signal (context not captured)";
            crashReport["exception"]["address"] = "N/A";
        }

        // Capture CPU register state from ucontext
        if (g_linuxContext)
        {
            mcontext_t* mctx = &g_linuxContext->uc_mcontext;
            greg_t* gregs = mctx->gregs;

            // General Purpose Registers (64-bit)
            auto& gpr = crashReport["registers"]["general"];
            gpr["rax"] = fmt::format("0x{:016X}", static_cast<uint64_t>(gregs[REG_RAX]));
            gpr["rbx"] = fmt::format("0x{:016X}", static_cast<uint64_t>(gregs[REG_RBX]));
            gpr["rcx"] = fmt::format("0x{:016X}", static_cast<uint64_t>(gregs[REG_RCX]));
            gpr["rdx"] = fmt::format("0x{:016X}", static_cast<uint64_t>(gregs[REG_RDX]));
            gpr["rsi"] = fmt::format("0x{:016X}", static_cast<uint64_t>(gregs[REG_RSI]));
            gpr["rdi"] = fmt::format("0x{:016X}", static_cast<uint64_t>(gregs[REG_RDI]));
            gpr["rbp"] = fmt::format("0x{:016X}", static_cast<uint64_t>(gregs[REG_RBP]));
            gpr["rsp"] = fmt::format("0x{:016X}", static_cast<uint64_t>(gregs[REG_RSP]));
            gpr["r8"] = fmt::format("0x{:016X}", static_cast<uint64_t>(gregs[REG_R8]));
            gpr["r9"] = fmt::format("0x{:016X}", static_cast<uint64_t>(gregs[REG_R9]));
            gpr["r10"] = fmt::format("0x{:016X}", static_cast<uint64_t>(gregs[REG_R10]));
            gpr["r11"] = fmt::format("0x{:016X}", static_cast<uint64_t>(gregs[REG_R11]));
            gpr["r12"] = fmt::format("0x{:016X}", static_cast<uint64_t>(gregs[REG_R12]));
            gpr["r13"] = fmt::format("0x{:016X}", static_cast<uint64_t>(gregs[REG_R13]));
            gpr["r14"] = fmt::format("0x{:016X}", static_cast<uint64_t>(gregs[REG_R14]));
            gpr["r15"] = fmt::format("0x{:016X}", static_cast<uint64_t>(gregs[REG_R15]));

            // Instruction Pointer
            crashReport["registers"]["rip"] = fmt::format("0x{:016X}", static_cast<uint64_t>(gregs[REG_RIP]));

            // Lower 32-bit portions (derived from 64-bit registers)
            uint64_t rax = static_cast<uint64_t>(gregs[REG_RAX]);
            uint64_t rbx = static_cast<uint64_t>(gregs[REG_RBX]);
            uint64_t rcx = static_cast<uint64_t>(gregs[REG_RCX]);
            uint64_t rdx = static_cast<uint64_t>(gregs[REG_RDX]);
            uint64_t rsi = static_cast<uint64_t>(gregs[REG_RSI]);
            uint64_t rdi = static_cast<uint64_t>(gregs[REG_RDI]);
            uint64_t rbp = static_cast<uint64_t>(gregs[REG_RBP]);
            uint64_t rsp = static_cast<uint64_t>(gregs[REG_RSP]);

            auto& gpr32 = crashReport["registers"]["general32"];
            gpr32["eax"] = fmt::format("0x{:08X}", static_cast<uint32_t>(rax & 0xFFFFFFFF));
            gpr32["ebx"] = fmt::format("0x{:08X}", static_cast<uint32_t>(rbx & 0xFFFFFFFF));
            gpr32["ecx"] = fmt::format("0x{:08X}", static_cast<uint32_t>(rcx & 0xFFFFFFFF));
            gpr32["edx"] = fmt::format("0x{:08X}", static_cast<uint32_t>(rdx & 0xFFFFFFFF));
            gpr32["esi"] = fmt::format("0x{:08X}", static_cast<uint32_t>(rsi & 0xFFFFFFFF));
            gpr32["edi"] = fmt::format("0x{:08X}", static_cast<uint32_t>(rdi & 0xFFFFFFFF));
            gpr32["ebp"] = fmt::format("0x{:08X}", static_cast<uint32_t>(rbp & 0xFFFFFFFF));
            gpr32["esp"] = fmt::format("0x{:08X}", static_cast<uint32_t>(rsp & 0xFFFFFFFF));

            // Legacy 16-bit and 8-bit views
            auto& gprLow = crashReport["registers"]["legacy"];
            gprLow["ax"] = fmt::format("0x{:04X}", static_cast<uint16_t>(rax & 0xFFFF));
            gprLow["bx"] = fmt::format("0x{:04X}", static_cast<uint16_t>(rbx & 0xFFFF));
            gprLow["cx"] = fmt::format("0x{:04X}", static_cast<uint16_t>(rcx & 0xFFFF));
            gprLow["dx"] = fmt::format("0x{:04X}", static_cast<uint16_t>(rdx & 0xFFFF));
            gprLow["al"] = fmt::format("0x{:02X}", static_cast<uint8_t>(rax & 0xFF));
            gprLow["bl"] = fmt::format("0x{:02X}", static_cast<uint8_t>(rbx & 0xFF));
            gprLow["cl"] = fmt::format("0x{:02X}", static_cast<uint8_t>(rcx & 0xFF));
            gprLow["dl"] = fmt::format("0x{:02X}", static_cast<uint8_t>(rdx & 0xFF));
            gprLow["ah"] = fmt::format("0x{:02X}", static_cast<uint8_t>((rax >> 8) & 0xFF));
            gprLow["bh"] = fmt::format("0x{:02X}", static_cast<uint8_t>((rbx >> 8) & 0xFF));
            gprLow["ch"] = fmt::format("0x{:02X}", static_cast<uint8_t>((rcx >> 8) & 0xFF));
            gprLow["dh"] = fmt::format("0x{:02X}", static_cast<uint8_t>((rdx >> 8) & 0xFF));

            // Segment registers
            auto& segments = crashReport["registers"]["segments"];
            segments["cs"] = fmt::format("0x{:04X}", static_cast<uint16_t>(gregs[REG_CSGSFS] & 0xFFFF));
            segments["gs"] = fmt::format("0x{:04X}", static_cast<uint16_t>((gregs[REG_CSGSFS] >> 16) & 0xFFFF));
            segments["fs"] = fmt::format("0x{:04X}", static_cast<uint16_t>((gregs[REG_CSGSFS] >> 32) & 0xFFFF));
            segments["ds"] = "N/A"; // Not directly available in ucontext
            segments["es"] = "N/A";
            segments["ss"] = "N/A";

            // Flags register
            uint64_t rflags = static_cast<uint64_t>(gregs[REG_EFL]);
            crashReport["registers"]["rflags"]["raw"] = fmt::format("0x{:016X}", rflags);
            auto& flags = crashReport["registers"]["rflags"]["bits"];
            flags["CF"] = (rflags & 0x0001) ? 1 : 0;
            flags["PF"] = (rflags & 0x0004) ? 1 : 0;
            flags["AF"] = (rflags & 0x0010) ? 1 : 0;
            flags["ZF"] = (rflags & 0x0040) ? 1 : 0;
            flags["SF"] = (rflags & 0x0080) ? 1 : 0;
            flags["TF"] = (rflags & 0x0100) ? 1 : 0;
            flags["IF"] = (rflags & 0x0200) ? 1 : 0;
            flags["DF"] = (rflags & 0x0400) ? 1 : 0;
            flags["OF"] = (rflags & 0x0800) ? 1 : 0;
            flags["IOPL"] = static_cast<int>((rflags >> 12) & 0x3);
            flags["NT"] = (rflags & 0x4000) ? 1 : 0;
            flags["RF"] = (rflags & 0x10000) ? 1 : 0;
            flags["VM"] = (rflags & 0x20000) ? 1 : 0;
            flags["AC"] = (rflags & 0x40000) ? 1 : 0;
            flags["VIF"] = (rflags & 0x80000) ? 1 : 0;
            flags["VIP"] = (rflags & 0x100000) ? 1 : 0;
            flags["ID"] = (rflags & 0x200000) ? 1 : 0;

            // FPU/XMM registers from fpregs
            auto& xmm = crashReport["registers"]["xmm"];
            if (mctx->fpregs)
            {
                auto* fpregs = mctx->fpregs;
                xmm["mxcsr"] = fmt::format("0x{:08X}", fpregs->mxcsr);
                xmm["fpuControlWord"] = fmt::format("0x{:04X}", fpregs->cwd);
                xmm["fpuStatusWord"] = fmt::format("0x{:04X}", fpregs->swd);
                xmm["fpuTagWord"] = fmt::format("0x{:04X}", fpregs->ftw);

                for (int i = 0; i < 16; i++)
                {
                    auto& xmmReg = xmm[fmt::format("xmm{}", i)];
                    // _libc_fpxreg uses 'element' array for XMM data
                    const auto& xmmData = fpregs->_xmm[i];
                    uint32_t* data = const_cast<uint32_t*>(xmmData.element);
                    uint64_t low = static_cast<uint64_t>(data[0]) | (static_cast<uint64_t>(data[1]) << 32);
                    uint64_t high = static_cast<uint64_t>(data[2]) | (static_cast<uint64_t>(data[3]) << 32);
                    xmmReg["low"] = fmt::format("0x{:016X}", low);
                    xmmReg["high"] = fmt::format("0x{:016X}", high);
                    xmmReg["full"] = fmt::format("0x{:016X}{:016X}", high, low);
                }
            }
            else
            {
                xmm["mxcsr"] = "N/A";
                xmm["fpuControlWord"] = "N/A";
                xmm["fpuStatusWord"] = "N/A";
                xmm["fpuTagWord"] = "N/A";
                for (int i = 0; i < 16; i++)
                {
                    auto& xmmReg = xmm[fmt::format("xmm{}", i)];
                    xmmReg["low"] = "N/A";
                    xmmReg["high"] = "N/A";
                    xmmReg["full"] = "N/A";
                }
            }

            // Debug registers (not available from ucontext)
            auto& debug = crashReport["registers"]["debug"];
            debug["dr0"] = "N/A";
            debug["dr1"] = "N/A";
            debug["dr2"] = "N/A";
            debug["dr3"] = "N/A";
            debug["dr6"] = "N/A";
            debug["dr7"] = "N/A";

            // Control registers (not available from userspace)
            crashReport["registers"]["control"]["contextFlags"] = "Linux ucontext";

            // Stack pointer info
            auto& stackInfo = crashReport["registers"]["stackInfo"];
            stackInfo["rsp"] = fmt::format("0x{:016X}", static_cast<uint64_t>(gregs[REG_RSP]));
            stackInfo["rbp"] = fmt::format("0x{:016X}", static_cast<uint64_t>(gregs[REG_RBP]));
            uint64_t frameSize = static_cast<uint64_t>(gregs[REG_RBP]) - static_cast<uint64_t>(gregs[REG_RSP]);
            stackInfo["frameSize"] = fmt::format("0x{:X} ({} bytes)", frameSize, frameSize);

            // Call stack using backtrace
            auto& callStack = crashReport["callstack"];
            auto& nativeStack = callStack["native"];
            void* bufferLocal[128];
            void** buffer = bufferLocal;
            int nptrs = 0;

            if (g_linuxBacktraceCaptured && g_linuxBacktraceCount > 0)
            {
                nativeStack["captureMethod"] = "backtrace (captured in signal handler) + dladdr";
                buffer = g_linuxBacktraceAddrs;
                nptrs = static_cast<int>(g_linuxBacktraceCount);
            }
            else
            {
                nativeStack["captureMethod"] = "backtrace (fallback) + dladdr";
                nptrs = backtrace(bufferLocal, 128);
            }

            nativeStack["frameCount"] = nptrs;

            auto& frames = nativeStack["frames"];
            frames = nlohmann::json::array();

            for (int i = 0; i < nptrs; i++)
            {
                nlohmann::json frame;
                frame["index"] = i;
                frame["address"] = fmt::format("0x{:016X}", reinterpret_cast<uintptr_t>(buffer[i]));

                Dl_info dlInfo;
                if (dladdr(buffer[i], &dlInfo))
                {
                    frame["module"] = dlInfo.dli_fname ? dlInfo.dli_fname : "UNKNOWN";
                    frame["baseAddress"] = fmt::format("0x{:016X}", reinterpret_cast<uintptr_t>(dlInfo.dli_fbase));

                    if (dlInfo.dli_sname)
                    {
                        frame["symbol"] = dlInfo.dli_sname;
                        frame["symbolAddress"] = fmt::format("0x{:016X}", reinterpret_cast<uintptr_t>(dlInfo.dli_saddr));
                        ptrdiff_t offset = reinterpret_cast<char*>(buffer[i]) - reinterpret_cast<char*>(dlInfo.dli_saddr);
                        frame["offset"] = fmt::format("+0x{:X}", offset);
                    }
                    else
                    {
                        frame["symbol"] = "UNKNOWN";
                        ptrdiff_t offset = reinterpret_cast<char*>(buffer[i]) - reinterpret_cast<char*>(dlInfo.dli_fbase);
                        frame["offset"] = fmt::format("+0x{:X}", offset);
                    }
                }
                else
                {
                    frame["module"] = "UNKNOWN";
                    frame["symbol"] = "UNKNOWN";
                }

                frames.push_back(frame);
            }

            // Stack memory dump
            auto& stackMemory = crashReport["stackMemory"];
            uint64_t rspVal = static_cast<uint64_t>(gregs[REG_RSP]);
            const size_t dumpSize = 256;
            const size_t beforeSize = 64;
            uint64_t stackStart = rspVal >= beforeSize ? rspVal - beforeSize : 0;

            stackMemory["dumpStart"] = fmt::format("0x{:016X}", stackStart);
            stackMemory["dumpSize"] = dumpSize;

            auto& stackData = stackMemory["data"];
            uint8_t* stackPtr = reinterpret_cast<uint8_t*>(stackStart);

            // Carefully read stack memory (may segfault if stack is corrupted)
            for (size_t i = 0; i < dumpSize && i < 256; i += 8)
            {
                // Use mincore or similar to check if memory is readable would be ideal,
                // but for simplicity we'll just try to read
                try
                {
                    uint64_t value = *reinterpret_cast<uint64_t*>(stackPtr + i);
                    stackData[fmt::format("0x{:016X}", stackStart + i)] = fmt::format("0x{:016X}", value);

                    if (stackStart + i == rspVal)
                    {
                        stackData[fmt::format("0x{:016X}Note", stackStart + i)] = "RSP";
                    }
                    else if (stackStart + i == static_cast<uint64_t>(gregs[REG_RBP]))
                    {
                        stackData[fmt::format("0x{:016X}Note", stackStart + i)] = "RBP";
                    }
                }
                catch (...)
                {
                    stackData[fmt::format("0x{:016X}", stackStart + i)] = "UNREADABLE";
                }
            }
        }
        else
        {
            // No context available
            auto& gpr = crashReport["registers"]["general"];
            gpr["rax"] = "N/A";
            gpr["rbx"] = "N/A";
            gpr["rcx"] = "N/A";
            gpr["rdx"] = "N/A";
            gpr["rsi"] = "N/A";
            gpr["rdi"] = "N/A";
            gpr["rbp"] = "N/A";
            gpr["rsp"] = "N/A";
            gpr["r8"] = "N/A";
            gpr["r9"] = "N/A";
            gpr["r10"] = "N/A";
            gpr["r11"] = "N/A";
            gpr["r12"] = "N/A";
            gpr["r13"] = "N/A";
            gpr["r14"] = "N/A";
            gpr["r15"] = "N/A";
            crashReport["registers"]["rip"] = "N/A";

            auto& callStack = crashReport["callstack"];
            auto& nativeStack = callStack["native"];
            void* bufferLocal[128];
            void** buffer = bufferLocal;
            int nptrs = 0;

            if (g_linuxBacktraceCaptured && g_linuxBacktraceCount > 0)
            {
                nativeStack["captureMethod"] = "backtrace (captured in signal handler, no ucontext) + dladdr";
                buffer = g_linuxBacktraceAddrs;
                nptrs = static_cast<int>(g_linuxBacktraceCount);
            }
            else
            {
                nativeStack["captureMethod"] = "backtrace (no context) + dladdr";
                nptrs = backtrace(bufferLocal, 128);
            }
            nativeStack["frameCount"] = nptrs;

            auto& frames = nativeStack["frames"];
            frames = nlohmann::json::array();
            for (int i = 0; i < nptrs; i++)
            {
                nlohmann::json frame;
                frame["index"] = i;
                frame["address"] = fmt::format("0x{:016X}", reinterpret_cast<uintptr_t>(buffer[i]));

                Dl_info dlInfo;
                if (dladdr(buffer[i], &dlInfo))
                {
                    frame["module"] = dlInfo.dli_fname ? dlInfo.dli_fname : "UNKNOWN";
                    frame["symbol"] = dlInfo.dli_sname ? dlInfo.dli_sname : "UNKNOWN";
                }
                else
                {
                    frame["module"] = "UNKNOWN";
                    frame["symbol"] = "UNKNOWN";
                }
                frames.push_back(frame);
            }

            auto& stackMemory = crashReport["stackMemory"];
            stackMemory["dumpStart"] = "N/A";
            stackMemory["dumpSize"] = 0;
            stackMemory["data"] = nlohmann::json::object();
        }

        // sysconf/sysinfo are NOT signal-safe, skip them entirely
        crashReport["system"]["processorArchitecture"] = "x86_64";
        crashReport["system"]["numberOfProcessors"] = "N/A";
        crashReport["system"]["pageSize"] = "N/A";

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

        std::string jsonPath = crashDir + WIN_LINUX("\\", "/") + "crashinfo.json";
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
    catch (const std::exception& e)
    {
#ifdef _WIN32
        const char* msg = "[CrashReporter] Exception while generating crash report: ";
        _write(_fileno(stdout), msg, strlen(msg));
        _write(_fileno(stdout), e.what(), strlen(e.what()));
        _write(_fileno(stdout), "\n", 1);
#else
        const char* msg = "[CrashReporter] Exception while generating crash report: ";
        write(STDOUT_FILENO, msg, strlen(msg));
        write(STDOUT_FILENO, e.what(), strlen(e.what()));
        write(STDOUT_FILENO, "\n", 1);
#endif
    }
    catch (...)
    {
#ifdef _WIN32
        const char* msg = "[CrashReporter] Unknown exception while generating crash report!\n";
        _write(_fileno(stdout), msg, strlen(msg));
#else
        const char* msg = "[CrashReporter] Unknown exception while generating crash report!\n";
        write(STDOUT_FILENO, msg, strlen(msg));
#endif
    }
}

#ifdef _WIN32
static PVOID g_vehHandle = nullptr;

static void BreakpadDumpCallback(PEXCEPTION_POINTERS exceptionInfo)
{
    if (g_dumpWritten)
    {
        return;
    }
    g_dumpWritten = true;

    uint64_t pid = static_cast<uint64_t>(GetCurrentProcessId());
    uint64_t tid = static_cast<uint64_t>(GetCurrentThreadId());

    std::string uuid = get_uuid();
    std::string crashDir = g_dumpPath + "\\" + uuid;
    std::error_code ec;
    std::filesystem::create_directories(crashDir, ec);

    std::wstring dumpFile = StringWide(crashDir + "\\minidump.dmp");
    HANDLE hFile = CreateFileW(dumpFile.c_str(), GENERIC_WRITE, 0, nullptr, CREATE_ALWAYS, FILE_ATTRIBUTE_NORMAL, nullptr);
    if (hFile == INVALID_HANDLE_VALUE)
    {
        const char* msg = "[CrashReporter] Failed to create dump file!\n";
        _write(_fileno(stdout), msg, strlen(msg));
        _exit(1);
    }

    MINIDUMP_EXCEPTION_INFORMATION mei;
    mei.ThreadId = static_cast<DWORD>(tid);
    mei.ExceptionPointers = exceptionInfo;
    mei.ClientPointers = FALSE;

    MINIDUMP_TYPE type = static_cast<MINIDUMP_TYPE>(
        MiniDumpWithDataSegs
        | MiniDumpNormal
        | MiniDumpWithHandleData
        | MiniDumpWithThreadInfo
        | MiniDumpWithUnloadedModules
        | MiniDumpWithFullMemoryInfo
        );
    BOOL result = MiniDumpWriteDump(GetCurrentProcess(), static_cast<DWORD>(pid), hFile, type, &mei, nullptr, nullptr);
    CloseHandle(hFile);

    if (result)
    {
        const char* msg = "[CrashReporter] Wrote minidump to: ";
        _write(_fileno(stdout), msg, strlen(msg));
        std::string path = StringTight(dumpFile);
        _write(_fileno(stdout), path.c_str(), path.size());
        _write(_fileno(stdout), "\n", 1);

        std::thread worker([crashDir, exceptionInfo, pid, tid]()
            {
                ReportCrashIncident(crashDir, exceptionInfo, pid, tid);
            });
        worker.join();
        _exit(1);
    }

    const char* msg = "[CrashReporter] Failed to write minidump!\n";
    _write(_fileno(stdout), msg, strlen(msg));
    _exit(1);
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

static siginfo_t g_savedSigInfo;
static ucontext_t g_savedContext;

static void CrashSignalHandler(int sig, siginfo_t* info, void* uctx)
{
    // Save context for later use in crash report
    if (info)
    {
        memcpy(&g_savedSigInfo, info, sizeof(siginfo_t));
        g_linuxSigInfo = &g_savedSigInfo;
    }
    if (uctx)
    {
        memcpy(&g_savedContext, uctx, sizeof(ucontext_t));
        g_linuxContext = &g_savedContext;
    }

    // Capture backtrace addresses on the crashing thread (often the main thread).
    // IMPORTANT: backtrace() is not guaranteed async-signal-safe, but this is the only
    // reliable way here to avoid capturing a worker thread stack later.
    if (!g_linuxBacktraceCaptured)
    {
        int n = backtrace(g_linuxBacktraceAddrs, static_cast<int>(sizeof(g_linuxBacktraceAddrs) / sizeof(g_linuxBacktraceAddrs[0])));
        if (n < 0) n = 0;
        g_linuxBacktraceCount = n;
        g_linuxBacktraceCaptured = 1;
    }

    // Let Breakpad handle the actual crash
    if (g_exceptionHandler)
    {
        g_exceptionHandler->HandleSignal(sig, info, uctx);
    }
}

static bool BreakpadDumpCallback(const google_breakpad::MinidumpDescriptor& descriptor, void* context, bool succeeded)
{
    if (g_dumpWritten)
    {
        return false;
    }
    g_dumpWritten = true;

    if (succeeded)
    {
        sys_write(STDOUT_FILENO, "[CrashReporter] Wrote minidump to: ", 35);
        sys_write(STDOUT_FILENO, descriptor.path(), my_strlen(descriptor.path()));
        sys_write(STDOUT_FILENO, "\n", 1);

        std::string dumpPath = descriptor.path();
        std::string dumpRoot = g_dumpPath;
        std::thread worker([dumpPath, dumpRoot]()
            {
                auto sep = dumpPath.find_last_of("/\\");
                std::string file = (sep == std::string::npos) ? dumpPath : dumpPath.substr(sep + 1);
                auto dot = file.rfind(".dmp");
                std::string uuid = (dot == std::string::npos) ? file : file.substr(0, dot);

                std::string crashDir = dumpRoot + "/" + uuid;
                std::error_code ec;
                std::filesystem::create_directories(crashDir, ec);

                std::string newDumpPath = crashDir + "/minidump.dmp";
                if (std::rename(dumpPath.c_str(), newDumpPath.c_str()) != 0)
                {
                    std::filesystem::copy_file(dumpPath, newDumpPath, std::filesystem::copy_options::overwrite_existing, ec);
                    std::filesystem::remove(dumpPath, ec);
                }

                ReportCrashIncident(crashDir, nullptr);
            });
        worker.join();
        _exit(1);
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

    // Create Breakpad handler but don't let it install signal handlers directly
    g_exceptionHandler = new google_breakpad::ExceptionHandler(descriptor, nullptr, BreakpadDumpCallback, nullptr, false, -1);

    // Install our own signal handlers that capture context first
    struct sigaction sa;
    memset(&sa, 0, sizeof(sa));
    sa.sa_sigaction = CrashSignalHandler;
    sa.sa_flags = SA_SIGINFO | SA_ONSTACK;
    sigemptyset(&sa.sa_mask);

    sigaction(SIGSEGV, &sa, nullptr);
    sigaction(SIGBUS, &sa, nullptr);
    sigaction(SIGFPE, &sa, nullptr);
    sigaction(SIGILL, &sa, nullptr);
    sigaction(SIGABRT, &sa, nullptr);
    sigaction(SIGTRAP, &sa, nullptr);
    sigaction(SIGSYS, &sa, nullptr);
}

void UnregisterCrashHandlers()
{
    signal(SIGSEGV, SIG_DFL);
    signal(SIGBUS, SIG_DFL);
    signal(SIGFPE, SIG_DFL);
    signal(SIGILL, SIG_DFL);
    signal(SIGABRT, SIG_DFL);
    signal(SIGTRAP, SIG_DFL);
    signal(SIGSYS, SIG_DFL);

    if (g_exceptionHandler)
    {
        delete g_exceptionHandler;
        g_exceptionHandler = nullptr;
    }

    g_linuxSigInfo = nullptr;
    g_linuxContext = nullptr;
    g_linuxBacktraceCaptured = 0;
    g_linuxBacktraceCount = 0;
}
#endif