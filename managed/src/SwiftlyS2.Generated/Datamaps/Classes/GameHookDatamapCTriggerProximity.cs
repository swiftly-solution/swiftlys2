using SwiftlyS2.Shared.GameHooks;

namespace SwiftlyS2.Core.GameHooks;

internal sealed class GameHookDatamapCTriggerProximity(GameHooksService owner) : IGameHookDatamapCTriggerProximity
{
    internal readonly CTriggerProximityMeasureThinkHook CTriggerProximityMeasureThinkHook = new(owner);

    public ICTriggerProximityMeasureThinkHook MeasureThink => CTriggerProximityMeasureThinkHook;

    internal void UnregisterListeners()
    {
        CTriggerProximityMeasureThinkHook.UnregisterListeners();
    }
}