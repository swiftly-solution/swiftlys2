using SwiftlyS2.Shared;

namespace SwiftlyS2.Core.GameHooks;

internal static partial class DatamapHooksPublisher
{
    private static ISwiftlyCore? _core;
    private static bool _serverStarted;

    internal static void Initialize( ISwiftlyCore core )
    {
        _core = core;
        core.Event.OnStartupServer += OnStartupServer;
    }

    private static readonly List<GameHooksService> subscribers = [];
    private static readonly Lock subscribersLock = new();

    private static readonly Dictionary<DatamapHookListener, int> hookListeners = [];
    private static readonly HashSet<DatamapHookListener> pendingHookListeners = [];
    private static readonly Lock hookListenersLock = new();

    private static readonly Dictionary<DatamapHookListener, Guid> hookIds = [];

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

    internal static void AddHookListener( DatamapHookListener hookName )
    {
        lock (hookListenersLock)
        {
            hookListeners[hookName] = hookListeners.TryGetValue(hookName, out var value) ? ++value : 1;
            if (hookListeners[hookName] == 1)
            {
                if (_serverStarted)
                {
                    hookIds[hookName] = HookFunction(hookName);
                }
                else
                {
                    _ = pendingHookListeners.Add(hookName);
                }
            }
        }
    }

    internal static void RemoveHookListener( DatamapHookListener hookName )
    {
        lock (hookListenersLock)
        {
            if (hookListeners.ContainsKey(hookName))
            {
                if (--hookListeners[hookName] <= 0)
                {
                    _ = hookListeners.Remove(hookName);
                    if (pendingHookListeners.Remove(hookName))
                    {
                        return;
                    }
                    _ = UnhookFunction(hookName);
                    _ = hookIds.Remove(hookName);
                }
            }
        }
    }

    private static void OnStartupServer()
    {
        lock (hookListenersLock)
        {
            _serverStarted = true;
            foreach (var hookName in pendingHookListeners)
            {
                hookIds[hookName] = HookFunction(hookName);
            }
            pendingHookListeners.Clear();
        }
    }
}
