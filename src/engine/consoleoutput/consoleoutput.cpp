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

#include "consoleoutput.h"

#include <core/entrypoint.h>

#include <api/interfaces/manager.h>
#include <pcre2.h>
#include <fmt/format.h>

#include <api/shared/jsonc.h>
#include <api/shared/files.h>

using json = nlohmann::json;

std::map<uint64_t, std::function<void(const std::string&)>> g_ConsoleListeners;
std::map<uint64_t, pcre2_code*> g_Filters;
std::map<uint64_t, pcre2_match_data*> g_FiltersMatchData;

bool g_bEnabled = false;
uint64_t g_filterIds = 1;

std::map<uint64_t, std::string> g_FilterNames;
std::map<uint64_t, uint64_t> g_FilteredMessages;

IFunctionHook* g_CLoggingSystem_LogDirect_Hook = nullptr;

int CLoggingSystem_LogDirectHook(void* loggingSystem, int channel, int severity, LeafCodeInfo_t* leafCode, char const* str, va_list* args)
{
    if (!str)
        return reinterpret_cast<decltype(&CLoggingSystem_LogDirectHook)>(g_CLoggingSystem_LogDirect_Hook->GetOriginal())(loggingSystem, channel, severity, leafCode, str, args);

    char buf[MAX_LOGGING_MESSAGE_LENGTH];
    if (args) {
        va_list cpargs;
        va_copy(cpargs, *args);
        V_vsnprintf(buf, sizeof(buf), str, cpargs);
        va_end(cpargs);
    }

    static auto consoleoutput = g_ifaceService.FetchInterface<IConsoleOutput>(CONSOLEOUTPUT_INTERFACE_VERSION);
    if (consoleoutput->NeedsFiltering(args ? buf : (char*)str)) return 0;

    for (const auto& [id, callback] : g_ConsoleListeners)
        callback(args ? buf : str);

    return reinterpret_cast<decltype(&CLoggingSystem_LogDirectHook)>(g_CLoggingSystem_LogDirect_Hook->GetOriginal())(loggingSystem, channel, severity, leafCode, str, args);
}

void CConsoleOutput::Initialize()
{
    auto hooksmanager = g_ifaceService.FetchInterface<IHooksManager>(HOOKSMANAGER_INTERFACE_VERSION);
    auto gamedata = g_ifaceService.FetchInterface<IGameDataManager>(GAMEDATA_INTERFACE_VERSION);

    void* LogDirectAddr = gamedata->GetSignatures()->Fetch("CLoggingSystem::LogDirect");
    if (!LogDirectAddr) return;

    g_CLoggingSystem_LogDirect_Hook = hooksmanager->CreateFunctionHook();
    g_CLoggingSystem_LogDirect_Hook->SetHookFunction(LogDirectAddr, (void*)CLoggingSystem_LogDirectHook);
    g_CLoggingSystem_LogDirect_Hook->Enable();

    ReloadFilterConfiguration();
}

void CConsoleOutput::Shutdown()
{
    auto hooksmanager = g_ifaceService.FetchInterface<IHooksManager>(HOOKSMANAGER_INTERFACE_VERSION);

    if (g_CLoggingSystem_LogDirect_Hook) {
        g_CLoggingSystem_LogDirect_Hook->Disable();
        hooksmanager->DestroyFunctionHook(g_CLoggingSystem_LogDirect_Hook);
        g_CLoggingSystem_LogDirect_Hook = nullptr;
    }
}

void CConsoleOutput::ReloadFilterConfiguration()
{
    static auto logger = g_ifaceService.FetchInterface<ILogger>(LOGGER_INTERFACE_VERSION);
 
    for (auto it = g_Filters.begin(); it != g_Filters.end(); ++it)
        pcre2_code_free(it->second);

    for(auto it = g_FiltersMatchData.begin(); it != g_FiltersMatchData.end(); ++it)
        pcre2_match_data_free(it->second);

    g_Filters.clear();
    g_FiltersMatchData.clear();
    g_FilteredMessages.clear();
    g_FilterNames.clear();

    json filters = json::object();
    filters = parseJsonc(Files::Read(g_SwiftlyCore.GetCorePath() + "/configs/confilter.jsonc"));

    for (auto& [key, value] : filters.items()) {
        pcre2_code* re;
        PCRE2_SIZE erroffset;
        int errorcode;

        re = pcre2_compile((PCRE2_SPTR8)(value.get<std::string>().c_str()), PCRE2_ZERO_TERMINATED, 0, &errorcode, &erroffset, nullptr);
        if (!re) {
            logger->Error("Console Filter", fmt::format("The regex for \"{}\" is not valid.\n", key));
            logger->Error("Console Filter", fmt::format("Failed to compile at offset {}.\n", erroffset));
            continue;
        }

        pcre2_match_data* match_data = pcre2_match_data_create_from_pattern(re, nullptr);
        if(!match_data) {
            logger->Error("Console Filter", fmt::format("Failed to create match data for \"{}\".\n", key));
            pcre2_code_free(re);
            continue;
        }

        g_Filters.insert({ g_filterIds, re });
        g_FiltersMatchData.insert({ g_filterIds, match_data });
        g_FilterNames.insert({ g_filterIds, key });
        g_FilteredMessages.insert({ g_filterIds, 0 });
        g_filterIds++;
    }
}

void CConsoleOutput::ToggleFilter()
{
    g_bEnabled = !g_bEnabled;
}

bool CConsoleOutput::IsEnabled()
{
    return g_bEnabled;
}

bool CConsoleOutput::NeedsFiltering(char* text)
{
    if (!IsEnabled()) return false;

    PCRE2_SPTR str = (PCRE2_SPTR)text;
    PCRE2_SIZE len = (PCRE2_SIZE)strlen(text);

    for (auto it = g_Filters.begin(); it != g_Filters.end(); ++it)
    {
        pcre2_code* re = it->second;
        pcre2_match_data* match_data = g_FiltersMatchData[it->first];

        if (pcre2_match(re, str, len, 0, 0, match_data, nullptr) > 0)
        {
            uint64_t key = it->first;
            g_FilteredMessages[key]++;
            return true;
        }
    }

    return false;
}

std::string CConsoleOutput::GetCounterText()
{
    std::string out;
    for (const auto& [msg, count] : g_FilteredMessages)
        out += "- " + g_FilterNames[msg] + " -> " + std::to_string(count) + "\n";

    return out;
}

uint64_t CConsoleOutput::AddConsoleListener(std::function<void(const std::string&)> callback)
{
    static uint64_t current_id = 0;
    g_ConsoleListeners[current_id] = callback;
    return current_id++;
}

void CConsoleOutput::RemoveConsoleListener(uint64_t id)
{
    if (g_ConsoleListeners.contains(id)) g_ConsoleListeners.erase(id);
}