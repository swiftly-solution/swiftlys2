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

#include <api/interfaces/manager.h>
#include <scripting/scripting.h>

#include <api/shared/string.h>

uint64_t Bridge_ConsoleOutput_AddConsoleListener(void* callback)
{
    auto consoleOutput = g_ifaceService.FetchInterface<IConsoleOutput>(CONSOLEOUTPUT_INTERFACE_VERSION);
    return consoleOutput->AddConsoleListener([callback](const std::string& text) {
        reinterpret_cast<void(*)(const char*)>(callback)(text.c_str());
        });
}

void Bridge_ConsoleOutput_RemoveConsoleListener(uint64_t listenerId)
{
    auto consoleOutput = g_ifaceService.FetchInterface<IConsoleOutput>(CONSOLEOUTPUT_INTERFACE_VERSION);
    consoleOutput->RemoveConsoleListener(listenerId);
}

bool Bridge_ConsoleOutput_IsEnabled()
{
    auto consoleOutput = g_ifaceService.FetchInterface<IConsoleOutput>(CONSOLEOUTPUT_INTERFACE_VERSION);
    return consoleOutput->IsEnabled();
}

void Bridge_ConsoleOutput_ToggleFilter()
{
    auto consoleOutput = g_ifaceService.FetchInterface<IConsoleOutput>(CONSOLEOUTPUT_INTERFACE_VERSION);
    consoleOutput->ToggleFilter();
}

void Bridge_ConsoleOutput_ReloadFilterConfiguration()
{
    auto consoleOutput = g_ifaceService.FetchInterface<IConsoleOutput>(CONSOLEOUTPUT_INTERFACE_VERSION);
    consoleOutput->ReloadFilterConfiguration();
}

bool Bridge_ConsoleOutput_NeedsFiltering(const char* text)
{
    auto consoleOutput = g_ifaceService.FetchInterface<IConsoleOutput>(CONSOLEOUTPUT_INTERFACE_VERSION);
    return consoleOutput->NeedsFiltering(std::string(text));
}

int Bridge_ConsoleOutput_GetCounterText(char* out)
{
    static std::string counterText;
    auto consoleOutput = g_ifaceService.FetchInterface<IConsoleOutput>(CONSOLEOUTPUT_INTERFACE_VERSION);

    if (out != nullptr) strcpy(out, counterText.c_str());
    else counterText = consoleOutput->GetCounterText();

    return counterText.size();
}

DEFINE_NATIVE("ConsoleOutput.AddConsoleListener", Bridge_ConsoleOutput_AddConsoleListener);
DEFINE_NATIVE("ConsoleOutput.RemoveConsoleListener", Bridge_ConsoleOutput_RemoveConsoleListener);
DEFINE_NATIVE("ConsoleOutput.IsEnabled", Bridge_ConsoleOutput_IsEnabled);
DEFINE_NATIVE("ConsoleOutput.ToggleFilter", Bridge_ConsoleOutput_ToggleFilter);
DEFINE_NATIVE("ConsoleOutput.ReloadFilterConfiguration", Bridge_ConsoleOutput_ReloadFilterConfiguration);
DEFINE_NATIVE("ConsoleOutput.NeedsFiltering", Bridge_ConsoleOutput_NeedsFiltering);
DEFINE_NATIVE("ConsoleOutput.GetCounterText", Bridge_ConsoleOutput_GetCounterText);