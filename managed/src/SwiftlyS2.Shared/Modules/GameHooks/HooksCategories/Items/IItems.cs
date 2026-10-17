namespace SwiftlyS2.Shared.GameHooks;

public interface IGameHookItem
{
    /// <summary>
    /// Hook triggered when an item can acquire logic is ran by game.
    /// </summary>
    public ICanAcquireItemHook CanAcquire { get; }

    /// <summary>
    /// Hook triggered when the game looks up the item in a player's loadout slot for a team
    /// (CCSPlayerInventory::GetItemInLoadout), e.g. to pick the skin of a weapon it gives.
    /// Requires "FollowCS2ServerGuidelines" to be disabled in core.jsonc.
    /// </summary>
    public IGetItemInLoadoutHook GetItemInLoadout { get; }
}
