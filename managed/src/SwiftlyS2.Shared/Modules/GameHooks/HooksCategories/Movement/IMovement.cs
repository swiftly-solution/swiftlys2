namespace SwiftlyS2.Shared.GameHooks;

public interface IGameHookMovement
{
    /// <summary>
    /// Hook triggered when the player movement tick is being processed.
    /// </summary>
    public IRunCommandMovementHook RunCommand { get; }

    /// <summary>
    /// Hook triggered when the player movement data is set up.
    /// </summary>
    public ISetupMoveMovementHook SetupMove { get; }

    /// <summary>
    /// Hook triggered when the player movement data is being processed.
    /// </summary>
    public IProcessMovementMovementHook ProcessMovement { get; }

    /// <summary>
    /// Hook triggered when the player falling check is performed.
    /// </summary>
    public ICheckFallingMovementHook CheckFalling { get; }

    /// <summary>
    /// Hook triggered when the player position is categorized.
    /// </summary>
    public ICategorizePositionMovementHook CategorizePosition { get; }

    /// <summary>
    /// Hook triggered when the player tries to move.
    /// </summary>
    public ITryPlayerMoveMovementHook TryPlayerMove { get; }

    /// <summary>
    /// Hook triggered when the player performs a walk move.
    /// </summary>
    public IWalkMoveMovementHook WalkMove { get; }

    /// <summary>
    /// Hook triggered when friction is applied to the player.
    /// </summary>
    public IFrictionMovementHook Friction { get; }

    /// <summary>
    /// Hook triggered when the player performs an air accelerate.
    /// </summary>
    public IAirAccelerateMovementHook AirAccelerate { get; }

    /// <summary>
    /// Hook triggered when the player performs an air move.
    /// </summary>
    public IAirMoveMovementHook AirMove { get; }

    /// <summary>
    /// Hook triggered when the player performs a modern jump.
    /// </summary>
    public IOnJumpModernMovementHook OnJumpModern { get; }

    /// <summary>
    /// Hook triggered when the player performs a legacy jump.
    /// </summary>
    public IOnJumpLegacyMovementHook OnJumpLegacy { get; }

    /// <summary>
    /// Hook triggered when the modern jump button is checked.
    /// </summary>
    public ICheckJumpButtonModernMovementHook CheckJumpButtonModern { get; }

    /// <summary>
    /// Hook triggered when the legacy jump button is checked.
    /// </summary>
    public ICheckJumpButtonLegacyMovementHook CheckJumpButtonLegacy { get; }

    /// <summary>
    /// Hook triggered when the player performs a ladder move.
    /// </summary>
    public ILadderMoveMovementHook LadderMove { get; }

    /// <summary>
    /// Hook triggered when the player unduck check is performed.
    /// </summary>
    public ICanUnduckMovementHook CanUnduck { get; }

    /// <summary>
    /// Hook triggered when the player performs a duck.
    /// </summary>
    public IDuckMovementHook Duck { get; }

    /// <summary>
    /// Hook triggered when the player velocity is checked.
    /// </summary>
    public ICheckVelocityMovementHook CheckVelocity { get; }

    /// <summary>
    /// Hook triggered when the player performs a water move.
    /// </summary>
    public IWaterMoveMovementHook WaterMove { get; }

    /// <summary>
    /// Hook triggered when the player water check is performed.
    /// </summary>
    public ICheckWaterMovementHook CheckWater { get; }

    /// <summary>
    /// Hook triggered when the player move is initialized.
    /// </summary>
    public IMoveInitMovementHook MoveInit { get; }

    /// <summary>
    /// Hook triggered when the player performs a full walk move.
    /// </summary>
    public IFullWalkMoveMovementHook FullWalkMove { get; }

    /// <summary>
    /// Hook triggered when the player movement parameters are checked.
    /// </summary>
    public ICheckParametersMovementHook CheckParameters { get; }

    /// <summary>
    /// Hook triggered when the player move is processed.
    /// </summary>
    public IPlayerMoveMovementHook PlayerMove { get; }

    /// <summary>
    /// Hook triggered when ground acceleration is applied to the player.
    /// </summary>
    public IGroundAccelerateMovementHook GroundAccelerate { get; }
}
