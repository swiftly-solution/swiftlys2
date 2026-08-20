using SwiftlyS2.Shared.GameHooks;

namespace SwiftlyS2.Core.GameHooks;

internal sealed class GameHookDatamapCFuncMoveLinear : IGameHookDatamapCFuncMoveLinear
{
    internal readonly CFuncMoveLinearNavMovableThinkHook CFuncMoveLinearNavMovableThinkHook = new();
    internal readonly CFuncMoveLinearNavObstacleThinkHook CFuncMoveLinearNavObstacleThinkHook = new();
    internal readonly CFuncMoveLinearStopMoveSoundHook CFuncMoveLinearStopMoveSoundHook = new();

    public ICFuncMoveLinearNavMovableThinkHook NavMovableThink => CFuncMoveLinearNavMovableThinkHook;
    public ICFuncMoveLinearNavObstacleThinkHook NavObstacleThink => CFuncMoveLinearNavObstacleThinkHook;
    public ICFuncMoveLinearStopMoveSoundHook StopMoveSound => CFuncMoveLinearStopMoveSoundHook;

    internal void UnregisterListeners()
    {
        CFuncMoveLinearNavMovableThinkHook.UnregisterListeners();
        CFuncMoveLinearNavObstacleThinkHook.UnregisterListeners();
        CFuncMoveLinearStopMoveSoundHook.UnregisterListeners();
    }
}