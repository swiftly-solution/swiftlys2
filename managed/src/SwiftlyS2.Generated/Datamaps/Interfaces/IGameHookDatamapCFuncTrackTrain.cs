namespace SwiftlyS2.Shared.GameHooks;

public interface IGameHookDatamapCFuncTrackTrain
{
    public ICFuncTrackTrainDeadEndHook DeadEnd { get; }
    public ICFuncTrackTrainFindHook Find { get; }
    public ICFuncTrackTrainNearestPathHook NearestPath { get; }
    public ICFuncTrackTrainNextHook Next { get; }
}