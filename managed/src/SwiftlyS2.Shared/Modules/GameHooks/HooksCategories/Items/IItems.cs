namespace SwiftlyS2.Shared.GameHooks;

public interface IGameHookItem
{
    /// <summary>
    /// Hook triggered when an item can acquire logic is ran by game.
    /// </summary>
    public ICanAcquireItemHook CanAcquire { get; }
}
