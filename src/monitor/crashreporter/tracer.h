#ifndef _src_monitor_crashreporter_tracer_h
#define _src_monitor_crashreporter_tracer_h

typedef void (*Sw2TracerDumpFunc)(const char* path);

#include <iostream>
#include <string>
#include <cstdlib>

#include "core/managed/host/dynlib.h"

#include <api/interfaces/interfaces.h>

#ifdef _WIN32
#include <windows.h>
#else
#include <stdlib.h>
#endif

bool setEnvVar(const std::string& key, const std::string& value) {
#ifdef _WIN32
    if (_putenv_s(key.c_str(), value.c_str()) == 0) {
        return true;
    }
#else
    if (setenv(key.c_str(), value.c_str(), 1) == 0) {
        return true;
    }
#endif
    return false;
}

void TracerDump(const std::string& corePath, const char* path)
{

    void* lib = load_library(WIN_LINUX(StringWide("sw2tracer.dll").c_str(), (Files::GeneratePath(corePath + "bin/linuxsteamrt64/libsw2tracer.so")).c_str()));
    if (!lib)
    {
        auto logger = g_ifaceService.FetchInterface<ILogger>(LOGGER_INTERFACE_VERSION);
        logger->Error("Crash Reporter", "Failed to load sw2tracer library\n");
        return;
    }

    Sw2TracerDumpFunc dumpFunc = (Sw2TracerDumpFunc)get_export(lib, "SW2TracerDump");
    if (dumpFunc)
    {
        dumpFunc(path);
    }
    else
    {
        auto logger = g_ifaceService.FetchInterface<ILogger>(LOGGER_INTERFACE_VERSION);
        logger->Error("Crash Reporter", "Failed to get SW2TracerDump function\n");
    }
}


#endif