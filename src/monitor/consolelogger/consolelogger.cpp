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

#include "consolelogger.h"

#include <api/engine/consoleoutput/consoleoutput.h>
#include <api/interfaces/interfaces.h>
#include <api/interfaces/manager.h>
#include <api/server/configuration/configuration.h>
#include <api/shared/files.h>
#include <api/shared/plat.h>
#include <api/shared/string.h>
#include <core/entrypoint.h>

#include <fmt/format.h>

#include <algorithm>
#include <chrono>
#include <cstdio>
#include <ctime>
#include <filesystem>
#include <vector>

ConsoleLogger g_ConsoleLogger;

namespace fs = std::filesystem;

static std::string GetTimestampString()
{
    std::time_t now = std::time(nullptr);
    char buf[32];
#ifdef _WIN32
    struct tm tm_info;
    localtime_s(&tm_info, &now);
    std::strftime(buf, sizeof(buf), "%Y-%m-%d %H:%M:%S", &tm_info);
#else
    struct tm tm_info;
    localtime_r(&now, &tm_info);
    std::strftime(buf, sizeof(buf), "%Y-%m-%d %H:%M:%S", &tm_info);
#endif
    return buf;
}

static std::string GetDateString()
{
    std::time_t now = std::time(nullptr);
    char buf[16];
#ifdef _WIN32
    struct tm tm_info;
    localtime_s(&tm_info, &now);
    std::strftime(buf, sizeof(buf), "%Y-%m-%d", &tm_info);
#else
    struct tm tm_info;
    localtime_r(&now, &tm_info);
    std::strftime(buf, sizeof(buf), "%Y-%m-%d", &tm_info);
#endif
    return buf;
}

void ConsoleLogger::Initialize()
{
    auto config = g_ifaceService.FetchInterface<IConfiguration>(CONFIGURATION_INTERFACE_VERSION);

    if (bool* b = std::get_if<bool>(&config->GetValue("core.ConsoleLogger.Enable")))
        m_enabled = *b;

    if (!m_enabled)
        return;

    if (bool* b = std::get_if<bool>(&config->GetValue("core.ConsoleLogger.Rotation.Enable")))
        m_rotationEnabled = *b;

    if (std::string* s = std::get_if<std::string>(&config->GetValue("core.ConsoleLogger.Rotation.Mode")))
        m_rotationMode = *s;

    if (int* i = std::get_if<int>(&config->GetValue("core.ConsoleLogger.Rotation.MaximumFiles")))
        m_maxFiles = *i;

    if (int* i = std::get_if<int>(&config->GetValue("core.ConsoleLogger.Rotation.DeleteOlderThanHours")))
        m_deleteOlderThanHours = *i;

    std::string relativeLogDir = g_SwiftlyCore.GetCorePath() + "logs" + WIN_LINUX("\\", "/") + "console" + WIN_LINUX("\\", "/");
    m_logDir = Files::GeneratePath(relativeLogDir);

    std::error_code mkdirEc;
    std::filesystem::create_directories(m_logDir, mkdirEc);

    m_currentLogFile = GetDailyLogPath();
    if (m_rotationEnabled)
    {
        if (m_rotationMode == "file_count")
            ApplyFileCountRotation();
        else if (m_rotationMode == "time_interval")
            ApplyTimeIntervalRotation();
    }

    m_running = true;
    m_thread  = std::thread(&ConsoleLogger::WorkerThread, this);

    auto consoleoutput = g_ifaceService.FetchInterface<IConsoleOutput>(CONSOLEOUTPUT_INTERFACE_VERSION);
    m_listenerId       = consoleoutput->AddConsoleListener([this](const std::string& message) {
        std::lock_guard<std::mutex> lock(m_mutex);
        m_queue.push(fmt::format("[{}] {}", GetTimestampString(), message));
        m_cv.notify_one();
    });
}

void ConsoleLogger::Shutdown()
{
    if (!m_enabled)
        return;

    if (m_listenerId != UINT64_MAX)
    {
        auto consoleoutput = g_ifaceService.FetchInterface<IConsoleOutput>(CONSOLEOUTPUT_INTERFACE_VERSION);
        consoleoutput->RemoveConsoleListener(m_listenerId);
        m_listenerId = UINT64_MAX;
    }

    m_running = false;
    m_cv.notify_all();
    if (m_thread.joinable())
        m_thread.join();

    FlushQueue();
}

void ConsoleLogger::FlushQueue()
{
    if (!m_enabled)
        return;

    std::string targetFile = m_currentLogFile;
    if (targetFile.empty())
        targetFile = GetDailyLogPath();

    if (targetFile.empty())
        return;

    std::queue<std::string> local;
    {
        std::lock_guard<std::mutex> lock(m_mutex);
        std::swap(local, m_queue);
    }

    if (local.empty())
        return;

    WriteEntries(local, targetFile);
}

void ConsoleLogger::WorkerThread()
{
    while (m_running)
    {
        std::queue<std::string> local;
        {
            std::unique_lock<std::mutex> lock(m_mutex);
            m_cv.wait_for(lock, std::chrono::milliseconds(500), [this] {
                return !m_queue.empty() || !m_running;
            });
            std::swap(local, m_queue);
        }

        if (local.empty())
            continue;

        std::string dayPath = GetDailyLogPath();
        if (dayPath != m_currentLogFile)
            m_currentLogFile = dayPath;

        if (m_currentLogFile.empty())
            continue;

        WriteEntries(local, m_currentLogFile);

        if (m_rotationEnabled)
        {
            if (m_rotationMode == "file_count")
                ApplyFileCountRotation();
            else if (m_rotationMode == "time_interval")
                ApplyTimeIntervalRotation();
        }
    }
}

void ConsoleLogger::WriteEntries(std::queue<std::string>& entries, const std::string& targetFile)
{
    if (targetFile.empty() || entries.empty())
        return;

    FILE* f = std::fopen(targetFile.c_str(), "a");
    if (!f)
        return;

    while (!entries.empty())
    {
        std::string entry = StripAnsiEscapes(ClearTerminalColors(entries.front()));
        entry.erase(std::remove(entry.begin(), entry.end(), '\r'), entry.end());
        std::fwrite(entry.c_str(), 1, entry.size(), f);
        if (entry.empty() || entry.back() != '\n')
            std::fputc('\n', f);
        entries.pop();
    }

    std::fflush(f);
    std::fclose(f);
}

std::string ConsoleLogger::GetDailyLogPath() const
{
    if (m_logDir.empty())
        return "";
    return m_logDir + GetDateString() + ".log";
}

void ConsoleLogger::ApplyFileCountRotation()
{
    if (m_logDir.empty() || m_maxFiles <= 0)
        return;

    std::error_code ec;
    std::vector<std::pair<fs::file_time_type, fs::path>> files;

    for (const auto& entry : fs::directory_iterator(m_logDir, ec))
    {
        if (ec)
            break;
        if (!entry.is_regular_file(ec) || ec)
            continue;
        if (entry.path().extension() != ".log")
            continue;
        auto mtime = entry.last_write_time(ec);
        if (!ec)
            files.emplace_back(mtime, entry.path());
    }

    int excess = static_cast<int>(files.size()) - m_maxFiles;
    if (excess <= 0)
        return;

    std::sort(files.begin(), files.end());
    for (int i = 0; i < excess; ++i)
        fs::remove(files[i].second, ec);
}

void ConsoleLogger::ApplyTimeIntervalRotation()
{
    if (m_logDir.empty() || m_deleteOlderThanHours <= 0)
        return;

    std::error_code ec;
    auto now    = fs::file_time_type::clock::now();
    auto maxAge = std::chrono::hours(m_deleteOlderThanHours);

    for (const auto& entry : fs::directory_iterator(m_logDir, ec))
    {
        if (ec)
            break;
        if (!entry.is_regular_file(ec) || ec)
            continue;
        if (entry.path().extension() != ".log")
            continue;
        auto mtime = entry.last_write_time(ec);
        if (!ec && (now - mtime) > maxAge)
            fs::remove(entry.path(), ec);
    }
}

void ConsoleLogger_FlushForCrash()
{
    g_ConsoleLogger.FlushQueue();
}
