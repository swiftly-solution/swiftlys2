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

#include <api/shared/string.h>
#include <core/entrypoint.h>

#include <api/shared/files.h>
#include <fmt/format.h>

#include <algorithm>
#include <api/interfaces/manager.h>
#include <cctype>
#include <cstdlib>
#include <unordered_map>

constexpr const char* PREFIX = "[Swiftly]";
static const std::unordered_map<LogType, std::string> logTypeToString = { {LogType::TRACE, "TRACE"}, {LogType::DEBUG, "DEBUG"},       {LogType::INFO, "INFO"}, {LogType::WARNING, "WARNING"},
                                                                         {LogType::ERR, "ERROR"},   {LogType::CRITICAL, "CRITICAL"}, {LogType::NONE, "NONE"} };
static const std::unordered_map<std::string, LogType> stringToLogType = { {"TRACE", LogType::TRACE}, {"DEBUG", LogType::DEBUG},       {"INFO", LogType::INFO}, {"WARNING", LogType::WARNING},
                                                                         {"ERROR", LogType::ERR},   {"CRITICAL", LogType::CRITICAL}, {"NONE", LogType::NONE} };

std::string GetLogTypeString(LogType type)
{
    auto it = logTypeToString.find(type);
    return (it != logTypeToString.end()) ? it->second : "UNKNOWN";
}

void Logger::Log(LogType type, const std::string& message)
{
    if (!ShouldLog(type))
    {
        return;
    }

    std::string final_output = fmt::format("{} [{}{}{}] {}", PREFIX, GetTerminalStringColor(GetLogTypeString(type)), GetLogTypeString(type), "[/]", message);
    std::string color_processed = TerminalProcessColor(final_output);
    std::string without_colors = ClearTerminalColors(final_output);

    if (m_bShouldOutputToConsole[(int)type])
    {
        g_SwiftlyCore.SendConsoleMessage(color_processed);
    }
    if (m_bShouldOutputToFile[(int)type] && !m_sLogFilePaths[(int)type].empty())
    {
        Files::Append(m_sLogFilePaths[(int)type], without_colors);
    }
}

void Logger::Log(LogType type, const std::string& category, const std::string& message)
{
    if (!ShouldLog(type))
    {
        return;
    }

    if (m_bShouldOutputToConsole[(int)type] && !m_sNonColoredCategories.contains(category))
    {
        Log(type, fmt::format("[{}{}{}] {}", GetTerminalStringColor(category), category, "[/]", message));
    }
    else
    {
        Log(type, fmt::format("[{}] {}", category, message));
    }
}

void Logger::Trace(const std::string& message)
{
    Log(LogType::TRACE, message);
}

void Logger::Debug(const std::string& message)
{
    Log(LogType::DEBUG, message);
}

void Logger::Info(const std::string& message)
{
    Log(LogType::INFO, message);
}

void Logger::Warning(const std::string& message)
{
    Log(LogType::WARNING, message);
}

void Logger::Error(const std::string& message)
{
    Log(LogType::ERR, message);
}

void Logger::Critical(const std::string& message)
{
    Log(LogType::CRITICAL, message);
}

void Logger::Trace(const std::string& category, const std::string& message)
{
    Log(LogType::TRACE, category, message);
}

void Logger::Debug(const std::string& category, const std::string& message)
{
    Log(LogType::DEBUG, category, message);
}

void Logger::Info(const std::string& category, const std::string& message)
{
    Log(LogType::INFO, category, message);
}

void Logger::Warning(const std::string& category, const std::string& message)
{
    Log(LogType::WARNING, category, message);
}

void Logger::Error(const std::string& category, const std::string& message)
{
    Log(LogType::ERR, category, message);
}

void Logger::Critical(const std::string& category, const std::string& message)
{
    Log(LogType::CRITICAL, category, message);
}

void Logger::SetLogFile(LogType type, const std::string& path)
{
    m_sLogFilePaths[static_cast<int>(type)] = path;
}

void Logger::ShouldOutputToFile(LogType type, bool enabled)
{
    m_bShouldOutputToFile[static_cast<int>(type)] = enabled;
}

void Logger::ShouldColorCategoryInConsole(const std::string& category, bool enabled)
{
    if (!enabled)
    {
        m_sNonColoredCategories.insert(category);
    }
    else
    {

        m_sNonColoredCategories.erase(category);
    }
}

void Logger::ShouldOutputToConsole(LogType type, bool enabled)
{
    m_bShouldOutputToConsole[static_cast<int>(type)] = enabled;
}

bool Logger::ShouldLog(LogType type)
{
    if (type == LogType::NONE)
    {
        return false;
    }

    static LogType minLevel = GetMinLogLevelFromEnv();
    return static_cast<int>(type) >= static_cast<int>(minLevel);
}

LogType Logger::GetMinLogLevelFromEnv()
{
    const char* level = std::getenv("SWIFTLY_LOG_LEVEL");
    if (!level)
    {
        return LogType::INFO;
    }

    std::string levelStr = level;
    std::transform(levelStr.begin(), levelStr.end(), levelStr.begin(), ::toupper);

    auto it = stringToLogType.find(levelStr);
    return (it != stringToLogType.end()) ? it->second : LogType::INFO;
}