using SwiftlyS2.Shared.GameHooks;

namespace SwiftlyS2.Core.GameHooks;

internal sealed class GameHookDatamapCGenericConstraint(GameHooksService owner) : IGameHookDatamapCGenericConstraint
{
    internal readonly CGenericConstraintUpdateThinkHook CGenericConstraintUpdateThinkHook = new(owner);

    public ICGenericConstraintUpdateThinkHook UpdateThink => CGenericConstraintUpdateThinkHook;

    internal void UnregisterListeners()
    {
        CGenericConstraintUpdateThinkHook.UnregisterListeners();
    }
}