using SwiftlyS2.Shared.GameHooks;

namespace SwiftlyS2.Core.GameHooks;

internal sealed class GameHookDatamapCGunTarget(GameHooksService owner) : IGameHookDatamapCGunTarget
{
    internal readonly CGunTargetNextHook CGunTargetNextHook = new(owner);
    internal readonly CGunTargetStartHook CGunTargetStartHook = new(owner);
    internal readonly CGunTargetWaitHook CGunTargetWaitHook = new(owner);

    public ICGunTargetNextHook Next => CGunTargetNextHook;
    public ICGunTargetStartHook Start => CGunTargetStartHook;
    public ICGunTargetWaitHook Wait => CGunTargetWaitHook;

    internal void UnregisterListeners()
    {
        CGunTargetNextHook.UnregisterListeners();
        CGunTargetStartHook.UnregisterListeners();
        CGunTargetWaitHook.UnregisterListeners();
    }
}