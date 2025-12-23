using SwiftlyS2.Shared.Players;

namespace SwiftlyS2.Shared.Menus;

/// <summary>
/// Defines configuration settings that control menu behavior.
/// </summary>
/// <summary>
/// Configuration settings that control menu behavior, appearance, and player interaction.
/// Defines various aspects of menu functionality including navigation, input handling, audio feedback, and display options.
/// </summary>
public record class MenuConfiguration
{
    private int maxVisibleItems = -1;
    private string? navigationMarkerColor = null;
    private string? footerColor = null;
    private string? visualGuideLineColor = null;
    private string? disabledColor = null;

    /// <summary>
    /// The title of the menu.
    /// </summary>
    public string Title { get; set; } = "Menu";

    /// <summary>
    /// Whether to hide the menu title.
    /// </summary>
    public bool HideTitle { get; set; } = false;

    /// <summary>
    /// Whether to hide the menu footer.
    /// </summary>
    public bool HideFooter { get; set; } = false;

    /// <summary>
    /// Whether to play sounds when players interact with the menu.
    /// </summary>
    public bool PlaySound { get; set; } = true;

    public bool HideTitleItemCount { get; set; } = false;

    /// <summary>
    /// Maximum number of menu options displayed on screen at once.
    /// </summary>
    /// <remarks>
    /// Valid range is [1, 5]. If set to a value outside this range, an exception will be thrown and the value will be set to -1.
    /// <para>
    /// When set to -1, the maximum visible items per page will use the <c>ItemsPerPage</c> value from the configuration file.
    /// </para>
    /// </remarks>
    public int MaxVisibleItems {
        get => maxVisibleItems;
        set {
            if (value < 1 || value > 5)
            {
                Spectre.Console.AnsiConsole.WriteException(new ArgumentOutOfRangeException(nameof(value), $"MaxVisibleItems: value {value} is out of range [1, 5]."));
                maxVisibleItems = -1;
            }
            else
            {
                maxVisibleItems = value;
            }
        }
    }

    /// <summary>
    /// Whether to automatically increase <see cref="MaxVisibleItems"/> when <see cref="HideTitle"/> or <see cref="HideFooter"/> is enabled.
    /// Each hidden section adds 1 to the visible items count.
    /// </summary>
    /// <remarks>
    /// This does not modify the actual <see cref="MaxVisibleItems"/> value.
    /// Instead, the increase is applied during rendering calculations only.
    /// </remarks>
    public bool AutoIncreaseVisibleItems { get; set; } = true;

    /// <summary>
    /// Whether to freeze player movement while the menu is open.
    /// </summary>
    public bool FreezePlayer { get; set; } = false;

    /// <summary>
    /// Whether to disable the exit button for this menu.
    /// </summary>
    public bool DisableExit { get; set; } = false;

    /// <summary>
    /// Time in seconds before the menu automatically closes. Set to 0 or less to disable auto-close.
    /// </summary>
    public float AutoCloseAfter { get; set; } = 0f;

    /// <summary>
    /// The color of navigation markers (selection indicators, page indicators, etc.) in hex format.
    /// </summary>
    /// <remarks>
    /// Supports "#RGB", "#RGBA", "#RRGGBB", and "#RRGGBBAA" formats.
    /// </remarks>
    public string? NavigationMarkerColor {
        get => navigationMarkerColor;
        set {
            if (string.IsNullOrWhiteSpace(value) || Helper.ParseHexColor(value) is not (not null, not null, not null, _))
            {
                Spectre.Console.AnsiConsole.WriteException(new ArgumentException($"NavigationMarkerColor: '{value}' is not a valid hex color format. Expected '#RRGGBB'.", nameof(value)));
                navigationMarkerColor = null;
            }
            else
            {
                navigationMarkerColor = value;
            }
        }
    }

    /// <summary>
    /// The color of the menu footer in hex format.
    /// </summary>
    /// <remarks>
    /// Supports "#RGB", "#RGBA", "#RRGGBB", and "#RRGGBBAA" formats.
    /// </remarks>
    public string? FooterColor {
        get => footerColor;
        set {
            if (string.IsNullOrWhiteSpace(value) || Helper.ParseHexColor(value) is not (not null, not null, not null, _))
            {
                Spectre.Console.AnsiConsole.WriteException(new ArgumentException($"FooterColor: '{value}' is not a valid hex color format. Expected '#RRGGBB'.", nameof(value)));
                footerColor = null;
            }
            else
            {
                footerColor = value;
            }
        }
    }

    /// <summary>
    /// The color of visual guide lines in hex format.
    /// </summary>
    /// <remarks>
    /// Supports "#RGB", "#RGBA", "#RRGGBB", and "#RRGGBBAA" formats.
    /// </remarks>
    public string? VisualGuideLineColor {
        get => visualGuideLineColor;
        set {
            if (string.IsNullOrWhiteSpace(value) || Helper.ParseHexColor(value) is not (not null, not null, not null, _))
            {
                Spectre.Console.AnsiConsole.WriteException(new ArgumentException($"VisualGuideLineColor: '{value}' is not a valid hex color format. Expected '#RRGGBB'.", nameof(value)));
                visualGuideLineColor = null;
            }
            else
            {
                visualGuideLineColor = value;
            }
        }
    }

    /// <summary>
    /// The color of disabled menu options in hex format.
    /// </summary>
    /// <remarks>
    /// Supports "#RGB", "#RGBA", "#RRGGBB", and "#RRGGBBAA" formats.
    /// </remarks>
    public string? DisabledColor {
        get => disabledColor;
        set {
            if (string.IsNullOrWhiteSpace(value) || Helper.ParseHexColor(value) is not (not null, not null, not null, _))
            {
                Spectre.Console.AnsiConsole.WriteException(new ArgumentException($"DisabledColor: '{value}' is not a valid hex color format. Expected '#RRGGBB'.", nameof(value)));
                disabledColor = null;
            }
            else
            {
                disabledColor = value;
            }
        }
    }
}

/// <summary>
/// Custom key bindings for menu actions.
/// Each property can be set to override the default bindings, or left null to use defaults.
/// </summary>
/// <remarks>
/// NOTE: For WASD input mode, any key binding overrides will not take effect.
/// </remarks>
public readonly record struct MenuKeybindOverrides
{
    /// <summary>
    /// Key binding for selecting or activating the highlighted menu option.
    /// </summary>
    public KeyBind? Select { get; init; }

    /// <summary>
    /// Key binding for moving forward through menu options.
    /// </summary>
    public KeyBind? Move { get; init; }

    /// <summary>
    /// Key binding for moving backward through menu options.
    /// </summary>
    public KeyBind? MoveBack { get; init; }

    /// <summary>
    /// Key binding for closing the menu.
    /// </summary>
    public KeyBind? Exit { get; init; }
}

/// <summary>
/// Provides event data for menu-related events.
/// </summary>
public sealed class MenuEventArgs : EventArgs
{
    /// <summary>
    /// The player who triggered this menu event.
    /// </summary>
    public IPlayer? Player { get; init; } = null;

    /// <summary>
    /// The menu options involved in this event.
    /// </summary>
    public IReadOnlyList<IMenuOption>? Options { get; init; } = null;
}

/// <summary>
/// Represents an interactive menu that can be displayed to players.
/// </summary>
public interface IMenuAPI : IDisposable
{
    /// <summary>
    /// The menu manager that this menu belongs to.
    /// </summary>
    public IMenuManagerAPI MenuManager { get; }

    /// <summary>
    /// Configuration settings for this menu.
    /// </summary>
    public MenuConfiguration Configuration { get; }

    /// <summary>
    /// Keybind overrides for this menu.
    /// </summary>
    public MenuKeybindOverrides KeybindOverrides { get; }

    /// <summary>
    /// The scroll style for this menu options.
    /// </summary>
    public MenuOptionScrollStyle OptionScrollStyle { get; }

    // /// <summary>
    // /// The text overflow style for menu options.
    // /// </summary>
    // public MenuOptionTextStyle OptionTextStyle { get; }

    /// <summary>
    /// The builder used to construct and configure this menu.
    /// </summary>
    public IMenuBuilderAPI? Builder { get; }

    /// <summary>
    /// Gets or sets the default comment text to use when a menu option's <see cref="IMenuOption.Comment"/> is not set.
    /// </summary>
    public string DefaultComment { get; set; }

    /// <summary>
    /// Gets or sets an object that contains data about this menu.
    /// </summary>
    public object? Tag { get; set; }

    /// <summary>
    /// The parent hierarchy information in a hierarchical menu structure.
    /// </summary>
    /// <remarks>
    /// ParentMenu is the parent menu instance, null for top-level menus.
    /// TriggerOption is the menu option that triggered this submenu, null for top-level or directly created menus.
    /// </remarks>
    public (IMenuAPI? ParentMenu, IMenuOption? TriggerOption) Parent { get; }

    /// <summary>
    /// Read-only collection of all options in this menu.
    /// </summary>
    public IReadOnlyList<IMenuOption> Options { get; }

    // /// <summary>
    // /// Fired before a player navigates to a different menu option.
    // /// </summary>
    // public event EventHandler<MenuEventArgs>? BeforeSelectionMove;

    // /// <summary>
    // /// Fired after a player navigates to a different menu option.
    // /// </summary>
    // public event EventHandler<MenuEventArgs>? AfterSelectionMove;

    /// <summary>
    /// Fired when the selection pointer is hovering over an option.
    /// </summary>
    /// <remarks>
    /// This event is fired once per render frame.
    /// </remarks>
    public event EventHandler<MenuEventArgs>? OptionHovering;

    /// <summary>
    /// Fired when a different option is hovered.
    /// </summary>
    /// <remarks>
    /// This event is only fired when the hovered option changes.
    /// </remarks>
    public event EventHandler<MenuEventArgs>? OptionHovered;

    /// <summary>
    /// Fired when a menu option is selected (activated) by the player.
    /// </summary>
    public event EventHandler<MenuEventArgs>? OptionSelected;

    // /// <summary>
    // /// Fired when an option is about to enter the visible viewport.
    // /// </summary>
    // public event EventHandler<MenuEventArgs>? OptionEntering;

    // /// <summary>
    // /// Fired when an option is about to leave the visible viewport.
    // /// </summary>
    // public event EventHandler<MenuEventArgs>? OptionLeaving;

    /// <summary>
    /// Shows this menu to the specified player by displaying its content.
    /// </summary>
    /// <param name="player">The player who will see the menu.</param>
    /// <remarks>
    /// This method only displays the menu visually. To properly open a menu (which handles state management, 
    /// closing other menus, and triggering events), use <see cref="IMenuManagerAPI.OpenMenuForPlayer"/> instead.
    /// </remarks>
    public void ShowForPlayer( IPlayer player );

    /// <summary>
    /// Hides this menu for the specified player by removing its visual display.
    /// </summary>
    /// <param name="player">The player whose menu will be hidden.</param>
    /// <remarks>
    /// This method only hides the menu visually. To properly close a menu (which handles state cleanup, 
    /// triggering events, and reopening parent menus), use <see cref="IMenuManagerAPI.CloseMenuForPlayer"/> instead.
    /// </remarks>
    public void HideForPlayer( IPlayer player );

    /// <summary>
    /// Adds a new option to this menu.
    /// </summary>
    /// <param name="option">The menu option to add.</param>
    public void AddOption( IMenuOption option );

    /// <summary>
    /// Removes an option from this menu.
    /// </summary>
    /// <param name="option">The menu option to remove.</param>
    /// <returns>True if the option was successfully removed, false if the option was not found.</returns>
    public bool RemoveOption( IMenuOption option );

    /// <summary>
    /// Moves the player's selection to the specified option.
    /// </summary>
    /// <param name="player">The player whose selection to move.</param>
    /// <param name="option">The option to move the selection to.</param>
    /// <returns>True if the move was successful, false if the option was not found.</returns>
    public bool MoveToOption( IPlayer player, IMenuOption option );

    /// <summary>
    /// Moves the player's selection to the specified option index.
    /// </summary>
    /// <param name="player">The player whose selection to move.</param>
    /// <param name="index">The index of the option to move the selection to.</param>
    /// <returns>True if the move was successful, false if the index was out of bounds.</returns>
    public bool MoveToOptionIndex( IPlayer player, int index );

    /// <summary>
    /// Gets the menu option currently highlighted by the specified player.
    /// </summary>
    /// <param name="player">The player whose current selection to retrieve.</param>
    /// <returns>The currently selected option, or null if nothing is selected.</returns>
    public IMenuOption? GetCurrentOption( IPlayer player );

    /// <summary>
    /// Gets the index of the currently highlighted option for the specified player.
    /// </summary>
    /// <param name="player">The player whose current selection index to retrieve.</param>
    /// <returns>The index of the currently selected option, or -1 if nothing is selected.</returns>
    public int GetCurrentOptionIndex( IPlayer player );

    // /// <summary>
    // /// Gets the display line index of the currently highlighted option for the specified player.
    // /// </summary>
    // /// <param name="player">The player whose current selection display line to retrieve.</param>
    // /// <returns>The display line index of the currently selected option, or -1 if nothing is selected.</returns>
    // public int GetCurrentOptionDisplayLine( IPlayer player );
}

/// <summary>
/// Defines how the menu scrolls when navigating between options.
/// </summary>
public enum MenuOptionScrollStyle
{
    /// <summary>
    /// The selection indicator moves up and down through the visible menu area.
    /// The menu content stays fixed until the indicator reaches the edge.
    /// </summary>
    LinearScroll,

    /// <summary>
    /// The selection indicator always stays in the center position.
    /// Menu options scroll circularly around it, wrapping from bottom to top (e.g., ...7, 8, 1, 2, 3...).
    /// </summary>
    CenterFixed,

    /// <summary>
    /// The selection indicator moves until it reaches the center, then stays there.
    /// At the top and bottom edges, the indicator can move away from center.
    /// </summary>
    WaitingCenter
}