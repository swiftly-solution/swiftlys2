using SwiftlyS2.Shared.GameHooks;

namespace SwiftlyS2.Core.GameHooks;

internal sealed class GameHookWeapon : IGameHookWeapon
{
    internal readonly WeaponDropHook DropHook = new();
    internal readonly CanUseWeaponHook CanUseHook = new();

    public IWeaponDropHook Drop => DropHook;
    public ICanUseWeaponHook CanUse => CanUseHook;
}
