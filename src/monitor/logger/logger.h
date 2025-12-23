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

#include <set>

class Logger : public ILogger
{
public:
    virtual void Log(LogType type, const std::string& message) override;
    virtual void Log(LogType type, const std::string& category, const std::string& message) override;

    virtual void Trace(const std::string& message) override;
    virtual void Debug(const std::string& message) override;
    virtual void Info(const std::string& message) override;
    virtual void Warning(const std::string& message) override;
    virtual void Error(const std::string& message) override;
    virtual void Critical(const std::string& message) override;

    virtual void Trace(const std::string& category, const std::string& message) override;
    virtual void Debug(const std::string& category, const std::string& message) override;
    virtual void Info(const std::string& category, const std::string& message) override;
    virtual void Warning(const std::string& category, const std::string& message) override;
    virtual void Error(const std::string& category, const std::string& message) override;
    virtual void Critical(const std::string& category, const std::string& message) override;

    virtual void SetLogFile(LogType type, const std::string& path) override;
    virtual void ShouldOutputToFile(LogType type, bool enabled) override;

    virtual void ShouldColorCategoryInConsole(const std::string& category, bool enabled) override;
    virtual void ShouldOutputToConsole(LogType type, bool enabled) override;

private:
    bool ShouldLog(LogType type);
    LogType GetMinLogLevelFromEnv();

    bool m_bShouldOutputToConsole[7] = { true, true, true, true, true, true, false };
    bool m_bShouldOutputToFile[7] = { false, false, false, false, false, false, false };
    std::string m_sLogFilePaths[7] = { "", "", "", "", "", "", "" };
    std::set<std::string> m_sNonColoredCategories;
};

#endif