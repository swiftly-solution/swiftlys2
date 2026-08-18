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

#ifdef _WIN32

#include <api/interfaces/interfaces.h>
#include <api/shared/files.h>

#include "crashreporter.h"

#include <core/managed/host/strconv.h>
#include <core/entrypoint.h>

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

void TracerDump(const std::string& corePath, const char* path);

extern std::string g_dumpPath;
extern std::string g_relativeDumpPath;
extern bool g_dumpWritten;

bool windowsDumpCallback(const wchar_t* dumpPath, const wchar_t* minidump_id, void* context, EXCEPTION_POINTERS* exinfo, MDRawAssertionInfo* assertion, bool succeeded);
void* vectoredHandler = NULL;
LONG CALLBACK BreakpadVectoredHandler(_In_ PEXCEPTION_POINTERS ExceptionInfo);

void InitCrashReporterWindows()
{
    std::wstring wideString = StringWide(g_dumpPath);

    MINIDUMP_TYPE mdmpType = static_cast<MINIDUMP_TYPE>(MiniDumpWithUnloadedModules | MiniDumpWithFullMemoryInfo);

    exceptionHandler = new google_breakpad::ExceptionHandler(
        wideString, NULL, windowsDumpCallback, NULL, google_breakpad::ExceptionHandler::HANDLER_ALL,
        mdmpType, static_cast<const wchar_t*>(NULL), NULL);

    vectoredHandler = AddVectoredExceptionHandler(1, BreakpadVectoredHandler);
}

void ShutdownCrashReporterWindows()
{
    if (vectoredHandler)
    {
        RemoveVectoredExceptionHandler(vectoredHandler);
        vectoredHandler = nullptr;
    }
}

LONG CALLBACK BreakpadVectoredHandler(_In_ PEXCEPTION_POINTERS ExceptionInfo)
{
    switch (ExceptionInfo->ExceptionRecord->ExceptionCode)
    {
    case EXCEPTION_ACCESS_VIOLATION:
        // Internal Windows exceptions (e.g. stack probing, module loading) use 0xc0000005
        // with fewer than 2 parameters — not real crashes, pass them through.
        if (ExceptionInfo->ExceptionRecord->NumberParameters < 2)
            return EXCEPTION_CONTINUE_SEARCH;
        break;
    case EXCEPTION_INVALID_HANDLE:
    case EXCEPTION_ARRAY_BOUNDS_EXCEEDED:
    case EXCEPTION_DATATYPE_MISALIGNMENT:
    case EXCEPTION_ILLEGAL_INSTRUCTION:
    case EXCEPTION_INT_DIVIDE_BY_ZERO:
    case EXCEPTION_STACK_OVERFLOW:
    case 0xC0000409: // STATUS_STACK_BUFFER_OVERRUN
    case 0xC0000374: // STATUS_HEAP_CORRUPTION
        break;
    case 0: // Valve use this for Sys_Error.
        if ((ExceptionInfo->ExceptionRecord->ExceptionFlags & EXCEPTION_NONCONTINUABLE) == 0)
            return EXCEPTION_CONTINUE_SEARCH;
        break;
    default:
        return EXCEPTION_CONTINUE_SEARCH;
    }

    if (exceptionHandler->WriteMinidumpForException(ExceptionInfo))
    {
        delete exceptionHandler;

        ExceptionInfo->ExceptionRecord->ExceptionCode = EXCEPTION_BREAKPOINT;

        return EXCEPTION_EXECUTE_HANDLER;
    }
    else {
        return EXCEPTION_CONTINUE_SEARCH;
    }
}

static bool windowsDumpCallback(const wchar_t* dumpPath, const wchar_t* minidump_id, void* context, EXCEPTION_POINTERS* exinfo, MDRawAssertionInfo* assertion, bool succeeded)
{
    std::string dmpFolder = StringTight(dumpPath);
    ConsoleLogger_FlushForCrash();

    auto tracerLevel = g_pCrashReporter->GetDotnetCrashTracerLevel();
    if (tracerLevel > 0)
    {
        std::string tracerPath = dmpFolder + "\\managedtrace.txt";
        g_pLogger->Warning("Crash Reporter", fmt::format("Dumping managed trace to: {}\n", tracerPath));
        TracerDump(g_SwiftlyCore.GetCorePath(), tracerPath.c_str());
    }

    std::string mdmpId = StringTight(minidump_id);

    std::string mdmpPath = fmt::format("{}\\{}.dmp", dmpFolder, mdmpId);

    if (!succeeded) {
        g_pLogger->Error("Crash Reporter", fmt::format("Failed to write minidump to '{}'\n", mdmpPath));
        return succeeded;
    }

    g_dumpWritten = true;
    g_pLogger->Info("Crash Reporter", fmt::format("Minidump written to '{}'\n", mdmpPath));

    google_breakpad::SimpleSymbolSupplier symbolSupplier("");
    google_breakpad::BasicSourceLineResolver resolver;
    google_breakpad::MinidumpProcessor minidump_processor(&symbolSupplier, &resolver);

#undef max
    google_breakpad::MinidumpThreadList::set_max_threads(std::numeric_limits<uint32_t>::max());
    google_breakpad::MinidumpMemoryList::set_max_regions(std::numeric_limits<uint32_t>::max());

    google_breakpad::Minidump mdmp(mdmpPath);
    if (!mdmp.Read()) {
        g_pLogger->Error("Crash Reporter", fmt::format("Failed to read minidump from '{}'\n", mdmpPath));
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
            Files::Write(g_relativeDumpPath + "\\crashinfo.json", crashInfoJson, false);
        }
    }

    return succeeded;
}

void ParseAndWriteCrashInfo(const std::string& mdmpAbsPath, const std::string& crashInfoRelPath)
{
    google_breakpad::SimpleSymbolSupplier symbolSupplier("");
    google_breakpad::BasicSourceLineResolver resolver;
    google_breakpad::MinidumpProcessor minidump_processor(&symbolSupplier, &resolver);

#undef max
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