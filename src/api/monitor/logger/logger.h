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

#ifndef src_api_monitor_logger_logger_h
#define src_api_monitor_logger_logger_h

#include <string>

enum class LogType
{
    TRACE,
    DEBUG,
    INFO,
    WARNING,
    ERR,
    CRITICAL,
    NONE
};

class ILogger
{
public:
    virtual void Log(LogType type, const std::string& message) = 0;
    virtual void Log(LogType type, const std::string& category, const std::string& message) = 0;

    virtual void Trace(const std::string& message) = 0;
    virtual void Debug(const std::string& message) = 0;
    virtual void Info(const std::string& message) = 0;
    virtual void Warning(const std::string& message) = 0;
    virtual void Error(const std::string& message) = 0;
    virtual void Critical(const std::string& message) = 0;

    virtual void Trace(const std::string& category, const std::string& message) = 0;
    virtual void Debug(const std::string& category, const std::string& message) = 0;
    virtual void Info(const std::string& category, const std::string& message) = 0;
    virtual void Warning(const std::string& category, const std::string& message) = 0;
    virtual void Error(const std::string& category, const std::string& message) = 0;
    virtual void Critical(const std::string& category, const std::string& message) = 0;

    virtual void SetLogFile(LogType type, const std::string& path) = 0;
    virtual void ShouldOutputToFile(LogType type, bool enabled) = 0;

    virtual void ShouldColorCategoryInConsole(const std::string& category, bool enabled) = 0;
    virtual void ShouldOutputToConsole(LogType type, bool enabled) = 0;
};

#endif