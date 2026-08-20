using SwiftlyS2.Shared.GameHooks;

namespace SwiftlyS2.Core.GameHooks;

internal sealed class GameHookDatamapCRevertSaved : IGameHookDatamapCRevertSaved
{
    internal readonly CRevertSavedLoadThinkHook CRevertSavedLoadThinkHook = new();

    public ICRevertSavedLoadThinkHook LoadThink => CRevertSavedLoadThinkHook;

    internal void UnregisterListeners()
    {
        CRevertSavedLoadThinkHook.UnregisterListeners();
    }
}