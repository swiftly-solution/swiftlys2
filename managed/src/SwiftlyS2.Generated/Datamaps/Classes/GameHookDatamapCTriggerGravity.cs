using SwiftlyS2.Shared.GameHooks;

namespace SwiftlyS2.Core.GameHooks;

internal sealed class GameHookDatamapCTriggerGravity(GameHooksService owner) : IGameHookDatamapCTriggerGravity
{
    internal readonly CTriggerGravityGravityTouchHook CTriggerGravityGravityTouchHook = new(owner);

    public ICTriggerGravityGravityTouchHook GravityTouch => CTriggerGravityGravityTouchHook;

    internal void UnregisterListeners()
    {
        CTriggerGravityGravityTouchHook.UnregisterListeners();
    }
}