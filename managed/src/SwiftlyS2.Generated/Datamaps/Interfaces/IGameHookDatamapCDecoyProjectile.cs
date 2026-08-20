namespace SwiftlyS2.Shared.GameHooks;

public interface IGameHookDatamapCDecoyProjectile
{
    public ICDecoyProjectileGunfireThinkHook GunfireThink { get; }
    public ICDecoyProjectileThink_DetonateHook Think_Detonate { get; }
}