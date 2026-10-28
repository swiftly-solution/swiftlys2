#pragma once

#include <cstdint>
#include <sys/types.h>
#include <ucontext.h>

namespace google_breakpad { class LinuxDumper; }

// Called only by the separate Breakpad collector, while target threads are
// stopped. The callback receives disjoint, page-aligned readable ranges.
using ClrMemorySink = bool (*)(void*, uint64_t, uint64_t);
bool CollectClrMemory(google_breakpad::LinuxDumper* dumper, pid_t crash_thread,
                      const ucontext_t* crash_context,
                      ClrMemorySink sink, void* sink_context);
