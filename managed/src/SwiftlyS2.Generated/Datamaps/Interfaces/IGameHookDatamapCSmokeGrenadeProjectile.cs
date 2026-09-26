namespace SwiftlyS2.Shared.GameHooks;

public interface IGameHookDatamapCSmokeGrenadeProjectile
{
    public ICSmokeGrenadeProjectileThink_BuildingSmokeVolumeHook Think_BuildingSmokeVolume { get; }
    public ICSmokeGrenadeProjectileThink_DetonateHook Think_Detonate { get; }
    public ICSmokeGrenadeProjectileThink_RemoveHook Think_Remove { get; }
    public ICSmokeGrenadeProjectileThink_UpdateHook Think_Update { get; }
}