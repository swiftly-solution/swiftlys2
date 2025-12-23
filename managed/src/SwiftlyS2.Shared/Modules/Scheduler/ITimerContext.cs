namespace SwiftlyS2.Shared.Scheduler;

public interface ITimerContext
{
    /// <summary>
    /// Gets the number of times the timer has been executed.
    /// The first execution count is 0.
    /// </summary>
    public ulong ExecutionCount { get; }
}
