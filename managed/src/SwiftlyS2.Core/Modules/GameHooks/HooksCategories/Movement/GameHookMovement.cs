using SwiftlyS2.Shared.GameHooks;

namespace SwiftlyS2.Core.GameHooks;

internal sealed class GameHookMovement : IGameHookMovement
{
    internal readonly RunCommandMovementHook RunCommandHook = new();
    internal readonly SetupMoveMovementHook SetupMoveHook = new();
    internal readonly ProcessMovementMovementHook ProcessMovementHook = new();
    internal readonly CheckFallingMovementHook CheckFallingHook = new();
    internal readonly CategorizePositionMovementHook CategorizePositionHook = new();
    internal readonly TryPlayerMoveMovementHook TryPlayerMoveHook = new();
    internal readonly WalkMoveMovementHook WalkMoveHook = new();
    internal readonly FrictionMovementHook FrictionHook = new();
    internal readonly AirAccelerateMovementHook AirAccelerateHook = new();
    internal readonly AirMoveMovementHook AirMoveHook = new();
    internal readonly OnJumpModernMovementHook OnJumpModernHook = new();
    internal readonly OnJumpLegacyMovementHook OnJumpLegacyHook = new();
    internal readonly CheckJumpButtonModernMovementHook CheckJumpButtonModernHook = new();
    internal readonly CheckJumpButtonLegacyMovementHook CheckJumpButtonLegacyHook = new();
    internal readonly LadderMoveMovementHook LadderMoveHook = new();
    internal readonly CanUnduckMovementHook CanUnduckHook = new();
    internal readonly DuckMovementHook DuckHook = new();
    internal readonly CheckVelocityMovementHook CheckVelocityHook = new();
    internal readonly WaterMoveMovementHook WaterMoveHook = new();
    internal readonly CheckWaterMovementHook CheckWaterHook = new();
    internal readonly MoveInitMovementHook MoveInitHook = new();
    internal readonly FullWalkMoveMovementHook FullWalkMoveHook = new();
    internal readonly CheckParametersMovementHook CheckParametersHook = new();
    internal readonly PlayerMoveMovementHook PlayerMoveHook = new();
    internal readonly GroundAccelerateMovementHook GroundAccelerateHook = new();

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
