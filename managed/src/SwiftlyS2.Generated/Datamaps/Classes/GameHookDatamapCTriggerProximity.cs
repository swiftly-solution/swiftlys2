using SwiftlyS2.Shared.GameHooks;

namespace SwiftlyS2.Core.GameHooks;

internal sealed class GameHookDatamapCTriggerProximity : IGameHookDatamapCTriggerProximity
{
    internal readonly CTriggerProximityMeasureThinkHook CTriggerProximityMeasureThinkHook = new();

    public ICTriggerProximityMeasureThinkHook MeasureThink => CTriggerProximityMeasureThinkHook;

    internal void UnregisterListeners()
    {
        CTriggerProximityMeasureThinkHook.UnregisterListeners();
    }
}