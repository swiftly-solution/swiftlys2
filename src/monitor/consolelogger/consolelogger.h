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

#ifndef src_monitor_consolelogger_consolelogger_h
#define src_monitor_consolelogger_consolelogger_h

#include <atomic>
#include <condition_variable>
#include <cstdint>
#include <mutex>
#include <queue>
#include <string>
#include <thread>

class ConsoleLogger
{
public:
    void Initialize();
    void Shutdown();
    void FlushQueue();

private:
    void        WorkerThread();
    std::string GetDailyLogPath() const;
    void        ApplyFileCountRotation();
    void        ApplyTimeIntervalRotation();
    void        WriteEntries(std::queue<std::string>& entries, const std::string& targetFile);

    std::queue<std::string>  m_queue;
    std::mutex               m_mutex;
    std::condition_variable  m_cv;
    std::thread              m_thread;
    std::atomic<bool>        m_running{ false };

    uint64_t    m_listenerId           = UINT64_MAX;
    bool        m_enabled              = false;
    bool        m_rotationEnabled      = false;
    std::string m_rotationMode;
    int         m_maxFiles             = 60;
    int         m_deleteOlderThanHours = 168;

    std::string m_logDir;
    std::string m_currentLogFile;
};

extern ConsoleLogger g_ConsoleLogger;

#endif
