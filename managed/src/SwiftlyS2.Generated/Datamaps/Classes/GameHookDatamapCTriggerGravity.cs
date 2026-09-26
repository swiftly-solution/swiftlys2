using SwiftlyS2.Shared.GameHooks;

namespace SwiftlyS2.Core.GameHooks;

internal sealed class GameHookDatamapCTriggerGravity : IGameHookDatamapCTriggerGravity
{
    internal readonly CTriggerGravityGravityTouchHook CTriggerGravityGravityTouchHook = new();

    public ICTriggerGravityGravityTouchHook GravityTouch => CTriggerGravityGravityTouchHook;

    internal void UnregisterListeners()
    {
        CTriggerGravityGravityTouchHook.UnregisterListeners();
    }
}