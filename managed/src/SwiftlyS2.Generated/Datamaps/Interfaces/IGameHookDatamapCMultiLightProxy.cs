namespace SwiftlyS2.Shared.GameHooks;

public interface IGameHookDatamapCMultiLightProxy
{
    public ICMultiLightProxyApproachBrightnessThinkHook ApproachBrightnessThink { get; }
    public ICMultiLightProxyRestoreFlashlightThinkHook RestoreFlashlightThink { get; }
}