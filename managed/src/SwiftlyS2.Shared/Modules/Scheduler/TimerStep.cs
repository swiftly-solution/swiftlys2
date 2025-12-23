using System.Runtime.CompilerServices;

namespace SwiftlyS2.Shared.Scheduler;

public abstract record TimerStep
{
    private TimerStep() { }

    internal sealed record SpinStep : TimerStep;

    internal sealed record WaitForTicksStep( long Ticks ) : TimerStep
    {
        public long Ticks { get; } = Ticks > 0 ? Ticks : throw new ArgumentException("Ticks must be greater than 0", nameof(Ticks));
    }

    internal sealed record WaitForMillisecondsStep( long Milliseconds ) : TimerStep
    {
        public long Milliseconds { get; } = Milliseconds > 0 ? Milliseconds : throw new ArgumentException("Milliseconds must be greater than 0", nameof(Milliseconds));
    }

    internal sealed record StopStep : TimerStep;

    /// <summary>
    /// Spin the timer.
    /// 
    /// The timer will be executed immediately on next tick.
    /// </summary>
    /// <returns>The timer step.</returns>
    public static TimerStep Spin() => new SpinStep();

    /// <summary>
    /// Wait for a number of ticks.
    /// </summary>
    /// <param name="ticks">The number of ticks to wait.</param>
    /// <returns>The timer step.</returns>
    public static TimerStep WaitForTicks( long ticks ) => new WaitForTicksStep(ticks);

    /// <summary>
    /// Wait for a number of milliseconds.
    /// </summary>
    /// <param name="milliseconds">The number of milliseconds to wait.</param>
    /// <returns>The timer step.</returns>
    public static TimerStep WaitForMilliseconds( long milliseconds ) => new WaitForMillisecondsStep(milliseconds);

    /// <summary>
    /// Wait for a number of seconds.
    /// </summary>
    /// <param name="seconds">The number of seconds to wait.</param>
    /// <returns>The timer step.</returns>
    public static TimerStep WaitForSeconds( float seconds ) => new WaitForMillisecondsStep((long)(seconds * 1000));

    /// <summary>
    /// Stop the timer.
    /// </summary>
    /// <returns>The timer step.</returns>
    public static TimerStep Stop() => new StopStep();
}