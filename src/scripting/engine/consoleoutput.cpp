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
#include <scripting/scripting.h>

#include <api/shared/string.h>

static char* Bridge_ConsoleOutput_CopyString(const std::string& value, int* size)
{
    int outSize = static_cast<int>(value.size());
    *size = outSize;

    char* out = (char*)g_pMemoryAllocator->Alloc(outSize + 1);
    g_pMemoryAllocator->Copy(out, (void*)value.c_str(), outSize);
    out[outSize] = '\0';
    return out;
}

uint64_t Bridge_ConsoleOutput_AddConsoleListener(void* callback)
{
    return g_pConsoleOutput->AddConsoleListener([callback](const std::string& text) {
        reinterpret_cast<void(*)(const char*)>(callback)(text.c_str());
        });
}

void Bridge_ConsoleOutput_RemoveConsoleListener(uint64_t listenerId)
{
    g_pConsoleOutput->RemoveConsoleListener(listenerId);
}

bool Bridge_ConsoleOutput_IsEnabled()
{
    return g_pConsoleOutput->IsEnabled();
}

void Bridge_ConsoleOutput_ToggleFilter()
{
    g_pConsoleOutput->ToggleFilter();
}

void Bridge_ConsoleOutput_ReloadFilterConfiguration()
{
    g_pConsoleOutput->ReloadFilterConfiguration();
}

bool Bridge_ConsoleOutput_NeedsFiltering(const char* text)
{
    return g_pConsoleOutput->NeedsFiltering((char*)text);
}

char* Bridge_ConsoleOutput_GetCounterText(int* size)
{
    std::string counterText = g_pConsoleOutput->GetCounterText();

    return Bridge_ConsoleOutput_CopyString(counterText, size);
}

DEFINE_NATIVE("ConsoleOutput.AddConsoleListener", Bridge_ConsoleOutput_AddConsoleListener);
DEFINE_NATIVE("ConsoleOutput.RemoveConsoleListener", Bridge_ConsoleOutput_RemoveConsoleListener);
DEFINE_NATIVE("ConsoleOutput.IsEnabled", Bridge_ConsoleOutput_IsEnabled);
DEFINE_NATIVE("ConsoleOutput.ToggleFilter", Bridge_ConsoleOutput_ToggleFilter);
DEFINE_NATIVE("ConsoleOutput.ReloadFilterConfiguration", Bridge_ConsoleOutput_ReloadFilterConfiguration);
DEFINE_NATIVE("ConsoleOutput.NeedsFiltering", Bridge_ConsoleOutput_NeedsFiltering);
DEFINE_NATIVE("ConsoleOutput.GetCounterText", Bridge_ConsoleOutput_GetCounterText);