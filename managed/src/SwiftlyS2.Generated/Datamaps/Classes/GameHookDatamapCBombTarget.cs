using SwiftlyS2.Shared.GameHooks;

namespace SwiftlyS2.Core.GameHooks;

internal sealed class GameHookDatamapCBombTarget : IGameHookDatamapCBombTarget
{
    internal readonly CBombTargetBombTargetTouchHook CBombTargetBombTargetTouchHook = new();
    internal readonly CBombTargetBombTargetUseHook CBombTargetBombTargetUseHook = new();

    public ICBombTargetBombTargetTouchHook BombTargetTouch => CBombTargetBombTargetTouchHook;
    public ICBombTargetBombTargetUseHook BombTargetUse => CBombTargetBombTargetUseHook;

    internal void UnregisterListeners()
    {
        CBombTargetBombTargetTouchHook.UnregisterListeners();
        CBombTargetBombTargetUseHook.UnregisterListeners();
    }
}