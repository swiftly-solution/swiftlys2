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

#ifndef src_monitor_logger_logger_h
#define src_monitor_logger_logger_h

#include <api/monitor/logger/logger.h>

#include <string>

class Logger : public ILogger
{
public:
    void Log(LogType type, const std::string& message) override;
    void Log(LogType type, const std::string& category, const std::string& message) override;

    void Trace(const std::string& message) override { Log(LogType::TRACE, message); }
    void Debug(const std::string& message) override { Log(LogType::DEBUG, message); }
    void Info(const std::string& message) override { Log(LogType::INFO, message); }
    void Warning(const std::string& message) override { Log(LogType::WARNING, message); }
    void Error(const std::string& message) override { Log(LogType::ERR, message); }
    void Critical(const std::string& message) override { Log(LogType::CRITICAL, message); }

    void Trace(const std::string& category, const std::string& message) override { Log(LogType::TRACE, category, message); }
    void Debug(const std::string& category, const std::string& message) override { Log(LogType::DEBUG, category, message); }
    void Info(const std::string& category, const std::string& message) override { Log(LogType::INFO, category, message); }
    void Warning(const std::string& category, const std::string& message) override { Log(LogType::WARNING, category, message); }
    void Error(const std::string& category, const std::string& message) override { Log(LogType::ERR, category, message); }
    void Critical(const std::string& category, const std::string& message) override { Log(LogType::CRITICAL, category, message); }

private:
    bool ShouldLog(LogType type);
};

#endif
