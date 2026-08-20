using SwiftlyS2.Shared.GameHooks;

namespace SwiftlyS2.Core.GameHooks;

internal sealed class GameHookDatamapCLogicDistanceAutosave : IGameHookDatamapCLogicDistanceAutosave
{
    internal readonly CLogicDistanceAutosaveSaveThinkHook CLogicDistanceAutosaveSaveThinkHook = new();

    public ICLogicDistanceAutosaveSaveThinkHook SaveThink => CLogicDistanceAutosaveSaveThinkHook;

    internal void UnregisterListeners()
    {
        CLogicDistanceAutosaveSaveThinkHook.UnregisterListeners();
    }
}