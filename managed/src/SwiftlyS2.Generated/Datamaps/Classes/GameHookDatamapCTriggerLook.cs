using SwiftlyS2.Shared.GameHooks;

namespace SwiftlyS2.Core.GameHooks;

internal sealed class GameHookDatamapCTriggerLook : IGameHookDatamapCTriggerLook
{
    internal readonly CTriggerLookTimeoutThinkHook CTriggerLookTimeoutThinkHook = new();

    public ICTriggerLookTimeoutThinkHook TimeoutThink => CTriggerLookTimeoutThinkHook;

    internal void UnregisterListeners()
    {
        CTriggerLookTimeoutThinkHook.UnregisterListeners();
    }
}