using System.Runtime.InteropServices;
using SwiftlyS2.Shared;
using SwiftlyS2.Core.Services;

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

    private static readonly Dictionary<HookListener, Guid> hookIds = [];
    private static readonly bool IsWindows = RuntimeInformation.IsOSPlatform(OSPlatform.Windows);

    private static readonly SubscriberRouter<HookListener, GameHooksService> router = new(
        onFirst: hookName => hookIds[hookName] = HookFunction(hookName),
        onLast: hookName =>
        {
            _ = UnhookFunction(hookName);
            _ = hookIds.Remove(hookName);
        });

    private static GameHooksService[] GetSubscribers( HookListener hookName ) => router.Get(hookName);

    internal static void AddHookListener( HookListener hookName, GameHooksService owner ) => router.Add(hookName, owner);

    internal static void RemoveHookListener( HookListener hookName, GameHooksService owner ) => router.Remove(hookName, owner);

    internal static void AddHookListener( HookListener hookName ) => router.AddExternal(hookName);

    internal static void RemoveHookListener( HookListener hookName ) => router.RemoveExternal(hookName);

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
