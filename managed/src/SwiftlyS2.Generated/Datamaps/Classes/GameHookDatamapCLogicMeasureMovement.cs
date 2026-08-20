using SwiftlyS2.Shared.GameHooks;

namespace SwiftlyS2.Core.GameHooks;

internal sealed class GameHookDatamapCLogicMeasureMovement : IGameHookDatamapCLogicMeasureMovement
{
    internal readonly CLogicMeasureMovementMeasureThinkHook CLogicMeasureMovementMeasureThinkHook = new();

    public ICLogicMeasureMovementMeasureThinkHook MeasureThink => CLogicMeasureMovementMeasureThinkHook;

    internal void UnregisterListeners()
    {
        CLogicMeasureMovementMeasureThinkHook.UnregisterListeners();
    }
}