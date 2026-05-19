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
}
