using SwiftlyS2.Shared.GameHooks;

namespace SwiftlyS2.Core.GameHooks;

internal sealed class GameHookDatamapCBarnLight(GameHooksService owner) : IGameHookDatamapCBarnLight
{
    internal readonly CBarnLightThink_ApplyLightStylesToTargetsHook CBarnLightThink_ApplyLightStylesToTargetsHook = new(owner);
    internal readonly CBarnLightThink_LightStyleEventHook CBarnLightThink_LightStyleEventHook = new(owner);
    internal readonly CBarnLightThink_SetNextQueuedLightStyleHook CBarnLightThink_SetNextQueuedLightStyleHook = new(owner);

    public ICBarnLightThink_ApplyLightStylesToTargetsHook Think_ApplyLightStylesToTargets => CBarnLightThink_ApplyLightStylesToTargetsHook;
    public ICBarnLightThink_LightStyleEventHook Think_LightStyleEvent => CBarnLightThink_LightStyleEventHook;
    public ICBarnLightThink_SetNextQueuedLightStyleHook Think_SetNextQueuedLightStyle => CBarnLightThink_SetNextQueuedLightStyleHook;

    internal void UnregisterListeners()
    {
        CBarnLightThink_ApplyLightStylesToTargetsHook.UnregisterListeners();
        CBarnLightThink_LightStyleEventHook.UnregisterListeners();
        CBarnLightThink_SetNextQueuedLightStyleHook.UnregisterListeners();
    }
}