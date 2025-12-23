using System.Collections.Concurrent;
using Spectre.Console;
using SwiftlyS2.Shared.Menus;
using SwiftlyS2.Shared.Players;

namespace SwiftlyS2.Core.Menus.OptionsBase;

/// <summary>
/// Represents a menu option that cycles through a list of choices.
/// </summary>
public sealed class ChoiceMenuOption : MenuOptionBase
{
    private readonly ConcurrentDictionary<int, int> selectedIndices = new();
    private readonly List<string> choices;
    private readonly int defaultIndex;

    /// <summary>
    /// Gets the read-only list of available choices.
    /// </summary>
    public IReadOnlyList<string> Choices => choices.AsReadOnly();

    /// <summary>
    /// Occurs when the selected choice changes for a player.
    /// </summary>
    public event EventHandler<MenuOptionValueChangedEventArgs<string>>? ValueChanged;

    /// <summary>
    /// Creates an instance of <see cref="ChoiceMenuOption"/> with a list of choices.
    /// </summary>
    /// <param name="choices">The list of available choices.</param>
    /// <param name="defaultChoice">The default choice to select. If null or not found, defaults to first choice.</param>
    /// <param name="updateIntervalMs">The interval in milliseconds between text updates. Defaults to 120ms.</param>
    /// <param name="pauseIntervalMs">The pause duration in milliseconds before starting the next text update cycle. Defaults to 1000ms.</param>
    /// <remarks>
    /// When using this constructor, the <see cref="MenuOptionBase.Text"/> property must be manually set to specify the initial text.
    /// </remarks>
    public ChoiceMenuOption(
        IEnumerable<string> choices,
        string? defaultChoice = null,
        int updateIntervalMs = 120,
        int pauseIntervalMs = 1000 ) : base(updateIntervalMs, pauseIntervalMs)
    {
        PlaySound = true;
        this.choices = choices.ToList();

        if (this.choices.Count == 0)
        {
            Spectre.Console.AnsiConsole.WriteException(new ArgumentException("Choices list cannot be empty. Adding a default choice.", nameof(choices)));
            this.choices.Add("Default");
        }

        this.defaultIndex = defaultChoice != null && this.choices.Contains(defaultChoice) ? this.choices.IndexOf(defaultChoice) : 0;

        selectedIndices.Clear();
        Click += OnChoiceClick;
    }

    /// <summary>
    /// Creates an instance of <see cref="ChoiceMenuOption"/> with a list of choices.
    /// </summary>
    /// <param name="text">The text content to display.</param>
    /// <param name="choices">The list of available choices.</param>
    /// <param name="defaultChoice">The default choice to select. If null or not found, defaults to first choice.</param>
    /// <param name="updateIntervalMs">The interval in milliseconds between text updates. Defaults to 120ms.</param>
    /// <param name="pauseIntervalMs">The pause duration in milliseconds before starting the next text update cycle. Defaults to 1000ms.</param>
    public ChoiceMenuOption(
        string text,
        IEnumerable<string> choices,
        string? defaultChoice = null,
        int updateIntervalMs = 120,
        int pauseIntervalMs = 1000 ) : this(choices, defaultChoice, updateIntervalMs, pauseIntervalMs)
    {
        Text = text;
    }

    public override string GetDisplayText( IPlayer player, int displayLine = 0 )
    {
        var text = base.GetDisplayText(player, displayLine);
        var index = selectedIndices.GetOrAdd(player.PlayerID, defaultIndex);
        var choice = choices[Math.Clamp(index, 0, choices.Count - 1)];
        return $"{text}: <font color='#FFFFFF'>[</font>{choice}<font color='#FF3333'>]</font>";
    }

    /// <summary>
    /// Gets the currently selected choice for the specified player.
    /// </summary>
    /// <param name="player">The player whose selected choice to retrieve.</param>
    /// <returns>The currently selected choice string.</returns>
    public string GetSelectedChoice( IPlayer player )
    {
        var index = selectedIndices.GetOrAdd(player.PlayerID, defaultIndex);
        return choices[Math.Clamp(index, 0, choices.Count - 1)];
    }

    /// <summary>
    /// Sets the selected choice for the specified player.
    /// </summary>
    /// <param name="player">The player whose choice to set.</param>
    /// <param name="choice">The choice to select. Must exist in the <see cref="Choices"/> list.</param>
    public void SetSelectedChoice( IPlayer player, string choice )
    {
        var index = choices.IndexOf(choice);
        if (index >= 0)
        {
            _ = selectedIndices.AddOrUpdate(player.PlayerID, index, ( _, _ ) => index);
        }
    }

    private ValueTask OnChoiceClick( object? sender, MenuOptionClickEventArgs args )
    {
        var newIndex = selectedIndices.AddOrUpdate(
            args.Player.PlayerID,
            (defaultIndex + 1) % choices.Count,
            ( _, current ) => (current + 1) % choices.Count
        );

        var selectedChoice = choices[newIndex];

        try
        {
            ValueChanged?.Invoke(this, new MenuOptionValueChangedEventArgs<string> {
                Player = args.Player,
                Option = this,
                OldValue = choices[(newIndex - 1 + choices.Count) % choices.Count],
                NewValue = selectedChoice
            });
        }
        catch (Exception e)
        {
            if (!GlobalExceptionHandler.Handle(e)) return ValueTask.CompletedTask;
            AnsiConsole.WriteException(e);
        }

        return ValueTask.CompletedTask;
    }
}