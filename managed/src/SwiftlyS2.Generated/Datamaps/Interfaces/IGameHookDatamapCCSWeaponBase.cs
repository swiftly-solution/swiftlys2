namespace SwiftlyS2.Shared.GameHooks;

public interface IGameHookDatamapCCSWeaponBase
{
    public ICCSWeaponBaseDefaultTouchHook DefaultTouch { get; }
    public ICCSWeaponBaseRemoveUnownedWeaponThinkHook RemoveUnownedWeaponThink { get; }
}