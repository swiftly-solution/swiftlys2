using SwiftlyS2.Shared.GameHooks;

namespace SwiftlyS2.Core.GameHooks;

internal sealed class GameHookDatamapCTriggerImpact : IGameHookDatamapCTriggerImpact
{
    internal readonly CTriggerImpactDisableThinkHook CTriggerImpactDisableThinkHook = new();

    public ICTriggerImpactDisableThinkHook DisableThink => CTriggerImpactDisableThinkHook;

    internal void UnregisterListeners()
    {
        CTriggerImpactDisableThinkHook.UnregisterListeners();
    }
}