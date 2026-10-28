// The DAC runs in a fresh process, outside the game's signal handler.
#include <atomic>
#include <chrono>
#include <csignal>
#include <cstdlib>
#include <dirent.h>
#include <string>
#include <sys/prctl.h>
#include <sys/socket.h>
#include <thread>
#include <unistd.h>

#include "client/linux/crash_generation/crash_generation_server.h"

static std::atomic<bool> done{false};

static void DumpDone(void*, const google_breakpad::ClientInfo*, const std::string* path)
{
    // Queue the path before Breakpad releases its acknowledgement pipe.
    send(STDIN_FILENO, path->c_str(), path->size() + 1, MSG_NOSIGNAL);
    done.store(true);
}

int main(int argc, char** argv)
{
    if (argc != 3) return 2;
    const pid_t parent = static_cast<pid_t>(std::strtol(argv[2], nullptr, 10));
    if (prctl(PR_SET_PDEATHSIG, SIGTERM) != 0 || getppid() != parent) return 2;
    // Do not keep the game's sockets and files alive through inherited FDs.
    if (DIR* files = opendir("/proc/self/fd"))
    {
        while (dirent* entry = readdir(files))
        {
            const int fd = std::atoi(entry->d_name);
            if (fd > STDERR_FILENO && fd != dirfd(files)) close(fd);
        }
        closedir(files);
    }
    const std::string directory = argv[1];
    google_breakpad::CrashGenerationServer server(STDIN_FILENO, DumpDone, nullptr,
                                                 nullptr, nullptr, true, &directory);
    if (!server.Start()) return 2;
    const char ready = 'R';
    if (send(STDIN_FILENO, &ready, 1, MSG_NOSIGNAL) != 1) return 2;
    while (!done.load()) std::this_thread::sleep_for(std::chrono::milliseconds(10));
    server.Stop();
    return 0;
}
