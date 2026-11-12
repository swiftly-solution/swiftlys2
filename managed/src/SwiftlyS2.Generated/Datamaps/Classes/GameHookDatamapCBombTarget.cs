using SwiftlyS2.Shared.GameHooks;

namespace SwiftlyS2.Core.GameHooks;

internal sealed class GameHookDatamapCBombTarget(GameHooksService owner) : IGameHookDatamapCBombTarget
{
    internal readonly CBombTargetBombTargetTouchHook CBombTargetBombTargetTouchHook = new(owner);
    internal readonly CBombTargetBombTargetUseHook CBombTargetBombTargetUseHook = new(owner);

    public ICBombTargetBombTargetTouchHook BombTargetTouch => CBombTargetBombTargetTouchHook;
    public ICBombTargetBombTargetUseHook BombTargetUse => CBombTargetBombTargetUseHook;

    internal void UnregisterListeners()
    {
        CBombTargetBombTargetTouchHook.UnregisterListeners();
        CBombTargetBombTargetUseHook.UnregisterListeners();
    }
}