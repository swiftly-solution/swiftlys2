using SwiftlyS2.Shared.GameHooks;

namespace SwiftlyS2.Core.GameHooks;

internal sealed class GameHookDatamapCSplineConstraint(GameHooksService owner) : IGameHookDatamapCSplineConstraint
{
    internal readonly CSplineConstraintTransitionThinkHook CSplineConstraintTransitionThinkHook = new(owner);

    public ICSplineConstraintTransitionThinkHook TransitionThink => CSplineConstraintTransitionThinkHook;

    internal void UnregisterListeners()
    {
        CSplineConstraintTransitionThinkHook.UnregisterListeners();
    }
}