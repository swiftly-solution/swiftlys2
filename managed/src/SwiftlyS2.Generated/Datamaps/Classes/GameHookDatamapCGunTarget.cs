using SwiftlyS2.Shared.GameHooks;

namespace SwiftlyS2.Core.GameHooks;

internal sealed class GameHookDatamapCGunTarget : IGameHookDatamapCGunTarget
{
    internal readonly CGunTargetNextHook CGunTargetNextHook = new();
    internal readonly CGunTargetStartHook CGunTargetStartHook = new();
    internal readonly CGunTargetWaitHook CGunTargetWaitHook = new();

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