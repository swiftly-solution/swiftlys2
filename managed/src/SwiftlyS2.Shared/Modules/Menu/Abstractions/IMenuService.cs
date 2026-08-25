using SwiftlyS2.Shared.Players;

namespace SwiftlyS2.Shared.Menu;

/// <summary>
/// A plugin's entry point into the menu framework.
/// </summary>
/// <remarks>
/// Each plugin gets its own instance over shared, core-owned registries. Everything created or
/// registered through it is attributed to that plugin and released when the plugin unloads, so one
/// plugin unloading never disturbs another plugin's menus.
/// </remarks>
public interface IMenuService
{
    /// <summary>
    /// The catalogue of bindable actions.
    /// </summary>
    public IMenuActionRegistry Actions { get; }

    /// <summary>
    /// The catalogue of renderers and component specialisations.
    /// </summary>
    public IMenuRendererRegistry Renderers { get; }

    /// <summary>
    /// Starts building a menu.
    /// </summary>
    /// <param name="id">
    /// The menu id, also used as its keybind configuration scope. Namespacing it with the plugin
    /// id, such as <c>"myplugin.shop"</c>, keeps server configuration files readable.
    /// </param>
    /// <returns>A builder for the new menu.</returns>
    public IMenuBuilder CreateMenu( string id );

    /// <summary>
    /// Gets the session for whatever menu a player currently has open.
    /// </summary>
    /// <param name="player">The player to look up.</param>
    /// <returns>The session, or null when no menu is open.</returns>
    /// <remarks>
    /// A player has at most one open menu at a time, across every plugin.
    /// </remarks>
    public IMenuSession? GetSession( IPlayer player );

    /// <summary>
    /// Closes a player's open menu when it belongs to this plugin.
    /// </summary>
    /// <param name="player">The player to close for.</param>
    public void CloseFor( IPlayer player );

    /// <summary>
    /// Closes every open menu belonging to this plugin.
    /// </summary>
    public void CloseAll();
}
