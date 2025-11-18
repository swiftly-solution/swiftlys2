using System.Collections.Concurrent;
using System.Text.RegularExpressions;
using SwiftlyS2.Shared.Menus;
using SwiftlyS2.Shared.Players;
using SwiftlyS2.Core.Menus.OptionsBase.Helpers;

namespace SwiftlyS2.Core.Menus.OptionsBase;

/// <summary>
/// Provides a base implementation for menu options with event-driven behavior.
/// </summary>
public abstract partial class MenuOptionBase : IMenuOption, IDisposable
{
    private string text = string.Empty;
    private string? dynamicText = null;
    private float maxWidth = 26f;
    private MenuOptionTextStyle textStyle = MenuOptionTextStyle.TruncateEnd;
    private bool visible = true;
    private bool enabled = true;
    private readonly DynamicTextUpdater? dynamicTextUpdater;
    private readonly ConcurrentDictionary<IPlayer, bool> playerVisible = new();
    private readonly ConcurrentDictionary<IPlayer, bool> playerEnabled = new();
    private readonly ConcurrentDictionary<IPlayer, Task> playerClickTask = new();

    private volatile bool disposed;

    /// <summary>
    /// Creates an instance of <see cref="MenuOptionBase"/>.
    /// </summary>
    /// <remarks>
    /// Using the parameterless constructor will not enable dynamic text updating features.
    /// Derived classes should override the <see cref="GetDisplayText"/> method to implement custom text style changes.
    /// </remarks>
    protected MenuOptionBase()
    {
        disposed = false;
    }

    /// <summary>
    /// Creates an instance of <see cref="MenuOptionBase"/> with dynamic text updating capabilities.
    /// </summary>
    /// <param name="updateIntervalMs">The interval in milliseconds between text updates.</param>
    /// <param name="pauseIntervalMs">The pause duration in milliseconds before starting the next text update cycle.</param>
    /// <remarks>
    /// Values less than 1/64f*1000 milliseconds (approximately 15.6ms) are meaningless,
    /// as the refresh rate would be higher than the game's frame interval.
    /// Both parameters will be automatically clamped to this minimum value.
    /// </remarks>
    protected MenuOptionBase( int updateIntervalMs, int pauseIntervalMs )
    {
        disposed = false;

        if (updateIntervalMs < (int)(1 / 64f * 1000))
        {
            Spectre.Console.AnsiConsole.WriteException(new ArgumentOutOfRangeException(nameof(updateIntervalMs), $"updateIntervalMs: value {updateIntervalMs} is out of range."));
        }

        if (pauseIntervalMs < (int)(1 / 64f * 1000))
        {
            Spectre.Console.AnsiConsole.WriteException(new ArgumentOutOfRangeException(nameof(pauseIntervalMs), $"pauseIntervalMs: value {pauseIntervalMs} is out of range."));
        }

        dynamicTextUpdater = new DynamicTextUpdater(
            () => text,
            () => textStyle,
            () => maxWidth,
            value => dynamicText = value,
            Math.Max((int)(1 / 64f * 1000), updateIntervalMs),
            Math.Max((int)(1 / 64f * 1000), pauseIntervalMs)
        );
    }

    ~MenuOptionBase()
    {
        Dispose();
    }

    public virtual void Dispose()
    {
        if (disposed)
        {
            return;
        }

        // Console.WriteLine($"{GetType().Name} has been disposed.");
        dynamicTextUpdater?.Dispose();

        disposed = true;
        GC.SuppressFinalize(this);
    }

    /// <summary>
    /// Gets or sets the menu that this option belongs to.
    /// </summary>
    /// <remarks>
    /// This property will be null until the option is added to a menu via <see cref="IMenuAPI.AddOption"/>.
    /// When implementing custom menu options, avoid accessing this property in the constructor as it will not be set yet.
    /// </remarks>
    public IMenuAPI? Menu { get; internal set; }

    /// <summary>
    /// Gets the number of lines this option requests to occupy in the menu.
    /// </summary>
    public virtual int LineCount => 1;

    /// <summary>
    /// Gets or sets the text content displayed for this menu option.
    /// </summary>
    /// <remarks>
    /// This is a global property. Changing it will affect what all players see.
    /// </remarks>
    public string Text {
        get => text;
        set {
            if (text == value)
            {
                return;
            }

            text = value;
            // dynamicText = null;

            TextChanged?.Invoke(this, new MenuOptionEventArgs { Player = null!, Option = this });
        }
    }

    /// <summary>
    /// The maximum display width for menu option text in relative units.
    /// </summary>
    public float MaxWidth {
        get => maxWidth;
        set {
            if (maxWidth == value)
            {
                return;
            }

            if (value < 1f)
            {
                Spectre.Console.AnsiConsole.WriteException(new ArgumentOutOfRangeException(nameof(MaxWidth), $"MaxWidth: value {value:F3} is out of range."));
            }

            maxWidth = Math.Max(value, 1f);
            // dynamicText = null;
        }
    }

    /// <summary>
    /// Gets or sets a value indicating whether this option is visible in the menu.
    /// </summary>
    /// <remarks>
    /// This is a global property. Changing it will affect what all players see.
    /// </remarks>
    public bool Visible {
        get => visible;
        set {
            if (visible == value)
            {
                return;
            }

            visible = value;
            VisibilityChanged?.Invoke(this, new MenuOptionEventArgs { Player = null!, Option = this });
        }
    }

    /// <summary>
    /// Gets or sets a value indicating whether this option can be interacted with.
    /// </summary>
    /// <remarks>
    /// This is a global property. Changing it will affect what all players see.
    /// </remarks>
    public bool Enabled {
        get => enabled;
        set {
            if (enabled == value)
            {
                return;
            }

            enabled = value;
            EnabledChanged?.Invoke(this, new MenuOptionEventArgs { Player = null!, Option = this });
        }
    }

    /// <summary>
    /// Gets or sets a value indicating whether the menu should be closed after handling the click.
    /// </summary>
    public bool CloseAfterClick { get; init; } = false;

    /// <summary>
    /// Gets or sets an object that contains data about this option.
    /// </summary>
    public object? Tag { get; set; }

    /// <summary>
    /// Gets or sets the text size for this option.
    /// </summary>
    public MenuOptionTextSize TextSize { get; set; } = MenuOptionTextSize.Medium;

    /// <summary>
    /// Gets or sets the text overflow style for this option.
    /// </summary>
    public MenuOptionTextStyle TextStyle {
        get => textStyle;
        set {
            if (textStyle == value)
            {
                return;
            }

            textStyle = value;
            // dynamicText = null;
        }
    }

    /// <summary>
    /// Gets or sets a value indicating whether a sound should play when this option is selected.
    /// </summary>
    public bool PlaySound { get; set; } = true;

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
    /// Occurs before a click is processed, allowing validation and cancellation.
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
    public virtual bool IsClickTaskCompleted( IPlayer player ) => !playerClickTask.TryGetValue(player, out var value) || value.IsCompleted;

    /// <summary>
    /// Determines whether this option is visible to the specified player.
    /// </summary>
    /// <param name="player">The player to check visibility for.</param>
    /// <returns>True if the option is visible to the player; otherwise, false.</returns>
    public virtual bool GetVisible( IPlayer player ) => playerVisible.TryGetValue(player, out var value) ? value : Visible;

    /// <summary>
    /// Sets the visibility of this option for a specific player.
    /// </summary>
    /// <param name="player">The player to set visibility for.</param>
    /// <param name="visible">True to make the option visible to the player; false to hide it.</param>
    /// <remarks>
    /// The per-player visibility has lower priority than the global <see cref="Visible"/> property.
    /// </remarks>
    public virtual void SetVisible( IPlayer player, bool visible ) => playerVisible.AddOrUpdate(player, visible, ( key, value ) => visible);

    /// <summary>
    /// Determines whether this option is enabled for the specified player.
    /// </summary>
    /// <param name="player">The player to check enabled state for.</param>
    /// <returns>True if the option is enabled for the player; otherwise, false.</returns>
    public virtual bool GetEnabled( IPlayer player ) => playerEnabled.TryGetValue(player, out var value) ? value : Enabled;

    /// <summary>
    /// Sets the enabled state of this option for a specific player.
    /// </summary>
    /// <param name="player">The player to set enabled state for.</param>
    /// <param name="enabled">True to enable the option for the player; false to disable it.</param>
    /// <remarks>
    /// The per-player enabled state has lower priority than the global <see cref="Enabled"/> property.
    /// </remarks>
    public virtual void SetEnabled( IPlayer player, bool enabled ) => playerEnabled.AddOrUpdate(player, enabled, ( key, value ) => enabled);

    // /// <summary>
    // /// Gets the text to display for this option for the specified player.
    // /// </summary>
    // /// <param name="player">The player requesting the text.</param>
    // /// <returns>The text to display.</returns>
    // public virtual string GetText( IPlayer player ) => Text;

    // /// <summary>
    // /// Gets the formatted HTML markup for this option.
    // /// </summary>
    // /// <param name="player">The player to format for.</param>
    // /// <returns>The formatted HTML string.</returns>
    // public virtual string GetFormattedHtmlText( IPlayer player )
    // {
    //     var args = new MenuOptionFormattingEventArgs {
    //         Player = player,
    //         Option = this,
    //         CustomText = null
    //     };

    //     BeforeFormat?.Invoke(this, args);

    //     var displayText = args.CustomText ?? GetText(player);
    //     var isEnabled = GetEnabled(player);
    //     var sizeClass = GetSizeClass(TextSize);

    //     var colorStyle = isEnabled ? "" : " color='grey'";
    //     var result = $"<font class='{sizeClass}'{colorStyle}>{displayText}</font>";

    //     args.CustomText = result;
    //     AfterFormat?.Invoke(this, args);

    //     return args.CustomText;
    // }

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
    public virtual string GetDisplayText( IPlayer player, int displayLine = 0 )
    {
        var args = new MenuOptionFormattingEventArgs {
            Player = player,
            Option = this,
            CustomText = null
        };

        BeforeFormat?.Invoke(this, args);

        if (playerClickTask.TryGetValue(player, out var value) && !value.IsCompleted)
        {
            args.CustomText = "<font color='#C0FF3E'>Waiting...</font>";
        }
        else
        {
            var displayText = args.CustomText ?? dynamicText ?? Text;

            if (displayLine > 0)
            {
                var lines = BrTagRegex().Split(displayText);
                if (displayLine <= lines.Length)
                {
                    displayText = lines[displayLine - 1];
                }
            }

            var isEnabled = Enabled && GetEnabled(player);
            var sizeClass = TextSize.ToCssClass();

            if (!isEnabled)
            {
                displayText = ColorTagRegex().Replace(displayText, string.Empty);
            }

            var colorStyle = isEnabled ? string.Empty : $" color='{Menu?.Configuration.DisabledColor ?? "#666666"}'";
            var result = $"<font class='{sizeClass}'{colorStyle}>{displayText}</font>";
            // Console.WriteLine($"displayText: {displayText}");

            args.CustomText = result;
        }
        AfterFormat?.Invoke(this, args);

        return args.CustomText;
    }

    /// <summary>
    /// Validates whether the specified player can interact with this option.
    /// </summary>
    /// <param name="player">The player to validate.</param>
    /// <returns>A task that represents the asynchronous operation. The task result is true if validation succeeds; otherwise, false.</returns>
    public virtual ValueTask<bool> OnValidatingAsync( IPlayer player )
    {
        if (Validating == null)
        {
            return ValueTask.FromResult(true);
        }

        var args = new MenuOptionValidatingEventArgs {
            Player = player,
            Option = this,
            Cancel = false
        };

        Validating?.Invoke(this, args);
        return ValueTask.FromResult(!args.Cancel);
    }

    // /// <summary>
    // /// Handles the click action for this option.
    // /// </summary>
    // /// <param name="player">The player who clicked the option.</param>
    // /// <param name="closeMenu">Whether to close the menu after handling the click.</param>
    // /// <returns>A task that represents the asynchronous operation.</returns>
    // public virtual async ValueTask OnClickAsync( IPlayer player, bool closeMenu = false )
    // {
    //     if (!await OnValidatingAsync(player))
    //     {
    //         return;
    //     }

    //     if (Click != null)
    //     {
    //         var args = new MenuOptionClickEventArgs {
    //             Player = player,
    //             Option = this,
    //             CloseMenu = closeMenu
    //         };

    //         await Click.Invoke(this, args);

    //         // if (args.CloseMenu)
    //         // {
    //         //     Menu?.CloseForPlayer(player);
    //         // }
    //     }
    // }

    /// <summary>
    /// Handles the click action for this option.
    /// </summary>
    /// <param name="player">The player who clicked the option.</param>
    /// <returns>A task that represents the asynchronous operation.</returns>
    public virtual async ValueTask OnClickAsync( IPlayer player )
    {
        if (!visible || !enabled || !GetVisible(player) || !GetEnabled(player))
        {
            return;
        }

        if (playerClickTask.TryGetValue(player, out var value) && !value.IsCompleted)
        {
            return;
        }

        if (!await OnValidatingAsync(player))
        {
            return;
        }

        if (Click != null)
        {
            var args = new MenuOptionClickEventArgs {
                Player = player,
                // Option = this,
                CloseMenu = CloseAfterClick
            };

            try
            {
                var clickTask = Click.Invoke(this, args).AsTask();
                _ = playerClickTask.AddOrUpdate(player, clickTask, ( _, _ ) => clickTask);
                await clickTask;
            }
            finally
            {
                _ = playerClickTask.TryRemove(player, out _);
            }
        }

        if (CloseAfterClick)
        {
            Menu?.MenuManager.CloseMenuForPlayer(player, Menu!);
        }
    }

    // /// <summary>
    // /// Raises the <see cref="Hover"/> event.
    // /// </summary>
    // /// <param name="player">The player whose cursor entered the option.</param>
    // protected virtual void OnHover( IPlayer player )
    // {
    //     Hover?.Invoke(this, new MenuOptionEventArgs { Player = player, Option = this });
    // }

    [GeneratedRegex(@"<[/\\]*br[/\\]*>", RegexOptions.IgnoreCase)]
    private static partial Regex BrTagRegex();

    [GeneratedRegex(@"\scolor\s*=\s*['""]{1}#[0-9A-Fa-f]{6}['""]{1}", RegexOptions.IgnoreCase)]
    private static partial Regex ColorTagRegex();
}