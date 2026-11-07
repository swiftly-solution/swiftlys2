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

#include "profiler.h"

#ifdef PROFILER

#include <api/shared/files.h>

#include <algorithm>
#include <cstdint>
#include <format>
#include <iterator>
#include <map>
#include <mutex>
#include <tuple>
#include <unordered_map>
#include <vector>

struct ProfilerStat
{
    uint64_t calls = 0;
    uint64_t totalNs = 0;
    uint64_t selfNs = 0;
    uint64_t minNs = UINT64_MAX;
    uint64_t maxNs = 0;

    void Add(const ProfilerStat& other)
    {
        calls += other.calls;
        totalNs += other.totalNs;
        selfNs += other.selfNs;
        minNs = (std::min)(minNs, other.minNs);
        maxNs = (std::max)(maxNs, other.maxNs);
    }
};

struct ProfilerKey
{
    const char* name;
    const char* file;
    uint_least32_t line;

    bool operator==(const ProfilerKey&) const = default;
};

static std::size_t HashCombine(std::size_t seed, std::size_t value) noexcept
{
    return seed ^ (value + 0x9e3779b97f4a7c15ULL + (seed << 6) + (seed >> 2));
}

struct ProfilerKeyHash
{
    std::size_t operator()(const ProfilerKey& key) const noexcept
    {
        std::size_t hash = std::hash<const void*>{}(key.name);
        hash = HashCombine(hash, std::hash<const void*>{}(key.file));
        return HashCombine(hash, std::hash<uint_least32_t>{}(key.line));
    }
};

struct ProfilerEdgeKey
{
    ProfilerKey caller;
    ProfilerKey callee;

    bool operator==(const ProfilerEdgeKey&) const = default;
};

struct ProfilerEdgeKeyHash
{
    std::size_t operator()(const ProfilerEdgeKey& key) const noexcept
    {
        return HashCombine(ProfilerKeyHash{}(key.caller), ProfilerKeyHash{}(key.callee));
    }
};

struct ProfilerThreadData
{
    std::mutex mutex;
    std::unordered_map<ProfilerKey, ProfilerStat, ProfilerKeyHash> stats;
    std::unordered_map<ProfilerEdgeKey, ProfilerStat, ProfilerEdgeKeyHash> edges;
    ProfilerScope* current = nullptr; // innermost live scope, only touched by the owning thread
};

struct ProfilerRegistry
{
    std::mutex mutex;
    std::vector<ProfilerThreadData*> threads;
};

static ProfilerRegistry& GetRegistry()
{
    static ProfilerRegistry& registry = *new ProfilerRegistry;
    return registry;
}

static ProfilerThreadData& GetThreadData()
{
    thread_local ProfilerThreadData* data = nullptr;
    if (!data)
    {
        data = new ProfilerThreadData;
        auto& registry = GetRegistry();
        std::lock_guard lock(registry.mutex);
        registry.threads.push_back(data);
    }
    return *data;
}

ProfilerScope::ProfilerScope(const char* name, std::source_location location)
    : m_name(name ? name : location.function_name()), m_file(location.file_name()), m_line(location.line())
{
    Resume();
}

void ProfilerScope::Resume()
{
    if (m_active) return;

    auto& data = GetThreadData();
    m_parent = data.current;
    data.current = this;

    m_active = true;
    m_start = std::chrono::steady_clock::now();
}

void ProfilerScope::Pause()
{
    if (!m_active) return;

    m_elapsedNs += static_cast<uint64_t>(std::chrono::duration_cast<std::chrono::nanoseconds>(std::chrono::steady_clock::now() - m_start).count());
    GetThreadData().current = m_parent;
    m_active = false;
}

ProfilerScope::~ProfilerScope()
{
    Pause();

    const uint64_t elapsed = m_elapsedNs;
    auto& data = GetThreadData();

    const uint64_t self = elapsed > m_childNs ? elapsed - m_childNs : 0;

    std::lock_guard lock(data.mutex);

    ProfilerStat& stat = data.stats[{m_name, m_file, m_line}];
    ++stat.calls;
    stat.totalNs += elapsed;
    stat.selfNs += self;
    stat.minNs = (std::min)(stat.minNs, elapsed);
    stat.maxNs = (std::max)(stat.maxNs, elapsed);

    if (m_parent)
    {
        m_parent->m_childNs += elapsed;

        ProfilerStat& edge = data.edges[{{m_parent->m_name, m_parent->m_file, m_parent->m_line}, {m_name, m_file, m_line}}];
        ++edge.calls;
        edge.totalNs += elapsed;
    }
}

using ProfilerSite = std::tuple<std::string, std::string, uint_least32_t>;

static ProfilerSite ToSite(const ProfilerKey& key)
{
    return {key.name, key.file, key.line};
}

static double Percent(uint64_t part, uint64_t whole)
{
    return whole ? part * 100.0 / static_cast<double>(whole) : 0.0;
}

void Profiler::Save(const std::string& path)
{
    std::map<ProfilerSite, ProfilerStat> merged;
    std::map<std::pair<ProfilerSite, ProfilerSite>, ProfilerStat> mergedEdges;
    {
        auto& registry = GetRegistry();
        std::lock_guard threadsLock(registry.mutex);
        for (const auto& thread : registry.threads)
        {
            std::lock_guard lock(thread->mutex);
            for (const auto& [key, stat] : thread->stats)
                merged[ToSite(key)].Add(stat);
            for (const auto& [key, stat] : thread->edges)
                mergedEdges[{ToSite(key.caller), ToSite(key.callee)}].Add(stat);
        }
    }

    std::vector<std::pair<ProfilerSite, ProfilerStat>> rows(merged.begin(), merged.end());
    std::ranges::sort(rows, [](const auto& lhs, const auto& rhs) { return lhs.second.selfNs > rhs.second.selfNs; });

    std::map<ProfilerSite, std::vector<std::pair<ProfilerSite, ProfilerStat>>> callees;
    for (const auto& [edge, stat] : mergedEdges)
        callees[edge.first].emplace_back(edge.second, stat);
    for (auto& [caller, list] : callees)
        std::ranges::sort(list, [](const auto& lhs, const auto& rhs) { return lhs.second.totalNs > rhs.second.totalNs; });

    uint64_t allSelfNs = 0;
    for (const auto& [site, stat] : rows)
        allSelfNs += stat.selfNs;

    std::string report = "Flat profile, sorted by self time\n";
    std::format_to(std::back_inserter(report), "{:<80} {:<50} {:>10} {:>14} {:>14} {:>8} {:>12} {:>12} {:>12}\n",
        "Function", "Location", "Calls", "Total (ms)", "Self (ms)", "Self %", "Avg (us)", "Min (us)", "Max (us)");
    for (const auto& [site, stat] : rows)
    {
        const auto& [name, file, line] = site;
        std::format_to(std::back_inserter(report), "{:<80} {:<50} {:>10} {:>14.3f} {:>14.3f} {:>7.1f}% {:>12.3f} {:>12.3f} {:>12.3f}\n",
            name, std::format("{}:{}", file, line), stat.calls, stat.totalNs / 1e6, stat.selfNs / 1e6, Percent(stat.selfNs, allSelfNs),
            stat.totalNs / static_cast<double>(stat.calls) / 1e3, stat.minNs / 1e3, stat.maxNs / 1e3);
    }

    report += "\nCall breakdown, sorted by self time (percent is of the caller's total)\n";
    for (const auto& [site, stat] : rows)
    {
        const auto& [name, file, line] = site;
        std::format_to(std::back_inserter(report), "\n{}  [{}:{}]  total {:.3f} ms, self {:.3f} ms\n", name, file, line, stat.totalNs / 1e6, stat.selfNs / 1e6);

        if (const auto it = callees.find(site); it != callees.end())
        {
            for (const auto& [calleeSite, calleeStat] : it->second)
            {
                const auto& [calleeName, calleeFile, calleeLine] = calleeSite;
                std::format_to(std::back_inserter(report), "    -> {:<73} {:<50} {:>10} {:>14.3f} {:>7.1f}%\n",
                    calleeName, std::format("{}:{}", calleeFile, calleeLine), calleeStat.calls, calleeStat.totalNs / 1e6, Percent(calleeStat.totalNs, stat.totalNs));
            }
        }

        std::format_to(std::back_inserter(report), "    {:<77} {:<50} {:>10} {:>14.3f} {:>7.1f}%\n", "(self)", "", "", stat.selfNs / 1e6, Percent(stat.selfNs, stat.totalNs));
    }

    Files::Write(path, report, false);
}

#endif
