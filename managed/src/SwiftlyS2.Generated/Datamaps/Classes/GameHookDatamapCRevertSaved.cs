using SwiftlyS2.Shared.GameHooks;

namespace SwiftlyS2.Core.GameHooks;

internal sealed class GameHookDatamapCRevertSaved(GameHooksService owner) : IGameHookDatamapCRevertSaved
{
    internal readonly CRevertSavedLoadThinkHook CRevertSavedLoadThinkHook = new(owner);

    public ICRevertSavedLoadThinkHook LoadThink => CRevertSavedLoadThinkHook;

    internal void UnregisterListeners()
    {
        CRevertSavedLoadThinkHook.UnregisterListeners();
    }
}