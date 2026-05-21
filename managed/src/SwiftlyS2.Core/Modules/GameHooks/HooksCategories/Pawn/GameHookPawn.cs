using SwiftlyS2.Shared.GameHooks;

namespace SwiftlyS2.Core.GameHooks;

internal sealed class GameHookPawn : IGameHookPawn
{
    internal readonly PostThinkPawnHook PostThinkHook = new();
    internal readonly CanMovePawnHook CanMoveHook = new();

    public IPostThinkPawnHook PostThink => PostThinkHook;
    public ICanMovePawnHook CanMove => CanMoveHook;
}
