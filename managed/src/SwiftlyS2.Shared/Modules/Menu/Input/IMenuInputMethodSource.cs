namespace SwiftlyS2.Shared.Menu;

/// <summary>
/// Supplies input method overrides for menus.
/// </summary>
/// <remarks>
/// Sources are consulted in descending <see cref="Priority"/> order and the first one that answers
/// wins, so a server-wide configuration can enforce an input method over whatever a plugin requested
/// in code. Implement this to bind input method choices from any store.
/// </remarks>
public interface IMenuInputMethodSource
{
    /// <summary>
    /// The resolution priority. Higher values are consulted first.
    /// </summary>
    /// <seealso cref="MenuInputMethodPriority"/>
    public int Priority { get; }

    /// <summary>
    /// Attempts to resolve the input method to use for a menu.
    /// </summary>
    /// <param name="menuId">The menu to resolve.</param>
    /// <param name="inputMethod">The input method, when this source overrides the menu.</param>
    /// <returns><see langword="true"/> when this source overrides the menu.</returns>
    public bool TryGetInputMethod( string menuId, out MenuInputMethod inputMethod );
}

/// <summary>
/// The priorities used by the built-in input method sources.
/// </summary>
public static class MenuInputMethodPriority
{
    /// <summary>
    /// The input method requested by the menu itself, through <see cref="IMenuBuilder.WithInputMethod"/>.
    /// Lowest priority.
    /// </summary>
    public const int CodeDefault = 0;

    /// <summary>
    /// An input method coming from the owning plugin's own configuration.
    /// </summary>
    public const int Plugin = 50;

    /// <summary>
    /// An input method coming from the server-wide menu configuration file. Highest priority.
    /// </summary>
    public const int Global = 100;
}
