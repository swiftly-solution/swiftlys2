namespace SwiftlyS2.Shared.GameHooks;

public interface IGameHookDatamapCTriggerMultiple
{
    public ICTriggerMultipleMultiTouchHook MultiTouch { get; }
    public ICTriggerMultipleMultiWaitOverHook MultiWaitOver { get; }
}