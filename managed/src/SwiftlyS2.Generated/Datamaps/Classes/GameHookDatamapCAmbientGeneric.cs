using SwiftlyS2.Shared.GameHooks;

namespace SwiftlyS2.Core.GameHooks;

internal sealed class GameHookDatamapCAmbientGeneric(GameHooksService owner) : IGameHookDatamapCAmbientGeneric
{
    internal readonly CAmbientGenericRampThinkHook CAmbientGenericRampThinkHook = new(owner);

    public ICAmbientGenericRampThinkHook RampThink => CAmbientGenericRampThinkHook;

    internal void UnregisterListeners()
    {
        CAmbientGenericRampThinkHook.UnregisterListeners();
    }
}