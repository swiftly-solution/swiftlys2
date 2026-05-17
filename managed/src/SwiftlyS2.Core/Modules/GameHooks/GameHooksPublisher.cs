using SwiftlyS2.Shared;

namespace SwiftlyS2.Core.GameHooks;

internal enum HookListener
{
    ProcessUsercmds,
    CanAcquire,
}

internal static partial class GameHooksPublisher
{
    private static ISwiftlyCore? _core;

    internal static void Initialize( ISwiftlyCore core )
    {
        _core = core;
    }

    private static readonly List<GameHooksService> subscribers = [];
    private static readonly Lock subscribersLock = new();

    private static readonly Dictionary<HookListener, int> hookListeners = [];
    private static readonly Lock hookListenersLock = new();

    private static readonly Dictionary<HookListener, Guid> hookIds = [];

    internal static void Subscribe( GameHooksService subscriber )
    {
        lock (subscribersLock)
        {
            subscribers.Add(subscriber);
        }
    }

    internal static void Unsubscribe( GameHooksService subscriber )
    {
        lock (subscribersLock)
        {
            _ = subscribers.Remove(subscriber);
        }
    }

    internal static void AddHookListener( HookListener hookName )
    {
        lock (hookListenersLock)
        {
            hookListeners[hookName] = hookListeners.TryGetValue(hookName, out var value) ? ++value : 1;
            if (hookListeners[hookName] == 1)
            {
                hookIds[hookName] = HookFunction(hookName);
            }
        }
    }

    internal static void RemoveHookListener( HookListener hookName )
    {
        lock (hookListenersLock)
        {
            if (hookListeners.ContainsKey(hookName))
            {
                if (--hookListeners[hookName] <= 0)
                {
                    _ = hookListeners.Remove(hookName);
                    _ = UnhookFunction(hookName);
                    _ = hookIds.Remove(hookName);
                }
            }
        }
    }

    internal static Guid HookFunction( HookListener hookName )
    {
        return hookName switch {
            HookListener.ProcessUsercmds => HookProcessUsercmds(),
            HookListener.CanAcquire => HookCanAcquire(),
            _ => throw new ArgumentOutOfRangeException(nameof(hookName), $"No hook found for {hookName}"),
        };

    }

    internal static Guid UnhookFunction( HookListener hookName )
    {
        return hookName switch {
            HookListener.ProcessUsercmds => UnhookProcessUsercmds(),
            HookListener.CanAcquire => UnhookCanAcquire(),
            _ => throw new ArgumentOutOfRangeException(nameof(hookName), $"No hook found for {hookName}"),
        };

    }
}
