using SwiftlyS2.Shared.Menu;

namespace SwiftlyS2.Shared.Menu.Components;

/// <summary>
/// A non-interactive line identifying the menu, drawn at the top of the header.
/// </summary>
/// <remarks>
/// Semantically distinct from <see cref="TextComponent"/> so a menu's title is a real component
/// rather than a builder side effect, even though it renders through the same node types.
/// </remarks>
public class TitleComponent : TextComponent
{
    /// <summary>
    /// Creates a title.
    /// </summary>
    /// <param name="title">The title text.</param>
    /// <param name="id">A stable id, or null to generate one.</param>
    public TitleComponent( string title = "", string? id = null ) : base(title, id)
    {
        Style = MenuTextStyle.Default.WithSize(MenuTextSize.Medium).WithColor("#FFFFFF");
    }

    /// <inheritdoc/>
    /// <remarks>
    /// A title normally lives in the header, which is never scanned for focus anyway, but this stays
    /// explicit in case one is ever added to the body directly.
    /// </remarks>
    public override bool IsFocusable => false;
}
