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

#include <nlohmann/json.hpp>

#include <algorithm>
#include <format>
#include <map>
#include <set>
#include <vector>

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

static std::string JsonString(const char* value)
{
    return nlohmann::json(value ? value : "").dump(-1, ' ', false, nlohmann::json::error_handler_t::replace);
}

static void WriteSpan(std::string& out, bool& first, const Event& begin, uint64_t endNs, bool open)
{
    const char* cat = "managed";
    const char* argKey = "module";
    if (begin.type == EventType::UnmanagedToManaged)
        cat = "native-to-managed";
    else if (begin.type == EventType::CustomBegin)
    {
        cat = "custom";
        argKey = "source";
    }

    const char* name = g_pTracer->GetString(begin.nameId);
    const char* source = g_pTracer->GetString(begin.sourceId);

    out += std::format("{}{{\"name\":{},\"cat\":\"{}\",\"ph\":\"X\",\"pid\":1,\"tid\":{},\"ts\":{:.3f},\"dur\":{:.3f},\"args\":{{", first ? "\n" : ",\n", JsonString(*name ? name : "<unknown>"), cat, begin.osThreadId, begin.timestampNs / 1000.0,
                       (endNs - begin.timestampNs) / 1000.0);
    first = false;
    if (*source)
        out += std::format("\"{}\":{}", argKey, JsonString(source));
    if (open)
        out += *source ? ",\"open\":true" : "\"open\":true";
    out += "}}";
}

static bool SaveProfileToFile(const char* path)
{
    if (!g_pTracer || !path || !*path)
        return false;

    const size_t total = g_pTracer->TakeProfileSnapshot(nullptr, 0);
    std::vector<Event> events(total);
    events.resize((std::min)(total, g_pTracer->TakeProfileSnapshot(events.data(), events.size())));
    std::stable_sort(events.begin(), events.end(), [](const Event& l, const Event& r) { return l.timestampNs < r.timestampNs; });

    const std::string file = GetRelativePath(path);
    Files::Write(file, "{\"displayTimeUnit\":\"ms\",\"traceEvents\":[", false);
    if (!Files::ExistsPath(file))
        return false;

    constexpr size_t flushSize = 4 * 1024 * 1024;
    std::string out;
    out.reserve(flushSize + 4096);
    auto flush = [&](bool force)
    {
        if (out.empty() || (!force && out.size() < flushSize))
            return;
        Files::Append(file, out, false);
        out.clear();
    };

    bool first = true;
    const uint64_t endOfTrace = events.empty() ? 0 : events.back().timestampNs;
    std::map<uint32_t, std::vector<const Event*>> stacks;
    std::set<uint32_t> threads;

    auto closeSpan = [&](std::vector<const Event*>& stack, uint64_t endNs, auto&& match)
    {
        for (size_t i = stack.size(); i-- > 0;)
        {
            if (!match(*stack[i]))
                continue;
            while (stack.size() > i)
            {
                WriteSpan(out, first, *stack.back(), (std::max)(endNs, stack.back()->timestampNs), false);
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
            out += std::format("{}{{\"name\":{},\"cat\":\"alloc\",\"ph\":\"i\",\"s\":\"t\",\"pid\":1,\"tid\":{},\"ts\":{:.3f},\"args\":{{\"size\":{},\"function\":{}}}}}", first ? "\n" : ",\n", JsonString(g_pTracer->GetString(e.nameId)), e.osThreadId,
                               e.timestampNs / 1000.0, e.b, JsonString(g_pTracer->GetString(e.sourceId)));
            first = false;
            break;
        default:
            break;
        }
        flush(false);
    }

    for (auto& [tid, stack] : stacks)
    {
        while (!stack.empty())
        {
            WriteSpan(out, first, *stack.back(), (std::max)(endOfTrace, stack.back()->timestampNs), true);
            stack.pop_back();
            flush(false);
        }
    }

    for (uint32_t tid : threads)
    {
        const char* name = g_pTracer->GetThreadName(tid);
        if (!*name)
            continue;
        out += std::format("{}{{\"name\":\"thread_name\",\"ph\":\"M\",\"pid\":1,\"tid\":{},\"args\":{{\"name\":{}}}}}", first ? "\n" : ",\n", tid, JsonString(name));
        first = false;
    }

    out += "\n]}\n";
    flush(true);
    return true;
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
