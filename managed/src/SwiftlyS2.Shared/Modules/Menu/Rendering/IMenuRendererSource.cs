namespace SwiftlyS2.Shared.Menu;

/// <summary>
/// Supplies renderer overrides for menus.
/// </summary>
/// <remarks>
/// Sources are consulted in descending <see cref="Priority"/> order and the first one that answers
/// wins, so a server-wide configuration can enforce a renderer over whatever a plugin requested in
/// code. Implement this to bind renderer choices from any store.
/// </remarks>
public interface IMenuRendererSource
{
    /// <summary>
    /// The resolution priority. Higher values are consulted first.
    /// </summary>
    /// <seealso cref="MenuRendererPriority"/>
    public int Priority { get; }

    /// <summary>
    /// Attempts to resolve the renderer to use for a menu.
    /// </summary>
    /// <param name="menuId">The menu to resolve.</param>
    /// <param name="rendererId">The renderer id, when this source overrides the menu.</param>
    /// <returns><see langword="true"/> when this source overrides the menu.</returns>
    public bool TryGetRenderer( string menuId, out string rendererId );
}

/// <summary>
/// The priorities used by the built-in renderer sources.
/// </summary>
public static class MenuRendererPriority
{
    /// <summary>
    /// The renderer requested by the menu itself, through <see cref="IMenuBuilder.WithRenderer"/>.
    /// Lowest priority.
    /// </summary>
    public const int CodeDefault = 0;

    /// <summary>
    /// A renderer coming from the owning plugin's own configuration.
    /// </summary>
    public const int Plugin = 50;

    /// <summary>
    /// A renderer coming from the server-wide menu configuration file. Highest priority.
    /// </summary>
    public const int Global = 100;
}
