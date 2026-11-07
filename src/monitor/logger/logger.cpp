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

#include "logger.h"

#include <api/interfaces/interfaces.h>
#include <api/shared/profiler.h>
#include <api/shared/string.h>

#include <core/entrypoint.h>

#include <cstdlib>
#include <format>
#include <string_view>
#include <utility>

static constexpr const char* PREFIX = "[Swiftly]";

static constexpr std::string_view LogTypeName(LogType type)
{
    switch (type) {
        case LogType::TRACE: return "TRACE";
        case LogType::DEBUG: return "DEBUG";
        case LogType::INFO: return "INFO";
        case LogType::WARNING: return "WARNING";
        case LogType::ERR: return "ERROR";
        case LogType::CRITICAL: return "CRITICAL";
        case LogType::NONE: return "NONE";
    }
    return "UNKNOWN";
}

static LogType GetMinLogLevelFromEnv()
{
    PROFILE_FUNCTION();

    const char* env = std::getenv("SWIFTLY_LOG_LEVEL");
    if (!env) return LogType::INFO;

    auto level = str_toupper(env);

    for (int i = 0; i <= std::to_underlying(LogType::NONE); ++i) {
        auto type = static_cast<LogType>(i);
        if (level == LogTypeName(type)) return type;
    }

    return LogType::INFO;
}

bool Logger::ShouldLog(LogType type)
{
    PROFILE_FUNCTION();

    static const LogType minLevel = GetMinLogLevelFromEnv();
    return type != LogType::NONE && type >= minLevel;
}

void Logger::Log(LogType type, const std::string& message)
{
    PROFILE_FUNCTION();

    if (!ShouldLog(type)) return;

    auto name = std::string(LogTypeName(type));
    auto output = std::format("{} [{}{}[/]] {}", PREFIX, GetTerminalStringColor(name), name, message);
    g_SwiftlyCore.SendConsoleMessage(TerminalProcessColor(output));
}

void Logger::Log(LogType type, const std::string& category, const std::string& message)
{
    PROFILE_FUNCTION();

    if (!ShouldLog(type)) return;

    Log(type, std::format("[{}{}[/]] {}", GetTerminalStringColor(category), category, message));
}
