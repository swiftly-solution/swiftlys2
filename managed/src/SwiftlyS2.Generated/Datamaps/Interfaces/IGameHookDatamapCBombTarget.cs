namespace SwiftlyS2.Shared.GameHooks;

public interface IGameHookDatamapCBombTarget
{
    public ICBombTargetBombTargetTouchHook BombTargetTouch { get; }
    public ICBombTargetBombTargetUseHook BombTargetUse { get; }
}