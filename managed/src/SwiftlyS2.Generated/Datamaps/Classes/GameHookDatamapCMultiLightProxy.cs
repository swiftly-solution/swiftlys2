using SwiftlyS2.Shared.GameHooks;

namespace SwiftlyS2.Core.GameHooks;

internal sealed class GameHookDatamapCMultiLightProxy : IGameHookDatamapCMultiLightProxy
{
    internal readonly CMultiLightProxyApproachBrightnessThinkHook CMultiLightProxyApproachBrightnessThinkHook = new();
    internal readonly CMultiLightProxyRestoreFlashlightThinkHook CMultiLightProxyRestoreFlashlightThinkHook = new();

    public ICMultiLightProxyApproachBrightnessThinkHook ApproachBrightnessThink => CMultiLightProxyApproachBrightnessThinkHook;
    public ICMultiLightProxyRestoreFlashlightThinkHook RestoreFlashlightThink => CMultiLightProxyRestoreFlashlightThinkHook;

    internal void UnregisterListeners()
    {
        CMultiLightProxyApproachBrightnessThinkHook.UnregisterListeners();
        CMultiLightProxyRestoreFlashlightThinkHook.UnregisterListeners();
    }
}