using SwiftlyS2.Shared.Players;

namespace SwiftlyS2.Shared.Menu;

/// <summary>
/// Describes an action being dispatched to a menu and its components.
/// </summary>
public readonly record struct MenuActionContext
{
    /// <summary>
    /// The action that was triggered.
    /// </summary>
    public required MenuActionId Action { get; init; }

    /// <summary>
    /// The key that triggered the action.
    /// </summary>
    public required MenuKey Key { get; init; }

    /// <summary>
    /// The session the action was dispatched to.
    /// </summary>
    public required IMenuSession Session { get; init; }

    /// <summary>
    /// The player who triggered the action.
    /// </summary>
    public IPlayer Player => Session.Player;

    /// <summary>
    /// The menu the action was dispatched to.
    /// </summary>
    public IMenu Menu => Session.Menu;
}
