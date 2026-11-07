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

#include <api/shared/files.h>
#include <api/shared/plat.h>
#include <api/shared/profiler.h>

#include <core/managed/host/strconv.h>

bool setEnvVar(const std::string& key, const std::string& value) {
    PROFILE_FUNCTION();

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
    PROFILE_FUNCTION();

    void* lib = load_library(WIN_LINUX(StringWide("sw2tracer.dll").c_str(), (Files::GeneratePath(corePath + "bin/linuxsteamrt64/libsw2tracer.so")).c_str()));
    if (!lib) return;

    Sw2TracerDumpFunc dumpFunc = (Sw2TracerDumpFunc)get_export(lib, "SW2TracerDump");
    if (dumpFunc)
    {
        dumpFunc(path);
    }
}