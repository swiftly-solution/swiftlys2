using SwiftlyS2.Shared.Players;

namespace SwiftlyS2.Shared.Menu;

/// <summary>
/// An arrangement of components, a renderer and a keymap.
/// </summary>
/// <remarks>
/// A menu arranges and manages components. It implements no interaction behaviour of its own
/// beyond traversal, and no drawing behaviour at all.
/// </remarks>
public interface IMenu : IDisposable
{
    /// <summary>
    /// The menu id. Doubles as its configuration scope for keybinds.
    /// </summary>
    public string Id { get; }

    /// <summary>
    /// The renderer this menu draws through.
    /// </summary>
    public IMenuRenderer Renderer { get; }

    /// <summary>
    /// The resolved actions and keys for this menu.
    /// </summary>
    public IMenuKeymap Keymap { get; }

    /// <summary>
    /// The menu to return to when this one closes, if any.
    /// </summary>
    /// <remarks>
    /// Normally set once through <see cref="IMenuBuilder.WithParent"/>. It stays writable so a menu
    /// built lazily by a <c>SubmenuComponent</c> can be told where it was opened from.
    /// </remarks>
    public IMenu? Parent { get; set; }

    /// <summary>
    /// How many body entries are visible at once.
    /// </summary>
    public int ItemsPerPage { get; }

    /// <summary>
    /// An arbitrary value carried alongside this menu.
    /// </summary>
    public object? Tag { get; set; }

    /// <summary>
    /// The sessions currently viewing this menu.
    /// </summary>
    public IReadOnlyList<IMenuSession> Sessions { get; }

    /// <summary>
    /// Lists the components in a region, in layout order.
    /// </summary>
    /// <param name="region">The region to read.</param>
    /// <returns>The components in that region.</returns>
    public IReadOnlyList<IMenuComponent> GetComponents( MenuRegion region );

    /// <summary>
    /// Appends a component to a region.
    /// </summary>
    /// <param name="region">The region to add to.</param>
    /// <param name="component">The component to add.</param>
    public void Add( MenuRegion region, IMenuComponent component );

    /// <summary>
    /// Inserts a component into a region at a position.
    /// </summary>
    /// <param name="region">The region to add to.</param>
    /// <param name="index">The position to insert at.</param>
    /// <param name="component">The component to add.</param>
    public void Insert( MenuRegion region, int index, IMenuComponent component );

    /// <summary>
    /// Removes a component from wherever it sits.
    /// </summary>
    /// <param name="component">The component to remove.</param>
    /// <returns><see langword="true"/> when the component was present.</returns>
    public bool Remove( IMenuComponent component );

    /// <summary>
    /// Opens this menu for a player, replacing whatever menu they had open.
    /// </summary>
    /// <param name="player">The player to show the menu to.</param>
    /// <returns>The new session.</returns>
    public IMenuSession Open( IPlayer player );

    /// <summary>
    /// Closes this menu for a player.
    /// </summary>
    /// <param name="player">The player to close for.</param>
    public void Close( IPlayer player );

    /// <summary>
    /// Closes this menu for everyone viewing it.
    /// </summary>
    public void CloseAll();

    /// <summary>
    /// Gets a player's session for this menu.
    /// </summary>
    /// <param name="player">The player to look up.</param>
    /// <returns>The session, or null when this menu is not open for them.</returns>
    public IMenuSession? GetSession( IPlayer player );

    /// <summary>
    /// Raised after a session is opened.
    /// </summary>
    public event Action<IMenuSession>? Opened;

    /// <summary>
    /// Raised after a session is closed.
    /// </summary>
    public event Action<IMenuSession>? Closed;

    /// <summary>
    /// Raised after the selection moves within a session.
    /// </summary>
    public event Action<IMenuSession>? FocusChanged;
}
