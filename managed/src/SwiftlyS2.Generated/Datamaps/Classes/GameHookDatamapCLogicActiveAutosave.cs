using SwiftlyS2.Shared.GameHooks;

namespace SwiftlyS2.Core.GameHooks;

internal sealed class GameHookDatamapCLogicActiveAutosave : IGameHookDatamapCLogicActiveAutosave
{
    internal readonly CLogicActiveAutosaveSaveThinkHook CLogicActiveAutosaveSaveThinkHook = new();

    public ICLogicActiveAutosaveSaveThinkHook SaveThink => CLogicActiveAutosaveSaveThinkHook;

    internal void UnregisterListeners()
    {
        CLogicActiveAutosaveSaveThinkHook.UnregisterListeners();
    }
}