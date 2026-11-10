using SwiftlyS2.Shared.GameHooks;

namespace SwiftlyS2.Core.GameHooks;

internal sealed class GameHookDatamapCDynamicLight(GameHooksService owner) : IGameHookDatamapCDynamicLight
{
    internal readonly CDynamicLightDynamicLightThinkHook CDynamicLightDynamicLightThinkHook = new(owner);

    public ICDynamicLightDynamicLightThinkHook DynamicLightThink => CDynamicLightDynamicLightThinkHook;

    internal void UnregisterListeners()
    {
        CDynamicLightDynamicLightThinkHook.UnregisterListeners();
    }
}