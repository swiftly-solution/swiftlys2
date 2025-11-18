using System.Collections.Concurrent;
using SwiftlyS2.Shared.Menus;
using SwiftlyS2.Shared.Players;

namespace SwiftlyS2.Core.Menus.OptionsBase;

/// <summary>
/// Represents a toggleable menu option that displays an on/off state.
/// </summary>
public sealed class ToggleMenuOption : MenuOptionBase
{
    private readonly ConcurrentDictionary<IPlayer, bool> toggled = new();
    private readonly bool defaultToggleState;
    private readonly string toggleOnSymbol;
    private readonly string toggleOffSymbol;

    /// <summary>
    /// Event triggered when the toggle value changes for a player.
    /// </summary>
    public event EventHandler<MenuOptionValueChangedEventArgs<bool>>? ValueChanged;

    /// <summary>
    /// Creates an instance of <see cref="ToggleMenuOption"/> with dynamic text updating capabilities.
    /// </summary>
    /// <param name="defaultToggleState">The default toggle state for new players. Defaults to true.</param>
    /// <param name="toggleOnSymbol">The HTML symbol to display when toggle is on. Defaults to green checkmark.</param>
    /// <param name="toggleOffSymbol">The HTML symbol to display when toggle is off. Defaults to red X mark.</param>
    /// <param name="updateIntervalMs">The interval in milliseconds between text updates. Defaults to 120ms.</param>
    /// <param name="pauseIntervalMs">The pause duration in milliseconds before starting the next text update cycle. Defaults to 1000ms.</param>
    /// <remarks>
    /// When using this constructor, the <see cref="MenuOptionBase.Text"/> property must be manually set to specify the initial text.
    /// </remarks>
    public ToggleMenuOption(
        bool defaultToggleState = true,
        string? toggleOnSymbol = null,
        string? toggleOffSymbol = null,
        int updateIntervalMs = 120,
        int pauseIntervalMs = 1000 ) : base(updateIntervalMs, pauseIntervalMs)
    {
        this.defaultToggleState = defaultToggleState;
        this.toggleOnSymbol = toggleOnSymbol ?? "✔";
        this.toggleOffSymbol = toggleOffSymbol ?? "✘";
        PlaySound = true;

        toggled.Clear();

        Click += OnToggleClick;
    }

    /// <summary>
    /// Creates an instance of <see cref="ToggleMenuOption"/> with dynamic text updating capabilities.
    /// </summary>
    /// <param name="text">The text content to display.</param>
    /// <param name="defaultToggleState">The default toggle state for new players. Defaults to true.</param>
    /// <param name="toggleOnSymbol">The HTML symbol to display when toggle is on. Defaults to green checkmark.</param>
    /// <param name="toggleOffSymbol">The HTML symbol to display when toggle is off. Defaults to red X mark.</param>
    /// <param name="updateIntervalMs">The interval in milliseconds between text updates. Defaults to 120ms.</param>
    /// <param name="pauseIntervalMs">The pause duration in milliseconds before starting the next text update cycle. Defaults to 1000ms.</param>
    public ToggleMenuOption(
        string text,
        bool defaultToggleState = true,
        string? toggleOnSymbol = null,
        string? toggleOffSymbol = null,
        int updateIntervalMs = 120,
        int pauseIntervalMs = 1000 ) : this(defaultToggleState, toggleOnSymbol, toggleOffSymbol, updateIntervalMs, pauseIntervalMs)
    {
        Text = text;
    }

    /// <summary>
    /// Gets the display text for this option, including the toggle state indicator.
    /// </summary>
    /// <param name="player">The player viewing the option.</param>
    /// <param name="displayLine">The display line number (not used in this implementation).</param>
    /// <returns>The formatted display text with toggle state indicator.</returns>
    public override string GetDisplayText( IPlayer player, int displayLine = 0 )
    {
        var text = base.GetDisplayText(player, displayLine);
        var isToggled = toggled.GetOrAdd(player, defaultToggleState);
        return $"{text} {(isToggled ? $"<font color='#008000'>{toggleOnSymbol}</font>" : $"<font color='#FF0000'>{toggleOffSymbol}</font>")}";
    }

    /// <summary>
    /// Gets the toggle state for the specified player.
    /// </summary>
    /// <param name="player">The player whose toggle state to retrieve.</param>
    /// <returns>True if toggled on, false if toggled off. Uses the configured default value for new players.</returns>
    public bool GetToggleState( IPlayer player )
    {
        return toggled.GetOrAdd(player, defaultToggleState);
    }

    /// <summary>
    /// Sets the toggle state for the specified player and triggers the value changed event.
    /// </summary>
    /// <param name="player">The player whose toggle state to set.</param>
    /// <param name="value">The toggle state to set.</param>
    /// <returns>True if the value was changed, false if it was already the same value.</returns>
    public bool SetToggleState( IPlayer player, bool value )
    {
        var oldValue = toggled.GetOrAdd(player, defaultToggleState);

        if (oldValue == value)
        {
            return false;
        }

        _ = toggled.AddOrUpdate(player, value, ( _, _ ) => value);

        ValueChanged?.Invoke(this, new MenuOptionValueChangedEventArgs<bool> {
            Player = player,
            Option = this,
            OldValue = oldValue,
            NewValue = value
        });

        return true;
    }

    private ValueTask OnToggleClick( object? sender, MenuOptionClickEventArgs args )
    {
        _ = SetToggleState(args.Player, !GetToggleState(args.Player));
        return ValueTask.CompletedTask;
    }
}