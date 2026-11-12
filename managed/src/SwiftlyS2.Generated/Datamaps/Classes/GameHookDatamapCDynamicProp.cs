using SwiftlyS2.Shared.GameHooks;

namespace SwiftlyS2.Core.GameHooks;

internal sealed class GameHookDatamapCDynamicProp(GameHooksService owner) : IGameHookDatamapCDynamicProp
{
    internal readonly CDynamicPropAnimThinkHook CDynamicPropAnimThinkHook = new(owner);

    public ICDynamicPropAnimThinkHook AnimThink => CDynamicPropAnimThinkHook;

    internal void UnregisterListeners()
    {
        CDynamicPropAnimThinkHook.UnregisterListeners();
    }
}