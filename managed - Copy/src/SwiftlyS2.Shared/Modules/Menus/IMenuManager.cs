// using SwiftlyS2.Shared.Players;

// namespace SwiftlyS2.Shared.Menus;

// /// <summary>
// /// Configuration settings for menu behavior, appearance, and interaction.
// /// Defines various aspects of menu functionality including navigation, input handling, audio feedback, and display options.
// /// </summary>
// [Obsolete("MenuSettings will be deprecared at the release of SwiftlyS2. Please use MenuConfig instead")]
// public struct MenuSettings
// {
//     /// <summary>
//     /// The prefix used for menu navigation commands and identifiers.
//     /// Used to distinguish menu-related commands from other game commands.
//     /// </summary>
//     public string NavigationPrefix;

//     /// <summary>
//     /// The input mode used for menu interaction.
//     /// Determines how player input is captured and processed for menu navigation.
//     /// </summary>
//     public string InputMode;

//     /// <summary>
//     /// The button configuration used for selecting or using menu options.
//     /// Defines which keys or buttons players press to activate menu items.
//     /// </summary>
//     public string ButtonsUse;

//     /// <summary>
//     /// The button configuration used for scrolling through menu options.
//     /// Defines which keys or buttons players use to navigate up and down in menus.
//     /// </summary>
//     public string ButtonsScroll;

//     /// <summary>
//     /// The button configuration used for scrolling back through menu options.
//     /// Defines which keys or buttons players use to navigate back up in menus.
//     /// </summary>
//     public string ButtonsScrollBack;

//     /// <summary>
//     /// The button configuration used for exiting or closing menus.
//     /// Defines which keys or buttons players press to close the current menu.
//     /// </summary>
//     public string ButtonsExit;

//     /// <summary>
//     /// The name of the sound effect played when a menu option is selected or used.
//     /// References a sound file or identifier in the game's audio system.
//     /// </summary>
//     public string SoundUseName;

//     /// <summary>
//     /// The volume level for the sound effect played when using menu options.
//     /// Typically a value between 0.0 (silent) and 1.0 (full volume).
//     /// </summary>
//     public float SoundUseVolume;

//     /// <summary>
//     /// The name of the sound effect played when scrolling through menu options.
//     /// References a sound file or identifier in the game's audio system.
//     /// </summary>
//     public string SoundScrollName;

//     /// <summary>
//     /// The volume level for the sound effect played when scrolling through menus.
//     /// Typically a value between 0.0 (silent) and 1.0 (full volume).
//     /// </summary>
//     public float SoundScrollVolume;

//     /// <summary>
//     /// The name of the sound effect played when exiting or closing menus.
//     /// References a sound file or identifier in the game's audio system.
//     /// </summary>
//     public string SoundExitName;

//     /// <summary>
//     /// The volume level for the sound effect played when exiting menus.
//     /// Typically a value between 0.0 (silent) and 1.0 (full volume).
//     /// </summary>
//     public float SoundExitVolume;

//     /// <summary>
//     /// The maximum number of menu items displayed on a single page.
//     /// When a menu has more items than this value, pagination is used to split the menu across multiple pages.
//     /// </summary>
//     public int ItemsPerPage;
// }


// /// <summary>
// /// Manages menu instances and provides functionality for creating, opening, closing, and tracking menus for players.
// /// Serves as the central hub for menu operations and maintains menu state across the application.
// /// </summary>
// [Obsolete("IMenuManager will be deprecared at the release of SwiftlyS2. Please use IMenuManagerAPI instead")]
// public interface IMenuManager
// {
//     /// <summary>
//     /// Creates a new menu instance with the specified title.
//     /// The created menu can be customized using its builder and then opened for players.
//     /// </summary>
//     /// <param name="title">The title to display at the top of the menu.</param>
//     /// <returns>A new menu instance ready for configuration and use.</returns>
//     public IMenu CreateMenu( string title );

//     /// <summary>
//     /// Retrieves the currently active menu for the specified player.
//     /// Returns null if the player does not have any menu currently open.
//     /// </summary>
//     /// <param name="player">The player whose current menu to retrieve.</param>
//     /// <returns>The player's current menu, or null if no menu is open.</returns>
//     public IMenu? GetMenu( IPlayer player );

//     /// <summary>
//     /// Closes all open menus for all players.
//     /// This includes all menus in the parent chain.
//     /// </summary>
//     public void CloseAllMenus();

//     /// <summary>
//     /// Closes the specified menu for all players who currently have it open.
//     /// This will trigger the OnClose event for each affected player.
//     /// </summary>
//     /// <param name="menu">The menu instance to close.</param>
//     public void CloseMenu( IMenu menu );

//     /// <summary>
//     /// Closes any currently open menu for the specified player.
//     /// If the player has no menu open, this method has no effect.
//     /// </summary>
//     /// <param name="player">The player whose menu should be closed.</param>
//     public void CloseMenuForPlayer( IPlayer player );

//     /// <summary>
//     /// Closes all menus with the specified title.
//     /// Can perform either exact title matching or partial matching based on the exact parameter.
//     /// </summary>
//     /// <param name="title">The title of the menu(s) to close.</param>
//     /// <param name="exact">If true, only menus with exactly matching titles are closed. If false, menus containing the title are closed. Defaults to false.</param>
//     public void CloseMenuByTitle( string title, bool exact = false );

//     /// <summary>
//     /// Opens the specified menu for the given player.
//     /// If the player already has a menu open, it will be closed first before opening the new menu.
//     /// </summary>
//     /// <param name="player">The player to open the menu for.</param>
//     /// <param name="menu">The menu instance to open.</param>
//     public void OpenMenu( IPlayer player, IMenu menu );

//     /// <summary>
//     /// Checks if the specified player currently has any menu open.
//     /// </summary>
//     /// <param name="player">The player to check if the menu is open for.</param>
//     public bool HasMenuOpen( IPlayer player );

//     /// <summary>
//     /// Event triggered when a menu is closed for a player.
//     /// Provides both the player and the menu that was closed as event arguments.
//     /// </summary>
//     event Action<IPlayer, IMenu>? OnMenuClosed;

//     /// <summary>
//     /// Event triggered when a menu is opened for a player.
//     /// Provides both the player and the menu that was opened as event arguments.
//     /// </summary>
//     event Action<IPlayer, IMenu>? OnMenuOpened;

//     /// <summary>
//     /// Event triggered when a menu is rendered for a player.
//     /// Provides both the player and the menu that was rendered as event arguments.
//     /// Useful for tracking menu display events or implementing custom rendering logic.
//     /// </summary>
//     event Action<IPlayer, IMenu>? OnMenuRendered;

//     /// <summary>
//     /// Gets the current menu settings configuration.
//     /// Contains various settings that control menu behavior, appearance, audio feedback, and interaction parameters.
//     /// </summary>
//     public MenuSettings Settings { get; }
// }