using SwiftlyS2.Shared.Events;
using SwiftlyS2.Shared.Scheduler;

namespace SwiftlyS2.Core.Scheduler;

internal class SchedulerService : ISchedulerService, IDisposable {

  private readonly List<CancellationTokenSource> _timers = new();
  private readonly object _lock = new object();
  private readonly CancellationTokenSource _lifecycleCts = new();
  private CancellationTokenSource _mapChangeCts = new();

  public SchedulerService(IEventSubscriber eventSubscriber) {
    eventSubscriber.OnMapUnload += (@event) => {
      lock (_lock) {
        _mapChangeCts.Cancel();
        _mapChangeCts.Dispose();
        _mapChangeCts = new CancellationTokenSource();
      }
      
      CleanFinishedTimers();
    };

  }

  public void NextTick(Action task) {
    SchedulerManager.NextTick(task, _lifecycleCts.Token);
  }

  public CancellationTokenSource Delay(int delayTick, Action task) {
    CleanFinishedTimers();
    var cts = SchedulerManager.AddTimer(delayTick, 0, task, _lifecycleCts.Token);
    lock (_lock) {
      _timers.Add(cts);
    }
    return cts;
  }

  public CancellationTokenSource Repeat(int periodTick, bool stopOnMapChange, Action task) {
    var cts = SchedulerManager.AddTimer(0, periodTick, task, _lifecycleCts.Token);
    lock (_lock) {
      if (stopOnMapChange)
      {
        _mapChangeCts.Token.Register(cts.Cancel);
      }
      _timers.Add(cts);
    }
    return cts;
  }

  public CancellationTokenSource DelayAndRepeat(int delayTick, int periodTick, bool stopOnMapChange, Action task) {
    var cts = SchedulerManager.AddTimer(delayTick, periodTick, task, _lifecycleCts.Token);
    lock (_lock) {
      if (stopOnMapChange)
      {
        _mapChangeCts.Token.Register(cts.Cancel);
      }
      _timers.Add(cts);
    }
    return cts;
  }

  public CancellationTokenSource AddTimer(int delayTick, Action task) {
    CleanFinishedTimers();
    var cts = SchedulerManager.AddTimer(delayTick, 0, task, _lifecycleCts.Token);
    lock (_lock) {
      _timers.Add(cts);
    }
    return cts;
  }

  private void CleanFinishedTimers() {
    lock (_lock) {
      _timers.RemoveAll(timer => timer.IsCancellationRequested);
    }
  }

  public void Dispose() {
    lock (_lock) {
      if (_lifecycleCts.IsCancellationRequested) return;
      _lifecycleCts.Cancel();
      _lifecycleCts.Dispose();
      _mapChangeCts.Cancel();
      _mapChangeCts.Dispose();

      foreach (var timer in _timers) {
        timer.Cancel();
        timer.Dispose();
      }
      _timers.Clear();
    }
  }
}
