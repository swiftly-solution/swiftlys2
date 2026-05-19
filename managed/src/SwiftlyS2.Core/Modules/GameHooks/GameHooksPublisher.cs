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
            _ => throw new ArgumentOutOfRangeException(nameof(hookName), $"No hook found for {hookName}"),
        };
    }
}
