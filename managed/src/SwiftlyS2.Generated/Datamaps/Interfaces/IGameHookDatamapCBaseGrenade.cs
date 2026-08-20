namespace SwiftlyS2.Shared.GameHooks;

public interface IGameHookDatamapCBaseGrenade
{
    public ICBaseGrenadeBounceTouchHook BounceTouch { get; }
    public ICBaseGrenadeDangerSoundThinkHook DangerSoundThink { get; }
    public ICBaseGrenadeDetonateHook Detonate { get; }
    public ICBaseGrenadeDetonateUseHook DetonateUse { get; }
    public ICBaseGrenadeExplodeTouchHook ExplodeTouch { get; }
    public ICBaseGrenadePreDetonateHook PreDetonate { get; }
    public ICBaseGrenadeSlideTouchHook SlideTouch { get; }
    public ICBaseGrenadeSmokeHook Smoke { get; }
    public ICBaseGrenadeTumbleThinkHook TumbleThink { get; }
}