using SwiftlyS2.Shared.GameHooks;

namespace SwiftlyS2.Core.GameHooks;

internal sealed class GameHookDatamapCPathNode(GameHooksService owner) : IGameHookDatamapCPathNode
{
    internal readonly CPathNodeParentedMoveThinkHook CPathNodeParentedMoveThinkHook = new(owner);

    public ICPathNodeParentedMoveThinkHook ParentedMoveThink => CPathNodeParentedMoveThinkHook;

    internal void UnregisterListeners()
    {
        CPathNodeParentedMoveThinkHook.UnregisterListeners();
    }
}