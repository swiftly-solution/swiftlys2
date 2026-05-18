using SwiftlyS2.Shared.GameHooks;

namespace SwiftlyS2.Core.GameHooks;

internal sealed class GameHookWeapon : IGameHookWeapon
{
    internal readonly WeaponDropEvents DropEvents = new();
    internal readonly CanUseWeaponEvents CanUseEvents = new();

    public IWeaponDropEvents Drop => DropEvents;
    public ICanUseWeaponEvents CanUse => CanUseEvents;
}
