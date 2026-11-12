using SwiftlyS2.Shared.GameHooks;

namespace SwiftlyS2.Core.GameHooks;

internal sealed class GameHookDatamapCBaseGrenade(GameHooksService owner) : IGameHookDatamapCBaseGrenade
{
    internal readonly CBaseGrenadeBounceTouchHook CBaseGrenadeBounceTouchHook = new(owner);
    internal readonly CBaseGrenadeDangerSoundThinkHook CBaseGrenadeDangerSoundThinkHook = new(owner);
    internal readonly CBaseGrenadeDetonateHook CBaseGrenadeDetonateHook = new(owner);
    internal readonly CBaseGrenadeDetonateUseHook CBaseGrenadeDetonateUseHook = new(owner);
    internal readonly CBaseGrenadeExplodeTouchHook CBaseGrenadeExplodeTouchHook = new(owner);
    internal readonly CBaseGrenadePreDetonateHook CBaseGrenadePreDetonateHook = new(owner);
    internal readonly CBaseGrenadeSlideTouchHook CBaseGrenadeSlideTouchHook = new(owner);
    internal readonly CBaseGrenadeSmokeHook CBaseGrenadeSmokeHook = new(owner);
    internal readonly CBaseGrenadeTumbleThinkHook CBaseGrenadeTumbleThinkHook = new(owner);

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