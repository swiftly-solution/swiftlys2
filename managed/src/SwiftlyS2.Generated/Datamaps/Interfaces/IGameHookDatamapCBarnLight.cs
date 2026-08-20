namespace SwiftlyS2.Shared.GameHooks;

public interface IGameHookDatamapCBarnLight
{
    public ICBarnLightThink_ApplyLightStylesToTargetsHook Think_ApplyLightStylesToTargets { get; }
    public ICBarnLightThink_LightStyleEventHook Think_LightStyleEvent { get; }
    public ICBarnLightThink_SetNextQueuedLightStyleHook Think_SetNextQueuedLightStyle { get; }
}