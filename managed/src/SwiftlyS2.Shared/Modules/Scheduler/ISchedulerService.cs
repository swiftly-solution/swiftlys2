namespace SwiftlyS2.Shared.Scheduler;

public interface ISchedulerService
{

  /// <summary>
  /// Add a task to be executed on the next tick.
  /// </summary>
  /// <param name="task">The task to execute.</param>
  public void NextTick( Action task );

  /// <summary>
  /// Never use this! you should never calls async callback in next tick,
  /// because async callback have chance to run in async context, which breaks the the synchronization safety this function gives.
  /// </summary>
  /// <exception cref="InvalidOperationException">Thrown when this method is called.</exception>
  [Obsolete("Please remove the async modifier on your callback for safety reason. See comments for more details.")]
  public void NextTick( Func<Task?> task );

  /// <summary>
  /// Never use this! you should never calls async callback in next tick,
  /// because async callback have chance to run in async context, which breaks the the synchronization safety this function gives.
  /// </summary>
  /// <exception cref="InvalidOperationException">Thrown when this method is called.</exception>
  [Obsolete("Please remove the async modifier on your callback for safety reason. See comments for more details.")]
  public void NextTick<T>( Func<Task<T?>> task );

  /// <summary>
  /// Add a task to be executed on the next tick asynchronously.
  /// </summary>
  /// <param name="task">The task to execute.</param>
  public Task NextTickAsync( Action task );

  /// <summary>
  /// Never use this! you should never calls async callback in next tick,
  /// because async callback have chance to run in async context, which breaks the the synchronization safety this function gives.
  /// </summary>
  /// <exception cref="InvalidOperationException">Thrown when this method is called.</exception>
  [Obsolete("Please remove the async modifier on your callback for safety reason. See comments for more details.")]
  public void NextTickAsync( Func<Task?> task );

  /// <summary>
  /// Never use this! you should never calls async callback in next tick,
  /// because async callback have chance to run in async context, which breaks the the synchronization safety this function gives.
  /// </summary>
  /// <exception cref="InvalidOperationException">Thrown when this method is called.</exception>
  [Obsolete("Please remove the async modifier on your callback for safety reason. See comments for more details.")]
  public void NextTickAsync<T>( Func<Task<T?>> task );

  /// <summary>
  /// Add a task to be executed on the next tick asynchronously.
  /// </summary>
  /// <param name="task">The task to execute.</param>
  public Task<T> NextTickAsync<T>( Func<T> task );

  /// <summary>
  /// Add a task to be executed on the next world update.
  /// </summary>
  /// <param name="task">The task to execute.</param>
  public void NextWorldUpdate( Action task );

  /// <summary>
  /// Never use this! you should never calls async callback in next world update,
  /// because async callback have chance to run in async context, which breaks the the synchronization safety this function gives.
  /// </summary>
  /// <exception cref="InvalidOperationException">Thrown when this method is called.</exception>
  [Obsolete("Please remove the async modifier on your callback for safety reason. See comments for more details.")]
  public void NextWorldUpdate( Func<Task?> task );

  /// <summary>
  /// Never use this! you should never calls async callback in next world update,
  /// because async callback have chance to run in async context, which breaks the the synchronization safety this function gives.
  /// </summary>
  /// <exception cref="InvalidOperationException">Thrown when this method is called.</exception>
  [Obsolete("Please remove the async modifier on your callback for safety reason. See comments for more details.")]
  public void NextWorldUpdate<T>( Func<Task<T?>> task );

  /// <summary>
  /// Add a task to be executed on the next world update asynchronously.
  /// </summary>
  /// <param name="task">The task to execute.</param>
  public Task NextWorldUpdateAsync( Action task );

  /// <summary>
  /// Never use this! you should never calls async callback in next world update,
  /// because async callback have chance to run in async context, which breaks the the synchronization safety this function gives.
  /// </summary>
  /// <exception cref="InvalidOperationException">Thrown when this method is called.</exception>
  [Obsolete("Please remove the async modifier on your callback for safety reason. See comments for more details.")]
  public void NextWorldUpdateAsync( Func<Task?> task );

  /// <summary>
  /// Never use this! you should never calls async callback in next world update,
  /// because async callback have chance to run in async context, which breaks the the synchronization safety this function gives.
  /// </summary>
  /// <exception cref="InvalidOperationException">Thrown when this method is called.</exception>
  [Obsolete("Please remove the async modifier on your callback for safety reason. See comments for more details.")]
  public Task<T> NextWorldUpdateAsync<T>( Func<Task<T?>> task );

  /// <summary>
  /// Add a task to be executed on the next world update asynchronously.
  /// </summary>
  /// <param name="task">The task to execute.</param>
  public Task<T> NextWorldUpdateAsync<T>( Func<T> task );

  /// <summary>
  /// Add a delayed task to the scheduler.
  /// </summary>
  /// <param name="delayTick">The delay of the timer in ticks.</param>
  /// <param name="task">The task to execute.</param>
  /// <returns>A CancellationTokenSource that can be used to cancel the timer.</returns>
  public CancellationTokenSource Delay( int delayTick, Action task );

  /// <summary>
  /// Add a repeated task to the scheduler.
  /// This will be executed once immediately, and then every periodTick ticks.
  /// </summary>
  /// <param name="periodTick">The period of the timer in ticks.</param>
  /// <param name="task">The task to execute.</param>
  /// <returns>A CancellationTokenSource that can be used to cancel the timer.</returns>
  public CancellationTokenSource Repeat( int periodTick, Action task );

  /// <summary>
  /// Add a delayed and repeated task to the scheduler.
  /// </summary>
  /// <param name="delayTick">The delay of the timer in ticks.</param>
  /// <param name="periodTick">The period of the timer in ticks.</param>
  /// <param name="task">The task to execute.</param>
  /// <returns>A CancellationTokenSource that can be used to cancel the timer.</returns>
  public CancellationTokenSource DelayAndRepeat( int delayTick, int periodTick, Action task );


  /// <summary>
  /// Add a delayed task to the scheduler.
  /// 
  /// The timing is based on game tick, which means it becomes inaccurate when intervals approachs 1 tick (approximately 15ms).
  /// </summary>
  /// <param name="delaySeconds">The delay of the timer in seconds.</param>
  /// <param name="task">The task to execute.</param>
  /// <returns>A CancellationTokenSource that can be used to cancel the timer.</returns>
  public CancellationTokenSource DelayBySeconds( float delaySeconds, Action task );

  /// <summary>
  /// Add a repeated task to the scheduler.
  /// This will be executed once immediately, and then every periodSeconds seconds.
  /// 
  /// The timing is based on game tick, which means it becomes inaccurate when intervals approachs 1 tick (approximately 15ms).
  /// </summary>
  /// <param name="periodSeconds">The period of the timer in seconds.</param>
  /// <param name="task">The task to execute.</param>
  /// <returns>A CancellationTokenSource that can be used to cancel the timer.</returns>
  public CancellationTokenSource RepeatBySeconds( float periodSeconds, Action task );

  /// <summary>
  /// Add a delayed and repeated task to the scheduler.
  /// 
  /// The timing is based on game tick, which means it becomes inaccurate when intervals approachs 1 tick (approximately 15ms).
  /// </summary>
  /// <param name="delaySeconds">The delay of the timer in seconds.</param>
  /// <param name="periodSeconds">The period of the timer in seconds.</param>
  /// <param name="task">The task to execute.</param>
  /// <returns>A CancellationTokenSource that can be used to cancel the timer.</returns>
  public CancellationTokenSource DelayAndRepeatBySeconds( float delaySeconds, float periodSeconds, Action task );


  /// <summary>
  /// Add an advanced timer to the scheduler.
  /// </summary>
  /// <param name="task">The task to execute.</param>
  /// <returns>A CancellationTokenSource that can be used to cancel the timer.</returns>
  public CancellationTokenSource AddTimer( Func<ITimerContext, TimerStep> task );

  /// <summary>
  /// Stop a timer when the map changes.
  /// </summary>
  /// <param name="cts">The CancellationTokenSource to stop.</param>
  public void StopOnMapChange( CancellationTokenSource cts );
}