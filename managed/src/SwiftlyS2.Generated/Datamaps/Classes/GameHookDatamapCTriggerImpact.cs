using SwiftlyS2.Shared.GameHooks;

namespace SwiftlyS2.Core.GameHooks;

internal sealed class GameHookDatamapCTriggerImpact(GameHooksService owner) : IGameHookDatamapCTriggerImpact
{
    internal readonly CTriggerImpactDisableThinkHook CTriggerImpactDisableThinkHook = new(owner);

    public ICTriggerImpactDisableThinkHook DisableThink => CTriggerImpactDisableThinkHook;

    internal void UnregisterListeners()
    {
        CTriggerImpactDisableThinkHook.UnregisterListeners();
    }
}