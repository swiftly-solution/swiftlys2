using SwiftlyS2.Shared.GameHooks;

namespace SwiftlyS2.Core.GameHooks;

internal sealed class GameHookDatamapCFuncTrackTrain(GameHooksService owner) : IGameHookDatamapCFuncTrackTrain
{
    internal readonly CFuncTrackTrainDeadEndHook CFuncTrackTrainDeadEndHook = new(owner);
    internal readonly CFuncTrackTrainFindHook CFuncTrackTrainFindHook = new(owner);
    internal readonly CFuncTrackTrainNearestPathHook CFuncTrackTrainNearestPathHook = new(owner);
    internal readonly CFuncTrackTrainNextHook CFuncTrackTrainNextHook = new(owner);

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