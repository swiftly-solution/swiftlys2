using SwiftlyS2.Shared.GameHooks;

namespace SwiftlyS2.Core.GameHooks;

internal sealed class GameHookDatamapCDynamicProp : IGameHookDatamapCDynamicProp
{
    internal readonly CDynamicPropAnimThinkHook CDynamicPropAnimThinkHook = new();

    public ICDynamicPropAnimThinkHook AnimThink => CDynamicPropAnimThinkHook;

    internal void UnregisterListeners()
    {
        CDynamicPropAnimThinkHook.UnregisterListeners();
    }
}