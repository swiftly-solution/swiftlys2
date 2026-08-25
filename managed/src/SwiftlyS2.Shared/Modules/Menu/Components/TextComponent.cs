using SwiftlyS2.Shared.Menu;

namespace SwiftlyS2.Shared.Menu.Components;

/// <summary>
/// A line of text that can be scrolled to but never does anything when activated.
/// </summary>
/// <remarks>
/// The text may be fixed or produced per player through <see cref="TextProvider"/>, which is
/// evaluated on every frame. Text wider than <see cref="MaxWidth"/> is truncated or scrolled
/// according to <see cref="Overflow"/>, independently for every player looking at it.
/// </remarks>
public class TextComponent : MenuComponentBase
{
    private int updateIntervalMs = 120;
    private int pauseIntervalMs = 1000;

    /// <summary>
    /// Creates a text component with fixed text.
    /// </summary>
    /// <param name="text">The text to display.</param>
    /// <param name="id">A stable id, or null to generate one.</param>
    public TextComponent( string text = "", string? id = null ) : base(id)
    {
        Text = text;
    }

    /// <inheritdoc/>
    /// <remarks>
    /// A label can hold the selection like any other body entry, so a menu scrolls past it rather
    /// than skipping over it, but it never overrides <see cref="HandleActionAsync"/> so activating it
    /// is always a no-op.
    /// </remarks>
    public override bool IsFocusable => true;

    /// <summary>
    /// The text shown when <see cref="TextProvider"/> is null or returns null.
    /// </summary>
    public string Text { get; set; }

    /// <summary>
    /// Produces the text per player. Returning null falls back to <see cref="Text"/>.
    /// </summary>
    public Func<IMenuSession, string?>? TextProvider { get; set; }

    /// <summary>
    /// Presentation hints applied to the text.
    /// </summary>
    public MenuTextStyle Style { get; set; } = MenuTextStyle.Default.WithSize(MenuTextSize.SmallMedium).WithColor("#FFFFFF");

    /// <summary>
    /// The colour applied when the component is disabled, or null to keep <see cref="Style"/>.
    /// </summary>
    public string? DisabledColor { get; set; } = "#666666";

    /// <summary>
    /// The width the text is fitted into.
    /// </summary>
    /// <remarks>
    /// Measured with <see cref="Helper.EstimateTextWidth"/>, so it is a weighted width rather than a
    /// character count. Values below one turn fitting off.
    /// </remarks>
    public float MaxWidth { get; set; } = 26f;

    /// <summary>
    /// What happens to text wider than <see cref="MaxWidth"/>.
    /// </summary>
    public MenuTextOverflow Overflow { get; set; } = MenuTextOverflow.TruncateEnd;

    /// <summary>
    /// How often a scrolling animation advances, in milliseconds. Never below 15.
    /// </summary>
    public int UpdateIntervalMs {
        get => updateIntervalMs;
        set => updateIntervalMs = Math.Max(MenuTextLayout.MinimumIntervalMs, value);
    }

    /// <summary>
    /// How long a scrolling animation rests at the end of a cycle, in milliseconds. Never below 15.
    /// </summary>
    public int PauseIntervalMs {
        get => pauseIntervalMs;
        set => pauseIntervalMs = Math.Max(MenuTextLayout.MinimumIntervalMs, value);
    }

    /// <inheritdoc/>
    public override bool NeedsRedraw( IMenuSession session, DateTime now )
    {
        return MenuTextLayout.NeedsRedraw(
            RawText(session),
            Overflow,
            MaxWidth,
            session.GetState<MenuTextScrollState>(this),
            now,
            UpdateIntervalMs);
    }

    /// <inheritdoc/>
    public override MenuNode Render( IMenuComponentRenderContext context )
    {
        return RenderWaiting(context) ?? new MenuTextNode(ResolveText(context.Session), ResolveStyle(context));
    }

    /// <summary>
    /// Produces the placeholder shown while an activation is still running.
    /// </summary>
    /// <param name="context">The context being drawn.</param>
    /// <returns>The placeholder node, or null when nothing is running.</returns>
    /// <remarks>
    /// Components that build their own node tree should return this first when it is not null, so a
    /// slow callback is visible to the player rather than looking like a dead key.
    /// </remarks>
    protected MenuNode? RenderWaiting( IMenuComponentRenderContext context )
    {
        if (!IsBusy(context.Session))
        {
            return null;
        }

        return new MenuTextNode(WaitingText, WaitingColor is null ? Style : Style.WithColor(WaitingColor));
    }

    /// <summary>
    /// Resolves the text to display for a session, fitted to <see cref="MaxWidth"/>.
    /// </summary>
    /// <param name="session">The session to resolve for.</param>
    /// <returns>The resolved text.</returns>
    protected string ResolveText( IMenuSession session )
    {
        return MenuTextLayout.Apply(
            RawText(session),
            Overflow,
            MaxWidth,
            session.GetState<MenuTextScrollState>(this),
            DateTime.UtcNow,
            UpdateIntervalMs,
            PauseIntervalMs);
    }

    /// <summary>
    /// Resolves the text to display for a session without fitting it.
    /// </summary>
    /// <param name="session">The session to resolve for.</param>
    /// <returns>The unfitted text.</returns>
    protected string RawText( IMenuSession session ) => TextProvider?.Invoke(session) ?? Text;

    /// <summary>
    /// Resolves the style to draw with, accounting for the disabled state.
    /// </summary>
    /// <param name="context">The context being drawn.</param>
    /// <returns>The style to use.</returns>
    protected virtual MenuTextStyle ResolveStyle( IMenuComponentRenderContext context )
    {
        return !context.IsEnabled && DisabledColor is not null
            ? Style.WithColor(DisabledColor)
            : Style;
    }
}
