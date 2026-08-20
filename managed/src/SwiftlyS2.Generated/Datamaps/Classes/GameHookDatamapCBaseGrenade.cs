using SwiftlyS2.Shared.GameHooks;

namespace SwiftlyS2.Core.GameHooks;

internal sealed class GameHookDatamapCBaseGrenade : IGameHookDatamapCBaseGrenade
{
    internal readonly CBaseGrenadeBounceTouchHook CBaseGrenadeBounceTouchHook = new();
    internal readonly CBaseGrenadeDangerSoundThinkHook CBaseGrenadeDangerSoundThinkHook = new();
    internal readonly CBaseGrenadeDetonateHook CBaseGrenadeDetonateHook = new();
    internal readonly CBaseGrenadeDetonateUseHook CBaseGrenadeDetonateUseHook = new();
    internal readonly CBaseGrenadeExplodeTouchHook CBaseGrenadeExplodeTouchHook = new();
    internal readonly CBaseGrenadePreDetonateHook CBaseGrenadePreDetonateHook = new();
    internal readonly CBaseGrenadeSlideTouchHook CBaseGrenadeSlideTouchHook = new();
    internal readonly CBaseGrenadeSmokeHook CBaseGrenadeSmokeHook = new();
    internal readonly CBaseGrenadeTumbleThinkHook CBaseGrenadeTumbleThinkHook = new();

    public ICBaseGrenadeBounceTouchHook BounceTouch => CBaseGrenadeBounceTouchHook;
    public ICBaseGrenadeDangerSoundThinkHook DangerSoundThink => CBaseGrenadeDangerSoundThinkHook;
    public ICBaseGrenadeDetonateHook Detonate => CBaseGrenadeDetonateHook;
    public ICBaseGrenadeDetonateUseHook DetonateUse => CBaseGrenadeDetonateUseHook;
    public ICBaseGrenadeExplodeTouchHook ExplodeTouch => CBaseGrenadeExplodeTouchHook;
    public ICBaseGrenadePreDetonateHook PreDetonate => CBaseGrenadePreDetonateHook;
    public ICBaseGrenadeSlideTouchHook SlideTouch => CBaseGrenadeSlideTouchHook;
    public ICBaseGrenadeSmokeHook Smoke => CBaseGrenadeSmokeHook;
    public ICBaseGrenadeTumbleThinkHook TumbleThink => CBaseGrenadeTumbleThinkHook;

    internal void UnregisterListeners()
    {
        CBaseGrenadeBounceTouchHook.UnregisterListeners();
        CBaseGrenadeDangerSoundThinkHook.UnregisterListeners();
        CBaseGrenadeDetonateHook.UnregisterListeners();
        CBaseGrenadeDetonateUseHook.UnregisterListeners();
        CBaseGrenadeExplodeTouchHook.UnregisterListeners();
        CBaseGrenadePreDetonateHook.UnregisterListeners();
        CBaseGrenadeSlideTouchHook.UnregisterListeners();
        CBaseGrenadeSmokeHook.UnregisterListeners();
        CBaseGrenadeTumbleThinkHook.UnregisterListeners();
    }
}