namespace SwiftlyS2.Shared.GameHooks;

public interface IGameHookMovement
{
    /// <summary>
    /// Event triggered when the player movement tick is being processed.
    /// </summary>
    public IRunCommandMovementEvents RunCommand { get; }

    /// <summary>
    /// Event triggered when the player movement data is set up.
    /// </summary>
    public ISetupMoveMovementEvents SetupMove { get; }

    /// <summary>
    /// Event triggered when the player movement data is being processed.
    /// </summary>
    public IProcessMovementMovementEvents ProcessMovement { get; }

    /// <summary>
    /// Event triggered when the player falling check is performed.
    /// </summary>
    public ICheckFallingMovementEvents CheckFalling { get; }

    /// <summary>
    /// Event triggered when the player position is categorized.
    /// </summary>
    public ICategorizePositionMovementEvents CategorizePosition { get; }

    /// <summary>
    /// Event triggered when the player tries to move.
    /// </summary>
    public ITryPlayerMoveMovementEvents TryPlayerMove { get; }

    /// <summary>
    /// Event triggered when the player performs a walk move.
    /// </summary>
    public IWalkMoveMovementEvents WalkMove { get; }

    /// <summary>
    /// Event triggered when friction is applied to the player.
    /// </summary>
    public IFrictionMovementEvents Friction { get; }

    /// <summary>
    /// Event triggered when the player performs an air accelerate.
    /// </summary>
    public IAirAccelerateMovementEvents AirAccelerate { get; }

    /// <summary>
    /// Event triggered when the player performs an air move.
    /// </summary>
    public IAirMoveMovementEvents AirMove { get; }

    /// <summary>
    /// Event triggered when the player performs a modern jump.
    /// </summary>
    public IOnJumpModernMovementEvents OnJumpModern { get; }

    /// <summary>
    /// Event triggered when the player performs a legacy jump.
    /// </summary>
    public IOnJumpLegacyMovementEvents OnJumpLegacy { get; }

    /// <summary>
    /// Event triggered when the modern jump button is checked.
    /// </summary>
    public ICheckJumpButtonModernMovementEvents CheckJumpButtonModern { get; }

    /// <summary>
    /// Event triggered when the legacy jump button is checked.
    /// </summary>
    public ICheckJumpButtonLegacyMovementEvents CheckJumpButtonLegacy { get; }

    /// <summary>
    /// Event triggered when the player performs a ladder move.
    /// </summary>
    public ILadderMoveMovementEvents LadderMove { get; }

    /// <summary>
    /// Event triggered when the player unduck check is performed.
    /// </summary>
    public ICanUnduckMovementEvents CanUnduck { get; }

    /// <summary>
    /// Event triggered when the player performs a duck.
    /// </summary>
    public IDuckMovementEvents Duck { get; }

    /// <summary>
    /// Event triggered when the player velocity is checked.
    /// </summary>
    public ICheckVelocityMovementEvents CheckVelocity { get; }

    /// <summary>
    /// Event triggered when the player performs a water move.
    /// </summary>
    public IWaterMoveMovementEvents WaterMove { get; }

    /// <summary>
    /// Event triggered when the player water check is performed.
    /// </summary>
    public ICheckWaterMovementEvents CheckWater { get; }

    /// <summary>
    /// Event triggered when the player move is initialized.
    /// </summary>
    public IMoveInitMovementEvents MoveInit { get; }

    /// <summary>
    /// Event triggered when the player performs a full walk move.
    /// </summary>
    public IFullWalkMoveMovementEvents FullWalkMove { get; }

    /// <summary>
    /// Event triggered when the player movement parameters are checked.
    /// </summary>
    public ICheckParametersMovementEvents CheckParameters { get; }

    /// <summary>
    /// Event triggered when the player move is processed.
    /// </summary>
    public IPlayerMoveMovementEvents PlayerMove { get; }

    /// <summary>
    /// Event triggered when ground acceleration is applied to the player.
    /// </summary>
    public IGroundAccelerateMovementEvents GroundAccelerate { get; }
}
