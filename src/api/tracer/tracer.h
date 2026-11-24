#ifndef _src_shared_api_h
#define _src_shared_api_h

#include <cstddef>
#include <cstdint>
#include <map>
#include <vector>

enum class EventType : uint32_t
{
    // a = FunctionID, b = code address, nameId = function name,
    // sourceId = module path (0 if unknown).
    FunctionEnter,

    // Same fields as FunctionEnter.
    FunctionExit,

    // Same fields as FunctionEnter.
    FunctionTailcall,

    // a = managed ThreadID, b = OS thread id (0 until assigned),
    // nameId = thread name (usually 0, the name is set later).
    ThreadCreated,

    // a = managed ThreadID, b = OS thread id, nameId = last thread name.
    ThreadDestroyed,

    // a = managed ThreadID, b = OS thread id, nameId = thread name.
    ThreadAssignedToOSThread,

    // Native code calls managed code (reverse P/Invoke).
    // Same fields as FunctionEnter.
    UnmanagedToManaged,

    // a = ObjectID, b = size in bytes, nameId = object type name,
    // sourceId = allocating function name (0 if unknown).
    // Only recorded while ITracer::IsObjectTracking() is true.
    ObjectAllocated,

    // a = managed ThreadID, b = OS thread id, nameId = new thread name.
    ThreadNameChanged,

    // nameId = section name, sourceId = user source (0 if none).
    // timestampNs = start time.
    CustomBegin,

    // nameId = section name, timestampNs = stop time.
    // Pairs with the latest open CustomBegin of the same name on the same
    // osThreadId.
    CustomEnd,

    // A managed function entered from native code returns to native code.
    // Same fields as FunctionEnter. Closes the latest UnmanagedToManaged
    // with the same a.
    ManagedToUnmanaged,
};

struct Event
{
    // Nanoseconds since the tracer loaded.
    uint64_t timestampNs;
    EventType type;
    uint32_t osThreadId;
    uint64_t a;
    uint64_t b;
    // String ids, resolve with ITracer::GetString. 0 means none.
    uint32_t nameId;
    uint32_t sourceId;
};
static_assert(sizeof(Event) == 40, "Event layout is part of the API");

class ITracer
{
public:
    // Returns "" for id 0 or an unknown id.
    virtual const char* GetString(uint32_t id) = 0;

    // Open FunctionEnter and UnmanagedToManaged events per OS thread id,
    // oldest first. Events closed by a later exit are removed.
    virtual std::map<uint32_t, std::vector<Event>> TakeCrashSnapshot() = 0;

    // Records every event from StartProfiling until StopProfiling.
    // StopProfiling discards the recorded events.
    virtual void StartProfiling() = 0;
    virtual std::vector<Event> TakeProfileSnapshot() = 0;
    virtual void StopProfiling() = 0;

    // Records a CustomBegin or CustomEnd event on the calling thread.
    virtual void CustomBegin(const char* name, const char* source) = 0;
    virtual void CustomEnd(const char* name) = 0;

    // Returns "" when the thread has no name.
    virtual const char* GetThreadName(uint32_t osThreadId) = 0;

    // Turns ObjectAllocated events on or off. Off by default.
    // Returns false when the runtime rejects the change.
    virtual bool SetObjectTracking(bool enabled) = 0;
    virtual bool IsObjectTracking() = 0;

protected:
    ~ITracer() = default;
};

using GetTracerFn = ITracer* (*)();

#endif
