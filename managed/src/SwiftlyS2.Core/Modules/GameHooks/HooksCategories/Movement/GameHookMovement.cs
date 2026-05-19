using SwiftlyS2.Shared.GameHooks;

namespace SwiftlyS2.Core.GameHooks;

internal sealed class GameHookMovement : IGameHookMovement
{
    internal readonly RunCommandMovementEvents RunCommandEvents = new();
    internal readonly SetupMoveMovementEvents SetupMoveEvents = new();
    internal readonly ProcessMovementMovementEvents ProcessMovementEvents = new();
    internal readonly CheckFallingMovementEvents CheckFallingEvents = new();
    internal readonly CategorizePositionMovementEvents CategorizePositionEvents = new();
    internal readonly TryPlayerMoveMovementEvents TryPlayerMoveEvents = new();
    internal readonly WalkMoveMovementEvents WalkMoveEvents = new();
    internal readonly FrictionMovementEvents FrictionEvents = new();
    internal readonly AirAccelerateMovementEvents AirAccelerateEvents = new();
    internal readonly AirMoveMovementEvents AirMoveEvents = new();
    internal readonly OnJumpModernMovementEvents OnJumpModernEvents = new();
    internal readonly OnJumpLegacyMovementEvents OnJumpLegacyEvents = new();
    internal readonly CheckJumpButtonModernMovementEvents CheckJumpButtonModernEvents = new();
    internal readonly CheckJumpButtonLegacyMovementEvents CheckJumpButtonLegacyEvents = new();
    internal readonly LadderMoveMovementEvents LadderMoveEvents = new();
    internal readonly CanUnduckMovementEvents CanUnduckEvents = new();
    internal readonly DuckMovementEvents DuckEvents = new();
    internal readonly CheckVelocityMovementEvents CheckVelocityEvents = new();
    internal readonly WaterMoveMovementEvents WaterMoveEvents = new();
    internal readonly CheckWaterMovementEvents CheckWaterEvents = new();
    internal readonly MoveInitMovementEvents MoveInitEvents = new();
    internal readonly FullWalkMoveMovementEvents FullWalkMoveEvents = new();
    internal readonly CheckParametersMovementEvents CheckParametersEvents = new();
    internal readonly PlayerMoveMovementEvents PlayerMoveEvents = new();
    internal readonly GroundAccelerateMovementEvents GroundAccelerateEvents = new();

    public IRunCommandMovementEvents RunCommand => RunCommandEvents;
    public ISetupMoveMovementEvents SetupMove => SetupMoveEvents;
    public IProcessMovementMovementEvents ProcessMovement => ProcessMovementEvents;
    public ICheckFallingMovementEvents CheckFalling => CheckFallingEvents;
    public ICategorizePositionMovementEvents CategorizePosition => CategorizePositionEvents;
    public ITryPlayerMoveMovementEvents TryPlayerMove => TryPlayerMoveEvents;
    public IWalkMoveMovementEvents WalkMove => WalkMoveEvents;
    public IFrictionMovementEvents Friction => FrictionEvents;
    public IAirAccelerateMovementEvents AirAccelerate => AirAccelerateEvents;
    public IAirMoveMovementEvents AirMove => AirMoveEvents;
    public IOnJumpModernMovementEvents OnJumpModern => OnJumpModernEvents;
    public IOnJumpLegacyMovementEvents OnJumpLegacy => OnJumpLegacyEvents;
    public ICheckJumpButtonModernMovementEvents CheckJumpButtonModern => CheckJumpButtonModernEvents;
    public ICheckJumpButtonLegacyMovementEvents CheckJumpButtonLegacy => CheckJumpButtonLegacyEvents;
    public ILadderMoveMovementEvents LadderMove => LadderMoveEvents;
    public ICanUnduckMovementEvents CanUnduck => CanUnduckEvents;
    public IDuckMovementEvents Duck => DuckEvents;
    public ICheckVelocityMovementEvents CheckVelocity => CheckVelocityEvents;
    public IWaterMoveMovementEvents WaterMove => WaterMoveEvents;
    public ICheckWaterMovementEvents CheckWater => CheckWaterEvents;
    public IMoveInitMovementEvents MoveInit => MoveInitEvents;
    public IFullWalkMoveMovementEvents FullWalkMove => FullWalkMoveEvents;
    public ICheckParametersMovementEvents CheckParameters => CheckParametersEvents;
    public IPlayerMoveMovementEvents PlayerMove => PlayerMoveEvents;
    public IGroundAccelerateMovementEvents GroundAccelerate => GroundAccelerateEvents;
}
