using SwiftlyS2.Shared.GameHooks;

namespace SwiftlyS2.Core.GameHooks;

internal sealed class GameHookDatamapCTriggerMultiple(GameHooksService owner) : IGameHookDatamapCTriggerMultiple
{
    internal readonly CTriggerMultipleMultiTouchHook CTriggerMultipleMultiTouchHook = new(owner);
    internal readonly CTriggerMultipleMultiWaitOverHook CTriggerMultipleMultiWaitOverHook = new(owner);

    public ICTriggerMultipleMultiTouchHook MultiTouch => CTriggerMultipleMultiTouchHook;
    public ICTriggerMultipleMultiWaitOverHook MultiWaitOver => CTriggerMultipleMultiWaitOverHook;

    internal void UnregisterListeners()
    {
        CTriggerMultipleMultiTouchHook.UnregisterListeners();
        CTriggerMultipleMultiWaitOverHook.UnregisterListeners();
    }
}