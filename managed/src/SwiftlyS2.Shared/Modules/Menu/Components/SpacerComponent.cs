using SwiftlyS2.Shared.Menu;

namespace SwiftlyS2.Shared.Menu.Components;

/// <summary>
/// Blank vertical space.
/// </summary>
public class SpacerComponent : MenuComponentBase
{
    /// <summary>
    /// Creates a spacer.
    /// </summary>
    /// <param name="lines">How many blank lines to reserve. Values below one are treated as one.</param>
    /// <param name="id">A stable id, or null to generate one.</param>
    public SpacerComponent( int lines = 1, string? id = null ) : base(id)
    {
        Lines = lines;
    }

    /// <summary>
    /// How many blank lines this spacer reserves. Never less than one.
    /// </summary>
    public int Lines {
        get;
        set => field = Math.Max(1, value);
    } = 1;

    /// <inheritdoc/>
    public override int LineCount => Lines;

    /// <inheritdoc/>
    public override MenuNode Render( IMenuComponentRenderContext context ) => new MenuBlankNode(Lines);
}
