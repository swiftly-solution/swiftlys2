using SwiftlyS2.Shared.GameHooks;

namespace SwiftlyS2.Core.GameHooks;

internal sealed class GameHookDatamapCFuncTrain(GameHooksService owner) : IGameHookDatamapCFuncTrain
{
    internal readonly CFuncTrainNextHook CFuncTrainNextHook = new(owner);
    internal readonly CFuncTrainWaitHook CFuncTrainWaitHook = new(owner);

    public ICFuncTrainNextHook Next => CFuncTrainNextHook;
    public ICFuncTrainWaitHook Wait => CFuncTrainWaitHook;

    internal void UnregisterListeners()
    {
        CFuncTrainNextHook.UnregisterListeners();
        CFuncTrainWaitHook.UnregisterListeners();
    }
}