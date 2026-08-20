using SwiftlyS2.Shared.GameHooks;

namespace SwiftlyS2.Core.GameHooks;

internal sealed class GameHookDatamapCPathNode : IGameHookDatamapCPathNode
{
    internal readonly CPathNodeParentedMoveThinkHook CPathNodeParentedMoveThinkHook = new();

    public ICPathNodeParentedMoveThinkHook ParentedMoveThink => CPathNodeParentedMoveThinkHook;

    internal void UnregisterListeners()
    {
        CPathNodeParentedMoveThinkHook.UnregisterListeners();
    }
}