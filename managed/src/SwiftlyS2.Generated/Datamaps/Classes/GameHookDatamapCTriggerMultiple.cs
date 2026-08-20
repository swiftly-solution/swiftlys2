using SwiftlyS2.Shared.GameHooks;

namespace SwiftlyS2.Core.GameHooks;

internal sealed class GameHookDatamapCTriggerMultiple : IGameHookDatamapCTriggerMultiple
{
    internal readonly CTriggerMultipleMultiTouchHook CTriggerMultipleMultiTouchHook = new();
    internal readonly CTriggerMultipleMultiWaitOverHook CTriggerMultipleMultiWaitOverHook = new();

    public ICTriggerMultipleMultiTouchHook MultiTouch => CTriggerMultipleMultiTouchHook;
    public ICTriggerMultipleMultiWaitOverHook MultiWaitOver => CTriggerMultipleMultiWaitOverHook;

    internal void UnregisterListeners()
    {
        CTriggerMultipleMultiTouchHook.UnregisterListeners();
        CTriggerMultipleMultiWaitOverHook.UnregisterListeners();
    }
}