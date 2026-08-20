using SwiftlyS2.Shared.GameHooks;

namespace SwiftlyS2.Core.GameHooks;

internal sealed class GameHookDatamapCTriggerSoundscape : IGameHookDatamapCTriggerSoundscape
{
    internal readonly CTriggerSoundscapePlayerUpdateThinkHook CTriggerSoundscapePlayerUpdateThinkHook = new();

    public ICTriggerSoundscapePlayerUpdateThinkHook PlayerUpdateThink => CTriggerSoundscapePlayerUpdateThinkHook;

    internal void UnregisterListeners()
    {
        CTriggerSoundscapePlayerUpdateThinkHook.UnregisterListeners();
    }
}