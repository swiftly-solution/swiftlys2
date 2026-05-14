namespace SwiftlyS2.Shared.GameHooks;

public interface IGameHookMovement
{
    /// <summary>
    /// Event triggered when the player movement tick is being processed.
    /// </summary>
    public IRunCommandMovementEvents RunCommand { get; }
}