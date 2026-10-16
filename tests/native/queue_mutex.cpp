#include <api/utils/mutex.h>

#include <atomic>
#include <chrono>
#include <iostream>
#include <stdexcept>
#include <thread>
#include <vector>

static void require(bool condition)
{
    if (!condition) throw std::runtime_error("mutex assertion failed");
}

int main()
{
    QueueMutex mutex;
    std::atomic<bool> entered{false};
    mutex.lock();
    std::thread contender([&] {
        QueueLockGuard guard(mutex);
        entered.store(true, std::memory_order_release);
    });
    const auto deadline = std::chrono::steady_clock::now() + std::chrono::milliseconds(50);
    while (!entered.load(std::memory_order_acquire) && std::chrono::steady_clock::now() < deadline)
        std::this_thread::yield();
    const bool overlap = entered.load(std::memory_order_acquire);
    mutex.unlock();
    contender.join();
    require(!overlap);

    std::atomic<int> active{0};
    std::atomic<int> violations{0};
    std::vector<std::thread> workers;
    for (int worker = 0; worker < 8; ++worker)
        workers.emplace_back([&] {
            for (int iteration = 0; iteration < 10000; ++iteration)
            {
                QueueLockGuard guard(mutex);
                if (active.fetch_add(1) != 0) ++violations;
                active.fetch_sub(1);
            }
        });
    for (auto& worker : workers) worker.join();
    require(violations.load() == 0);

    mutex.lock();
    bool acquired = true;
    std::thread try_contender([&] {
        acquired = mutex.try_lock();
        if (acquired) mutex.unlock();
    });
    try_contender.join();
    mutex.unlock();
    require(!acquired);

    try
    {
        QueueLockGuard guard(mutex);
        throw std::runtime_error("release guard");
    }
    catch (const std::runtime_error&) {}
    require(mutex.try_lock());
    mutex.unlock();

    QueueMutex second;
    std::thread reverse([&] {
        for (int iteration = 0; iteration < 1000; ++iteration)
        {
            std::scoped_lock guard(second, mutex);
        }
    });
    for (int iteration = 0; iteration < 1000; ++iteration)
    {
        std::scoped_lock guard(mutex, second);
    }
    reverse.join();
    std::cout << "PASS mutual exclusion, contention, try_lock, RAII and scoped_lock\n";
}
