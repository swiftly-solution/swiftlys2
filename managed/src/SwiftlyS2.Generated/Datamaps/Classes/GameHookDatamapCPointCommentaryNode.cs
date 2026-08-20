using SwiftlyS2.Shared.GameHooks;

namespace SwiftlyS2.Core.GameHooks;

internal sealed class GameHookDatamapCPointCommentaryNode : IGameHookDatamapCPointCommentaryNode
{
    internal readonly CPointCommentaryNodeAcculumatePlayTimeThinkHook CPointCommentaryNodeAcculumatePlayTimeThinkHook = new();
    internal readonly CPointCommentaryNodeSpinThinkHook CPointCommentaryNodeSpinThinkHook = new();
    internal readonly CPointCommentaryNodeUpdateViewPostThinkHook CPointCommentaryNodeUpdateViewPostThinkHook = new();
    internal readonly CPointCommentaryNodeUpdateViewThinkHook CPointCommentaryNodeUpdateViewThinkHook = new();

    public ICPointCommentaryNodeAcculumatePlayTimeThinkHook AcculumatePlayTimeThink => CPointCommentaryNodeAcculumatePlayTimeThinkHook;
    public ICPointCommentaryNodeSpinThinkHook SpinThink => CPointCommentaryNodeSpinThinkHook;
    public ICPointCommentaryNodeUpdateViewPostThinkHook UpdateViewPostThink => CPointCommentaryNodeUpdateViewPostThinkHook;
    public ICPointCommentaryNodeUpdateViewThinkHook UpdateViewThink => CPointCommentaryNodeUpdateViewThinkHook;

    internal void UnregisterListeners()
    {
        CPointCommentaryNodeAcculumatePlayTimeThinkHook.UnregisterListeners();
        CPointCommentaryNodeSpinThinkHook.UnregisterListeners();
        CPointCommentaryNodeUpdateViewPostThinkHook.UnregisterListeners();
        CPointCommentaryNodeUpdateViewThinkHook.UnregisterListeners();
    }
}