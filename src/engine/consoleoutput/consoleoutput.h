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

#ifndef src_engine_consoleoutput_consoleoutput_h
#define src_engine_consoleoutput_consoleoutput_h

#include <api/engine/consoleoutput/consoleoutput.h>
#include <mutex>

#include <atomic>
#include <cstdarg>
#include <map>
#include <memory>
#include <thread>
#include <vector>

struct ConsoleFilter;

class CConsoleOutput : public IConsoleOutput
{
public:
    CConsoleOutput();
    ~CConsoleOutput();

    bool ProcessLogMessage(const char* str, va_list* args);

    virtual void Initialize() override;
    virtual void Shutdown() override;

    virtual void ReloadFilterConfiguration() override;
    virtual void ToggleFilter() override;
    virtual bool IsEnabled() override;
    virtual bool NeedsFiltering(char* text) override;
    virtual std::string GetCounterText() override;

    virtual uint64_t AddConsoleListener(std::function<void(const std::string&)> callback) override;
    virtual void RemoveConsoleListener(uint64_t id) override;
    virtual void DispatchQueuedListeners() override;

private:
    static constexpr uint32_t FILTER_ENABLED = 1;
    static constexpr uint32_t LISTENER_INCREMENT = 2;

    bool FilterOrQueueMessage(const char* str, va_list* args, bool filterEnabled, bool hasListeners);
    void DispatchWorker();

    std::atomic<uint32_t> m_WorkState{ 0 };
    std::atomic<bool> m_SkipNextNewlineOnlyLog{ false };

    std::mutex m_FiltersMutex;
    std::map<uint64_t, std::unique_ptr<ConsoleFilter>> m_Filters;
    uint64_t m_NextFilterId = 1;

    std::mutex m_ListenersMutex;
    std::map<uint64_t, std::function<void(const std::string&)>> m_Listeners;
    uint64_t m_NextListenerId = 0;

    std::mutex m_QueueMutex;
    std::mutex m_DispatchMutex;
    std::vector<std::string> m_QueuedMessages;
    std::vector<std::string> m_DispatchedMessages;
    std::atomic<bool> m_HasQueuedMessages{ false };

    std::thread m_DispatchThread;
    std::atomic<bool> m_DispatchRunning{ false };
};

#endif
