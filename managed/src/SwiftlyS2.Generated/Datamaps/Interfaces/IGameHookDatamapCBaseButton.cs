namespace SwiftlyS2.Shared.GameHooks;

public interface IGameHookDatamapCBaseButton
{
    public ICBaseButtonActivateTouchHook ActivateTouch { get; }
    public ICBaseButtonButtonBackHomeHook ButtonBackHome { get; }
    public ICBaseButtonButtonReturnHook ButtonReturn { get; }
    public ICBaseButtonButtonSparkHook ButtonSpark { get; }
    public ICBaseButtonButtonTouchHook ButtonTouch { get; }
    public ICBaseButtonButtonUseHook ButtonUse { get; }
    public ICBaseButtonTriggerAndWaitHook TriggerAndWait { get; }
}