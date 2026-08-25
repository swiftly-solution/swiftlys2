using SwiftlyS2.Shared.Players;

namespace SwiftlyS2.Shared.Menu;

/// <summary>
/// Everything a component needs to draw itself for one player.
/// </summary>
public interface IMenuComponentRenderContext
{
    /// <summary>
    /// The menu being drawn.
    /// </summary>
    public IMenu Menu { get; }

    /// <summary>
    /// The session being drawn.
    /// </summary>
    public IMenuSession Session { get; }

    /// <summary>
    /// The player the menu is being drawn for.
    /// </summary>
    public IPlayer Player { get; }

    /// <summary>
    /// The component being drawn.
    /// </summary>
    public IMenuComponent Component { get; }

    /// <summary>
    /// Whether this component currently holds the selection.
    /// </summary>
    public bool IsFocused { get; }

    /// <summary>
    /// Whether this component is interactive for this player right now.
    /// </summary>
    public bool IsEnabled { get; }

    /// <summary>
    /// The id of the renderer that will consume the produced nodes.
    /// </summary>
    /// <remarks>
    /// Provided so a component can attach a <see cref="MenuRawNode"/> aimed at a renderer it knows
    /// about. Components must still return usable nodes when they do not recognise the id.
    /// </remarks>
    public string RendererId { get; }
}

/// <summary>
/// Everything a renderer needs to draw one composed frame.
/// </summary>
public interface IMenuRenderContext
{
    /// <summary>
    /// The menu being drawn.
    /// </summary>
    public IMenu Menu { get; }

    /// <summary>
    /// The session being drawn.
    /// </summary>
    public IMenuSession Session { get; }

    /// <summary>
    /// The player the menu is being drawn for.
    /// </summary>
    public IPlayer Player { get; }

    /// <summary>
    /// The composed frame to draw.
    /// </summary>
    public MenuFrame Frame { get; }

    /// <summary>
    /// Reports a node the renderer could not draw.
    /// </summary>
    /// <param name="node">The unsupported node.</param>
    /// <remarks>
    /// Reporting is deduplicated per renderer and node type, so calling this on a hot render path
    /// is safe.
    /// </remarks>
    public void ReportUnsupported( MenuNode node );
}
