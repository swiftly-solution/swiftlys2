using System.Collections.Concurrent;
using Spectre.Console;
using SwiftlyS2.Shared;
using SwiftlyS2.Shared.Menus;
using SwiftlyS2.Shared.Players;
using SwiftlyS2.Core.Menus.OptionsBase.Helpers;

namespace SwiftlyS2.Core.Menus.OptionsBase;

/// <summary>
/// Represents a selector menu option that allows cycling through a list of choices using left/right keys.
/// Displays as: Label: PrevChoice [CurrentChoice] NextChoice
/// This option claims the Exit and Use keys for previous and next selection respectively.
/// </summary>
/// <typeparam name="T">The type of the choices.</typeparam>
public sealed class SelectorMenuOption<T> : MenuOptionBase
{
    private readonly ConcurrentDictionary<int, int> selectedIndices = new();
    private readonly IReadOnlyList<string> formattedChoices;
    private readonly IReadOnlyList<TextStyleProcessor?> textProcessors; // Per-choice processor
    private readonly int defaultIndex;
    private readonly float itemMaxWidth;
    private readonly int updateIntervalMs;
    private readonly int pauseIntervalMs;

    // Animation state per choice
    private readonly string?[] dynamicTexts;
    private readonly DateTime[] lastUpdateTimes;
    private readonly DateTime[] pauseEndTimes;
    private volatile bool isPaused;

    /// <summary>
    /// Gets the available choices for this selector.
    /// </summary>
    public IReadOnlyList<T> Choices { get; init; }

    /// <summary>
    /// Gets or sets whether the selector should wrap around when reaching the end.
    /// </summary>
    public bool WrapAround { get; set; } = true;

    /// <summary>
    /// Occurs when the selected choice changes for a player.
    /// </summary>
    public event EventHandler<MenuOptionValueChangedEventArgs<T>>? SelectionChanged;

    /// <summary>
    /// Creates an instance of <see cref="SelectorMenuOption{T}"/>.
    /// </summary>
    /// <param name="choices">The list of available choices.</param>
    /// <param name="defaultIndex">The default selected index. Defaults to 0.</param>
    /// <param name="displayFormatter">A function to format each choice for display. Defaults to ToString().</param>
    /// <param name="itemMaxWidth">The maximum width for each choice item. Defaults to 10.</param>
    /// <param name="updateIntervalMs">The interval in milliseconds between text animation updates. Defaults to 120ms.</param>
    /// <param name="pauseIntervalMs">The pause duration in milliseconds before starting the next animation cycle. Defaults to 1000ms.</param>
    /// <remarks>
    /// When using this constructor, the <see cref="MenuOptionBase.Text"/> property must be manually set to specify the label.
    /// </remarks>
    public SelectorMenuOption(
        IEnumerable<T> choices,
        int defaultIndex = 0,
        Func<T, string>? displayFormatter = null,
        float itemMaxWidth = 10f,
        int updateIntervalMs = 120,
        int pauseIntervalMs = 1000 ) : base()
    {
        var formatter = displayFormatter ?? (item => item?.ToString() ?? string.Empty);
        var choicesList = choices.ToList();
        this.Choices = choicesList.AsReadOnly();
        this.formattedChoices = choicesList.Select(c => formatter(c)).ToList().AsReadOnly();

        if (this.Choices.Count == 0)
        {
            Spectre.Console.AnsiConsole.WriteException(new ArgumentException("Choices cannot be empty.", nameof(choices)));
        }

        this.defaultIndex = Math.Clamp(defaultIndex, 0, Math.Max(0, this.Choices.Count - 1));
        this.itemMaxWidth = Math.Max(1f, itemMaxWidth);
        this.updateIntervalMs = Math.Max(15, updateIntervalMs);
        this.pauseIntervalMs = Math.Max(15, pauseIntervalMs);

        var processors = new TextStyleProcessor?[this.formattedChoices.Count];
        for (var i = 0; i < this.formattedChoices.Count; i++)
        {
            if (Helper.EstimateTextWidth(this.formattedChoices[i]) > itemMaxWidth)
            {
                processors[i] = new TextStyleProcessor();
            }
        }
        this.textProcessors = processors.ToList().AsReadOnly();
        this.dynamicTexts = new string?[this.formattedChoices.Count];
        this.lastUpdateTimes = new DateTime[this.formattedChoices.Count];
        this.pauseEndTimes = new DateTime[this.formattedChoices.Count];

        PlaySound = true;
        isPaused = false;
        selectedIndices.Clear();

        InputClaimInfo = new MenuInputClaimInfo {
            Claims = MenuInputClaim.Exit | MenuInputClaim.Use,
            ExitLabel = "L",
            UseLabel = "R"
        };
    }

    /// <summary>
    /// Creates an instance of <see cref="SelectorMenuOption{T}"/>.
    /// </summary>
    /// <param name="text">The label text to display.</param>
    /// <param name="choices">The list of available choices.</param>
    /// <param name="defaultIndex">The default selected index. Defaults to 0.</param>
    /// <param name="displayFormatter">A function to format each choice for display. Defaults to ToString().</param>
    /// <param name="itemMaxWidth">The maximum width for each choice item. Defaults to 10.</param>
    /// <param name="updateIntervalMs">The interval in milliseconds between text animation updates. Defaults to 120ms.</param>
    /// <param name="pauseIntervalMs">The pause duration in milliseconds before starting the next animation cycle. Defaults to 1000ms.</param>
    public SelectorMenuOption(
        string text,
        IEnumerable<T> choices,
        int defaultIndex = 0,
        Func<T, string>? displayFormatter = null,
        float itemMaxWidth = 10f,
        int updateIntervalMs = 120,
        int pauseIntervalMs = 1000 ) : this(choices, defaultIndex, displayFormatter, itemMaxWidth, updateIntervalMs, pauseIntervalMs)
    {
        Text = text;
    }

    public override void Dispose()
    {
        foreach (var processor in textProcessors)
        {
            processor?.Dispose();
        }
        selectedIndices.Clear();
        base.Dispose();
    }

    public override void PauseTextAnimation()
    {
        base.PauseTextAnimation();
        isPaused = true;
    }

    public override void ResumeTextAnimation()
    {
        base.ResumeTextAnimation();
        isPaused = false;
    }

    public override string GetDisplayText( IPlayer player, int displayLine = 0 )
    {
        if (Choices.Count == 0)
        {
            return $"<font color='#666666'>[Empty]</font>";
        }

        var currentIndex = selectedIndices.GetOrAdd(player.PlayerID, defaultIndex);

        // Calculate prev/next indices with wrap-around
        var prevIndex = (((currentIndex - 1) % Choices.Count) + Choices.Count) % Choices.Count;
        var nextIndex = (currentIndex + 1) % Choices.Count;

        // Get displayed texts
        var prevText = GetStyledText(prevIndex);
        var currentText = GetStyledText(currentIndex);
        var nextText = GetStyledText(nextIndex);

        // Format: Label: prev [current] next
        var disabledColor = Menu?.Configuration.DisabledColor ?? "#666666";
        var selector = Choices.Count == 1
            ? $"<font color='#FFFFFF'>[{currentText}]</font>"
            : $"<font color='{disabledColor}'>{prevText}</font> <font color='#FFFFFF'>[{currentText}]</font> <font color='{disabledColor}'>{nextText}</font>";

        return selector;
    }

    /// <summary>
    /// Gets the currently selected index for the specified player.
    /// </summary>
    /// <param name="player">The player whose selection to retrieve.</param>
    /// <returns>The selected index.</returns>
    public int GetSelectedIndex( IPlayer player )
    {
        return selectedIndices.GetOrAdd(player.PlayerID, defaultIndex);
    }

    /// <summary>
    /// Gets the currently selected choice for the specified player.
    /// </summary>
    /// <param name="player">The player whose selection to retrieve.</param>
    /// <returns>The selected choice, or default if no choices available.</returns>
    public T? GetSelectedChoice( IPlayer player )
    {
        if (Choices.Count == 0)
        {
            return default;
        }

        var index = selectedIndices.GetOrAdd(player.PlayerID, defaultIndex);
        return Choices[index];
    }

    /// <summary>
    /// Sets the selected index for the specified player.
    /// </summary>
    /// <param name="player">The player whose selection to set.</param>
    /// <param name="index">The index to select. Will be clamped to valid range.</param>
    public void SetSelectedIndex( IPlayer player, int index )
    {
        if (Choices.Count == 0)
        {
            return;
        }

        var clampedIndex = Math.Clamp(index, 0, Choices.Count - 1);
        _ = selectedIndices.AddOrUpdate(player.PlayerID, clampedIndex, ( _, _ ) => clampedIndex);
    }

    internal override void UpdateCustomAnimations( DateTime now )
    {
        if (isPaused)
        {
            return;
        }

        for (var i = 0; i < formattedChoices.Count; i++)
        {
            var processor = textProcessors[i];
            if (processor == null)
            {
                continue;
            }

            if (now < pauseEndTimes[i])
            {
                continue;
            }

            if (lastUpdateTimes[i] != DateTime.MinValue && (now - lastUpdateTimes[i]).TotalMilliseconds < updateIntervalMs)
            {
                continue;
            }

            var (styledText, offset) = processor.ApplyHorizontalStyle(
                formattedChoices[i],
                TextStyle,
                itemMaxWidth
            );

            dynamicTexts[i] = styledText;
            lastUpdateTimes[i] = now;

            if (offset == 0)
            {
                pauseEndTimes[i] = now.AddMilliseconds(pauseIntervalMs);
            }
        }
    }

    internal override void OnClaimedExit( IPlayer player )
    {
        Navigate(player, -1);
    }

    internal override void OnClaimedUse( IPlayer player )
    {
        Navigate(player, 1);
    }

    private string GetStyledText( int index )
    {
        if (index < 0 || index >= formattedChoices.Count)
        {
            return string.Empty;
        }

        var dynamic = dynamicTexts[index];
        if (!string.IsNullOrEmpty(dynamic))
        {
            return dynamic;
        }

        var processor = textProcessors[index];
        if (processor == null)
        {
            return formattedChoices[index];
        }

        var (truncated, _) = processor.ApplyHorizontalStyle(
            formattedChoices[index],
            MenuOptionTextStyle.TruncateEnd,
            itemMaxWidth
        );
        return truncated;
    }

    private void Navigate( IPlayer player, int direction )
    {
        if (Choices.Count == 0)
        {
            return;
        }

        var oldIndex = selectedIndices.GetOrAdd(player.PlayerID, defaultIndex);
        var newIndex = WrapAround
            ? (((oldIndex + direction) % Choices.Count) + Choices.Count) % Choices.Count
            : Math.Clamp(oldIndex + direction, 0, Choices.Count - 1);

        if (newIndex == oldIndex)
        {
            return;
        }

        _ = selectedIndices.AddOrUpdate(player.PlayerID, newIndex, ( _, _ ) => newIndex);

        try
        {
            SelectionChanged?.Invoke(this, new MenuOptionValueChangedEventArgs<T> {
                Player = player,
                Option = this,
                OldValue = Choices[oldIndex],
                NewValue = Choices[newIndex]
            });
        }
        catch (Exception e)
        {
            if (!GlobalExceptionHandler.Handle(e)) return;
            AnsiConsole.WriteException(e);
        }
    }
}