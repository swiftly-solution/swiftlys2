using SwiftlyS2.Shared.GameHooks;

namespace SwiftlyS2.Core.GameHooks;

internal sealed class GameHookDatamapCCSWeaponBase : IGameHookDatamapCCSWeaponBase
{
    internal readonly CCSWeaponBaseDefaultTouchHook CCSWeaponBaseDefaultTouchHook = new();
    internal readonly CCSWeaponBaseRemoveUnownedWeaponThinkHook CCSWeaponBaseRemoveUnownedWeaponThinkHook = new();

    public ICCSWeaponBaseDefaultTouchHook DefaultTouch => CCSWeaponBaseDefaultTouchHook;
    public ICCSWeaponBaseRemoveUnownedWeaponThinkHook RemoveUnownedWeaponThink => CCSWeaponBaseRemoveUnownedWeaponThinkHook;

    internal void UnregisterListeners()
    {
        CCSWeaponBaseDefaultTouchHook.UnregisterListeners();
        CCSWeaponBaseRemoveUnownedWeaponThinkHook.UnregisterListeners();
    }
}