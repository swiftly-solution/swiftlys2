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
#include <cstdlib>
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
                    auto& xmmReg = xmm[fmt::format("XMM{}", i)];
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

            // Placeholder for managed stack
            auto& managedStack = callStack["managed"];
            managedStack["msg"] = "Managed stack capture not yet implemented";

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

        // Exception details
        crashReport["exception"]["code"] = "N/A";
        crashReport["exception"]["codeName"] = "Linux signal (details in minidump)";
        crashReport["exception"]["address"] = "N/A";
        crashReport["exception"]["flags"] = "N/A";

        // Access violation details placeholder
        crashReport["exception"]["accessViolation"]["type"] = "N/A";
        crashReport["exception"]["accessViolation"]["address"] = "N/A";

        // General Purpose Registers (64-bit)
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

        // 32-bit register view
        auto& gpr32 = crashReport["registers"]["general32"];
        gpr32["eax"] = "N/A";
        gpr32["ebx"] = "N/A";
        gpr32["ecx"] = "N/A";
        gpr32["edx"] = "N/A";
        gpr32["esi"] = "N/A";
        gpr32["edi"] = "N/A";
        gpr32["ebp"] = "N/A";
        gpr32["esp"] = "N/A";

        // Legacy registers
        auto& gprLow = crashReport["registers"]["legacy"];
        gprLow["ax"] = "N/A";
        gprLow["bx"] = "N/A";
        gprLow["cx"] = "N/A";
        gprLow["dx"] = "N/A";
        gprLow["al"] = "N/A";
        gprLow["bl"] = "N/A";
        gprLow["cl"] = "N/A";
        gprLow["dl"] = "N/A";
        gprLow["ah"] = "N/A";
        gprLow["bh"] = "N/A";
        gprLow["ch"] = "N/A";
        gprLow["dh"] = "N/A";

        // Segment registers
        auto& segments = crashReport["registers"]["segments"];
        segments["cs"] = "N/A";
        segments["ds"] = "N/A";
        segments["es"] = "N/A";
        segments["fs"] = "N/A";
        segments["gs"] = "N/A";
        segments["ss"] = "N/A";

        // Flags register
        crashReport["registers"]["rflags"]["raw"] = "N/A";
        auto& flags = crashReport["registers"]["rflags"]["bits"];
        flags["CF"] = "N/A";
        flags["PF"] = "N/A";
        flags["AF"] = "N/A";
        flags["ZF"] = "N/A";
        flags["SF"] = "N/A";
        flags["TF"] = "N/A";
        flags["IF"] = "N/A";
        flags["DF"] = "N/A";
        flags["OF"] = "N/A";
        flags["IOPL"] = "N/A";
        flags["NT"] = "N/A";
        flags["RF"] = "N/A";
        flags["VM"] = "N/A";
        flags["AC"] = "N/A";
        flags["VIF"] = "N/A";
        flags["VIP"] = "N/A";
        flags["ID"] = "N/A";

        // XMM registers structure
        auto& xmm = crashReport["registers"]["xmm"];
        xmm["mxcsr"] = "N/A";
        for (int i = 0; i < 16; i++)
        {
            auto& xmmReg = xmm[fmt::format("XMM{}", i)];
            xmmReg["low"] = "N/A";
            xmmReg["high"] = "N/A";
            xmmReg["full"] = "N/A";
        }
        xmm["fpuControlWord"] = "N/A";
        xmm["fpuStatusWord"] = "N/A";
        xmm["fpuTagWord"] = "N/A";

        // Debug registers
        auto& debug = crashReport["registers"]["debug"];
        debug["dr0"] = "N/A";
        debug["dr1"] = "N/A";
        debug["dr2"] = "N/A";
        debug["dr3"] = "N/A";
        debug["dr6"] = "N/A";
        debug["dr7"] = "N/A";

        // Control registers
        crashReport["registers"]["control"]["contextFlags"] = "N/A";

        // Stack pointer info
        auto& stack = crashReport["registers"]["stackInfo"];
        stack["rsp"] = "N/A";
        stack["rbp"] = "N/A";
        stack["frameSize"] = "N/A";

        // Call stack structure
        auto& callStack = crashReport["callstack"];

        // Native stack
        auto& nativeStack = callStack["native"];
        nativeStack["capture_method"] = "backtrace";
        nativeStack["frames"] = nlohmann::json::array();
        nativeStack["frameCount"] = 0;
        nativeStack["symbolInitWarning"] = "Limited stack trace in signal handler context";

        // Managed stack placeholder
        auto& managedStack = callStack["managed"];
        managedStack["msg"] = "Managed stack capture not yet implemented";

        // Stack memory dump
        auto& stackMemory = crashReport["stackMemory"];
        stackMemory["dumpStart"] = "N/A";
        stackMemory["dumpSize"] = 0;
        stackMemory["data"] = nlohmann::json::object();
        stackMemory["note"] = "Stack memory dump not available in signal handler context";

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