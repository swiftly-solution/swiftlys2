namespace SwiftlyS2.Shared.GameHooks;

public interface IGameHookDatamapCFuncMoveLinear
{
    public ICFuncMoveLinearNavMovableThinkHook NavMovableThink { get; }
    public ICFuncMoveLinearNavObstacleThinkHook NavObstacleThink { get; }
    public ICFuncMoveLinearStopMoveSoundHook StopMoveSound { get; }
}