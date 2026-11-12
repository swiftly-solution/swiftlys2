using SwiftlyS2.Shared.GameHooks;

namespace SwiftlyS2.Core.GameHooks;

internal sealed class GameHookDatamapCBaseButton(GameHooksService owner) : IGameHookDatamapCBaseButton
{
    internal readonly CBaseButtonActivateTouchHook CBaseButtonActivateTouchHook = new(owner);
    internal readonly CBaseButtonButtonBackHomeHook CBaseButtonButtonBackHomeHook = new(owner);
    internal readonly CBaseButtonButtonReturnHook CBaseButtonButtonReturnHook = new(owner);
    internal readonly CBaseButtonButtonSparkHook CBaseButtonButtonSparkHook = new(owner);
    internal readonly CBaseButtonButtonTouchHook CBaseButtonButtonTouchHook = new(owner);
    internal readonly CBaseButtonButtonUseHook CBaseButtonButtonUseHook = new(owner);
    internal readonly CBaseButtonTriggerAndWaitHook CBaseButtonTriggerAndWaitHook = new(owner);

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