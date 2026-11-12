using SwiftlyS2.Shared.GameHooks;

namespace SwiftlyS2.Core.GameHooks;

internal sealed class GameHookDatamapCTriggerLook(GameHooksService owner) : IGameHookDatamapCTriggerLook
{
    internal readonly CTriggerLookTimeoutThinkHook CTriggerLookTimeoutThinkHook = new(owner);

    public ICTriggerLookTimeoutThinkHook TimeoutThink => CTriggerLookTimeoutThinkHook;

    internal void UnregisterListeners()
    {
        CTriggerLookTimeoutThinkHook.UnregisterListeners();
    }
}