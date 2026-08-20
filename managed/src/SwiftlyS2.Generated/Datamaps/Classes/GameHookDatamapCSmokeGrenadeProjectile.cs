using SwiftlyS2.Shared.GameHooks;

namespace SwiftlyS2.Core.GameHooks;

internal sealed class GameHookDatamapCSmokeGrenadeProjectile : IGameHookDatamapCSmokeGrenadeProjectile
{
    internal readonly CSmokeGrenadeProjectileThink_BuildingSmokeVolumeHook CSmokeGrenadeProjectileThink_BuildingSmokeVolumeHook = new();
    internal readonly CSmokeGrenadeProjectileThink_DetonateHook CSmokeGrenadeProjectileThink_DetonateHook = new();
    internal readonly CSmokeGrenadeProjectileThink_RemoveHook CSmokeGrenadeProjectileThink_RemoveHook = new();
    internal readonly CSmokeGrenadeProjectileThink_UpdateHook CSmokeGrenadeProjectileThink_UpdateHook = new();

    public ICSmokeGrenadeProjectileThink_BuildingSmokeVolumeHook Think_BuildingSmokeVolume => CSmokeGrenadeProjectileThink_BuildingSmokeVolumeHook;
    public ICSmokeGrenadeProjectileThink_DetonateHook Think_Detonate => CSmokeGrenadeProjectileThink_DetonateHook;
    public ICSmokeGrenadeProjectileThink_RemoveHook Think_Remove => CSmokeGrenadeProjectileThink_RemoveHook;
    public ICSmokeGrenadeProjectileThink_UpdateHook Think_Update => CSmokeGrenadeProjectileThink_UpdateHook;

    internal void UnregisterListeners()
    {
        CSmokeGrenadeProjectileThink_BuildingSmokeVolumeHook.UnregisterListeners();
        CSmokeGrenadeProjectileThink_DetonateHook.UnregisterListeners();
        CSmokeGrenadeProjectileThink_RemoveHook.UnregisterListeners();
        CSmokeGrenadeProjectileThink_UpdateHook.UnregisterListeners();
    }
}