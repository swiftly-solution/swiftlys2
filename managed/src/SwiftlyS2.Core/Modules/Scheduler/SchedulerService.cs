using SwiftlyS2.Shared.Events;
using SwiftlyS2.Shared.Scheduler;

namespace SwiftlyS2.Core.Scheduler;

internal class SchedulerService : ISchedulerService, IDisposable
{
    private readonly List<CancellationTokenSource> _timers = new();
    private readonly Lock _lock = new();
    private readonly CancellationTokenSource _lifecycleCts = new();
    private CancellationTokenSource _mapChangeCts = new();

    private static int tickPerSecond = 64;

    public SchedulerService( IEventSubscriber eventSubscriber )
    {
        eventSubscriber.OnMapUnload += ( @event ) =>
        {
            lock (_lock)
            {
                _mapChangeCts.Cancel();
                _mapChangeCts.Dispose();
                _mapChangeCts = new CancellationTokenSource();
            }

            CleanFinishedTimers();
        };
    }

    public void NextTick( Action task )
    {
        SchedulerManager.NextTick(task, _lifecycleCts.Token);
    }

    public void NextTick( Func<Task?> task ) => throw new InvalidOperationException(
        "NextTick does not support async callbacks.");

    public void NextTick<T>( Func<Task<T?>> task ) => throw new InvalidOperationException(
        "NextTick does not support async callbacks.");

    public Task NextTickAsync( Action task )
    {
        return SchedulerManager.NextTickAsync(task);
    }

    public void NextTickAsync( Func<Task?> task ) => throw new InvalidOperationException(
        "NextTickAsync does not support async callbacks.");

    public void NextTickAsync<T>( Func<Task<T?>> task ) => throw new InvalidOperationException(
        "NextTickAsync does not support async callbacks.");

    public Task<T> NextTickAsync<T>( Func<T> task )
    {
        return SchedulerManager.NextTickAsync(task);
    }

    public void NextWorldUpdate( Action task )
    {
        SchedulerManager.NextWorldUpdate(task, _lifecycleCts.Token);
    }

    public void NextWorldUpdate( Func<Task?> task ) => throw new InvalidOperationException(
        "NextWorldUpdate does not support async callbacks.");

    public void NextWorldUpdate<T>( Func<Task<T?>> task ) => throw new InvalidOperationException(
        "NextWorldUpdate does not support async callbacks.");

    public Task NextWorldUpdateAsync( Action task )
    {
        return SchedulerManager.NextWorldUpdateAsync(task);
    }

    public void NextWorldUpdateAsync( Func<Task?> task ) => throw new InvalidOperationException(
        "NextWorldUpdateAsync does not support async callbacks.");

    public Task<T> NextWorldUpdateAsync<T>( Func<Task<T?>> task ) => throw new InvalidOperationException(
        "NextWorldUpdateAsync does not support async callbacks.");

    public Task<T> NextWorldUpdateAsync<T>( Func<T> task )
    {
        return SchedulerManager.NextWorldUpdateAsync(task);
    }

    public CancellationTokenSource Delay( int delayTick, Action task )
    {
        CleanFinishedTimers();
        var cts = SchedulerManager.AddTimer(( ctx ) =>
        {
            if (ctx.ExecutionCount == 0)
            {
                return TimerStep.WaitForTicks(delayTick);
            }
            task();
            return TimerStep.Stop();
        }, _lifecycleCts.Token);
        lock (_lock)
        {
            _timers.Add(cts);
        }

        return cts;
    }

    public CancellationTokenSource Repeat( int periodTick, Action task )
    {
        var cts = SchedulerManager.AddTimer(( ctx ) =>
        {
            task();
            return TimerStep.WaitForTicks(periodTick);
        }, _lifecycleCts.Token);
        lock (_lock)
        {
            _timers.Add(cts);
        }

        return cts;
    }

    public CancellationTokenSource DelayAndRepeat( int delayTick, int periodTick, Action task )
    {
        var cts = SchedulerManager.AddTimer(( ctx ) =>
        {
            if (ctx.ExecutionCount == 0)
            {
                return TimerStep.WaitForTicks(delayTick);
            }
            task();
            return TimerStep.WaitForTicks(periodTick);
        }, _lifecycleCts.Token);
        lock (_lock)
        {
            _timers.Add(cts);
        }

        return cts;
    }

    public CancellationTokenSource DelayBySeconds( float delaySeconds, Action task )
    {
        return SchedulerManager.AddTimer(( ctx ) =>
        {
            if (ctx.ExecutionCount == 0)
            {
                return TimerStep.WaitForSeconds(delaySeconds);
            }
            task();
            return TimerStep.Stop();
        }, _lifecycleCts.Token);
    }

    public CancellationTokenSource RepeatBySeconds( float periodSeconds, Action task )
    {
        return SchedulerManager.AddTimer(( ctx ) =>
        {
            task();
            return TimerStep.WaitForSeconds(periodSeconds);
        }, _lifecycleCts.Token);
    }

    public CancellationTokenSource DelayAndRepeatBySeconds( float delaySeconds, float periodSeconds, Action task )
    {
        return SchedulerManager.AddTimer(( ctx ) =>
        {
            if (ctx.ExecutionCount == 0)
            {
                return TimerStep.WaitForSeconds(delaySeconds);
            }
            task();
            return TimerStep.WaitForSeconds(periodSeconds);
        }, _lifecycleCts.Token);
    }

    public CancellationTokenSource AddTimer( Func<ITimerContext, TimerStep> task)
    {
        return SchedulerManager.AddTimer(task, _lifecycleCts.Token);
    }

    public void StopOnMapChange( CancellationTokenSource cts )
    {
        _mapChangeCts.Token.Register(cts.Cancel);
    }

    private void CleanFinishedTimers()
    {
        lock (_lock)
        {
            _timers.RemoveAll(timer => timer.IsCancellationRequested);
        }
    }

    public void Dispose()
    {
        lock (_lock)
        {
            if (_lifecycleCts.IsCancellationRequested) return;
            _lifecycleCts.Cancel();
            _lifecycleCts.Dispose();
            _mapChangeCts.Cancel();
            _mapChangeCts.Dispose();

            foreach (var timer in _timers)
            {
                timer.Cancel();
                timer.Dispose();
            }

            _timers.Clear();
        }
    }
}
