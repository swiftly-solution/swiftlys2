using SwiftlyS2.Shared.GameHooks;

namespace SwiftlyS2.Core.GameHooks;

internal sealed class GameHookPawn : IGameHookPawn
{
    internal readonly PostThinkPawnEvents PostThinkEvents = new();
    internal readonly CanMovePawnEvents CanMoveEvents = new();

    public IPostThinkPawnEvents PostThink => PostThinkEvents;
    public ICanMovePawnEvents CanMove => CanMoveEvents;
}
