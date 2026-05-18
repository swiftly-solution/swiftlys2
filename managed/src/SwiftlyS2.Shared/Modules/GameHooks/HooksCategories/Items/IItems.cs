namespace SwiftlyS2.Shared.GameHooks;

public interface IGameHookItem
{
    /// <summary>
    /// Event triggered when an item can acquire logic is ran by game.
    /// </summary>
    public ICanAcquireItemEvents CanAcquire { get; }
}