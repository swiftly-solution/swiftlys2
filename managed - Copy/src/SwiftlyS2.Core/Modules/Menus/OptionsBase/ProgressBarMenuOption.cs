using System.Collections.Concurrent;
using SwiftlyS2.Shared.Players;

namespace SwiftlyS2.Core.Menus.OptionsBase;

/// <summary>
/// Represents a progress bar menu option that displays progress visually.
/// </summary>
public sealed class ProgressBarMenuOption : MenuOptionBase
{
    private readonly ConcurrentDictionary<IPlayer, Func<float>> progressProviders = new();
    private readonly Func<float> defaultProgressProvider;
    private readonly bool multiLine;
    private readonly string filledChar;
    private readonly string emptyChar;

    /// <summary>
    /// Gets the width of the progress bar in characters.
    /// </summary>
    public int BarWidth { get; private init; }

    /// <summary>
    /// Gets whether to display the percentage value.
    /// </summary>
    public bool ShowPercentage { get; private init; }

    public override int LineCount => multiLine ? 2 : 1;

    /// <summary>
    /// Creates an instance of <see cref="ProgressBarMenuOption"/>.
    /// </summary>
    /// <param name="progressProvider">Function that returns progress value (0.0 to 1.0).</param>
    /// <param name="multiLine">If true, uses 2 lines; if false, uses 1 line. Defaults to false.</param>
    /// <param name="showPercentage">Whether to show percentage text. Defaults to true.</param>
    /// <param name="filledChar">Character for filled portion. Defaults to "█".</param>
    /// <param name="emptyChar">Character for empty portion. Defaults to "░".</param>
    /// <param name="updateIntervalMs">The interval in milliseconds between text updates. Defaults to 120ms.</param>
    /// <param name="pauseIntervalMs">The pause duration in milliseconds before starting the next text update cycle. Defaults to 1000ms.</param>
    /// <remarks>
    /// When using this constructor, the <see cref="MenuOptionBase.Text"/> property must be manually set to specify the initial text.
    /// </remarks>
    public ProgressBarMenuOption(
        Func<float> progressProvider,
        bool multiLine = false,
        bool showPercentage = true,
        string filledChar = "█",
        string emptyChar = "░",
        int updateIntervalMs = 120,
        int pauseIntervalMs = 1000 ) : base(updateIntervalMs, pauseIntervalMs)
    {
        PlaySound = false;
        this.defaultProgressProvider = progressProvider;
        this.multiLine = multiLine;
        this.BarWidth = multiLine ? 20 : 10;
        this.ShowPercentage = showPercentage;
        this.filledChar = filledChar;
        this.emptyChar = emptyChar;

        progressProviders.Clear();
    }

    /// <summary>
    /// Creates an instance of <see cref="ProgressBarMenuOption"/>.
    /// </summary>
    /// <param name="text">The text content to display.</param>
    /// <param name="progressProvider">Function that returns progress value (0.0 to 1.0).</param>
    /// <param name="multiLine">If true, uses 2 lines; if false, uses 1 line. Defaults to false.</param>
    /// <param name="showPercentage">Whether to show percentage text. Defaults to true.</param>
    /// <param name="filledChar">Character for filled portion. Defaults to "█".</param>
    /// <param name="emptyChar">Character for empty portion. Defaults to "░".</param>
    /// <param name="updateIntervalMs">The interval in milliseconds between text updates. Defaults to 120ms.</param>
    /// <param name="pauseIntervalMs">The pause duration in milliseconds before starting the next text update cycle. Defaults to 1000ms.</param>
    public ProgressBarMenuOption(
        string text,
        Func<float> progressProvider,
        bool multiLine = false,
        bool showPercentage = true,
        string filledChar = "█",
        string emptyChar = "░",
        int updateIntervalMs = 120,
        int pauseIntervalMs = 1000 ) : this(progressProvider, multiLine, showPercentage, filledChar, emptyChar, updateIntervalMs, pauseIntervalMs)
    {
        Text = text;
    }

    public override string GetDisplayText( IPlayer player, int displayLine = 0 )
    {
        var provider = progressProviders.GetOrAdd(player, defaultProgressProvider);
        var progress = Math.Clamp(provider(), 0f, 1f);
        var filledCount = (int)(progress * BarWidth);
        var emptyCount = BarWidth - filledCount;

        var bar = string.Concat(
            Enumerable.Range(0, filledCount).Select(_ => $"<font color='#FFFFFF'>{filledChar}</font>")
                .Concat(Enumerable.Range(0, emptyCount).Select(_ => $"<font color='{Menu?.Configuration.DisabledColor ?? "#666666"}'>{emptyChar}</font>"))
        );

        var progressBar = $"<font color='#FFFFFF'>(</font>{bar}<font color='#FF3333'>)</font>{(ShowPercentage ? $" <font color='#FFFFFF'>{(int)(progress * 100)}%</font>" : string.Empty)}";

        return multiLine
            ? displayLine switch {
                1 => base.GetDisplayText(player, displayLine),
                2 => progressBar,
                _ => $"{base.GetDisplayText(player, displayLine)}:<br>{progressBar}"
            }
            : $"{base.GetDisplayText(player, displayLine)}: {progressBar}";
    }

    /// <summary>
    /// Sets or updates the progress provider function for a specific player.
    /// </summary>
    /// <param name="player">The player whose progress provider to set.</param>
    /// <param name="progressProvider">Function that returns progress value (0.0 to 1.0).</param>
    public void SetProgressProvider( IPlayer player, Func<float> progressProvider )
    {
        _ = progressProviders.AddOrUpdate(player, progressProvider, ( _, _ ) => progressProvider);
    }

    /// <summary>
    /// Gets the current progress value for the specified player.
    /// </summary>
    /// <param name="player">The player whose progress to retrieve.</param>
    /// <returns>The current progress value (0.0 to 1.0).</returns>
    public float GetProgress( IPlayer player )
    {
        var provider = progressProviders.GetOrAdd(player, defaultProgressProvider);
        return Math.Clamp(provider(), 0f, 1f);
    }
}