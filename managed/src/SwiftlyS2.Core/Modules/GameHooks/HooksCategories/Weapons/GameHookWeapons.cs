using SwiftlyS2.Shared.GameHooks;

namespace SwiftlyS2.Core.GameHooks;

internal sealed class GameHookWeapons : IGameHookWeapons
{
    internal readonly OnWeaponDropEvents OnDropEvents = new();
    internal readonly CanUseWeaponEvents CanUseEvents = new();

    public IOnWeaponDropEvents OnDrop => OnDropEvents;
    public ICanUseWeaponEvents CanUse => CanUseEvents;
}
