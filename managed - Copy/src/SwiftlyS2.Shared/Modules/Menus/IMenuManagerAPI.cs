using SwiftlyS2.Shared.Players;

namespace SwiftlyS2.Shared.Menus;

/// <summary>
/// Configuration settings that control menu behavior, appearance, and player interaction.
/// </summary>
public readonly record struct MenuManagerConfiguration
{
    /// <summary>
    /// Prefix used for menu navigation commands to distinguish them from other game commands.
    /// </summary>
    public required string NavigationPrefix { get; init; }

    /// <summary>
    /// Input mode that determines how player input is captured for menu navigation.
    /// </summary>
    public required string InputMode { get; init; }

    /// <summary>
    /// Button configuration for selecting and activating menu options.
    /// </summary>
    public required string ButtonsUse { get; init; }

    /// <summary>
    /// Button configuration for scrolling down through menu options.
    /// </summary>
    public required string ButtonsScroll { get; init; }

    /// <summary>
    /// Button configuration for scrolling up through menu options.
    /// </summary>
    public required string ButtonsScrollBack { get; init; }

    /// <summary>
    /// Button configuration for closing menus.
    /// </summary>
    public required string ButtonsExit { get; init; }

    /// <summary>
    /// Sound effect name played when selecting a menu option.
    /// </summary>
    public required string SoundUseName { get; init; }

    /// <summary>
    /// Volume level for the selection sound (0.0 to 1.0).
    /// </summary>
    public required float SoundUseVolume { get; init; }

    /// <summary>
    /// Sound effect name played when scrolling through menu options.
    /// </summary>
    public required string SoundScrollName { get; init; }

    /// <summary>
    /// Volume level for the scroll sound (0.0 to 1.0).
    /// </summary>
    public required float SoundScrollVolume { get; init; }

    /// <summary>
    /// Sound effect name played when exiting menus.
    /// </summary>
    public required string SoundExitName { get; init; }

    /// <summary>
    /// Volume level for the exit sound (0.0 to 1.0).
    /// </summary>
    public required float SoundExitVolume { get; init; }

    /// <summary>
    /// Maximum items per page. Menus exceeding this limit will be paginated.
    /// </summary>
    public required int ItemsPerPage { get; init; }
}

/// <summary>
/// Provides event data for menu manager events.
/// </summary>
public sealed class MenuManagerEventArgs : EventArgs
{
    /// <summary>
    /// The player involved in this menu event.
    /// </summary>
    public IPlayer? Player { get; init; }

    /// <summary>
    /// The menu involved in this event.
    /// </summary>
    public IMenuAPI? Menu { get; init; }
}

/// <summary>
/// Central manager for creating and controlling all player menus.
/// </summary>
public interface IMenuManagerAPI
{
    /// <summary>
    /// The SwiftlyS2 core instance.
    /// </summary>
    public ISwiftlyCore Core { get; }

    /// <summary>
    /// Global configuration settings for all menus.
    /// </summary>
    public MenuManagerConfiguration Configuration { get; }

    /// <summary>
    /// Fired when a menu is closed for a player.
    /// </summary>
    public event EventHandler<MenuManagerEventArgs>? MenuClosed;

    /// <summary>
    /// Fired when a menu is opened for a player.
    /// </summary>
    public event EventHandler<MenuManagerEventArgs>? MenuOpened;

    /// <summary>
    /// Creates a new menu builder.
    /// </summary>
    /// <returns>A new menu builder instance.</returns>
    public IMenuBuilderAPI CreateBuilder();

    /// <summary>
    /// Creates a new menu with an optional title.
    /// </summary>
    /// <param name="configuration">The configuration for the menu.</param>
    /// <param name="keybindOverrides">The keybind overrides for the menu.</param>
    /// <param name="parent">The parent menu, or null for no parent.</param>
    /// <param name="optionScrollStyle">The scroll style for the menu options.</param>
    /// <param name="optionTextStyle">The text overflow style for the menu options.</param>
    /// <returns>A new menu instance ready to be configured.</returns>
    public IMenuAPI CreateMenu( MenuConfiguration configuration, MenuKeybindOverrides keybindOverrides, IMenuAPI? parent = null, MenuOptionScrollStyle optionScrollStyle = MenuOptionScrollStyle.CenterFixed, MenuOptionTextStyle optionTextStyle = MenuOptionTextStyle.TruncateEnd );

    /// <summary>
    /// Gets the menu currently open for the specified player.
    /// </summary>
    /// <param name="player">The player to check.</param>
    /// <returns>The player's active menu, or null if they have no menu open.</returns>
    public IMenuAPI? GetCurrentMenu( IPlayer player );

    /// <summary>
    /// Opens the specified menu for all players.
    /// </summary>
    /// <param name="menu">The menu to display.</param>
    public void OpenMenu( IMenuAPI menu );

    /// <summary>
    /// Opens the specified menu for all players.
    /// </summary>
    /// <param name="menu">The menu to display.</param>
    /// <param name="onClosed">Callback invoked when the menu is closed.</param>
    public void OpenMenu( IMenuAPI menu, Action<IPlayer, IMenuAPI> onClosed );

    /// <summary>
    /// Opens the specified menu for a player. Any currently open menu will be closed first.
    /// </summary>
    /// <param name="player">The player who will see the menu.</param>
    /// <param name="menu">The menu to display.</param>
    public void OpenMenuForPlayer( IPlayer player, IMenuAPI menu );

    /// <summary>
    /// Opens the specified menu for a player. Any currently open menu will be closed first.
    /// </summary>
    /// <param name="player">The player who will see the menu.</param>
    /// <param name="menu">The menu to display.</param>
    /// <param name="onClosed">Callback invoked when the menu is closed for the player.</param>
    public void OpenMenuForPlayer( IPlayer player, IMenuAPI menu, Action<IPlayer, IMenuAPI> onClosed );

    /// <summary>
    /// Closes the specified menu for all players who have it open.
    /// </summary>
    /// <param name="menu">The menu to close.</param>
    public void CloseMenu( IMenuAPI menu );

    /// <summary>
    /// Closes the specified menu for a player. If the menu is not open for that player, this has no effect.
    /// </summary>
    /// <param name="player">The player whose menu will be closed.</param>
    /// <param name="menu">The menu to close.</param>
    public void CloseMenuForPlayer( IPlayer player, IMenuAPI menu );

    /// <summary>
    /// Closes every open menu for every player.
    /// </summary>
    public void CloseAllMenus();
}