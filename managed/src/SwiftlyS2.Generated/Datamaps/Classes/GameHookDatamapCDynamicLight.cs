using SwiftlyS2.Shared.GameHooks;

namespace SwiftlyS2.Core.GameHooks;

internal sealed class GameHookDatamapCDynamicLight : IGameHookDatamapCDynamicLight
{
    internal readonly CDynamicLightDynamicLightThinkHook CDynamicLightDynamicLightThinkHook = new();

    public ICDynamicLightDynamicLightThinkHook DynamicLightThink => CDynamicLightDynamicLightThinkHook;

    internal void UnregisterListeners()
    {
        CDynamicLightDynamicLightThinkHook.UnregisterListeners();
    }
}