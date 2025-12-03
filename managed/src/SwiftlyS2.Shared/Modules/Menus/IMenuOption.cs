using SwiftlyS2.Shared.Players;

namespace SwiftlyS2.Shared.Menus;

/// <summary>
/// Represents an asynchronous event handler that returns a ValueTask.
/// </summary>
/// <typeparam name="TEventArgs">The type of event arguments.</typeparam>
/// <param name="sender">The source of the event.</param>
/// <param name="args">Event data.</param>
/// <returns>A task that represents the asynchronous operation.</returns>
public delegate ValueTask AsyncEventHandler<TEventArgs>( object? sender, TEventArgs args ) where TEventArgs : EventArgs;

/// <summary>
/// Provides event data for menu option events.
/// </summary>
/// <remarks>
/// The Player property will be null for this event since it's a global property change.
/// </remarks>
public sealed class MenuOptionEventArgs : EventArgs
{
    /// <summary>
    /// The player who triggered this menu event.
    /// </summary>
    public IPlayer? Player { get; init; }

    /// <summary>
    /// The menu option involved in this event, or null for lifecycle events like opening or closing the menu.
    /// </summary>
    public IMenuOption? Option { get; init; }
}

/// <summary>
/// Provides event data for menu option HTML formatting events.
/// </summary>
public sealed class MenuOptionFormattingEventArgs : EventArgs
{
    /// <summary>
    /// The player for whom the option is being formatted.
    /// </summary>
    public required IPlayer Player { get; init; }

    /// <summary>
    /// The menu option being formatted.
    /// </summary>
    public required IMenuOption Option { get; init; }

    /// <summary>
    /// Gets or sets custom text to use instead of the default text during HTML assembly.
    /// </summary>
    public string? CustomText { get; set; }
}

/// <summary>
/// Provides event data for menu option validation events.
/// </summary>
public sealed class MenuOptionValidatingEventArgs : EventArgs
{
    /// <summary>
    /// The player attempting to interact with the option.
    /// </summary>
    public required IPlayer Player { get; init; }

    /// <summary>
    /// The menu option being validated.
    /// </summary>
    public required IMenuOption Option { get; init; }

    /// <summary>
    /// Gets or sets a value indicating whether the interaction should be canceled.
    /// </summary>
    public bool Cancel { get; set; }

    /// <summary>
    /// Gets or sets the reason why the interaction was canceled.
    /// </summary>
    public string? CancelReason { get; set; }
}

/// <summary>
/// Provides event data for menu option click events.
/// </summary>
/// <remarks>
/// NOTE: When handling click events, the sender parameter must be passed as IMenuOption.
/// </remarks>
public sealed class MenuOptionClickEventArgs : EventArgs
{
    /// <summary>
    /// The player who clicked the option.
    /// </summary>
    public required IPlayer Player { get; init; }

    // /// <summary>
    // /// The menu option that was clicked.
    // /// </summary>
    // public required IMenuOption Option { get; init; }

    /// <summary>
    /// Gets a value indicating whether the menu should be closed after handling the click.
    /// </summary>
    public bool CloseMenu { get; internal set; }
}

/// <summary>
/// Event arguments for when a menu option's value changes.
/// </summary>
/// <typeparam name="T">The type of the value.</typeparam>
public sealed class MenuOptionValueChangedEventArgs<T> : EventArgs
{
    /// <summary>
    /// Gets the player who triggered the value change.
    /// </summary>
    public required IPlayer Player { get; init; }

    /// <summary>
    /// Gets the menu option whose value changed.
    /// </summary>
    public required IMenuOption Option { get; init; }

    /// <summary>
    /// Gets the previous value before the change.
    /// </summary>
    public required T OldValue { get; init; }

    /// <summary>
    /// Gets the new value after the change.
    /// </summary>
    public required T NewValue { get; init; }
}

/// <summary>
/// Represents a menu option that can be displayed and interacted with by players.
/// </summary>
public interface IMenuOption : IDisposable
{
    /// <summary>
    /// Gets the menu that this option belongs to.
    /// </summary>
    /// <remarks>
    /// This property will be null until the option is added to a menu via <see cref="IMenuAPI.AddOption"/>.
    /// When implementing custom menu options, avoid accessing this property in the constructor as it will not be set yet.
    /// </remarks>
    public IMenuAPI? Menu { get; }

    /// <summary>
    /// Gets the number of lines this option requests to occupy in the menu.
    /// </summary>
    public int LineCount { get; }

    /// <summary>
    /// Gets or sets the text content displayed for this menu option.
    /// </summary>
    /// <remarks>
    /// This is a global property. Changing it will affect what all players see.
    /// </remarks>
    public string Text { get; set; }

    /// <summary>
    /// Gets or sets the comment content displayed for this menu option.
    /// </summary>
    /// <remarks>
    /// This is a global property. Changing it will affect what all players see.
    /// </remarks>
    public string Comment { get; set; }

    /// <summary>
    /// The maximum display width for menu option text in relative units.
    /// </summary>
    public float MaxWidth { get; set; }

    /// <summary>
    /// Gets or sets a value indicating whether this option is visible in the menu.
    /// </summary>
    /// <remarks>
    /// This is a global property. Changing it will affect what all players see.
    /// </remarks>
    public bool Visible { get; set; }

    /// <summary>
    /// Gets or sets a value indicating whether this option can be interacted with.
    /// </summary>
    /// <remarks>
    /// This is a global property. Changing it will affect what all players see.
    /// </remarks>
    public bool Enabled { get; set; }

    /// <summary>
    /// Gets a value indicating whether the menu should be closed after handling the click.
    /// </summary>
    public bool CloseAfterClick { get; }

    /// <summary>
    /// Gets or sets an object that contains data about this option.
    /// </summary>
    public object? Tag { get; set; }

    /// <summary>
    /// Gets or sets the text size for this option.
    /// </summary>
    public MenuOptionTextSize TextSize { get; set; }

    /// <summary>
    /// Gets or sets the text overflow style for this option.
    /// </summary>
    public MenuOptionTextStyle TextStyle { get; set; }

    /// <summary>
    /// Gets or sets a value indicating whether a sound should play when this option is selected.
    /// </summary>
    public bool PlaySound { get; set; }

    /// <summary>
    /// Occurs when the visibility of the option changes.
    /// </summary>
    public event EventHandler<MenuOptionEventArgs>? VisibilityChanged;

    /// <summary>
    /// Occurs when the enabled state of the option changes.
    /// </summary>
    public event EventHandler<MenuOptionEventArgs>? EnabledChanged;

    /// <summary>
    /// Occurs when the text of the option changes.
    /// </summary>
    public event EventHandler<MenuOptionEventArgs>? TextChanged;

    /// <summary>
    /// Occurs before a <see cref="Click"/> event is processed, allowing validation and cancellation.
    /// </summary>
    public event EventHandler<MenuOptionValidatingEventArgs>? Validating;

    /// <summary>
    /// Occurs when the option is clicked by a player.
    /// </summary>
    public event AsyncEventHandler<MenuOptionClickEventArgs>? Click;

    // /// <summary>
    // /// Occurs when a player's cursor enters this option.
    // /// </summary>
    // public event EventHandler<MenuOptionEventArgs>? Hover;

    /// <summary>
    /// Occurs before HTML markup is assembled, allowing customization of the text content.
    /// </summary>
    public event EventHandler<MenuOptionFormattingEventArgs>? BeforeFormat;

    /// <summary>
    /// Occurs after HTML markup is assembled, allowing customization of the final HTML output.
    /// </summary>
    public event EventHandler<MenuOptionFormattingEventArgs>? AfterFormat;

    /// <summary>
    /// Determines whether the click task for the specified player is completed.
    /// </summary>
    /// <param name="player">The player to check.</param>
    /// <returns>True if the click task is completed; otherwise, false.</returns>
    public bool IsClickTaskCompleted( IPlayer player );

    /// <summary>
    /// Determines whether this option is visible to the specified player.
    /// </summary>
    /// <param name="player">The player to check visibility for.</param>
    /// <returns>True if the option is visible to the player, otherwise, false.</returns>
    public bool GetVisible( IPlayer player );

    /// <summary>
    /// Sets the visibility of this option for a specific player.
    /// </summary>
    /// <param name="player">The player to set visibility for.</param>
    /// <param name="visible">True to make the option visible to the player; false to hide it.</param>
    /// <remarks>
    /// The per-player visibility has lower priority than the global <see cref="Visible"/> property.
    /// </remarks>
    public void SetVisible( IPlayer player, bool visible );

    /// <summary>
    /// Determines whether this option is enabled for the specified player.
    /// </summary>
    /// <param name="player">The player to check enabled state for.</param>
    /// <returns>True if the option is enabled for the player, otherwise, false.</returns>
    public bool GetEnabled( IPlayer player );

    /// <summary>
    /// Sets the enabled state of this option for a specific player.
    /// </summary>
    /// <param name="player">The player to set enabled state for.</param>
    /// <param name="enabled">True to enable the option for the player; false to disable it.</param>
    /// <remarks>
    /// The per-player enabled state has lower priority than the global <see cref="Enabled"/> property.
    /// </remarks>
    public void SetEnabled( IPlayer player, bool enabled );

    // /// <summary>
    // /// Gets the text to display for this option for the specified player.
    // /// </summary>
    // /// <param name="player">The player requesting the text.</param>
    // /// <returns>The text to display.</returns>
    // public string GetText( IPlayer player );

    // /// <summary>
    // /// Gets the formatted HTML markup for this option.
    // /// </summary>
    // /// <param name="player">The player to format for.</param>
    // /// <returns>The formatted HTML string.</returns>
    // public string GetFormattedHtmlText( IPlayer player );

    /// <summary>
    /// Gets the display text for this option as it should appear to the specified player.
    /// </summary>
    /// <param name="player">The player requesting the display text.</param>
    /// <param name="displayLine">The display line index of the option.</param>
    /// <returns>The formatted display text for the option.</returns>
    /// <remarks>
    /// When a menu option occupies multiple lines, MenuAPI may only need to display a specific line of that option.
    /// <list type="bullet">
    /// <item>When <c>LineCount=1</c>: The <c>displayLine</c> parameter is not needed; return the HTML-formatted string directly.</item>
    /// <item>When <c>LineCount>=2</c>: Check the <c>displayLine</c> parameter:
    ///   <list type="bullet">
    ///   <item><c>displayLine=0</c>: Return all content</item>
    ///   <item><c>displayLine=1</c>: Return only the first line content</item>
    ///   <item><c>displayLine=2</c>: Return only the second line content</item>
    ///   <item>And so on...</item>
    ///   </list>
    /// </item>
    /// </list>
    /// Note: MenuAPI ensures that the <c>displayLine</c> parameter will not exceed the option's <c>LineCount</c>.
    /// </remarks>
    public string GetDisplayText( IPlayer player, int displayLine = 0 );

    /// <summary>
    /// Validates whether the specified player can interact with this option.
    /// </summary>
    /// <param name="player">The player to validate.</param>
    /// <returns>A task that represents the asynchronous operation. The task result is true if validation succeeds; otherwise, false.</returns>
    public ValueTask<bool> OnValidatingAsync( IPlayer player );

    // /// <summary>
    // /// Handles the click action for this option.
    // /// </summary>
    // /// <param name="player">The player who clicked the option.</param>
    // /// <param name="closeMenu">Whether to close the menu after handling the click.</param>
    // /// <returns>A task that represents the asynchronous operation.</returns>
    // public ValueTask OnClickAsync( IPlayer player, bool closeMenu = false );

    /// <summary>
    /// Handles the click action for this option.
    /// </summary>
    /// <param name="player">The player who clicked the option.</param>
    /// <returns>A task that represents the asynchronous operation.</returns>
    public ValueTask OnClickAsync( IPlayer player );
}

/// <summary>
/// Defines the available text size options for menu items.
/// </summary>
public enum MenuOptionTextSize
{
    /// <summary>
    /// Extra small text size (fontSize-xs).
    /// </summary>
    ExtraSmall,

    /// <summary>
    /// Small text size (fontSize-s).
    /// </summary>
    Small,

    /// <summary>
    /// Small-medium text size (fontSize-sm).
    /// </summary>
    SmallMedium,

    /// <summary>
    /// Medium text size (fontSize-m).
    /// </summary>
    Medium,

    /// <summary>
    /// Medium-large text size (fontSize-ml).
    /// </summary>
    MediumLarge,

    /// <summary>
    /// Large text size (fontSize-l).
    /// </summary>
    Large,

    /// <summary>
    /// Extra large text size (fontSize-xl).
    /// </summary>
    ExtraLarge
}

/// <summary>
/// Provides extension methods for <see cref="MenuOptionTextSize"/>.
/// </summary>
internal static class MenuOptionTextSizeExtensions
{
    /// <summary>
    /// Converts a <see cref="MenuOptionTextSize"/> value to its corresponding CSS class name.
    /// </summary>
    /// <param name="textSize">The text size to convert.</param>
    /// <returns>The CSS class name for the specified size.</returns>
    /// <exception cref="ArgumentException">Thrown when <paramref name="textSize"/> is not recognized.</exception>
    public static string ToCssClass( this MenuOptionTextSize textSize )
    {
        return textSize switch {
            MenuOptionTextSize.ExtraSmall => "fontSize-xs",
            MenuOptionTextSize.Small => "fontSize-s",
            MenuOptionTextSize.SmallMedium => "fontSize-sm",
            MenuOptionTextSize.Medium => "fontSize-m",
            MenuOptionTextSize.MediumLarge => "fontSize-ml",
            MenuOptionTextSize.Large => "fontSize-l",
            MenuOptionTextSize.ExtraLarge => "fontSize-xl",
            _ => throw new ArgumentException($"Unknown text size: {textSize}.")
        };
    }
}

/// <summary>
/// Defines the horizontal text overflow behavior for menu options.
/// </summary>
public enum MenuOptionTextStyle
{
    /// <summary>
    /// Truncates text at the end when it exceeds the maximum width, keeping the start portion.
    /// Example: "Very Long Text Item" becomes "Very Long..."
    /// </summary>
    TruncateEnd,

    /// <summary>
    /// Truncates text from both ends when it exceeds the maximum width, keeping the middle portion.
    /// Example: "Very Long Text Item" becomes "Long Text"
    /// </summary>
    TruncateBothEnds,

    /// <summary>
    /// Scrolls text to the left with fade-out effect.
    /// Text scrolls left and gradually fades out at the left edge.
    /// </summary>
    ScrollLeftFade,

    /// <summary>
    /// Scrolls text to the right with fade-out effect.
    /// Text scrolls right and gradually fades out at the right edge.
    /// </summary>
    ScrollRightFade,

    /// <summary>
    /// Scrolls text to the left in a continuous loop.
    /// Text exits from the left edge and re-enters from the right edge.
    /// </summary>
    ScrollLeftLoop,

    /// <summary>
    /// Scrolls text to the right in a continuous loop.
    /// Text exits from the right edge and re-enters from the left edge.
    /// </summary>
    ScrollRightLoop
}