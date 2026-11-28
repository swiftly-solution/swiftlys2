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

#include <api/interfaces/interfaces.h>
#include <api/shared/files.h>
#include <api/shared/profiler.h>
#include <scripting/scripting.h>

#include <cstdio>
#include <string>

void Bridge_Tracer_StartProfiling()
{
    PROFILE_FUNCTION();

    if (g_pTracer)
        g_pTracer->StartProfiling();
}

void Bridge_Tracer_StopProfiling()
{
    PROFILE_FUNCTION();

    if (g_pTracer)
        g_pTracer->StopProfiling();
}

void Bridge_Tracer_CustomBegin(char* name, char* source)
{
    PROFILE_FUNCTION();

    if (g_pTracer)
        g_pTracer->CustomBegin(name, source);
}

void Bridge_Tracer_CustomEnd(char* name)
{
    PROFILE_FUNCTION();

    if (g_pTracer)
        g_pTracer->CustomEnd(name);
}

void Bridge_Tracer_SetObjectTracking(bool value)
{
    PROFILE_FUNCTION();

    if (g_pTracer)
        g_pTracer->SetObjectTracking(value);
}

uint8_t Bridge_Tracer_IsObjectTracking()
{
    PROFILE_FUNCTION();

    return g_pTracer && g_pTracer->IsObjectTracking() ? 1 : 0;
}

std::string GetRelativePath(std::string path);

static bool WriteProfileBlock(const void* data, size_t size, void* context)
{
    return std::fwrite(data, 1, size, static_cast<FILE*>(context)) == size;
}

static bool SaveProfileToFile(const char* path)
{
    if (!g_pTracer || !path || !*path)
        return false;

    FILE* file = std::fopen(Files::GeneratePath(GetRelativePath(path)).c_str(), "wb");
    if (!file)
        return false;

    const bool written = g_pTracer->WriteProfile(WriteProfileBlock, file);
    return std::fclose(file) == 0 && written;
}

uint8_t Bridge_Tracer_SaveProfile(char* path)
{
    PROFILE_FUNCTION();

    return SaveProfileToFile(path) ? 1 : 0;
}

DEFINE_NATIVE("Tracer.StartProfiling", Bridge_Tracer_StartProfiling);
DEFINE_NATIVE("Tracer.StopProfiling", Bridge_Tracer_StopProfiling);
DEFINE_NATIVE("Tracer.CustomBegin", Bridge_Tracer_CustomBegin);
DEFINE_NATIVE("Tracer.CustomEnd", Bridge_Tracer_CustomEnd);
DEFINE_NATIVE("Tracer.SetObjectTracking", Bridge_Tracer_SetObjectTracking);
DEFINE_NATIVE("Tracer.IsObjectTracking", Bridge_Tracer_IsObjectTracking);
DEFINE_NATIVE("Tracer.SaveProfile", Bridge_Tracer_SaveProfile);
