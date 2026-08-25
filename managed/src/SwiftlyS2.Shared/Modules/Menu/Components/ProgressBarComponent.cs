using SwiftlyS2.Shared.Menu;

namespace SwiftlyS2.Shared.Menu.Components;

/// <summary>
/// A non-interactive bar showing a value that something else owns.
/// </summary>
/// <remarks>
/// The value is pulled from <see cref="ProgressProvider"/> rather than stored, and the component
/// asks for a redraw on its own whenever the value has moved, so a bar tracking live state stays
/// current without the plugin invalidating anything.
/// </remarks>
public class ProgressBarComponent : TextComponent
{
    private int barWidth = 10;

    /// <inheritdoc/>
    public override bool IsFocusable => false;

    /// <summary>
    /// Creates a progress bar.
    /// </summary>
    /// <param name="text">The label to display.</param>
    /// <param name="progressProvider">Produces the progress, from zero to one.</param>
    /// <param name="multiLine">Whether the bar is drawn on its own line below the label.</param>
    /// <param name="id">A stable id, or null to generate one.</param>
    public ProgressBarComponent(
        string text = "",
        Func<IMenuSession, float>? progressProvider = null,
        bool multiLine = false,
        string? id = null ) : base(text, id)
    {
        ProgressProvider = progressProvider;
        MultiLine = multiLine;
        BarWidth = multiLine ? 20 : 10;
    }

    /// <summary>
    /// Produces the progress for a player, from zero to one. Null reads as zero.
    /// </summary>
    public Func<IMenuSession, float>? ProgressProvider { get; set; }

    /// <summary>
    /// Whether the bar is drawn on its own line below the label.
    /// </summary>
    public bool MultiLine { get; set; }

    /// <summary>
    /// How many segments the bar is drawn with. Never below one.
    /// </summary>
    public int BarWidth {
        get => barWidth;
        set => barWidth = Math.Max(1, value);
    }

    /// <summary>
    /// Whether a percentage is shown next to the bar.
    /// </summary>
    public bool ShowPercentage { get; set; } = true;

    /// <summary>
    /// The character drawn for a filled segment.
    /// </summary>
    public string FilledChar { get; set; } = "█";

    /// <summary>
    /// The character drawn for an empty segment.
    /// </summary>
    public string EmptyChar { get; set; } = "░";

    /// <summary>
    /// The colour of the filled segments, or null to keep <see cref="TextComponent.Style"/>.
    /// </summary>
    public string? FilledColor { get; set; } = "#FFFFFF";

    /// <summary>
    /// The colour of the empty segments.
    /// </summary>
    public string? EmptyColor { get; set; } = "#666666";

    /// <summary>
    /// The bracket drawn before the bar.
    /// </summary>
    public string OpeningBracket { get; set; } = "(";

    /// <summary>
    /// The bracket drawn after the bar.
    /// </summary>
    public string ClosingBracket { get; set; } = ")";

    /// <summary>
    /// The colour of the opening bracket, or null to keep <see cref="TextComponent.Style"/>.
    /// </summary>
    public string? OpeningBracketColor { get; set; } = "#FFFFFF";

    /// <summary>
    /// The colour of the closing bracket, or null to keep <see cref="TextComponent.Style"/>.
    /// </summary>
    public string? ClosingBracketColor { get; set; } = "#FF3333";

    /// <inheritdoc/>
    /// <remarks>
    /// Two lines in multi-line mode, since the label and the bar are stacked. A bar with no label is
    /// drawn on one line either way.
    /// </remarks>
    public override int LineCount => MultiLine && (TextProvider is not null || Text.Length > 0) ? 2 : 1;

    /// <summary>
    /// Reads the progress for a player, clamped between zero and one.
    /// </summary>
    /// <param name="session">The session to read for.</param>
    /// <returns>The progress.</returns>
    public float GetProgress( IMenuSession session )
        => ProgressProvider is null ? 0f : Math.Clamp(ProgressProvider(session), 0f, 1f);

    /// <inheritdoc/>
    public override bool NeedsRedraw( IMenuSession session, DateTime now )
    {
        if (base.NeedsRedraw(session, now))
        {
            return true;
        }

        if (ProgressProvider is null)
        {
            return false;
        }

        var state = session.GetState<ProgressState>(this);

        if ((now - state.LastUpdate).TotalMilliseconds < UpdateIntervalMs)
        {
            return false;
        }

        return Segments(GetProgress(session)) != state.Segments;
    }

    /// <inheritdoc/>
    public override MenuNode Render( IMenuComponentRenderContext context )
    {
        var progress = GetProgress(context.Session);
        var filled = Segments(progress);
        var state = context.Session.GetState<ProgressState>(this);

        state.Segments = filled;
        state.LastUpdate = DateTime.UtcNow;

        var bar = new List<MenuNode>(4) { Colored(context, OpeningBracket, OpeningBracketColor) };

        if (filled > 0)
        {
            bar.Add(Colored(context, string.Concat(Enumerable.Repeat(FilledChar, filled)), FilledColor));
        }

        if (filled < BarWidth)
        {
            bar.Add(Colored(context, string.Concat(Enumerable.Repeat(EmptyChar, BarWidth - filled)), EmptyColor));
        }

        bar.Add(Colored(context, ClosingBracket, ClosingBracketColor));

        if (ShowPercentage)
        {
            bar.Add(Colored(context, $" {(int)(progress * 100)}%", FilledColor));
        }

        var label = ResolveText(context.Session);

        if (string.IsNullOrEmpty(label))
        {
            return new MenuLineNode(bar);
        }

        var labelNode = new MenuTextNode(MultiLine ? label : $"{label}: ", ResolveStyle(context));

        return MultiLine
            ? MenuStackNode.Of(labelNode, new MenuLineNode(bar))
            : new MenuLineNode([labelNode, .. bar]);
    }

    private int Segments( float progress ) => Math.Clamp((int)(progress * BarWidth), 0, BarWidth);

    private MenuNode Colored( IMenuComponentRenderContext context, string text, string? color )
    {
        var resolved = context.IsEnabled ? color : DisabledColor;

        return new MenuTextNode(text, resolved is null ? Style : Style.WithColor(resolved));
    }

    private sealed class ProgressState
    {
        public int Segments { get; set; } = -1;

        public DateTime LastUpdate { get; set; }
    }
}
