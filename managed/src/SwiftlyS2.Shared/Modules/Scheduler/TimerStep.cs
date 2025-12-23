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

    public static TimerStep Spin() => new SpinStep();
    public static TimerStep WaitForTicks( long ticks ) => new WaitForTicksStep(ticks);
    public static TimerStep WaitForMilliseconds( long milliseconds ) => new WaitForMillisecondsStep(milliseconds);
    public static TimerStep WaitForSeconds( float seconds ) => new WaitForMillisecondsStep((long)(seconds * 1000));
    public static TimerStep Stop() => new StopStep();
}