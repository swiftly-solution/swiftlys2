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

#include <api/interfaces/interfaces.h>
#include <pcre2.h>
#include <fmt/format.h>

#include <api/shared/jsonc.h>
#include <api/shared/files.h>

#include <atomic>
#include <mutex>
#include <thread>
#include <utility>
#include <vector>

using json = nlohmann::json;

std::map<uint64_t, std::function<void(const std::string&)>> g_ConsoleListeners;
std::mutex g_ConsoleListenersMutex;

constexpr uint32_t CONSOLE_FILTER_ENABLED = 1;
constexpr uint32_t CONSOLE_LISTENER_INCREMENT = 2;
std::atomic<uint32_t> g_ConsoleWorkState{ 0 };

std::vector<std::string> g_QueuedConsoleMessages;
std::vector<std::string> g_DispatchedConsoleMessages;
std::mutex g_QueuedConsoleMessagesMutex;
std::mutex g_DispatchConsoleMessagesMutex;
std::atomic<bool> g_HasQueuedConsoleMessages{ false };
std::thread g_ConsoleDispatchThread;
std::atomic<bool> g_ConsoleDispatchRunning{ false };

std::map<uint64_t, pcre2_code*> g_Filters;
std::map<uint64_t, pcre2_match_data*> g_FiltersMatchData;

uint64_t g_filterIds = 1;

std::map<uint64_t, std::string> g_FilterNames;
std::map<uint64_t, uint64_t> g_FilteredMessages;
std::atomic<bool> skipNextNewlineOnlyLog{ false };

IFunctionHook* g_CLoggingSystem_LogDirect_Hook = nullptr;

#if defined(_MSC_VER)
#define SWIFTLY_CONSOLE_NOINLINE __declspec(noinline)
#else
#define SWIFTLY_CONSOLE_NOINLINE __attribute__((noinline))
#endif

static SWIFTLY_CONSOLE_NOINLINE bool FilterOrQueueConsoleMessage(
    const char* str,
    va_list* args,
    bool filterEnabled,
    bool hasListeners
)
{
    char buf[MAX_LOGGING_MESSAGE_LENGTH];
    const char* message = str;
    if (args) {
        va_list cpargs;
        va_copy(cpargs, *args);
        V_vsnprintf(buf, sizeof(buf), str, cpargs);
        va_end(cpargs);
        message = buf;
    }

    if (filterEnabled && g_pConsoleOutput->NeedsFiltering(const_cast<char*>(message))) return true;

    bool wakeDispatcher = false;
    if (hasListeners)
    {
        std::lock_guard<std::mutex> lock(g_QueuedConsoleMessagesMutex);

        // A listener may have been removed after the fast-path load.
        if (g_ConsoleWorkState.load(std::memory_order_relaxed) >= CONSOLE_LISTENER_INCREMENT)
        {
            wakeDispatcher = g_QueuedConsoleMessages.empty();
            g_QueuedConsoleMessages.emplace_back(message);
            if (wakeDispatcher)
                g_HasQueuedConsoleMessages.store(true, std::memory_order_release);
        }
    }

    // One wake-up is enough for a complete batch. Avoid notifying the worker for
    // every message while producers are already filling a non-empty queue.
    if (wakeDispatcher)
        g_HasQueuedConsoleMessages.notify_one();

    return false;
}

static void ConsoleDispatchWorker()
{
    while (g_ConsoleDispatchRunning.load(std::memory_order_acquire))
    {
        g_HasQueuedConsoleMessages.wait(false, std::memory_order_acquire);

        if (!g_ConsoleDispatchRunning.load(std::memory_order_acquire))
            break;

        g_pConsoleOutput->DispatchQueuedListeners();
    }
}

#undef SWIFTLY_CONSOLE_NOINLINE

int CLoggingSystem_LogDirectHook(void* loggingSystem, int channel, int severity, LeafCodeInfo_t* leafCode, char const* str, va_list* args)
{
    const auto original = reinterpret_cast<decltype(&CLoggingSystem_LogDirectHook)>(g_CLoggingSystem_LogDirect_Hook->GetOriginal());

    if (!str)
        return original(loggingSystem, channel, severity, leafCode, str, args);

    if (skipNextNewlineOnlyLog.load(std::memory_order_relaxed) && strcmp(str, "\n") == 0) {
        skipNextNewlineOnlyLog.store(false, std::memory_order_relaxed);
        return 0;
    }

    const uint32_t workState = g_ConsoleWorkState.load(std::memory_order_relaxed);
    const bool filterEnabled = (workState & CONSOLE_FILTER_ENABLED) != 0;
    const bool hasListeners = workState >= CONSOLE_LISTENER_INCREMENT;

    // The overwhelmingly common no-consumer path must not format, allocate, copy, or lock.
    if (!filterEnabled && !hasListeners) [[likely]]
        return original(loggingSystem, channel, severity, leafCode, str, args);

    if (FilterOrQueueConsoleMessage(str, args, filterEnabled, hasListeners)) return 0;

    return original(loggingSystem, channel, severity, leafCode, str, args);
}

void CConsoleOutput::Initialize()
{
    void* LogDirectAddr = g_pGameDataManager->GetSignatures()->Fetch("CLoggingSystem::LogDirect");
    if (!LogDirectAddr) return;

    g_CLoggingSystem_LogDirect_Hook = g_pHooksManager->CreateFunctionHook();
    g_CLoggingSystem_LogDirect_Hook->SetHookFunction(LogDirectAddr, (void*)CLoggingSystem_LogDirectHook);
    g_CLoggingSystem_LogDirect_Hook->Enable();

    ReloadFilterConfiguration();

    g_ConsoleDispatchRunning.store(true, std::memory_order_release);
    g_ConsoleDispatchThread = std::thread(ConsoleDispatchWorker);
}

void CConsoleOutput::Shutdown()
{
    if (g_CLoggingSystem_LogDirect_Hook) {
        g_CLoggingSystem_LogDirect_Hook->Disable();
        g_pHooksManager->DestroyFunctionHook(g_CLoggingSystem_LogDirect_Hook);
        g_CLoggingSystem_LogDirect_Hook = nullptr;
    }

    g_ConsoleDispatchRunning.store(false, std::memory_order_release);
    g_HasQueuedConsoleMessages.store(true, std::memory_order_release);
    g_HasQueuedConsoleMessages.notify_one();
    if (g_ConsoleDispatchThread.joinable())
        g_ConsoleDispatchThread.join();

    {
        std::scoped_lock lock(g_DispatchConsoleMessagesMutex, g_QueuedConsoleMessagesMutex);
        g_QueuedConsoleMessages.clear();
        g_DispatchedConsoleMessages.clear();
        g_HasQueuedConsoleMessages.store(false, std::memory_order_release);
    }
}

void CConsoleOutput::ReloadFilterConfiguration()
{
    for (auto it = g_Filters.begin(); it != g_Filters.end(); ++it)
        pcre2_code_free(it->second);

    for (auto it = g_FiltersMatchData.begin(); it != g_FiltersMatchData.end(); ++it)
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
            g_pLogger->Error("Console Filter", fmt::format("The regex for \"{}\" is not valid.\n", key));
            g_pLogger->Error("Console Filter", fmt::format("Failed to compile at offset {}.\n", erroffset));
            continue;
        }

        pcre2_match_data* match_data = pcre2_match_data_create_from_pattern(re, nullptr);
        if (!match_data) {
            g_pLogger->Error("Console Filter", fmt::format("Failed to create match data for \"{}\".\n", key));
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
    g_ConsoleWorkState.fetch_xor(CONSOLE_FILTER_ENABLED, std::memory_order_relaxed);
}

bool CConsoleOutput::IsEnabled()
{
    return (g_ConsoleWorkState.load(std::memory_order_relaxed) & CONSOLE_FILTER_ENABLED) != 0;
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

            skipNextNewlineOnlyLog.store(
                g_FilterNames[key] == "Framerate_Values" || g_FilterNames[key] == "Framerate_Total",
                std::memory_order_relaxed
            );

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
    std::lock_guard<std::mutex> lock(g_ConsoleListenersMutex);

    const uint64_t id = current_id++;
    g_ConsoleListeners.emplace(id, std::move(callback));
    g_ConsoleWorkState.fetch_add(CONSOLE_LISTENER_INCREMENT, std::memory_order_relaxed);
    return id;
}

void CConsoleOutput::RemoveConsoleListener(uint64_t id)
{
    bool removedLastListener = false;
    {
        std::lock_guard<std::mutex> lock(g_ConsoleListenersMutex);
        if (g_ConsoleListeners.erase(id) == 0) return;

        const uint32_t previousState = g_ConsoleWorkState.fetch_sub(CONSOLE_LISTENER_INCREMENT, std::memory_order_relaxed);
        removedLastListener = previousState < CONSOLE_LISTENER_INCREMENT * 2;
    }

    if (removedLastListener)
    {
        std::lock_guard<std::mutex> lock(g_QueuedConsoleMessagesMutex);
        g_QueuedConsoleMessages.clear();
        g_HasQueuedConsoleMessages.store(false, std::memory_order_release);
    }
}

void CConsoleOutput::DispatchQueuedListeners()
{
    // The idle dispatcher path is a single atomic load with no mutex acquisition.
    if (!g_HasQueuedConsoleMessages.load(std::memory_order_acquire)) return;

    // The worker is the normal consumer. An explicit command-output flush may race it;
    // wait for the active batch so the capture window cannot close too early.
    std::unique_lock<std::mutex> dispatchLock(g_DispatchConsoleMessagesMutex);

    // Avoid taking the queue mutex when no console messages were produced.
    if (!g_HasQueuedConsoleMessages.exchange(false, std::memory_order_acq_rel)) return;

    {
        std::lock_guard<std::mutex> lock(g_QueuedConsoleMessagesMutex);
        g_QueuedConsoleMessages.swap(g_DispatchedConsoleMessages);
    }

    if (g_DispatchedConsoleMessages.empty()) return;

    std::vector<std::function<void(const std::string&)>> listeners;
    {
        std::lock_guard<std::mutex> lock(g_ConsoleListenersMutex);
        listeners.reserve(g_ConsoleListeners.size());
        for (const auto& [id, callback] : g_ConsoleListeners)
            listeners.emplace_back(callback);
    }

    for (const auto& message : g_DispatchedConsoleMessages)
        for (const auto& callback : listeners)
            callback(message);

    // Keep the allocation for the next buffer swap while releasing message payloads now.
    g_DispatchedConsoleMessages.clear();
}
