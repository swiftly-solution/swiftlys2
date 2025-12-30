using System.Text;
using System.Collections.Concurrent;
using Spectre.Console;
using SwiftlyS2.Shared.Misc;
using SwiftlyS2.Shared.Events;

namespace SwiftlyS2.Core.Services;

internal sealed class CommandTrackerManager : IDisposable
{
    private sealed record CommandIdContainer( Guid Value )
    {
        public static readonly CommandIdContainer Empty = new(Guid.Empty);
    }

    private readonly record struct ExecutingCommand( Action<string> Callback )
    {
        public ConcurrentQueue<string> Output { get; } = new();
        public DateTime Created { get; } = DateTime.UtcNow;
        public bool IsExpired => DateTime.UtcNow - Created > TimeSpan.FromMilliseconds(5000);
    }

    private volatile CommandIdContainer currentCommandContainer = CommandIdContainer.Empty;
    private readonly ConcurrentDictionary<Guid, ExecutingCommand> activeCommands = new();
    private readonly CancellationTokenSource cancellationTokenSource = new();
    private readonly ConcurrentQueue<Action<string>> pendingCallbacks = new();
    private volatile bool disposed;

    public CommandTrackerManager()
    {
        disposed = false;
        StartCleanupTimer();
    }

    ~CommandTrackerManager()
    {
        Dispose();
    }

    public void Dispose()
    {
        if (disposed)
        {
            return;
        }
        disposed = true;

        cancellationTokenSource.Cancel();

        while (pendingCallbacks.TryDequeue(out _)) { }
        activeCommands.Clear();

        cancellationTokenSource.Dispose();
    }

    public void EnqueueCommand( Action<string> callback )
    {
        if (disposed)
        {
            return;
        }

        pendingCallbacks.Enqueue(callback);
    }

    public void ProcessCommand( IOnCommandExecuteHookEvent @event )
    {
        if (@event.HookMode == HookMode.Pre)
        {
            if (@event.Command[0]?.StartsWith("ecwb", StringComparison.OrdinalIgnoreCase) ?? false)
            {
                ProcessCommandStart(@event);
            }
        }
        else if (@event.HookMode == HookMode.Post)
        {
            ProcessCommandEnd();
        }
    }

    public void ProcessOutput( IOnConsoleOutputEvent @event )
    {
        if (disposed)
        {
            return;
        }

        var commandId = currentCommandContainer?.Value ?? Guid.Empty;
        if (commandId == Guid.Empty)
        {
            return;
        }

        if (activeCommands.TryGetValue(commandId, out var command) && command.Output.Count < 100)
        {
            command.Output.Enqueue(@event.Message);
        }
    }

    private void ProcessCommandStart( IOnCommandExecuteHookEvent @event )
    {
        if (pendingCallbacks.TryDequeue(out var callback))
        {
            var newCommandId = Guid.NewGuid();
            if (activeCommands.TryAdd(newCommandId, new ExecutingCommand(callback)))
            {
                _ = Interlocked.Exchange(ref currentCommandContainer, new CommandIdContainer(newCommandId));
                var arg0 = @event.Command[0] ?? string.Empty;
                _ = @event.Command.Tokenize($"{arg0.Trim().Replace("ecwb", string.Empty)} {@event.Command.ArgS?.Trim()}");
            }
        }
        else
        {
            _ = Interlocked.Exchange(ref currentCommandContainer, CommandIdContainer.Empty);
        }
    }

    private void ProcessCommandEnd()
    {
        var commandId = Interlocked.Exchange(ref currentCommandContainer, CommandIdContainer.Empty)?.Value ?? Guid.Empty;
        if (commandId != Guid.Empty && activeCommands.TryRemove(commandId, out var command))
        {
            var output = new StringBuilder();
            while (command.Output.TryDequeue(out var line))
            {
                if (output.Length > 0)
                {
                    output = output.AppendLine();
                }
                output = output.Append(line);
            }

            _ = Task.Run(() => command.Callback.Invoke(output.ToString()));
        }
    }

    private void StartCleanupTimer()
    {
        _ = Task.Run(async () =>
        {
            while (!cancellationTokenSource.Token.IsCancellationRequested)
            {
                try
                {
                    await Task.Delay(TimeSpan.FromMilliseconds(200), cancellationTokenSource.Token);
                    foreach (var kvp in activeCommands.ToArray())
                    {
                        if (kvp.Value.IsExpired)
                        {
                            _ = activeCommands.TryRemove(kvp.Key, out _);
                        }
                    }
                }
                catch (Exception ex)
                {
                    if (!GlobalExceptionHandler.Handle(ex))
                    {
                        return;
                    }
                    AnsiConsole.WriteException(ex);
                }
            }
        }, cancellationTokenSource.Token);
    }
}