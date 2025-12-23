using System.Collections.Concurrent;
using Spectre.Console;
using SwiftlyS2.Core.Natives;
using SwiftlyS2.Shared.Scheduler;
using System.Collections.Generic;
using System.Linq;
using System.Threading;

namespace SwiftlyS2.Core.Scheduler;

internal class Timer
{
    public TimerContext Context { get; set; } = new();
    public required Func<TimerContext, TimerStep> Task { get; set; }
    public required CancellationTokenSource CancellationTokenSource { get; set; }
    public CancellationToken OwnerToken { get; set; }
}

internal static class SchedulerManager
{
    private static readonly Lock _lock = new();
    private static long _currentTick = 0;
    private static long _currentTimeMs = 0;

    private static readonly ConcurrentQueue<Action> _asyncOnTickTaskQueue = new();
    private static readonly ConcurrentQueue<Action> _asyncOnWorldUpdateTaskQueue = new();

    // Min-heap keyed by DueTick
    private static readonly PriorityQueue<Timer, long> _timerQueue = new();
    private static readonly PriorityQueue<Timer, long> _timerQueueMs = new();

    // Next-tick tasks keyed by guid so services can remove them before they run
    private static readonly List<(Action action, CancellationToken ownerToken)> _nextTickTasks = new();

    private static readonly List<(Action action, CancellationToken ownerToken)> _nextWorldUpdateTasks = new();

    public static void OnWorldUpdate()
    {
        ExecuteOnWorldUpdateAsyncTasks();
        ExecuteOnWorldUpdateTimers();
    }

    private static void ExecuteOnWorldUpdateAsyncTasks()
    {
        int batchCount = _asyncOnWorldUpdateTaskQueue.Count;
        while (batchCount > 0 && _asyncOnWorldUpdateTaskQueue.TryDequeue(out var task))
        {
            batchCount--;
            try
            {
                task.Invoke();
            }
            catch (Exception ex)
            {
                AnsiConsole.WriteException(ex);
            }
        }
    }

    private static void ExecuteOnWorldUpdateTimers()
    {
        List<(Action action, CancellationToken ownerToken)> nextWorldUpdateActions;

        lock (_lock)
        {
            nextWorldUpdateActions = _nextWorldUpdateTasks.ToList();
            _nextWorldUpdateTasks.Clear();
        }

        if (nextWorldUpdateActions.Count > 0)
        {
            foreach (var tuple in nextWorldUpdateActions)
            {
                if (tuple.ownerToken.IsCancellationRequested) continue;
                try
                {
                    tuple.action();
                }
                catch (Exception ex)
                {
                    if (!GlobalExceptionHandler.Handle(ex)) return;
                    AnsiConsole.WriteException(ex);
                }
            }
        }
    }


    public static void OnTick()
    {
        _currentTimeMs = DateTimeOffset.Now.ToUnixTimeMilliseconds();
        ExecuteOnTickAsyncTasks();
        ExecuteOnTickTimers();
    }

    private static void ExecuteOnTickAsyncTasks()
    {
        int batchCount = _asyncOnTickTaskQueue.Count;
        while (batchCount > 0 && _asyncOnTickTaskQueue.TryDequeue(out var task))
        {
            batchCount--;
            try
            {
                task.Invoke();
            }
            catch (Exception ex)
            {
                AnsiConsole.WriteException(ex);
            }
        }
    }

    private static void ExecuteOnTickTimers()
    {
        List<(Action action, CancellationToken ownerToken)> nextTickActions;
        List<Timer> dueTimers = [];

        lock (_lock)
        {
            _currentTick++;

            // Drain next-tick tasks
            nextTickActions = _nextTickTasks.ToList();
            _nextTickTasks.Clear();

            // Pop all due timers from the heap
            while (_timerQueue.Count > 0)
            {
                if (!_timerQueue.TryPeek(out var timer, out var due)) break;
                if (due > _currentTick) break;
                _timerQueue.Dequeue();

                // Skip canceled/owner-disposed timers
                if (timer.CancellationTokenSource.IsCancellationRequested || timer.OwnerToken.IsCancellationRequested)
                {
                    continue;
                }

                dueTimers.Add(timer);
            }

            while (_timerQueueMs.Count > 0)
            {
                if (!_timerQueueMs.TryPeek(out var timer, out var due)) break;
                if (due > _currentTimeMs) break;
                _timerQueueMs.Dequeue();

                // Skip canceled/owner-disposed timers
                if (timer.CancellationTokenSource.IsCancellationRequested || timer.OwnerToken.IsCancellationRequested)
                {
                    continue;
                }

                dueTimers.Add(timer);
            }
        }

        // Execute next-tick actions outside the lock
        if (nextTickActions.Count > 0)
        {
            foreach (var tuple in nextTickActions)
            {
                if (tuple.ownerToken.IsCancellationRequested) continue;
                try
                {
                    tuple.action();
                }
                catch (Exception ex)
                {
                    if (!GlobalExceptionHandler.Handle(ex)) return;
                    AnsiConsole.WriteException(ex);
                }
            }
        }

        // Execute due timers outside the lock and reschedule if repeating
        if (dueTimers.Count > 0)
        {
            foreach (var timer in dueTimers)
            {
                try
                {
                    ExecuteTimer(timer);
                }
                catch (Exception ex)
                {
                    if (!GlobalExceptionHandler.Handle(ex)) return;
                    AnsiConsole.WriteException(ex);
                }
            }
        }
    }

    private static void ExecuteTimer( Timer timer )
    {
        var step = timer.Task(timer.Context);

        switch (step)
        {
            case TimerStep.SpinStep:
                timer.Context.IncrementExecutionCount();
                timer.Context.ExpectedNextTimeMs = _currentTimeMs;
                _timerQueue.Enqueue(timer, _currentTick);
                break;
            case TimerStep.WaitForTicksStep(var ticks):
                timer.Context.IncrementExecutionCount();
                timer.Context.ExpectedNextTimeMs = _currentTimeMs;
                _timerQueue.Enqueue(timer, _currentTick + ticks);
                break;
            case TimerStep.WaitForMillisecondsStep(var milliseconds):
                timer.Context.IncrementExecutionCount();
                timer.Context.ExpectedNextTimeMs += milliseconds;
                _timerQueueMs.Enqueue(timer, timer.Context.ExpectedNextTimeMs);
                break;
            case TimerStep.StopStep:
                timer.CancellationTokenSource.Cancel();
                break;
            default:
                break;
        }
    }

    public static void NextTick( Action task, CancellationToken ownerToken )
    {
        lock (_lock)
        {
            _nextTickTasks.Add((task, ownerToken));
        }
    }

    public static void NextWorldUpdate( Action task, CancellationToken ownerToken )
    {
        lock (_lock)
        {
            _nextWorldUpdateTasks.Add((task, ownerToken));
        }
    }

    public static CancellationTokenSource AddTimer( Func<TimerContext, TimerStep> task, CancellationToken ownerToken )
    {

        var cancellationTokenSource = new CancellationTokenSource();

        var _ = NextTickAsync(() => {
            var timer = new Timer {
                Task = task,
                CancellationTokenSource = cancellationTokenSource,
                OwnerToken = ownerToken,
            };
            ExecuteTimer(timer);
        });

        return cancellationTokenSource;
    }

    public static Task NextTickAsync()
    {
        var tcs = new TaskCompletionSource<bool>();

        _asyncOnTickTaskQueue.Enqueue(() =>
        {
            try
            {
                tcs.SetResult(true);
            }
            catch (Exception ex)
            {
                tcs.SetException(ex);
            }
        });

        return tcs.Task;
    }

    public static Task NextWorldUpdateAsync()
    {
        var tcs = new TaskCompletionSource<bool>();

        _asyncOnWorldUpdateTaskQueue.Enqueue(() =>
        {
            try
            {
                tcs.SetResult(true);
            }
            catch (Exception ex)
            {
                tcs.SetException(ex);
            }
        });

        return tcs.Task;
    }

    public static Task<T> NextTickAsync<T>( Func<T> task )
    {
        var tcs = new TaskCompletionSource<T>();

        _asyncOnTickTaskQueue.Enqueue(() =>
        {
            try
            {
                tcs.SetResult(task());
            }
            catch (Exception ex)
            {
                tcs.SetException(ex);
            }
        });

        return tcs.Task;
    }

    public static Task<T> NextWorldUpdateAsync<T>( Func<T> task )
    {
        var tcs = new TaskCompletionSource<T>();

        _asyncOnWorldUpdateTaskQueue.Enqueue(() =>
        {
            try
            {
                tcs.SetResult(task());
            }
            catch (Exception ex)
            {
                tcs.SetException(ex);
            }
        });

        return tcs.Task;
    }

    public static Task NextTickAsync( Action action )
    {
        var tcs = new TaskCompletionSource<bool>();

        _asyncOnTickTaskQueue.Enqueue(() =>
        {
            try
            {
                action();
                tcs.SetResult(true);
            }
            catch (Exception ex)
            {
                tcs.SetException(ex);
            }
        });

        return tcs.Task;
    }

    public static Task NextWorldUpdateAsync( Action action )
    {
        var tcs = new TaskCompletionSource<bool>();

        _asyncOnWorldUpdateTaskQueue.Enqueue(() =>
        {
            try
            {
                action();
                tcs.SetResult(true);
            }
            catch (Exception ex)
            {
                tcs.SetException(ex);
            }
        });

        return tcs.Task;
    }

    public static Task QueueOrNow( Action action )
    {
        if (NativeBinding.IsMainThread)
        {
            action();
            return Task.CompletedTask;
        }

        return NextWorldUpdateAsync(action);
    }

    public static Task<T> QueueOrNow<T>( Func<T> task )
    {
        if (NativeBinding.IsMainThread)
        {
            return Task.FromResult(task());
        }

        return NextWorldUpdateAsync(task);
    }
}