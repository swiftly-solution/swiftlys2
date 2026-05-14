namespace SwiftlyS2.Shared.GameHooks;

public interface IGameHookWeapons
{
    /// <summary>
    /// Event triggered when a weapon is dropped.
    /// </summary>
    public IOnWeaponDropEvents OnDrop { get; }

    /// <summary>
    /// Event triggered when a weapon can use logic is ran by game.
    /// </summary>
    public ICanUseWeaponEvents CanUse { get; }
}