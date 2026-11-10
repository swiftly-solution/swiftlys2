using SwiftlyS2.Shared.GameHooks;

namespace SwiftlyS2.Core.GameHooks;

internal sealed class GameHookDatamapCMultiLightProxy(GameHooksService owner) : IGameHookDatamapCMultiLightProxy
{
    internal readonly CMultiLightProxyApproachBrightnessThinkHook CMultiLightProxyApproachBrightnessThinkHook = new(owner);
    internal readonly CMultiLightProxyRestoreFlashlightThinkHook CMultiLightProxyRestoreFlashlightThinkHook = new(owner);

    public ICMultiLightProxyApproachBrightnessThinkHook ApproachBrightnessThink => CMultiLightProxyApproachBrightnessThinkHook;
    public ICMultiLightProxyRestoreFlashlightThinkHook RestoreFlashlightThink => CMultiLightProxyRestoreFlashlightThinkHook;

    internal void UnregisterListeners()
    {
        CMultiLightProxyApproachBrightnessThinkHook.UnregisterListeners();
        CMultiLightProxyRestoreFlashlightThinkHook.UnregisterListeners();
    }
}