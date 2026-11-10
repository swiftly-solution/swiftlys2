using SwiftlyS2.Shared.GameHooks;

namespace SwiftlyS2.Core.GameHooks;

internal sealed class GameHookDatamapCLogicMeasureMovement(GameHooksService owner) : IGameHookDatamapCLogicMeasureMovement
{
    internal readonly CLogicMeasureMovementMeasureThinkHook CLogicMeasureMovementMeasureThinkHook = new(owner);

    public ICLogicMeasureMovementMeasureThinkHook MeasureThink => CLogicMeasureMovementMeasureThinkHook;

    internal void UnregisterListeners()
    {
        CLogicMeasureMovementMeasureThinkHook.UnregisterListeners();
    }
}