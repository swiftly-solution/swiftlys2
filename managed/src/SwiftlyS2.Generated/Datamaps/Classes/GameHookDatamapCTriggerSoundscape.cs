using SwiftlyS2.Shared.GameHooks;

namespace SwiftlyS2.Core.GameHooks;

internal sealed class GameHookDatamapCTriggerSoundscape(GameHooksService owner) : IGameHookDatamapCTriggerSoundscape
{
    internal readonly CTriggerSoundscapePlayerUpdateThinkHook CTriggerSoundscapePlayerUpdateThinkHook = new(owner);

    public ICTriggerSoundscapePlayerUpdateThinkHook PlayerUpdateThink => CTriggerSoundscapePlayerUpdateThinkHook;

    internal void UnregisterListeners()
    {
        CTriggerSoundscapePlayerUpdateThinkHook.UnregisterListeners();
    }
}