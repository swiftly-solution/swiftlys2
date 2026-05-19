using System.Runtime.InteropServices;
using SwiftlyS2.Shared;

namespace SwiftlyS2.Core.GameHooks;

internal enum HookListener
{
    ProcessUsercmds,
    CanAcquire,
    RunCommand,
    PostThink,
    CanUse,
    WeaponDrop,
    SimulateUserCommands,
    SetupMove,
    ProcessMovement,
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
    private static readonly bool IsWindows = RuntimeInformation.IsOSPlatform(OSPlatform.Windows);

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
            HookListener.RunCommand => HookRunCommand(),
            HookListener.PostThink => HookPostThink(),
            HookListener.CanUse => HookCanUse(),
            HookListener.WeaponDrop => HookDropWeapon(),
            HookListener.SimulateUserCommands => HookSimulateUserCommands(),
            HookListener.SetupMove => HookSetupMove(),
            HookListener.ProcessMovement => HookProcessMovement(),
            _ => throw new ArgumentOutOfRangeException(nameof(hookName), $"No hook found for {hookName}"),
        };

    }

    internal static Guid UnhookFunction( HookListener hookName )
    {
        return hookName switch {
            HookListener.ProcessUsercmds => UnhookProcessUsercmds(),
            HookListener.CanAcquire => UnhookCanAcquire(),
            HookListener.RunCommand => UnhookRunCommand(),
            HookListener.PostThink => UnhookPostThink(),
            HookListener.CanUse => UnhookCanUse(),
            HookListener.WeaponDrop => UnhookDropWeapon(),
            HookListener.SimulateUserCommands => UnhookSimulateUserCommands(),
            HookListener.SetupMove => UnhookSetupMove(),
            HookListener.ProcessMovement => UnhookProcessMovement(),
            _ => throw new ArgumentOutOfRangeException(nameof(hookName), $"No hook found for {hookName}"),
        };
    }
}
