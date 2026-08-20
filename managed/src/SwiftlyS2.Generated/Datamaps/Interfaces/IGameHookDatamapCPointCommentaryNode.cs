namespace SwiftlyS2.Shared.GameHooks;

public interface IGameHookDatamapCPointCommentaryNode
{
    public ICPointCommentaryNodeAcculumatePlayTimeThinkHook AcculumatePlayTimeThink { get; }
    public ICPointCommentaryNodeSpinThinkHook SpinThink { get; }
    public ICPointCommentaryNodeUpdateViewPostThinkHook UpdateViewPostThink { get; }
    public ICPointCommentaryNodeUpdateViewThinkHook UpdateViewThink { get; }
}