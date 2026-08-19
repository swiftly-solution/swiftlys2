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

#ifndef _WIN32

#include "crashreporter.h"
#include "tracer.h"
#ifdef PAGE_SIZE
#undef PAGE_SIZE
#endif
#include <core/managed/host/strconv.h>
#include <core/entrypoint.h>

#include <api/interfaces/interfaces.h>
#include <api/shared/files.h>
#include <fmt/format.h>

#include <common/path_helper.h>
#include <common/scoped_ptr.h>
#include <google_breakpad/processor/basic_source_line_resolver.h>
#include <google_breakpad/processor/minidump_processor.h>
#include <google_breakpad/processor/process_state.h>
#include <processor/simple_symbol_supplier.h>
#include <processor/stackwalk_common.h>
#include <google_breakpad/processor/call_stack.h>
#include <google_breakpad/processor/stack_frame.h>
#include <processor/pathname_stripper.h>

extern std::string g_dumpPath;
extern std::string g_relativeDumpPath;
extern bool g_dumpWritten;
bool linuxDumpCallback(const google_breakpad::MinidumpDescriptor& descriptor, void* context, bool succeeded);
void TracerDump(const std::string& corePath, const char* path);

void (*SignalHandler)(int, siginfo_t*, void*);
const int kExceptionSignals[] = { SIGSEGV, SIGABRT, SIGFPE, SIGILL, SIGBUS };
const int kNumHandledSignals = std::size(kExceptionSignals);

static void CrashSignalHandler(int sig, siginfo_t* info, void* uctx)
{
    if (exceptionHandler) exceptionHandler->HandleSignal(sig, info, uctx);
    _exit(1);
}

void InitCrashReporterLinux()
{
    google_breakpad::MinidumpDescriptor descriptor(g_dumpPath);

    exceptionHandler = new google_breakpad::ExceptionHandler(descriptor, NULL, linuxDumpCallback, NULL, true, -1);

    struct sigaction sa;
    memset(&sa, 0, sizeof(sa));
    sa.sa_sigaction = CrashSignalHandler;
    sa.sa_flags = SA_SIGINFO | SA_ONSTACK;
    sigemptyset(&sa.sa_mask);

    for (int i = 0; i < kNumHandledSignals; ++i)
    {
        sigaction(kExceptionSignals[i], &sa, nullptr);
    }
}

void CrashReporterOnTickLinux()
{
    bool signalChanged = false;
    struct sigaction oact;

    for (int i = 0; i < kNumHandledSignals; ++i)
    {
        sigaction(kExceptionSignals[i], NULL, &oact);

        if (oact.sa_sigaction != CrashSignalHandler)
        {
            signalChanged = true;
            break;
        }
    }

    if (!signalChanged)
        return;

    struct sigaction act;
    memset(&act, 0, sizeof(act));
    sigemptyset(&act.sa_mask);

    for (int i = 0; i < kNumHandledSignals; ++i)
        sigaddset(&act.sa_mask, kExceptionSignals[i]);

    act.sa_sigaction = CrashSignalHandler;
    act.sa_flags = SA_ONSTACK | SA_SIGINFO;

    for (int i = 0; i < kNumHandledSignals; ++i)
        sigaction(kExceptionSignals[i], &act, NULL);
}

bool linuxDumpCallback(const google_breakpad::MinidumpDescriptor& descriptor, void* context, bool succeeded)
{
    auto tracerLevel = g_pCrashReporter->GetDotnetCrashTracerLevel();
    if (tracerLevel > 0)
    {
        std::string tracerPath = g_dumpPath + "/managedtrace.txt";
        g_pLogger->Warning("Crash Reporter", fmt::format("Dumping managed trace to: {}\n", tracerPath));
        TracerDump(g_SwiftlyCore.GetCorePath(), tracerPath.c_str());
    }

    std::string mdmpPath = descriptor.path();

    if (!succeeded) {
        g_pLogger->Error("Crash Reporter", fmt::format("Failed to write minidump to '{}'\n", mdmpPath));
        ConsoleLogger_FlushForCrash();
        _exit(1);
        return succeeded;
    }

    g_dumpWritten = true;
    g_pLogger->Info("Crash Reporter", fmt::format("Minidump written to '{}'\n", mdmpPath));

    google_breakpad::SimpleSymbolSupplier symbolSupplier("");
    google_breakpad::BasicSourceLineResolver resolver;
    google_breakpad::MinidumpProcessor minidump_processor(&symbolSupplier, &resolver);

    google_breakpad::MinidumpThreadList::set_max_threads(std::numeric_limits<uint32_t>::max());
    google_breakpad::MinidumpMemoryList::set_max_regions(std::numeric_limits<uint32_t>::max());

    google_breakpad::Minidump mdmp(mdmpPath);
    if (!mdmp.Read()) {
        g_pLogger->Error("Crash Reporter", fmt::format("Failed to read minidump from '{}'\n", mdmpPath));
        ConsoleLogger_FlushForCrash();
        _exit(1);
        return succeeded;
    }
    else {
        google_breakpad::ProcessState processState;
        if (minidump_processor.Process(&mdmp, &processState) != google_breakpad::PROCESS_OK)
        {
            g_pLogger->Error("Crash Reporter", fmt::format("MinidumpProcessor::Process failed\n", mdmpPath));
        }
        else
        {
            auto crashInfoJson = FormatProcessState(processState, &resolver);
            Files::Write(g_relativeDumpPath + "/crashinfo.json", crashInfoJson, false);
        }
    }

    ConsoleLogger_FlushForCrash();
    _exit(1);
    return succeeded;
}

void ParseAndWriteCrashInfo(const std::string& mdmpAbsPath, const std::string& crashInfoRelPath)
{
    google_breakpad::SimpleSymbolSupplier symbolSupplier("");
    google_breakpad::BasicSourceLineResolver resolver;
    google_breakpad::MinidumpProcessor minidump_processor(&symbolSupplier, &resolver);

    google_breakpad::MinidumpThreadList::set_max_threads(std::numeric_limits<uint32_t>::max());
    google_breakpad::MinidumpMemoryList::set_max_regions(std::numeric_limits<uint32_t>::max());

    google_breakpad::Minidump mdmp(mdmpAbsPath);
    if (!mdmp.Read())
    {
        g_pLogger->Error("Crash Reporter", fmt::format("Failed to read minidump from '{}'\n", mdmpAbsPath));
        return;
    }

    google_breakpad::ProcessState processState;
    if (minidump_processor.Process(&mdmp, &processState) != google_breakpad::PROCESS_OK)
    {
        g_pLogger->Error("Crash Reporter", fmt::format("MinidumpProcessor::Process failed for '{}'\n", mdmpAbsPath));
        return;
    }

    auto crashInfoJson = FormatProcessState(processState, &resolver);
    Files::Write(crashInfoRelPath, crashInfoJson, false);
}

#endif