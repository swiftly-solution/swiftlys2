using SwiftlyS2.Shared.GameHooks;

namespace SwiftlyS2.Core.GameHooks;

internal sealed class GameHookDatamapCFuncTrainControls : IGameHookDatamapCFuncTrainControls
{
    internal readonly CFuncTrainControlsFindHook CFuncTrainControlsFindHook = new();

    public ICFuncTrainControlsFindHook Find => CFuncTrainControlsFindHook;

    internal void UnregisterListeners()
    {
        CFuncTrainControlsFindHook.UnregisterListeners();
    }
}