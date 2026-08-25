using System.Text;

namespace SwiftlyS2.Shared.Menu;

/// <summary>
/// What a component does with text wider than the space it is given.
/// </summary>
public enum MenuTextOverflow
{
    /// <summary>Leave the text alone and let the renderer deal with it.</summary>
    None,

    /// <summary>Cut the tail off and append an ellipsis.</summary>
    TruncateEnd,

    /// <summary>Keep the middle and drop both ends.</summary>
    TruncateBothEnds,

    /// <summary>Scroll towards the start, stopping once the tail is reached.</summary>
    ScrollLeftFade,

    /// <summary>Scroll towards the end, stopping once the head is reached.</summary>
    ScrollRightFade,

    /// <summary>Scroll towards the start, wrapping around forever.</summary>
    ScrollLeftLoop,

    /// <summary>Scroll towards the end, wrapping around forever.</summary>
    ScrollRightLoop
}

/// <summary>
/// One player's scroll position for one piece of animated text.
/// </summary>
/// <remarks>
/// Held in <see cref="IMenuSession"/> state so a single component instance animates independently
/// for every player looking at it.
/// </remarks>
public sealed class MenuTextScrollState
{
    /// <summary>
    /// How many characters the window has moved.
    /// </summary>
    public int Offset { get; set; }

    /// <summary>
    /// When the offset last advanced.
    /// </summary>
    public DateTime LastUpdate { get; set; }

    /// <summary>
    /// When the pause at the end of a cycle expires.
    /// </summary>
    public DateTime PauseUntil { get; set; }
}

/// <summary>
/// Fits text into a width budget, scrolling it when asked.
/// </summary>
/// <remarks>
/// Widths are measured with <see cref="Helper.EstimateTextWidth"/>, which weighs wide scripts more
/// than latin characters, so a budget is roughly "this many capital letters" rather than a plain
/// character count.
/// </remarks>
public static class MenuTextLayout
{
    /// <summary>
    /// The shortest interval an animation may advance at.
    /// </summary>
    public const int MinimumIntervalMs = 15;

    private const string Ellipsis = "...";

    /// <summary>
    /// Fits text into a width budget, advancing the animation when one is running.
    /// </summary>
    /// <param name="text">The text to fit.</param>
    /// <param name="overflow">What to do when the text does not fit.</param>
    /// <param name="maxWidth">The width budget. Values below one disable fitting.</param>
    /// <param name="state">This player's scroll position. Ignored by the truncating modes.</param>
    /// <param name="now">The current time.</param>
    /// <param name="updateIntervalMs">How often the animation advances.</param>
    /// <param name="pauseIntervalMs">How long to rest at the end of a cycle.</param>
    /// <returns>The text to display.</returns>
    public static string Apply(
        string text,
        MenuTextOverflow overflow,
        float maxWidth,
        MenuTextScrollState state,
        DateTime now,
        int updateIntervalMs = 120,
        int pauseIntervalMs = 1000 )
    {
        if (overflow == MenuTextOverflow.None || maxWidth < 1f || string.IsNullOrEmpty(text))
        {
            return text;
        }

        if (Helper.EstimateTextWidth(text) <= maxWidth)
        {
            state.Offset = 0;
            return text;
        }

        return overflow switch {
            MenuTextOverflow.TruncateEnd => TruncateEnd(text, maxWidth),
            MenuTextOverflow.TruncateBothEnds => TruncateBothEnds(text, maxWidth),
            _ => Scroll(text, overflow, maxWidth, state, now, updateIntervalMs, pauseIntervalMs)
        };
    }

    /// <summary>
    /// Whether the animation would advance if a frame were drawn now.
    /// </summary>
    /// <param name="text">The text being fitted.</param>
    /// <param name="overflow">The overflow mode in use.</param>
    /// <param name="maxWidth">The width budget.</param>
    /// <param name="state">This player's scroll position.</param>
    /// <param name="now">The current time.</param>
    /// <param name="updateIntervalMs">How often the animation advances.</param>
    /// <returns><see langword="true"/> when a redraw would show something different.</returns>
    public static bool NeedsRedraw(
        string text,
        MenuTextOverflow overflow,
        float maxWidth,
        MenuTextScrollState state,
        DateTime now,
        int updateIntervalMs = 120 )
    {
        if (!IsScrolling(overflow) || maxWidth < 1f || string.IsNullOrEmpty(text))
        {
            return false;
        }

        if (Helper.EstimateTextWidth(text) <= maxWidth)
        {
            return false;
        }

        if (now < state.PauseUntil)
        {
            return false;
        }

        return (now - state.LastUpdate).TotalMilliseconds >= Math.Max(MinimumIntervalMs, updateIntervalMs);
    }

    /// <summary>
    /// Whether an overflow mode animates over time.
    /// </summary>
    /// <param name="overflow">The mode to test.</param>
    /// <returns><see langword="true"/> for the scrolling modes.</returns>
    public static bool IsScrolling( MenuTextOverflow overflow )
        => overflow is MenuTextOverflow.ScrollLeftFade
            or MenuTextOverflow.ScrollRightFade
            or MenuTextOverflow.ScrollLeftLoop
            or MenuTextOverflow.ScrollRightLoop;

    private static string Scroll(
        string text,
        MenuTextOverflow overflow,
        float maxWidth,
        MenuTextScrollState state,
        DateTime now,
        int updateIntervalMs,
        int pauseIntervalMs )
    {
        var loops = overflow is MenuTextOverflow.ScrollLeftLoop or MenuTextOverflow.ScrollRightLoop;
        var towardsStart = overflow is MenuTextOverflow.ScrollLeftFade or MenuTextOverflow.ScrollLeftLoop;

        var source = loops
            ? towardsStart ? $"{text.TrimEnd()} " : $" {text.TrimStart()}"
            : text;

        var cycle = loops ? source.Length : source.Length + 1;
        var interval = Math.Max(MinimumIntervalMs, updateIntervalMs);
        var pause = Math.Max(MinimumIntervalMs, pauseIntervalMs);

        if (now >= state.PauseUntil && (now - state.LastUpdate).TotalMilliseconds >= interval)
        {
            state.LastUpdate = now;
            state.Offset = cycle <= 0 ? 0 : (state.Offset + 1) % cycle;

            if (state.Offset == 0)
            {
                state.PauseUntil = now.AddMilliseconds(pause);
            }
        }

        var offset = cycle <= 0 ? 0 : Math.Clamp(state.Offset, 0, cycle - 1);

        return loops
            ? WindowLooping(source, maxWidth, offset, towardsStart)
            : WindowFading(source, maxWidth, offset, towardsStart);
    }

    private static string WindowFading( string text, float maxWidth, int offset, bool towardsStart )
    {
        var window = new StringBuilder();
        var width = 0f;

        if (towardsStart)
        {
            for (var index = offset; index < text.Length; index++)
            {
                var charWidth = Helper.GetCharWidth(text[index]);

                if (width + charWidth > maxWidth)
                {
                    break;
                }

                width += charWidth;
                _ = window.Append(text[index]);
            }

            return window.ToString();
        }

        for (var index = text.Length - offset - 1; index >= 0; index--)
        {
            var charWidth = Helper.GetCharWidth(text[index]);

            if (width + charWidth > maxWidth)
            {
                break;
            }

            width += charWidth;
            _ = window.Insert(0, text[index]);
        }

        return window.ToString();
    }

    private static string WindowLooping( string text, float maxWidth, int offset, bool towardsStart )
    {
        var start = towardsStart ? offset : (text.Length - offset) % text.Length;
        var window = new StringBuilder();
        var width = 0f;

        for (var step = 0; step < text.Length; step++)
        {
            var character = text[(start + step) % text.Length];
            var charWidth = Helper.GetCharWidth(character);

            if (width + charWidth > maxWidth)
            {
                break;
            }

            width += charWidth;
            _ = window.Append(character);
        }

        return window.ToString();
    }

    private static string TruncateEnd( string text, float maxWidth )
    {
        var budget = maxWidth - Helper.EstimateTextWidth(Ellipsis);

        if (budget <= 0f)
        {
            return Ellipsis;
        }

        var kept = new StringBuilder();
        var width = 0f;

        foreach (var character in text)
        {
            var charWidth = Helper.GetCharWidth(character);

            if (width + charWidth > budget)
            {
                break;
            }

            width += charWidth;
            _ = kept.Append(character);
        }

        return $"{kept}{Ellipsis}";
    }

    private static string TruncateBothEnds( string text, float maxWidth )
    {
        var total = Helper.EstimateTextWidth(text);
        var excess = total - maxWidth;

        if (excess <= 0f)
        {
            return text;
        }

        var skip = 0;
        var skipped = 0f;

        while (skip < text.Length && skipped < excess / 2f)
        {
            skipped += Helper.GetCharWidth(text[skip]);
            skip++;
        }

        var kept = new StringBuilder();
        var width = 0f;

        for (var index = skip; index < text.Length; index++)
        {
            var charWidth = Helper.GetCharWidth(text[index]);

            if (width + charWidth > maxWidth)
            {
                break;
            }

            width += charWidth;
            _ = kept.Append(text[index]);
        }

        return kept.ToString();
    }
}
