using SwiftlyS2.Shared.GameHooks;

namespace SwiftlyS2.Core.GameHooks;

internal sealed class GameHookDatamapCCSWeaponBase(GameHooksService owner) : IGameHookDatamapCCSWeaponBase
{
    internal readonly CCSWeaponBaseDefaultTouchHook CCSWeaponBaseDefaultTouchHook = new(owner);
    internal readonly CCSWeaponBaseRemoveUnownedWeaponThinkHook CCSWeaponBaseRemoveUnownedWeaponThinkHook = new(owner);

    public ICCSWeaponBaseDefaultTouchHook DefaultTouch => CCSWeaponBaseDefaultTouchHook;
    public ICCSWeaponBaseRemoveUnownedWeaponThinkHook RemoveUnownedWeaponThink => CCSWeaponBaseRemoveUnownedWeaponThinkHook;

    internal void UnregisterListeners()
    {
        CCSWeaponBaseDefaultTouchHook.UnregisterListeners();
        CCSWeaponBaseRemoveUnownedWeaponThinkHook.UnregisterListeners();
    }
}