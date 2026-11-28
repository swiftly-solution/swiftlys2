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

#include "tracer_profile.pb.h"

#include <algorithm>
#include <cstdio>
#include <map>
#include <set>
#include <string>
#include <unordered_map>
#include <vector>

namespace profile_pb = swiftlys2::tracer;

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

struct TraceEntry
{
    const Event* event;
    uint64_t endNs;
    bool open;
    bool allocation;
};

static std::vector<TraceEntry> BuildTraceEntries(const std::vector<Event>& events, std::set<uint32_t>& threads)
{
    std::vector<TraceEntry> entries;
    const uint64_t endOfTrace = events.empty() ? 0 : events.back().timestampNs;
    std::map<uint32_t, std::vector<const Event*>> stacks;

    auto closeSpan = [&](std::vector<const Event*>& stack, uint64_t endNs, auto&& match)
    {
        for (size_t i = stack.size(); i-- > 0;)
        {
            if (!match(*stack[i]))
                continue;
            while (stack.size() > i)
            {
                entries.push_back({stack.back(), (std::max)(endNs, stack.back()->timestampNs), false, false});
                stack.pop_back();
            }
            return;
        }
    };

    for (const Event& e : events)
    {
        threads.insert(e.osThreadId);
        auto& stack = stacks[e.osThreadId];

        switch (e.type)
        {
        case EventType::FunctionEnter:
        case EventType::UnmanagedToManaged:
        case EventType::CustomBegin:
            stack.push_back(&e);
            break;
        case EventType::FunctionExit:
        case EventType::FunctionTailcall:
            closeSpan(stack, e.timestampNs, [&](const Event& b) { return b.type == EventType::FunctionEnter && b.a == e.a; });
            break;
        case EventType::ManagedToUnmanaged:
            closeSpan(stack, e.timestampNs, [&](const Event& b) { return b.type == EventType::UnmanagedToManaged && b.a == e.a; });
            break;
        case EventType::CustomEnd:
            closeSpan(stack, e.timestampNs, [&](const Event& b) { return b.type == EventType::CustomBegin && b.nameId == e.nameId; });
            break;
        case EventType::ObjectAllocated:
            entries.push_back({&e, 0, false, true});
            break;
        default:
            break;
        }
    }

    for (auto& [tid, stack] : stacks)
    {
        while (!stack.empty())
        {
            entries.push_back({stack.back(), (std::max)(endOfTrace, stack.back()->timestampNs), true, false});
            stack.pop_back();
        }
    }

    return entries;
}

static profile_pb::Span::Category SpanCategory(EventType type)
{
    switch (type)
    {
    case EventType::UnmanagedToManaged:
        return profile_pb::Span::NATIVE_TO_MANAGED;
    case EventType::CustomBegin:
        return profile_pb::Span::CUSTOM;
    default:
        return profile_pb::Span::MANAGED;
    }
}

static bool WriteBinaryProfile(const std::string& relativePath, const std::vector<TraceEntry>& entries, const std::set<uint32_t>& threads)
{
    FILE* file = std::fopen(Files::GeneratePath(relativePath).c_str(), "wb");
    if (!file)
        return false;

    constexpr size_t flushSize = 4 * 1024 * 1024;
    bool ok = true;
    size_t pending = 0;
    uint32_t stringCount = 1;
    std::string data;

    profile_pb::Profile profile;
    profile.set_version(1);
    profile.add_strings("");

    std::unordered_map<std::string, uint32_t> ids;
    auto intern = [&](const char* value) -> uint32_t
    {
        if (!value || !*value)
            return 0;

        const auto [it, inserted] = ids.try_emplace(value, stringCount);
        if (inserted)
        {
            profile.add_strings(value);
            pending += it->first.size() + 2;
            ++stringCount;
        }
        return it->second;
    };

    auto flush = [&](bool force)
    {
        if (!force && pending < flushSize)
            return;
        profile.SerializeToString(&data);
        ok = std::fwrite(data.data(), 1, data.size(), file) == data.size() && ok;
        profile.Clear();
        pending = 0;
    };

    for (const TraceEntry& entry : entries)
    {
        const Event& e = *entry.event;

        if (entry.allocation)
        {
            profile_pb::Allocation* alloc = profile.add_allocations();
            alloc->set_type_name(intern(g_pTracer->GetString(e.nameId)));
            alloc->set_os_thread_id(e.osThreadId);
            alloc->set_timestamp_ns(e.timestampNs);
            alloc->set_size(e.b);
            alloc->set_function(intern(g_pTracer->GetString(e.sourceId)));
            pending += alloc->ByteSizeLong() + 2;
        }
        else
        {
            profile_pb::Span* span = profile.add_spans();
            span->set_name(intern(g_pTracer->GetString(e.nameId)));
            span->set_category(SpanCategory(e.type));
            span->set_os_thread_id(e.osThreadId);
            span->set_start_ns(e.timestampNs);
            span->set_duration_ns(entry.endNs - e.timestampNs);
            span->set_source(intern(g_pTracer->GetString(e.sourceId)));
            span->set_open(entry.open);
            pending += span->ByteSizeLong() + 2;
        }

        flush(false);
    }

    for (uint32_t tid : threads)
    {
        const char* name = g_pTracer->GetThreadName(tid);
        if (!*name)
            continue;
        profile_pb::ThreadName* thread = profile.add_threads();
        thread->set_os_thread_id(tid);
        thread->set_name(intern(name));
    }

    flush(true);
    return std::fclose(file) == 0 && ok;
}

static bool SaveProfileToFile(const char* path)
{
    if (!g_pTracer || !path || !*path)
        return false;

    const size_t total = g_pTracer->TakeProfileSnapshot(nullptr, 0);
    std::vector<Event> events(total);
    events.resize((std::min)(total, g_pTracer->TakeProfileSnapshot(events.data(), events.size())));
    std::stable_sort(events.begin(), events.end(), [](const Event& l, const Event& r) { return l.timestampNs < r.timestampNs; });

    std::set<uint32_t> threads;
    const std::vector<TraceEntry> entries = BuildTraceEntries(events, threads);

    const std::string file = GetRelativePath(path);
    return WriteBinaryProfile(file, entries, threads);
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
