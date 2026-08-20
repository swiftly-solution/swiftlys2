using SwiftlyS2.Shared.GameHooks;

namespace SwiftlyS2.Core.GameHooks;

internal sealed class GameHookDatamapCSplineConstraint : IGameHookDatamapCSplineConstraint
{
    internal readonly CSplineConstraintTransitionThinkHook CSplineConstraintTransitionThinkHook = new();

    public ICSplineConstraintTransitionThinkHook TransitionThink => CSplineConstraintTransitionThinkHook;

    internal void UnregisterListeners()
    {
        CSplineConstraintTransitionThinkHook.UnregisterListeners();
    }
}