using SwiftlyS2.Shared.GameHooks;

namespace SwiftlyS2.Core.GameHooks;

internal sealed class GameHookDatamapCFuncTrackTrain : IGameHookDatamapCFuncTrackTrain
{
    internal readonly CFuncTrackTrainDeadEndHook CFuncTrackTrainDeadEndHook = new();
    internal readonly CFuncTrackTrainFindHook CFuncTrackTrainFindHook = new();
    internal readonly CFuncTrackTrainNearestPathHook CFuncTrackTrainNearestPathHook = new();
    internal readonly CFuncTrackTrainNextHook CFuncTrackTrainNextHook = new();

    public ICFuncTrackTrainDeadEndHook DeadEnd => CFuncTrackTrainDeadEndHook;
    public ICFuncTrackTrainFindHook Find => CFuncTrackTrainFindHook;
    public ICFuncTrackTrainNearestPathHook NearestPath => CFuncTrackTrainNearestPathHook;
    public ICFuncTrackTrainNextHook Next => CFuncTrackTrainNextHook;

    internal void UnregisterListeners()
    {
        CFuncTrackTrainDeadEndHook.UnregisterListeners();
        CFuncTrackTrainFindHook.UnregisterListeners();
        CFuncTrackTrainNearestPathHook.UnregisterListeners();
        CFuncTrackTrainNextHook.UnregisterListeners();
    }
}