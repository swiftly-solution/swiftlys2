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

#include "tracer.h"

#include <api/interfaces/interfaces.h>
#include <api/shared/env.h>
#include <api/shared/files.h>
#include <api/shared/plat.h>
#include <api/shared/profiler.h>

#include <core/managed/host/strconv.h>

ITracer* g_pTracer = nullptr;

void InitializeTraceAPI(const std::string& corePath)
{
    PROFILE_FUNCTION();

    const std::string tracerPath = Files::GeneratePath(corePath + WIN_LINUX("bin\\win64\\coreclr-tracer.dll", "bin/linuxsteamrt64/coreclr-tracer.so"));

    envvar("CORECLR_ENABLE_PROFILING", "1", 1);
    envvar("CORECLR_PROFILER", "{2e7d6025-9ef7-4942-b35e-4a9d3c4aa024}", 1);
    envvar("CORECLR_PROFILER_PATH", tracerPath.c_str(), 1);

    void* lib = load_library(WIN_LINUX(StringWide(tracerPath).c_str(), tracerPath.c_str()));
    if (!lib)
        return;

    GetTracerFn getTracerFunc = (GetTracerFn)get_export(lib, "GetTracer");
    if (getTracerFunc)
    {
        g_pTracer = getTracerFunc();
    }
}
