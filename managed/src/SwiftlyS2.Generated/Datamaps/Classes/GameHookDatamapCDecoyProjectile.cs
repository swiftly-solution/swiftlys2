using SwiftlyS2.Shared.GameHooks;

namespace SwiftlyS2.Core.GameHooks;

internal sealed class GameHookDatamapCDecoyProjectile(GameHooksService owner) : IGameHookDatamapCDecoyProjectile
{
    internal readonly CDecoyProjectileGunfireThinkHook CDecoyProjectileGunfireThinkHook = new(owner);
    internal readonly CDecoyProjectileThink_DetonateHook CDecoyProjectileThink_DetonateHook = new(owner);

    public ICDecoyProjectileGunfireThinkHook GunfireThink => CDecoyProjectileGunfireThinkHook;
    public ICDecoyProjectileThink_DetonateHook Think_Detonate => CDecoyProjectileThink_DetonateHook;

    internal void UnregisterListeners()
    {
        CDecoyProjectileGunfireThinkHook.UnregisterListeners();
        CDecoyProjectileThink_DetonateHook.UnregisterListeners();
    }
}