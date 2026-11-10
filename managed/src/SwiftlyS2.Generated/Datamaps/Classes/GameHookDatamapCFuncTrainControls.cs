using SwiftlyS2.Shared.GameHooks;

namespace SwiftlyS2.Core.GameHooks;

internal sealed class GameHookDatamapCFuncTrainControls(GameHooksService owner) : IGameHookDatamapCFuncTrainControls
{
    internal readonly CFuncTrainControlsFindHook CFuncTrainControlsFindHook = new(owner);

    public ICFuncTrainControlsFindHook Find => CFuncTrainControlsFindHook;

    internal void UnregisterListeners()
    {
        CFuncTrainControlsFindHook.UnregisterListeners();
    }
}