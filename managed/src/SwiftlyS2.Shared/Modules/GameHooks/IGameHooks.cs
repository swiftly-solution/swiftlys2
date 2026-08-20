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

    /// <summary>
    /// Hooks related to entities.
    /// </summary>
    public IGameHookEntities Entities { get; }

    /// <summary>
    /// Datamap (think/touch/use) function hooks, grouped by owning schema class.
    /// </summary>
    public IGameHookDatamaps Datamaps { get; }
}
