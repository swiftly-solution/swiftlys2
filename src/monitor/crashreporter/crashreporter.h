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

#ifndef src_monitor_crashreporter_crashreporter_h
#define src_monitor_crashreporter_crashreporter_h

#include "config.h"

#include <api/monitor/crashreporter/crashreporter.h>

#include <google_breakpad/processor/process_state.h>
#include <google_breakpad/processor/source_line_resolver_interface.h>
#include <google_breakpad/processor/call_stack.h>

#if defined _LINUX
#include <client/linux/handler/exception_handler.h>
#include <common/linux/linux_libc_support.h>
#include <third_party/lss/linux_syscall_support.h>
#include <common/linux/http_upload.h>

#include <dirent.h>
#include <unistd.h>
#else
#include <client/windows/handler/exception_handler.h>
#endif

class CrashReporter : public ICrashReporter
{
private:
    int m_tracerLevel;
public:
    virtual void Init() override;
    virtual void Shutdown() override;

    virtual void EnableDotnetCrashTracer(int level) override;
    virtual int GetDotnetCrashTracerLevel() override;
    virtual void ReportPreventionIncident(std::string category, std::string reason) override;
    virtual void OnTick() override;
};

extern void InitCrashReporterWindows();
extern void InitCrashReporterLinux();
extern void ShutdownCrashReporterWindows();
extern void ShutdownCrashReporterLinux();

extern void CrashReporterOnTickLinux();

std::string FormatProcessState(const google_breakpad::ProcessState& process_state, google_breakpad::SourceLineResolverInterface* resolver);
void ParseAndWriteCrashInfo(const std::string& mdmpAbsPath, const std::string& crashInfoRelPath);

extern void ConsoleLogger_FlushForCrash();

extern google_breakpad::ExceptionHandler* exceptionHandler;

#endif
