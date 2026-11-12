using SwiftlyS2.Shared.GameHooks;

namespace SwiftlyS2.Core.GameHooks;

internal sealed class GameHookMovement( GameHooksService owner ) : IGameHookMovement
{
    internal readonly RunCommandMovementHook RunCommandHook = new(owner);
    internal readonly SetupMoveMovementHook SetupMoveHook = new(owner);
    internal readonly ProcessMovementMovementHook ProcessMovementHook = new(owner);
    internal readonly CheckFallingMovementHook CheckFallingHook = new(owner);
    internal readonly CategorizePositionMovementHook CategorizePositionHook = new(owner);
    internal readonly TryPlayerMoveMovementHook TryPlayerMoveHook = new(owner);
    internal readonly WalkMoveMovementHook WalkMoveHook = new(owner);
    internal readonly FrictionMovementHook FrictionHook = new(owner);
    internal readonly AirAccelerateMovementHook AirAccelerateHook = new(owner);
    internal readonly AirMoveMovementHook AirMoveHook = new(owner);
    internal readonly OnJumpModernMovementHook OnJumpModernHook = new(owner);
    internal readonly OnJumpLegacyMovementHook OnJumpLegacyHook = new(owner);
    internal readonly CheckJumpButtonModernMovementHook CheckJumpButtonModernHook = new(owner);
    internal readonly CheckJumpButtonLegacyMovementHook CheckJumpButtonLegacyHook = new(owner);
    internal readonly LadderMoveMovementHook LadderMoveHook = new(owner);
    internal readonly CanUnduckMovementHook CanUnduckHook = new(owner);
    internal readonly DuckMovementHook DuckHook = new(owner);
    internal readonly CheckVelocityMovementHook CheckVelocityHook = new(owner);
    internal readonly WaterMoveMovementHook WaterMoveHook = new(owner);
    internal readonly CheckWaterMovementHook CheckWaterHook = new(owner);
    internal readonly MoveInitMovementHook MoveInitHook = new(owner);
    internal readonly FullWalkMoveMovementHook FullWalkMoveHook = new(owner);
    internal readonly CheckParametersMovementHook CheckParametersHook = new(owner);
    internal readonly PlayerMoveMovementHook PlayerMoveHook = new(owner);
    internal readonly GroundAccelerateMovementHook GroundAccelerateHook = new(owner);

    public IRunCommandMovementHook RunCommand => RunCommandHook;
    public ISetupMoveMovementHook SetupMove => SetupMoveHook;
    public IProcessMovementMovementHook ProcessMovement => ProcessMovementHook;
    public ICheckFallingMovementHook CheckFalling => CheckFallingHook;
    public ICategorizePositionMovementHook CategorizePosition => CategorizePositionHook;
    public ITryPlayerMoveMovementHook TryPlayerMove => TryPlayerMoveHook;
    public IWalkMoveMovementHook WalkMove => WalkMoveHook;
    public IFrictionMovementHook Friction => FrictionHook;
    public IAirAccelerateMovementHook AirAccelerate => AirAccelerateHook;
    public IAirMoveMovementHook AirMove => AirMoveHook;
    public IOnJumpModernMovementHook OnJumpModern => OnJumpModernHook;
    public IOnJumpLegacyMovementHook OnJumpLegacy => OnJumpLegacyHook;
    public ICheckJumpButtonModernMovementHook CheckJumpButtonModern => CheckJumpButtonModernHook;
    public ICheckJumpButtonLegacyMovementHook CheckJumpButtonLegacy => CheckJumpButtonLegacyHook;
    public ILadderMoveMovementHook LadderMove => LadderMoveHook;
    public ICanUnduckMovementHook CanUnduck => CanUnduckHook;
    public IDuckMovementHook Duck => DuckHook;
    public ICheckVelocityMovementHook CheckVelocity => CheckVelocityHook;
    public IWaterMoveMovementHook WaterMove => WaterMoveHook;
    public ICheckWaterMovementHook CheckWater => CheckWaterHook;
    public IMoveInitMovementHook MoveInit => MoveInitHook;
    public IFullWalkMoveMovementHook FullWalkMove => FullWalkMoveHook;
    public ICheckParametersMovementHook CheckParameters => CheckParametersHook;
    public IPlayerMoveMovementHook PlayerMove => PlayerMoveHook;
    public IGroundAccelerateMovementHook GroundAccelerate => GroundAccelerateHook;
}
