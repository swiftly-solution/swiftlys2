using SwiftlyS2.Shared.GameHooks;

namespace SwiftlyS2.Core.GameHooks;

internal sealed class GameHookDatamapCFuncTrackChange : IGameHookDatamapCFuncTrackChange
{
    internal readonly CFuncTrackChangeFindHook CFuncTrackChangeFindHook = new();

    public ICFuncTrackChangeFindHook Find => CFuncTrackChangeFindHook;

    internal void UnregisterListeners()
    {
        CFuncTrackChangeFindHook.UnregisterListeners();
    }
}