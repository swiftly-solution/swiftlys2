#ifndef _src_monitor_crashreporter_tracer_h
#define _src_monitor_crashreporter_tracer_h

#include <cstdlib>
#include <string>

#include "core/managed/host/dynlib.h"

#ifdef _WIN32
#include <windows.h>
#else
#include <stdlib.h>
#endif

void InitializeTraceAPI(const std::string& corePath);

#endif
