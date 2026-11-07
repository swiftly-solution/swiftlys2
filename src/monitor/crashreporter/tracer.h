#ifndef _src_monitor_crashreporter_tracer_h
#define _src_monitor_crashreporter_tracer_h

typedef void (*Sw2TracerDumpFunc)(const char* path);

#include <string>
#include <cstdlib>

#include "core/managed/host/dynlib.h"

#ifdef _WIN32
#include <windows.h>
#else
#include <stdlib.h>
#endif

bool setEnvVar(const std::string& key, const std::string& value);
void TracerDump(const std::string& corePath, const char* path);


#endif