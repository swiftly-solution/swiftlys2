using System.Reflection;
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
    CheckFalling,
    CategorizePosition,
    TryPlayerMove,
    WalkMove,
    Friction,
    AirAccelerate,
    AirMove,
    OnJumpModern,
    OnJumpLegacy,
    CheckJumpButtonModern,
    CheckJumpButtonLegacy,
    LadderMove,
    CanUnduck,
    Duck,
    CheckVelocity,
    WaterMove,
    CheckWater,
    MoveInit,
    FullWalkMove,
    CheckParameters,
    PlayerMove,
    CanMove,
    GroundAccelerate,
    TakeDamage,
    StartTouch,
    Touch,
    EndTouch,
    AcceptInput,
    FireOutput,
    GetItemInLoadout,
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

    private static class Listeners<TDelegate> where TDelegate : Delegate
    {
        internal static GameHooksService[] Snapshot = [];

        private const BindingFlags Fields = BindingFlags.Instance | BindingFlags.NonPublic;
        private static Func<GameHooksService, bool>? hasListeners;

        // Services subscribe empty and clear their listeners before unsubscribing.
        // Rebuild only on presence transitions, under subscribersLock.
        internal static void Refresh()
        {
            // Discover fields on subscription, never on a phase's first empty dispatch.
            hasListeners ??= CreateListenerPredicate();
            var snapshot = subscribers.Where(hasListeners).ToArray();
            Volatile.Write(ref Snapshot, snapshot);
        }

        private static Func<GameHooksService, bool> CreateListenerPredicate()
        {
            return typeof(GameHooksService).GetFields(Fields)
                .SelectMany(category => category.FieldType.GetFields(Fields),
                    ( category, hook ) => new { Category = category, Hook = hook })
                .SelectMany(path => path.Hook.FieldType.GetFields(Fields),
                    ( path, listener ) => new { path.Category, path.Hook, Listener = listener })
                .Where(path => path.Listener.FieldType == typeof(TDelegate))
                .Select(path => new Func<GameHooksService, bool>(service =>
                {
                    var category = path.Category.GetValue(service);
                    var hook = path.Hook.GetValue(category);
                    return path.Listener.GetValue(hook) != null;
                }))
                .Single();
        }
    }

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

    internal static void UpdateListener<TDelegate>( HookListener hook, ref TDelegate? listeners, TDelegate? value, bool add ) where TDelegate : Delegate
    {
        lock (subscribersLock)
        {
            var updated = (TDelegate?)(add ? Delegate.Combine(listeners, value) : Delegate.Remove(listeners, value));
            var hadListeners = listeners != null;
            var hasListeners = updated != null;

            if (!hadListeners && hasListeners)
            {
                AddHookListener(hook);
            }

            Volatile.Write(ref listeners, updated);

            if (hadListeners && !hasListeners)
            {
                RemoveHookListener(hook);
            }

            if (hadListeners != hasListeners)
            {
                Listeners<TDelegate>.Refresh();
            }
        }
    }

    internal static void ClearListeners<TDelegate>( HookListener hook, ref TDelegate? listeners ) where TDelegate : Delegate
    {
        lock (subscribersLock)
        {
            UpdateListener(hook, ref listeners, listeners, add: false);
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
            HookListener.CheckFalling => HookCheckFalling(),
            HookListener.CategorizePosition => HookCategorizePosition(),
            HookListener.TryPlayerMove => HookTryPlayerMove(),
            HookListener.WalkMove => HookWalkMove(),
            HookListener.Friction => HookFriction(),
            HookListener.AirAccelerate => HookAirAccelerate(),
            HookListener.AirMove => HookAirMove(),
            HookListener.OnJumpModern => HookOnJumpModern(),
            HookListener.OnJumpLegacy => HookOnJumpLegacy(),
            HookListener.CheckJumpButtonModern => HookCheckJumpButtonModern(),
            HookListener.CheckJumpButtonLegacy => HookCheckJumpButtonLegacy(),
            HookListener.LadderMove => HookLadderMove(),
            HookListener.CanUnduck => HookCanUnduck(),
            HookListener.Duck => HookDuck(),
            HookListener.CheckVelocity => HookCheckVelocity(),
            HookListener.WaterMove => HookWaterMove(),
            HookListener.CheckWater => HookCheckWater(),
            HookListener.MoveInit => HookMoveInit(),
            HookListener.FullWalkMove => HookFullWalkMove(),
            HookListener.CheckParameters => HookCheckParameters(),
            HookListener.PlayerMove => HookPlayerMove(),
            HookListener.CanMove => HookCanMove(),
            HookListener.GroundAccelerate => HookGroundAccelerate(),
            HookListener.TakeDamage => HookTakeDamage(),
            HookListener.StartTouch => HookStartTouch(),
            HookListener.Touch => HookTouch(),
            HookListener.EndTouch => HookEndTouch(),
            HookListener.AcceptInput => HookAcceptInput(),
            HookListener.FireOutput => HookFireOutput(),
            HookListener.GetItemInLoadout => HookGetItemInLoadout(),
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
            HookListener.CheckFalling => UnhookCheckFalling(),
            HookListener.CategorizePosition => UnhookCategorizePosition(),
            HookListener.TryPlayerMove => UnhookTryPlayerMove(),
            HookListener.WalkMove => UnhookWalkMove(),
            HookListener.Friction => UnhookFriction(),
            HookListener.AirAccelerate => UnhookAirAccelerate(),
            HookListener.AirMove => UnhookAirMove(),
            HookListener.OnJumpModern => UnhookOnJumpModern(),
            HookListener.OnJumpLegacy => UnhookOnJumpLegacy(),
            HookListener.CheckJumpButtonModern => UnhookCheckJumpButtonModern(),
            HookListener.CheckJumpButtonLegacy => UnhookCheckJumpButtonLegacy(),
            HookListener.LadderMove => UnhookLadderMove(),
            HookListener.CanUnduck => UnhookCanUnduck(),
            HookListener.Duck => UnhookDuck(),
            HookListener.CheckVelocity => UnhookCheckVelocity(),
            HookListener.WaterMove => UnhookWaterMove(),
            HookListener.CheckWater => UnhookCheckWater(),
            HookListener.MoveInit => UnhookMoveInit(),
            HookListener.FullWalkMove => UnhookFullWalkMove(),
            HookListener.CheckParameters => UnhookCheckParameters(),
            HookListener.PlayerMove => UnhookPlayerMove(),
            HookListener.CanMove => UnhookCanMove(),
            HookListener.GroundAccelerate => UnhookGroundAccelerate(),
            HookListener.TakeDamage => UnhookTakeDamage(),
            HookListener.StartTouch => UnhookStartTouch(),
            HookListener.Touch => UnhookTouch(),
            HookListener.EndTouch => UnhookEndTouch(),
            HookListener.AcceptInput => UnhookAcceptInput(),
            HookListener.FireOutput => UnhookFireOutput(),
            HookListener.GetItemInLoadout => UnhookGetItemInLoadout(),
            _ => throw new ArgumentOutOfRangeException(nameof(hookName), $"No hook found for {hookName}"),
        };
    }
}
