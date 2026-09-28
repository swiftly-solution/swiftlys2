using System.Text;
using System.Collections.Concurrent;
using Spectre.Console;
using SwiftlyS2.Core.Events;
using SwiftlyS2.Shared.Misc;
using SwiftlyS2.Shared.Events;

namespace SwiftlyS2.Core.Services;

internal sealed class ExecutingCommand
{
    public required Action<string> Callback { get; init; }
    public ConcurrentQueue<string> Output { get; } = new();
    public DateTime Created { get; } = DateTime.UtcNow;
    public bool IsExpired => DateTime.UtcNow - Created > TimeSpan.FromMilliseconds(5000);
}

internal static class CommandTrackerManager
{
    private static readonly Lock _lock = new();
    private static readonly ConcurrentDictionary<Guid, ExecutingCommand> activeCommands = new();
    private static readonly ConcurrentQueue<Action<string>> pendingCallbacks = new();
    private static Guid currentCommandId = Guid.Empty;

    public static void EnqueueCommand( Action<string> callback )
    {
        pendingCallbacks.Enqueue(callback);
    }

    public static void ProcessCommand( IOnCommandExecuteHookEvent @event )
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

    public static bool IsTracking
    {
        get
        {
            lock (_lock)
            {
                return currentCommandId != Guid.Empty;
            }
        }
    }

    public static void ProcessOutput( string message )
    {
        Guid commandId;
        lock (_lock)
        {
            commandId = currentCommandId;
        }

        if (commandId == Guid.Empty)
        {
            return;
        }

        if (activeCommands.TryGetValue(commandId, out var command) && command.Output.Count < 100)
        {
            command.Output.Enqueue(message);
        }
    }

    private static void ProcessCommandStart( IOnCommandExecuteHookEvent @event )
    {
        PurgeExpired();

        if (pendingCallbacks.TryDequeue(out var callback))
        {
            var newCommandId = Guid.NewGuid();
            if (activeCommands.TryAdd(newCommandId, new ExecutingCommand { Callback = callback }))
            {
                lock (_lock)
                {
                    currentCommandId = newCommandId;
                }
                EventPublisher.AddConsoleOutputListener();
                var arg0 = @event.Command[0] ?? string.Empty;
                _ = @event.Command.Tokenize($"{arg0.Trim().Replace("ecwb", string.Empty)} {@event.Command.ArgS?.Trim()}");
            }
        }
        else
        {
            lock (_lock)
            {
                currentCommandId = Guid.Empty;
            }
        }
    }

    private static void ProcessCommandEnd()
    {
        Guid commandId;
        lock (_lock)
        {
            commandId = currentCommandId;
            currentCommandId = Guid.Empty;
        }

        if (commandId == Guid.Empty) return;

        EventPublisher.RemoveConsoleOutputListener();

        if (activeCommands.TryRemove(commandId, out var command))
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

            _ = Task.Run(() =>
            {
                try
                {
                    command.Callback.Invoke(output.ToString());
                }
                catch (Exception ex)
                {
                    if (GlobalExceptionHandler.Handle(ref ex)) AnsiConsole.WriteException(ex);
                }
            });
        }
    }

    public static void CancelCommand()
    {
        ProcessCommandEnd();
    }

    private static void PurgeExpired()
    {
        foreach (var kvp in activeCommands.ToArray())
        {
            if (kvp.Value.IsExpired)
            {
                _ = activeCommands.TryRemove(kvp.Key, out _);
            }
        }
    }
}
