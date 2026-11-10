using SwiftlyS2.Shared.GameHooks;

namespace SwiftlyS2.Core.GameHooks;

internal sealed class GameHookDatamapCLogicDistanceAutosave(GameHooksService owner) : IGameHookDatamapCLogicDistanceAutosave
{
    internal readonly CLogicDistanceAutosaveSaveThinkHook CLogicDistanceAutosaveSaveThinkHook = new(owner);

    public ICLogicDistanceAutosaveSaveThinkHook SaveThink => CLogicDistanceAutosaveSaveThinkHook;

    internal void UnregisterListeners()
    {
        CLogicDistanceAutosaveSaveThinkHook.UnregisterListeners();
    }
}