using SwiftlyS2.Shared;
using SwiftlyS2.Core.Services;

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

    private static readonly HashSet<DatamapHookListener> pendingHookListeners = [];
    private static readonly Dictionary<DatamapHookListener, Guid> hookIds = [];

    private static readonly SubscriberRouter<DatamapHookListener, GameHooksService> router = new(
        onFirst: hookName =>
        {
            if (_serverStarted)
            {
                hookIds[hookName] = HookFunction(hookName);
            }
            else
            {
                _ = pendingHookListeners.Add(hookName);
            }
        },
        onLast: hookName =>
        {
            if (pendingHookListeners.Remove(hookName))
            {
                return;
            }
            _ = UnhookFunction(hookName);
            _ = hookIds.Remove(hookName);
        });

    private static GameHooksService[] GetSubscribers( DatamapHookListener hookName ) => router.Get(hookName);

    internal static void AddHookListener( DatamapHookListener hookName, GameHooksService owner ) => router.Add(hookName, owner);

    internal static void RemoveHookListener( DatamapHookListener hookName, GameHooksService owner ) => router.Remove(hookName, owner);

    private static void OnStartupServer()
    {
        lock (router.Gate)
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
