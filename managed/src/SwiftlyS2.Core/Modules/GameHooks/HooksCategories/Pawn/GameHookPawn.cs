using SwiftlyS2.Shared.GameHooks;

namespace SwiftlyS2.Core.GameHooks;

internal sealed class GameHookPawn( GameHooksService owner ) : IGameHookPawn
{
    internal readonly PostThinkPawnHook PostThinkHook = new(owner);
    internal readonly CanMovePawnHook CanMoveHook = new(owner);

    public IPostThinkPawnHook PostThink => PostThinkHook;
    public ICanMovePawnHook CanMove => CanMoveHook;
}
