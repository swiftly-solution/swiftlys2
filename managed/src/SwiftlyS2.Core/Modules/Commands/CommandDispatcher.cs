using System.Collections.Concurrent;
using SwiftlyS2.Core.Events;
using SwiftlyS2.Core.Natives;
using SwiftlyS2.Shared.Misc;

namespace SwiftlyS2.Core.Commands;

internal static unsafe class CommandDispatcher
{
    private static readonly ConcurrentDictionary<string, List<CommandCallback>> commands = new();

    private static readonly Dictionary<string, ulong> nativeCommandIds = [];

    private static readonly ConcurrentDictionary<string, string> aliasToCommand = new();

    private static ClientCommandListenerCallback[] clientCommandListeners = [];
    private static ClientChatListenerCallback[] clientChatListeners = [];

    private static readonly Lock writeLock = new();

    internal static void Register( CommandCallbackBase callback )
    {
        lock (writeLock)
        {
            switch (callback)
            {
                case CommandCallback command:
                    RegisterCommand(command);
                    break;
                case ClientCommandListenerCallback listener:
                    clientCommandListeners = [.. clientCommandListeners, listener];
                    if (clientCommandListeners.Length == 1)
                    {
                        NativeCommands.SetClientCommandHandler((nint)(delegate* unmanaged<int, nint, int>)&EventPublisher.OnClientCommandDispatch);
                    }
                    break;
                case ClientChatListenerCallback listener:
                    clientChatListeners = [.. clientChatListeners, listener];
                    if (clientChatListeners.Length == 1)
                    {
                        NativeCommands.SetClientChatHandler((nint)(delegate* unmanaged<int, nint, byte, int>)&EventPublisher.OnClientChatDispatch);
                    }
                    break;
            }
        }
    }

    internal static void Unregister( CommandCallbackBase callback )
    {
        lock (writeLock)
        {
            switch (callback)
            {
                case CommandCallback command:
                    UnregisterCommand(command);
                    break;
                case ClientCommandListenerCallback listener:
                    clientCommandListeners = [.. clientCommandListeners.Where(other => !ReferenceEquals(other, listener))];
                    if (clientCommandListeners.Length == 0)
                    {
                        NativeCommands.SetClientCommandHandler(0);
                    }
                    break;
                case ClientChatListenerCallback listener:
                    clientChatListeners = [.. clientChatListeners.Where(other => !ReferenceEquals(other, listener))];
                    if (clientChatListeners.Length == 0)
                    {
                        NativeCommands.SetClientChatHandler(0);
                    }
                    break;
            }
        }
    }

    internal static string? ResolveCommandKey( string commandName )
    {
        var lowerName = commandName.ToLowerInvariant();
        if (commands.ContainsKey(lowerName))
        {
            return lowerName;
        }

        var prefixedName = "sw_" + lowerName;
        return commands.ContainsKey(prefixedName) ? prefixedName : null;
    }

    internal static void AddAlias( string normalizedAlias, string commandKey ) =>
        aliasToCommand[normalizedAlias] = commandKey;

    internal static void RemoveAlias( string normalizedAlias ) =>
        _ = aliasToCommand.TryRemove(normalizedAlias, out _);

    internal static IEnumerable<CommandCallback> GetCommands() =>
        commands.Values.SelectMany(callbacks => callbacks);

    internal static string NormalizeName( string commandName, bool registerRaw ) =>
        registerRaw ? commandName.ToLowerInvariant() : "sw_" + commandName.ToLowerInvariant();

    internal static void DispatchCommand( string commandName, int playerId, string[] args, string originalCommandName, string prefix, bool silent )
    {
        var key = aliasToCommand.TryGetValue(commandName, out var original) ? original : commandName;
        if (!commands.TryGetValue(key, out var callbacks))
        {
            return;
        }

        foreach (var callback in callbacks)
        {
            callback.Invoke(playerId, args, originalCommandName, prefix, silent);
        }
    }

    internal static int DispatchClientCommand( int playerId, string commandLine )
    {
        var cancelOriginal = false;
        foreach (var listener in clientCommandListeners)
        {
            switch (listener.Invoke(playerId, commandLine))
            {
                case HookResult.Stop:
                    return (int)HookResult.Stop;
                case HookResult.Handled:
                    return (int)HookResult.Handled;
                case HookResult.CancelOriginal:
                    cancelOriginal = true;
                    break;
            }
        }

        return (int)(cancelOriginal ? HookResult.CancelOriginal : HookResult.Continue);
    }

    internal static int DispatchClientChat( int playerId, string text, bool teamonly )
    {
        var cancelOriginal = false;
        foreach (var listener in clientChatListeners)
        {
            switch (listener.Invoke(playerId, text, teamonly))
            {
                case HookResult.Stop:
                    return (int)HookResult.Stop;
                case HookResult.Handled:
                    return (int)HookResult.Handled;
                case HookResult.CancelOriginal:
                    cancelOriginal = true;
                    break;
            }
        }

        return (int)(cancelOriginal ? HookResult.CancelOriginal : HookResult.Continue);
    }

    private static void RegisterCommand( CommandCallback callback )
    {
        var key = callback.NormalizedName;
        if (commands.TryGetValue(key, out var existing))
        {
            commands[key] = [.. existing, callback];
            return;
        }

        commands[key] = [callback];
        nativeCommandIds[key] = NativeCommands.RegisterCommand(callback.CommandName, callback.RegisterRaw, callback.HelpText);
    }

    private static void UnregisterCommand( CommandCallback callback )
    {
        var key = callback.NormalizedName;
        if (!commands.TryGetValue(key, out var existing) || !existing.Contains(callback))
        {
            return;
        }

        var remaining = existing.FindAll(other => !ReferenceEquals(other, callback));
        if (remaining.Count != 0)
        {
            commands[key] = remaining;
            return;
        }

        _ = commands.TryRemove(key, out _);

        if (nativeCommandIds.Remove(key, out var nativeId))
        {
            NativeCommands.UnregisterCommand(nativeId);
        }
    }
}
