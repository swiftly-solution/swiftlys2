namespace SwiftlyS2.Shared.GameHooks;

public interface IGameHookPawn
{
    /// <summary>
    /// Hook triggered when the player pawn post think hook is triggered.
    /// </summary>
    public IPostThinkPawnHook PostThink { get; }

    /// <summary>
    /// Hook triggered when the player pawn can move check is performed.
    /// </summary>
    public ICanMovePawnHook CanMove { get; }
}
