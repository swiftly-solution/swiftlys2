namespace SwiftlyS2.Shared.GameHooks;

public interface IGameHooks
{
    /// <summary>
    /// Hooks related to controller.
    /// </summary>
    public IGameHookController Controller { get; }

    /// <summary>
    /// Hooks related to items.
    /// </summary>
    public IGameHookItem Items { get; }

    /// <summary>
    /// Hooks related to movement.
    /// </summary>
    public IGameHookMovement Movement { get; }

    /// <summary>
    /// Hooks related to player pawn.
    /// </summary>
    public IGameHookPawn Pawn { get; }

    /// <summary>
    /// Hooks related to weapons.
    /// </summary>
    public IGameHookWeapon Weapons { get; }
}
