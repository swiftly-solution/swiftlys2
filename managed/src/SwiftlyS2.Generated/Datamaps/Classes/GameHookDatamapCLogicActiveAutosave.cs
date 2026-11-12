using SwiftlyS2.Shared.GameHooks;

namespace SwiftlyS2.Core.GameHooks;

internal sealed class GameHookDatamapCLogicActiveAutosave(GameHooksService owner) : IGameHookDatamapCLogicActiveAutosave
{
    internal readonly CLogicActiveAutosaveSaveThinkHook CLogicActiveAutosaveSaveThinkHook = new(owner);

    public ICLogicActiveAutosaveSaveThinkHook SaveThink => CLogicActiveAutosaveSaveThinkHook;

    internal void UnregisterListeners()
    {
        CLogicActiveAutosaveSaveThinkHook.UnregisterListeners();
    }
}