// using SwiftlyS2.Shared.Events;

// namespace SwiftlyS2.Shared.Menus;

// /// <summary>
// /// Defines custom button overrides for menu navigation and interaction.
// /// Allows customization of the default key bindings used for menu operations.
// /// </summary>
// [Obsolete("IMenuButtonOverrides will be deprecared at the release of SwiftlyS2. Please use MenuKeybindOverrides instead")]
// public interface IMenuButtonOverrides
// {
//     /// <summary>
//     /// Gets or sets the key binding used for selecting menu options.
//     /// When set, overrides the default key used to activate or select the currently highlighted menu item.
//     /// Set to null to use the default selection key binding.
//     /// </summary>
//     public KeyKind? Select { get; set; }

//     /// <summary>
//     /// Gets or sets the key binding used for moving through menu options.
//     /// When set, overrides the default keys used to navigate up and down through menu items.
//     /// Set to null to use the default movement key bindings.
//     /// </summary>
//     public KeyKind? Move { get; set; }

//     /// <summary>
//     /// Gets or sets the key binding used for moving back through menu options.
//     /// When set, overrides the default key used to navigate back up in menus.
//     /// Set to null to use the default back key binding.
//     /// </summary>
//     public KeyKind? MoveBack { get; set; }

//     /// <summary>
//     /// Gets or sets the key binding used for exiting or closing the menu.
//     /// When set, overrides the default key used to close the menu and return to the game.
//     /// Set to null to use the default exit key binding.
//     /// </summary>
//     public KeyKind? Exit { get; set; }
// }