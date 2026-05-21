namespace SwiftlyS2.Shared.GameHooks;

public interface IGameHookWeapon
{
    /// <summary>
    /// Hook triggered when a weapon is dropped.
    /// </summary>
    public IWeaponDropHook Drop { get; }

    /// <summary>
    /// Hook triggered when a weapon can use logic is ran by game.
    /// </summary>
    public ICanUseWeaponHook CanUse { get; }
}
