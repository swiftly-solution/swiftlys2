// using SwiftlyS2.Shared.Players;

// namespace SwiftlyS2.Shared.Menus;

// /// <summary>
// /// Represents a menu option that can be displayed and interacted with by players.
// /// </summary>
// [Obsolete("IOption will be deprecared at the release of SwiftlyS2. Please use IMenuOption instead")]
// public interface IOption
// {
//     /// <summary>
//     /// Gets or sets the text content of the menu option.
//     /// </summary>
//     public string Text { get; set; }

//     /// <summary>
//     /// Gets a value indicating whether the option is visible in the menu.
//     /// </summary>
//     public bool Visible { get; }

//     /// <summary>
//     /// Gets a value indicating whether the option can be interacted with.
//     /// </summary>
//     public bool Enabled { get; }

//     /// <summary>
//     /// Gets the menu that this option belongs to, or null if not associated with a menu.
//     /// </summary>
//     public IMenu? Menu { get; set; }

//     /// <summary>
//     /// Gets the horizontal overflow style for this option's text display.
//     /// </summary>
//     public MenuHorizontalStyle? OverflowStyle { get; }

//     /// <summary>
//     /// Determines whether this option should be shown to the specified player.
//     /// </summary>
//     /// <param name="player">The player to check visibility for.</param>
//     /// <returns>True if the option should be shown to the player; otherwise, false.</returns>
//     public bool ShouldShow( IPlayer player );

//     /// <summary>
//     /// Determines whether the specified player can interact with this option.
//     /// </summary>
//     /// <param name="player">The player to check interaction capability for.</param>
//     /// <returns>True if the player can interact with the option; otherwise, false.</returns>
//     public bool CanInteract( IPlayer player );

//     /// <summary>
//     /// Gets the display text for this option as it should appear to the specified player.
//     /// </summary>
//     /// <param name="player">The player requesting the display text.</param>
//     /// <param name="updateHorizontalStyle">Indicates whether to update the horizontal style of the text.</param>
//     /// <returns>The formatted display text for the option.</returns>
//     public string GetDisplayText( IPlayer player, bool updateHorizontalStyle );

//     /// <summary>
//     /// Gets the text size configuration for this option.
//     /// </summary>
//     /// <returns>The text size setting for the option.</returns>
//     public IMenuTextSize GetTextSize();

//     /// <summary>
//     /// Determines whether this option should play a sound when selected.
//     /// </summary>
//     /// <returns>True if the option should play a sound; otherwise, false.</returns>
//     public bool HasSound();
// }

// /// <summary>
// /// Defines the available text size options for menu items.
// /// </summary>
// public enum IMenuTextSize
// {
//     /// <summary>
//     /// Extra small text size (fontSize-xs).
//     /// </summary>
//     ExtraSmall,

//     /// <summary>
//     /// Small text size (fontSize-s).
//     /// </summary>
//     Small,

//     /// <summary>
//     /// Small-medium text size (fontSize-sm).
//     /// </summary>
//     SmallMedium,

//     /// <summary>
//     /// Medium text size (fontSize-m).
//     /// </summary>
//     Medium,

//     /// <summary>
//     /// Medium-large text size (fontSize-ml).
//     /// </summary>
//     MediumLarge,

//     /// <summary>
//     /// Large text size (fontSize-l).
//     /// </summary>
//     Large,

//     /// <summary>
//     /// Extra large text size (fontSize-xl).
//     /// </summary>
//     ExtraLarge
// }