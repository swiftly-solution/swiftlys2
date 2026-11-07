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

#ifndef src_api_shared_profiler_h
#define src_api_shared_profiler_h

#include <chrono>
#include <cstdint>
#include <source_location>
#include <string>

#ifdef PROFILER

class ProfilerScope
{
public:
    explicit ProfilerScope(const char* name = nullptr, std::source_location location = std::source_location::current());
    ~ProfilerScope();

    ProfilerScope(const ProfilerScope&) = delete;
    ProfilerScope& operator=(const ProfilerScope&) = delete;

    void Pause();
    void Resume();

private:
    bool m_active = false;
    uint64_t m_elapsedNs = 0;
    const char* m_name;
    const char* m_file;
    uint_least32_t m_line;
    ProfilerScope* m_parent = nullptr;
    uint64_t m_childNs = 0;           
    std::chrono::steady_clock::time_point m_start;
};

namespace Profiler
{
    void Save(const std::string& path);
}

#define PROFILER_CONCAT_INNER(a, b) a##b
#define PROFILER_CONCAT(a, b) PROFILER_CONCAT_INNER(a, b)

#define PROFILE_SCOPE(name) ProfilerScope PROFILER_CONCAT(profilerScope_, __LINE__)(name)
#define PROFILE_FUNCTION() ProfilerScope profilerFunctionScope
#define PROFILE_PAUSE() profilerFunctionScope.Pause()
#define PROFILE_RESUME() profilerFunctionScope.Resume()

#else

namespace Profiler
{
    inline void Save(const std::string&) {}
}

#define PROFILE_SCOPE(name) ((void)0)
#define PROFILE_FUNCTION() ((void)0)
#define PROFILE_PAUSE() ((void)0)
#define PROFILE_RESUME() ((void)0)

#endif

#endif
