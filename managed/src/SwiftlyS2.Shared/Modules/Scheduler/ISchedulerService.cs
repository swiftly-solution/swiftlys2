namespace SwiftlyS2.Shared.Scheduler;

public interface ISchedulerService {

  /// <summary>
  /// Add a task to be executed on the next tick.
  /// </summary>
  /// <param name="task">The task to execute.</param>
  void NextTick(Action task);

  /// <summary>
  /// Add a timer to the scheduler.
  /// </summary>
  /// <param name="periodTick">The period of the timer in ticks.</param>
  /// <param name="task">The task to execute.</param>
  /// <param name="stopOnMapChange">Whether to stop the timer when the map changes.</param>
  /// <returns>A CancellationTokenSource that can be used to cancel the timer.</returns>
  CancellationTokenSource Delay(int delayTick, Action task);

  /// <summary>
  /// Add a timer to the scheduler.
  /// </summary>
  /// <param name="periodTick">The period of the timer in ticks.</param>
  /// <param name="stopOnMapChange">Whether to stop the timer when the map changes.</param>
  /// <param name="task">The task to execute.</param>
  /// <returns>A CancellationTokenSource that can be used to cancel the timer.</returns>
  CancellationTokenSource Repeat(int periodTick, bool stopOnMapChange, Action task);

  /// <summary>
  /// Add a timer to the scheduler.
  /// </summary>
  /// <param name="delayTick">The delay of the timer in ticks.</param>
  /// <param name="periodTick">The period of the timer in ticks.</param>
  /// <param name="stopOnMapChange">Whether to stop the timer when the map changes.</param>
  /// <param name="task">The task to execute.</param>
  /// <returns>A CancellationTokenSource that can be used to cancel the timer.</returns>
  CancellationTokenSource DelayAndRepeat(int delayTick, int periodTick, bool stopOnMapChange, Action task);

  /// <summary>
  /// Add a timer to the scheduler with flexible delay and period configuration.
  /// </summary>
  /// <param name="delayTick">The delay of the timer in ticks.</param>
  /// <param name="task">The task to execute.</param>
  /// <returns>A CancellationTokenSource that can be used to cancel the timer.</returns>
  CancellationTokenSource AddTimer(int delayTick, Action task);
}