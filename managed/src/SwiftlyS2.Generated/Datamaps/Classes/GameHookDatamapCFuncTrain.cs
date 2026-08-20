using SwiftlyS2.Shared.GameHooks;

namespace SwiftlyS2.Core.GameHooks;

internal sealed class GameHookDatamapCFuncTrain : IGameHookDatamapCFuncTrain
{
    internal readonly CFuncTrainNextHook CFuncTrainNextHook = new();
    internal readonly CFuncTrainWaitHook CFuncTrainWaitHook = new();

    public ICFuncTrainNextHook Next => CFuncTrainNextHook;
    public ICFuncTrainWaitHook Wait => CFuncTrainWaitHook;

    internal void UnregisterListeners()
    {
        CFuncTrainNextHook.UnregisterListeners();
        CFuncTrainWaitHook.UnregisterListeners();
    }
}