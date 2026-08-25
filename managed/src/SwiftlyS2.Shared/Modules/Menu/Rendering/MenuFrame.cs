namespace SwiftlyS2.Shared.Menu;

/// <summary>
/// One fully composed, ready to draw snapshot of a menu for a single player.
/// </summary>
/// <remarks>
/// The frame is the boundary between menu logic and rendering. By the time a renderer sees it,
/// component selection, focus and paging have already been resolved, so a renderer only has to
/// turn nodes into output.
/// </remarks>
public sealed record MenuFrame
{
    /// <summary>
    /// Pinned nodes drawn above the body.
    /// </summary>
    public required IReadOnlyList<MenuNode> Header { get; init; }

    /// <summary>
    /// The scrollable nodes, already reduced to the visible page.
    /// </summary>
    public required IReadOnlyList<MenuNode> Body { get; init; }

    /// <summary>
    /// Pinned nodes drawn below the body.
    /// </summary>
    public required IReadOnlyList<MenuNode> Footer { get; init; }

    /// <summary>
    /// The index of the first visible body entry within the full component list.
    /// </summary>
    public required int PageOffset { get; init; }

    /// <summary>
    /// The total number of focusable body entries across all pages.
    /// </summary>
    public required int TotalItems { get; init; }
}
