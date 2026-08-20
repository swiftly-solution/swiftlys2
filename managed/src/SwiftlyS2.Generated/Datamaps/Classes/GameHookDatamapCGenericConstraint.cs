using SwiftlyS2.Shared.GameHooks;

namespace SwiftlyS2.Core.GameHooks;

internal sealed class GameHookDatamapCGenericConstraint : IGameHookDatamapCGenericConstraint
{
    internal readonly CGenericConstraintUpdateThinkHook CGenericConstraintUpdateThinkHook = new();

    public ICGenericConstraintUpdateThinkHook UpdateThink => CGenericConstraintUpdateThinkHook;

    internal void UnregisterListeners()
    {
        CGenericConstraintUpdateThinkHook.UnregisterListeners();
    }
}