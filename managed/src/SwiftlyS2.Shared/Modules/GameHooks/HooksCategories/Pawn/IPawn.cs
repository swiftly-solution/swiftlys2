namespace SwiftlyS2.Shared.GameHooks;

public interface IGameHookPawn
{
    /// <summary>
    /// Event triggered when the player pawn post think hook is triggered.
    /// </summary>
    public IPostThinkPawnEvents PostThink { get; }

    /// <summary>
    /// Event triggered when the player pawn can move check is performed.
    /// </summary>
    public ICanMovePawnEvents CanMove { get; }
}
