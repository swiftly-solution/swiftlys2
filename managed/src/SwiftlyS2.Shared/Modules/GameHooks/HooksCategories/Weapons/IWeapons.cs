namespace SwiftlyS2.Shared.GameHooks;

public interface IGameHookWeapon
{
    /// <summary>
    /// Event triggered when a weapon is dropped.
    /// </summary>
    public IWeaponDropEvents Drop { get; }

    /// <summary>
    /// Event triggered when a weapon can use logic is ran by game.
    /// </summary>
    public ICanUseWeaponEvents CanUse { get; }
}