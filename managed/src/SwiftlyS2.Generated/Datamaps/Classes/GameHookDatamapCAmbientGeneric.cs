using SwiftlyS2.Shared.GameHooks;

namespace SwiftlyS2.Core.GameHooks;

internal sealed class GameHookDatamapCAmbientGeneric : IGameHookDatamapCAmbientGeneric
{
    internal readonly CAmbientGenericRampThinkHook CAmbientGenericRampThinkHook = new();

    public ICAmbientGenericRampThinkHook RampThink => CAmbientGenericRampThinkHook;

    internal void UnregisterListeners()
    {
        CAmbientGenericRampThinkHook.UnregisterListeners();
    }
}