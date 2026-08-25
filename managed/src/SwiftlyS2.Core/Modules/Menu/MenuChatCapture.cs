using System.Collections.Concurrent;
using Microsoft.Extensions.Logging;
using SwiftlyS2.Shared.Commands;
using SwiftlyS2.Shared.Misc;

namespace SwiftlyS2.Core.Menu;

internal sealed class MenuChatCapture( ILogger<MenuChatCapture> logger ) : IDisposable
{
    private readonly ConcurrentDictionary<int, Func<string, bool>> handlers = new();
    private readonly Lock hookLock = new();

    private ICommandService? commands;
    private Guid hook;
    private bool hooked;

    public void Attach( ICommandService commandService )
    {
        lock (hookLock)
        {
            commands = commandService;
        }
    }

    public IDisposable Capture( int playerId, Func<string, bool> handler )
    {
        EnsureHooked();
        handlers[playerId] = handler;
        return new Handle(this, playerId, handler);
    }

    public void Release( int playerId ) => _ = handlers.TryRemove(playerId, out _);

    public void Dispose()
    {
        handlers.Clear();

        lock (hookLock)
        {
            if (hooked && commands is not null)
            {
                commands.UnhookClientChat(hook);
            }

            hooked = false;
            hook = Guid.Empty;
            commands = null;
        }
    }

    private void EnsureHooked()
    {
        lock (hookLock)
        {
            if (hooked)
            {
                return;
            }

            if (commands is null)
            {
                logger.LogWarning("Menu chat capture was requested before the menu framework finished starting.");
                return;
            }

            hook = commands.HookClientChat(OnClientChat);
            hooked = true;
        }
    }

    private HookResult OnClientChat( int playerId, string text, bool teamonly )
    {
        if (!handlers.TryGetValue(playerId, out var handler))
        {
            return HookResult.Continue;
        }

        try
        {
            return handler(text) ? HookResult.Stop : HookResult.Continue;
        }
        catch (Exception ex)
        {
            logger.LogError(ex, "Menu chat capture threw for player {PlayerId}.", playerId);
            return HookResult.Continue;
        }
    }

    private sealed class Handle( MenuChatCapture owner, int playerId, Func<string, bool> handler ) : IDisposable
    {
        public void Dispose()
        {
            if (owner.handlers.TryGetValue(playerId, out var current) && ReferenceEquals(current, handler))
            {
                owner.Release(playerId);
            }
        }
    }
}
