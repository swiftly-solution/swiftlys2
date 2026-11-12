using SwiftlyS2.Shared.GameHooks;

namespace SwiftlyS2.Core.GameHooks;

internal sealed class GameHookDatamapCFuncMoveLinear(GameHooksService owner) : IGameHookDatamapCFuncMoveLinear
{
    internal readonly CFuncMoveLinearNavMovableThinkHook CFuncMoveLinearNavMovableThinkHook = new(owner);
    internal readonly CFuncMoveLinearNavObstacleThinkHook CFuncMoveLinearNavObstacleThinkHook = new(owner);
    internal readonly CFuncMoveLinearStopMoveSoundHook CFuncMoveLinearStopMoveSoundHook = new(owner);

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