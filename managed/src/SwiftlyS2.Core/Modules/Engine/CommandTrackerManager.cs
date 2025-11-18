using System.Collections.Concurrent;
using System.Text;
using Spectre.Console;
using SwiftlyS2.Shared.Events;
using SwiftlyS2.Shared.Misc;

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
        StartCleanupTimer();
    }

    public void ProcessCommand( IOnCommandExecuteHookEvent @event )
    {
        if (@event.HookMode == HookMode.Pre)
        {
            if (
                string.IsNullOrWhiteSpace(@event.Command[0])
                || !@event.Command[0]!.StartsWith("^wb^")
            )
            {
                return;
            }
            ProcessCommandStart(@event);
        }
        else if (@event.HookMode == HookMode.Post)
        {
            ProcessCommandEnd(@event);
        }
    }

    public void ProcessOutput( IOnConsoleOutputEvent @event )
    {
        if (disposed)
            return;

        var commandId = currentCommandContainer?.Value ?? Guid.Empty;
        if (commandId == Guid.Empty)
            return;

        if (activeCommands.TryGetValue(commandId, out var command) && command.Output.Count < 100)
        {
            command.Output.Enqueue(@event.Message);
        }
    }

    public void ProcessCommandStart( IOnCommandExecuteHookEvent @event )
    {
        if (pendingCallbacks.TryDequeue(out var callback))
        {
            var newCommandId = Guid.NewGuid();
            var newCommand = new ExecutingCommand(callback);

            if (activeCommands.TryAdd(newCommandId, newCommand))
            {
                var newContainer = new CommandIdContainer(newCommandId);
                _ = Interlocked.Exchange(ref currentCommandContainer, newContainer);
                _ = @event.Command.Tokenize(
                    $"{@event.Command[0]!.Replace("^wb^", string.Empty)} {@event.Command.ArgS}"
                );
            }
        }
        else
        {
            _ = Interlocked.Exchange(ref currentCommandContainer, CommandIdContainer.Empty);
        }
    }

#pragma warning disable IDE0058 // Expression value is never used
    public void ProcessCommandEnd( IOnCommandExecuteHookEvent _ )
    {
        var previousContainer = Interlocked.Exchange(
            ref currentCommandContainer,
            CommandIdContainer.Empty
        );
        var commandId = previousContainer?.Value ?? Guid.Empty;

        if (commandId != Guid.Empty && activeCommands.TryRemove(commandId, out var command))
        {
            var output = new StringBuilder();
            while (command.Output.TryDequeue(out var line))
            {
                if (output.Length > 0)
                    output.AppendLine();

                output.Append(line);
            }

            Task.Run(() => command.Callback.Invoke(output.ToString()));
        }
    }
#pragma warning restore IDE0058 // Expression value is never used

    private void StartCleanupTimer()
    {
        _ = Task.Run(
            async () =>
            {
                while (!cancellationTokenSource.Token.IsCancellationRequested)
                {
                    try
                    {
                        await Task.Delay(
                            TimeSpan.FromMilliseconds(200),
                            cancellationTokenSource.Token
                        );
                        CleanupExpiredCommands();
                    }
                    catch (Exception ex)
                    {
                        if (!GlobalExceptionHandler.Handle(ex))
                            return;
                        AnsiConsole.WriteException(ex);
                    }
                }
            },
            cancellationTokenSource.Token
        );
    }

    private void CleanupExpiredCommands()
    {
        foreach (var kvp in activeCommands.ToArray())
        {
            if (kvp.Value.IsExpired)
            {
                _ = activeCommands.TryRemove(kvp.Key, out _);
            }
        }
    }

    public void EnqueueCommand( Action<string> callback )
    {
        if (disposed)
            return;

        pendingCallbacks.Enqueue(callback);
    }

    public void Dispose()
    {
        if (disposed)
            return;
        disposed = true;

        cancellationTokenSource.Cancel();

        while (pendingCallbacks.TryDequeue(out _)) { }
        activeCommands.Clear();

        cancellationTokenSource.Dispose();
    }
}
