using SwiftlyS2.Shared.Scheduler;

namespace SwiftlyS2.Core.Scheduler;

internal class TimerContext : ITimerContext
{
    public ulong ExecutionCount { get; private set; }
    public long ExpectedNextTimeMs { get; set; }

    public TimerContext()
    {
        ExpectedNextTimeMs = DateTimeOffset.Now.ToUnixTimeMilliseconds();
        ExecutionCount = 0;
    }

    public void IncrementExecutionCount()
    {
        ExecutionCount++;
    }
}