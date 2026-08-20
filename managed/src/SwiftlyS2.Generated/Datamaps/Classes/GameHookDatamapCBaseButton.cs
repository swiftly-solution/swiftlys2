using SwiftlyS2.Shared.GameHooks;

namespace SwiftlyS2.Core.GameHooks;

internal sealed class GameHookDatamapCBaseButton : IGameHookDatamapCBaseButton
{
    internal readonly CBaseButtonActivateTouchHook CBaseButtonActivateTouchHook = new();
    internal readonly CBaseButtonButtonBackHomeHook CBaseButtonButtonBackHomeHook = new();
    internal readonly CBaseButtonButtonReturnHook CBaseButtonButtonReturnHook = new();
    internal readonly CBaseButtonButtonSparkHook CBaseButtonButtonSparkHook = new();
    internal readonly CBaseButtonButtonTouchHook CBaseButtonButtonTouchHook = new();
    internal readonly CBaseButtonButtonUseHook CBaseButtonButtonUseHook = new();
    internal readonly CBaseButtonTriggerAndWaitHook CBaseButtonTriggerAndWaitHook = new();

    public ICBaseButtonActivateTouchHook ActivateTouch => CBaseButtonActivateTouchHook;
    public ICBaseButtonButtonBackHomeHook ButtonBackHome => CBaseButtonButtonBackHomeHook;
    public ICBaseButtonButtonReturnHook ButtonReturn => CBaseButtonButtonReturnHook;
    public ICBaseButtonButtonSparkHook ButtonSpark => CBaseButtonButtonSparkHook;
    public ICBaseButtonButtonTouchHook ButtonTouch => CBaseButtonButtonTouchHook;
    public ICBaseButtonButtonUseHook ButtonUse => CBaseButtonButtonUseHook;
    public ICBaseButtonTriggerAndWaitHook TriggerAndWait => CBaseButtonTriggerAndWaitHook;

    internal void UnregisterListeners()
    {
        CBaseButtonActivateTouchHook.UnregisterListeners();
        CBaseButtonButtonBackHomeHook.UnregisterListeners();
        CBaseButtonButtonReturnHook.UnregisterListeners();
        CBaseButtonButtonSparkHook.UnregisterListeners();
        CBaseButtonButtonTouchHook.UnregisterListeners();
        CBaseButtonButtonUseHook.UnregisterListeners();
        CBaseButtonTriggerAndWaitHook.UnregisterListeners();
    }
}