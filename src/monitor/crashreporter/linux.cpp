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
#include <core/entrypoint.h>

#include <api/interfaces/interfaces.h>
#include <api/shared/files.h>
#include <api/shared/profiler.h>
#include <format>
#include <cerrno>
#include <limits.h>
#include <poll.h>
#include <spawn.h>
#include <sys/prctl.h>
#include <sys/socket.h>
#include <sys/wait.h>

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

extern char** environ;
static int collectorSocket = -1;
static pid_t collectorPid = -1;
const int kExceptionSignals[] = { SIGSEGV, SIGABRT, SIGFPE, SIGILL, SIGBUS };
const int kNumHandledSignals = std::size(kExceptionSignals);
static bool FinishLinuxDump(const std::string& mdmpPath, bool succeeded);

void ShutdownCrashReporterLinux()
{
    if (collectorSocket >= 0) close(collectorSocket);
    collectorSocket = -1;
    if (collectorPid > 0)
    {
        pid_t state;
        do { state = waitpid(collectorPid, nullptr, WNOHANG); } while (state < 0 && errno == EINTR);
        if (state == 0)
        {
            kill(collectorPid, SIGTERM);
            while (waitpid(collectorPid, nullptr, 0) < 0 && errno == EINTR) {}
        }
    }
    collectorPid = -1;
}

static int StartManagedDumpCollector()
{
    int sockets[2];
    if (socketpair(AF_UNIX, SOCK_SEQPACKET | SOCK_CLOEXEC, 0, sockets) != 0) return -1;
    const int passCredentials = 1;
    if (setsockopt(sockets[1], SOL_SOCKET, SO_PASSCRED, &passCredentials, sizeof(passCredentials)) != 0)
    {
        close(sockets[0]);
        close(sockets[1]);
        return -1;
    }
    const std::string executable = Files::GeneratePath(g_SwiftlyCore.GetCorePath() + "bin/linuxsteamrt64/sw2dump");
    const std::string parent = std::to_string(getpid());
    char* args[] = {const_cast<char*>(executable.c_str()), const_cast<char*>(g_dumpPath.c_str()),
                    const_cast<char*>(parent.c_str()), nullptr};
    posix_spawn_file_actions_t actions;
    if (posix_spawn_file_actions_init(&actions) != 0)
    {
        close(sockets[0]);
        close(sockets[1]);
        return -1;
    }
    int result = posix_spawn_file_actions_addclose(&actions, sockets[0]);
    if (result == 0) result = posix_spawn_file_actions_adddup2(&actions, sockets[1], STDIN_FILENO);
    if (result == 0 && sockets[1] != STDIN_FILENO) result = posix_spawn_file_actions_addclose(&actions, sockets[1]);
    pid_t child = -1;
    if (result == 0) result = posix_spawn(&child, executable.c_str(), &actions, nullptr, args, environ);
    collectorPid = result == 0 ? child : -1;
    posix_spawn_file_actions_destroy(&actions);
    close(sockets[1]);
    collectorSocket = sockets[0];
    if (result == 0 && prctl(PR_SET_PTRACER, collectorPid, 0, 0, 0) == 0)
    {
        pollfd ready{collectorSocket, POLLIN, 0};
        char response = 0;
        if (poll(&ready, 1, 2000) > 0 && recv(collectorSocket, &response, 1, MSG_DONTWAIT) == 1 && response == 'R')
            return collectorSocket;
    }
    ShutdownCrashReporterLinux();
    g_pLogger->Warning("Crash Reporter", "Managed dump collector unavailable; using native minidumps.\n");
    return -1;
}

static void CrashSignalHandler(int sig, siginfo_t* info, void* uctx)
{
    PROFILE_FUNCTION();

    if (exceptionHandler)
    {
        const bool succeeded = exceptionHandler->HandleSignal(sig, info, uctx);
        if (exceptionHandler->IsOutOfProcess())
        {
            char path[PATH_MAX]{};
            const ssize_t length = recv(collectorSocket, path, sizeof(path) - 1, MSG_DONTWAIT);
            FinishLinuxDump(length > 0 ? std::string(path) : g_dumpPath, succeeded && length > 0);
        }
    }
    _exit(1);
}

void InitCrashReporterLinux()
{
    PROFILE_FUNCTION();

    google_breakpad::MinidumpDescriptor descriptor(g_dumpPath);

    exceptionHandler = new google_breakpad::ExceptionHandler(descriptor, NULL, linuxDumpCallback, NULL, true, StartManagedDumpCollector());

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
    // CoreCLR installs its handlers after ours and chains fatal signals to us.
    // Replacing them here turns recoverable managed faults into process crashes.
}

bool linuxDumpCallback(const google_breakpad::MinidumpDescriptor& descriptor, void* context, bool succeeded)
{
    return FinishLinuxDump(descriptor.path(), succeeded);
}

static bool FinishLinuxDump(const std::string& mdmpPath, bool succeeded)
{
    PROFILE_FUNCTION();

    auto tracerLevel = g_pCrashReporter->GetDotnetCrashTracerLevel();
    if (tracerLevel > 0)
    {
        std::string tracerPath = g_dumpPath + "/managedtrace.txt";
        g_pLogger->Warning("Crash Reporter", std::format("Dumping managed trace to: {}\n", tracerPath));
        TracerDump(g_SwiftlyCore.GetCorePath(), tracerPath.c_str());
    }

    if (!succeeded) {
        g_pLogger->Error("Crash Reporter", std::format("Failed to write minidump to '{}'\n", mdmpPath));
        ConsoleLogger_FlushForCrash();
        _exit(1);
        return succeeded;
    }

    g_dumpWritten = true;
    g_pLogger->Info("Crash Reporter", std::format("Minidump written to '{}'\n", mdmpPath));

    google_breakpad::SimpleSymbolSupplier symbolSupplier("");
    google_breakpad::BasicSourceLineResolver resolver;
    google_breakpad::MinidumpProcessor minidump_processor(&symbolSupplier, &resolver);

    google_breakpad::MinidumpThreadList::set_max_threads(std::numeric_limits<uint32_t>::max());
    google_breakpad::MinidumpMemoryList::set_max_regions(std::numeric_limits<uint32_t>::max());

    google_breakpad::Minidump mdmp(mdmpPath);
    if (!mdmp.Read()) {
        g_pLogger->Error("Crash Reporter", std::format("Failed to read minidump from '{}'\n", mdmpPath));
        ConsoleLogger_FlushForCrash();
        _exit(1);
        return succeeded;
    }
    else {
        google_breakpad::ProcessState processState;
        if (minidump_processor.Process(&mdmp, &processState) != google_breakpad::PROCESS_OK)
        {
            g_pLogger->Error("Crash Reporter", "MinidumpProcessor::Process failed\n");
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
    PROFILE_FUNCTION();

    google_breakpad::SimpleSymbolSupplier symbolSupplier("");
    google_breakpad::BasicSourceLineResolver resolver;
    google_breakpad::MinidumpProcessor minidump_processor(&symbolSupplier, &resolver);

    google_breakpad::MinidumpThreadList::set_max_threads(std::numeric_limits<uint32_t>::max());
    google_breakpad::MinidumpMemoryList::set_max_regions(std::numeric_limits<uint32_t>::max());

    google_breakpad::Minidump mdmp(mdmpAbsPath);
    if (!mdmp.Read())
    {
        g_pLogger->Error("Crash Reporter", std::format("Failed to read minidump from '{}'\n", mdmpAbsPath));
        return;
    }

    google_breakpad::ProcessState processState;
    if (minidump_processor.Process(&mdmp, &processState) != google_breakpad::PROCESS_OK)
    {
        g_pLogger->Error("Crash Reporter", std::format("MinidumpProcessor::Process failed for '{}'\n", mdmpAbsPath));
        return;
    }

    auto crashInfoJson = FormatProcessState(processState, &resolver);
    Files::Write(crashInfoRelPath, crashInfoJson, false);
}

#endif
