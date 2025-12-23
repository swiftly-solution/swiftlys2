/************************************************************************************************
 * SwiftlyS2 is a scripting framework for Source2-based games.
 * Copyright (C) 2023-2026 Swiftly Solution SRL via Sava Andrei-Sebastian and it's contributors
 *
 * This program is free software: you can redistribute it and/or modify
 * it under the terms of the GNU General Public License as published by
 * the Free Software Foundation, either version 3 of the License, or
 * (at your option) any later version.
 *
 * This program is distributed in the hope that it will be useful,
 * but WITHOUT ANY WARRANTY; without even the implied warranty of
 * MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.  See the
 * GNU General Public License for more details.
 *
 * You should have received a copy of the GNU General Public License
 * along with this program.  If not, see <https://www.gnu.org/licenses/>.
 ************************************************************************************************/

#ifndef src_api_utils_mutex_h
#define src_api_utils_mutex_h

#include <cstdint>
#include <mutex>
#include <queue>
#include <condition_variable>

class QueueMutex
{
public:
    QueueMutex() : locked(false) {}

    void lock()
    {
        std::unique_lock<std::mutex> lk(internal_m);

        if (!locked)
        {
            locked = true;
            return;
        }

        std::condition_variable cv;
        waiters.push(&cv);

        cv.wait(lk, [&]
            { return &cv == waiters.front(); });

        locked = true;
        waiters.pop();
    }

    void unlock()
    {
        std::lock_guard<std::mutex> lk(internal_m);
        locked = false;
        if (!waiters.empty())
        {
            waiters.front()->notify_one();
        }
    }

    bool try_lock()
    {
        std::lock_guard<std::mutex> lk(internal_m);
        if (!locked)
        {
            locked = true;
            return true;
        }
        return false;
    }

private:
    std::mutex internal_m;
    bool locked;
    std::queue<std::condition_variable*> waiters;
};

class QueueLockGuard
{
public:
    explicit QueueLockGuard(QueueMutex& m) : mtx(m), owns(true) { mtx.lock(); }
    ~QueueLockGuard()
    {
        if (owns)
            mtx.unlock();
    }
    QueueLockGuard(const QueueLockGuard&) = delete;
    QueueLockGuard& operator=(const QueueLockGuard&) = delete;

private:
    QueueMutex& mtx;
    bool owns;
};

#endif