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
#include <memory/hooks/function.h>

#include <core/entrypoint.h>

#include <api/interfaces/interfaces.h>
#include <pcre2.h>
#include <fmt/format.h>

#include <api/shared/jsonc.h>
#include <api/shared/files.h>

#include <utility>

using json = nlohmann::json;

extern CConsoleOutput g_ConsoleOutput;

int CLoggingSystem_LogDirectHook(void* loggingSystem, int channel, int severity, LeafCodeInfo_t* leafCode, char const* str, va_list* args);
FunctionHook g_CLoggingSystem_LogDirect_Hook(reinterpret_cast<void*>(CLoggingSystem_LogDirectHook), "CLoggingSystem::LogDirect");

int CLoggingSystem_LogDirectHook(void* loggingSystem, int channel, int severity, LeafCodeInfo_t* leafCode, char const* str, va_list* args)
{
    const auto original = reinterpret_cast<decltype(&CLoggingSystem_LogDirectHook)>(g_CLoggingSystem_LogDirect_Hook.GetOriginal());

    if (str && g_ConsoleOutput.ProcessLogMessage(str, args))
        return 0;

    return original(loggingSystem, channel, severity, leafCode, str, args);
}

struct ConsoleFilter
{
    std::string name;
    pcre2_code* code = nullptr;
    pcre2_match_data* matchData = nullptr;
    uint64_t hits = 0;

    ConsoleFilter(std::string filterName, pcre2_code* filterCode, pcre2_match_data* filterMatchData)
        : name(std::move(filterName)), code(filterCode), matchData(filterMatchData)
    {
    }

    ConsoleFilter(const ConsoleFilter&) = delete;
    ConsoleFilter& operator=(const ConsoleFilter&) = delete;

    ~ConsoleFilter()
    {
        pcre2_match_data_free(matchData);
        pcre2_code_free(code);
    }

    bool Matches(PCRE2_SPTR text, PCRE2_SIZE length) const
    {
        return pcre2_match(code, text, length, 0, 0, matchData, nullptr) > 0;
    }

    bool SuppressesFollowingNewline() const
    {
        return name == "Framerate_Values" || name == "Framerate_Total";
    }
};

CConsoleOutput::CConsoleOutput() = default;
CConsoleOutput::~CConsoleOutput() = default;

struct ConsoleDispatchScope
{
    CConsoleOutput* output;
    const ConsoleDispatchScope* previous;
    static inline thread_local const ConsoleDispatchScope* active = nullptr;

    explicit ConsoleDispatchScope(CConsoleOutput* instance) : output(instance), previous(active) { active = this; }
    ~ConsoleDispatchScope() { active = previous; }

    static bool Contains(const CConsoleOutput* instance)
    {
        for (auto scope = active; scope; scope = scope->previous)
            if (scope->output == instance) return true;
        return false;
    }
};

bool CConsoleOutput::ProcessLogMessage(const char* str, va_list* args)
{
    if (m_SkipNextNewlineOnlyLog.load(std::memory_order_relaxed) && strcmp(str, "\n") == 0) {
        m_SkipNextNewlineOnlyLog.store(false, std::memory_order_relaxed);
        return true;
    }

    const uint32_t workState = m_WorkState.load(std::memory_order_relaxed);
    const bool filterEnabled = (workState & FILTER_ENABLED) != 0;
    const bool hasListeners = workState >= LISTENER_INCREMENT;

    if (!filterEnabled && !hasListeners) [[likely]]
        return false;

    return FilterOrQueueMessage(str, args, filterEnabled, hasListeners);
}

#if defined(_MSC_VER)
#define SWIFTLY_CONSOLE_NOINLINE __declspec(noinline)
#else
#define SWIFTLY_CONSOLE_NOINLINE __attribute__((noinline))
#endif

SWIFTLY_CONSOLE_NOINLINE bool CConsoleOutput::FilterOrQueueMessage(const char* str, va_list* args, bool filterEnabled, bool hasListeners)
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

    if (filterEnabled && NeedsFiltering(const_cast<char*>(message))) return true;

    bool wakeDispatcher = false;
    if (hasListeners)
    {
        std::lock_guard<std::mutex> lock(m_QueueMutex);

        // A listener may have been removed after the fast-path load.
        if (m_WorkState.load(std::memory_order_relaxed) >= LISTENER_INCREMENT)
        {
            wakeDispatcher = m_QueuedMessages.empty();
            m_QueuedMessages.emplace_back(message);
            if (wakeDispatcher)
                m_HasQueuedMessages.store(true, std::memory_order_release);
        }
    }

    // One wake-up is enough for a complete batch. Avoid notifying the worker for
    // every message while producers are already filling a non-empty queue.
    if (wakeDispatcher)
        m_HasQueuedMessages.notify_one();

    return false;
}

#undef SWIFTLY_CONSOLE_NOINLINE

void CConsoleOutput::DispatchWorker()
{
    while (m_DispatchRunning.load(std::memory_order_acquire))
    {
        m_HasQueuedMessages.wait(false, std::memory_order_acquire);

        if (!m_DispatchRunning.load(std::memory_order_acquire))
            break;

        DispatchQueuedListeners();
    }
}

void CConsoleOutput::Initialize()
{
    ReloadFilterConfiguration();

    m_DispatchRunning.store(true, std::memory_order_release);
    m_DispatchThread = std::thread(&CConsoleOutput::DispatchWorker, this);
}

void CConsoleOutput::Shutdown()
{
    m_DispatchRunning.store(false, std::memory_order_release);
    m_HasQueuedMessages.store(true, std::memory_order_release);
    m_HasQueuedMessages.notify_one();
    if (m_DispatchThread.joinable())
        m_DispatchThread.join();

    std::scoped_lock lock(m_DispatchMutex, m_QueueMutex);
    m_QueuedMessages.clear();
    m_DispatchedMessages.clear();
    m_HasQueuedMessages.store(false, std::memory_order_release);
}

void CConsoleOutput::ReloadFilterConfiguration()
{
    json filters = parseJsonc(Files::Read(g_SwiftlyCore.GetCorePath() + "/configs/confilter.jsonc"));

    std::map<uint64_t, std::unique_ptr<ConsoleFilter>> compiled;
    uint64_t nextId = m_NextFilterId;

    for (auto& [key, value] : filters.items()) {
        PCRE2_SIZE erroffset;
        int errorcode;

        pcre2_code* code = pcre2_compile((PCRE2_SPTR8)(value.get<std::string>().c_str()), PCRE2_ZERO_TERMINATED, 0, &errorcode, &erroffset, nullptr);
        if (!code) {
            g_pLogger->Error("Console Filter", fmt::format("The regex for \"{}\" is not valid.\n", key));
            g_pLogger->Error("Console Filter", fmt::format("Failed to compile at offset {}.\n", erroffset));
            continue;
        }

        pcre2_jit_compile(code, PCRE2_JIT_COMPLETE);

        pcre2_match_data* matchData = pcre2_match_data_create_from_pattern(code, nullptr);
        if (!matchData) {
            g_pLogger->Error("Console Filter", fmt::format("Failed to create match data for \"{}\".\n", key));
            pcre2_code_free(code);
            continue;
        }

        compiled.emplace(nextId++, std::make_unique<ConsoleFilter>(key, code, matchData));
    }

    std::lock_guard<std::mutex> lock(m_FiltersMutex);
    m_Filters = std::move(compiled);
    m_NextFilterId = nextId;
}

void CConsoleOutput::ToggleFilter()
{
    m_WorkState.fetch_xor(FILTER_ENABLED, std::memory_order_relaxed);
}

bool CConsoleOutput::IsEnabled()
{
    return (m_WorkState.load(std::memory_order_relaxed) & FILTER_ENABLED) != 0;
}

bool CConsoleOutput::NeedsFiltering(char* text)
{
    if (!IsEnabled()) return false;

    const PCRE2_SPTR str = (PCRE2_SPTR)text;
    const PCRE2_SIZE len = (PCRE2_SIZE)strlen(text);

    std::lock_guard<std::mutex> lock(m_FiltersMutex);

    for (auto& [id, filter] : m_Filters)
    {
        if (!filter->Matches(str, len)) continue;

        filter->hits++;
        m_SkipNextNewlineOnlyLog.store(filter->SuppressesFollowingNewline(), std::memory_order_relaxed);
        return true;
    }

    return false;
}

std::string CConsoleOutput::GetCounterText()
{
    std::lock_guard<std::mutex> lock(m_FiltersMutex);

    std::string out;
    for (const auto& [id, filter] : m_Filters)
        out += "- " + filter->name + " -> " + std::to_string(filter->hits) + "\n";

    return out;
}

uint64_t CConsoleOutput::AddConsoleListener(std::function<void(const std::string&)> callback)
{
    std::lock_guard<std::mutex> lock(m_ListenersMutex);

    const uint64_t id = m_NextListenerId++;
    m_Listeners.emplace(id, std::move(callback));
    m_WorkState.fetch_add(LISTENER_INCREMENT, std::memory_order_relaxed);
    return id;
}

void CConsoleOutput::RemoveConsoleListener(uint64_t id)
{
    bool removedLastListener = false;
    {
        std::lock_guard<std::mutex> lock(m_ListenersMutex);
        if (m_Listeners.erase(id) == 0) return;

        const uint32_t previousState = m_WorkState.fetch_sub(LISTENER_INCREMENT, std::memory_order_relaxed);
        removedLastListener = previousState < LISTENER_INCREMENT * 2;
    }

    if (removedLastListener)
    {
        std::lock_guard<std::mutex> lock(m_QueueMutex);
        if (m_WorkState.load(std::memory_order_relaxed) < LISTENER_INCREMENT)
            m_QueuedMessages.clear();
    }
}

void CConsoleOutput::DispatchQueuedListeners()
{
    // The idle dispatcher path is a single atomic load with no mutex acquisition.
    if (!m_HasQueuedMessages.load(std::memory_order_acquire)) return;
    if (ConsoleDispatchScope::Contains(this)) return;

    // The worker is the normal consumer. An explicit command-output flush may race it;
    // wait for the active batch so the capture window cannot close too early.
    std::lock_guard<std::mutex> dispatchLock(m_DispatchMutex);
    ConsoleDispatchScope scope(this);

    if (!m_HasQueuedMessages.load(std::memory_order_acquire)) return;

    {
        std::lock_guard<std::mutex> lock(m_QueueMutex);
        m_QueuedMessages.swap(m_DispatchedMessages);
        if (m_DispatchedMessages.empty())
        {
            m_HasQueuedMessages.store(false, std::memory_order_release);
            return;
        }
    }

    std::vector<std::function<void(const std::string&)>> listeners;
    {
        std::lock_guard<std::mutex> lock(m_ListenersMutex);
        listeners.reserve(m_Listeners.size());
        for (const auto& [id, callback] : m_Listeners)
            listeners.emplace_back(callback);
    }

    for (const auto& message : m_DispatchedMessages)
        for (const auto& callback : listeners)
            callback(message);

    // Keep the allocation for the next buffer swap while releasing message payloads now.
    m_DispatchedMessages.clear();

    // Keep the flag set through callbacks so another thread's flush waits for this batch.
    std::lock_guard<std::mutex> lock(m_QueueMutex);
    m_HasQueuedMessages.store(!m_QueuedMessages.empty(), std::memory_order_release);
}
