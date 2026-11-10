using SwiftlyS2.Shared.GameHooks;

namespace SwiftlyS2.Core.GameHooks;

internal sealed class GameHookWeapon( GameHooksService owner ) : IGameHookWeapon
{
    internal readonly WeaponDropHook DropHook = new(owner);
    internal readonly CanUseWeaponHook CanUseHook = new(owner);

    public IWeaponDropHook Drop => DropHook;
    public ICanUseWeaponHook CanUse => CanUseHook;
}
