using SwiftlyS2.Shared.GameHooks;

namespace SwiftlyS2.Core.GameHooks;

internal sealed class GameHookDatamapCFuncTrackChange(GameHooksService owner) : IGameHookDatamapCFuncTrackChange
{
    internal readonly CFuncTrackChangeFindHook CFuncTrackChangeFindHook = new(owner);

    public ICFuncTrackChangeFindHook Find => CFuncTrackChangeFindHook;

    internal void UnregisterListeners()
    {
        CFuncTrackChangeFindHook.UnregisterListeners();
    }
}