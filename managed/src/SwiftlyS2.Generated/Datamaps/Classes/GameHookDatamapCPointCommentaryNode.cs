using SwiftlyS2.Shared.GameHooks;

namespace SwiftlyS2.Core.GameHooks;

internal sealed class GameHookDatamapCPointCommentaryNode(GameHooksService owner) : IGameHookDatamapCPointCommentaryNode
{
    internal readonly CPointCommentaryNodeAcculumatePlayTimeThinkHook CPointCommentaryNodeAcculumatePlayTimeThinkHook = new(owner);
    internal readonly CPointCommentaryNodeSpinThinkHook CPointCommentaryNodeSpinThinkHook = new(owner);
    internal readonly CPointCommentaryNodeUpdateViewPostThinkHook CPointCommentaryNodeUpdateViewPostThinkHook = new(owner);
    internal readonly CPointCommentaryNodeUpdateViewThinkHook CPointCommentaryNodeUpdateViewThinkHook = new(owner);

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