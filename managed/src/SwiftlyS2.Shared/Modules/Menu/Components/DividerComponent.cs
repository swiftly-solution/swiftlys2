using SwiftlyS2.Shared.Menu;

namespace SwiftlyS2.Shared.Menu.Components;

/// <summary>
/// A non-interactive horizontal rule, drawn from repeated <c>─</c> characters.
/// </summary>
public class DividerComponent : MenuComponentBase
{
    private const char DividerChar = '─';

    private int length = 26;

    /// <summary>
    /// Creates a divider.
    /// </summary>
    /// <param name="length">How many <c>─</c> characters to draw. Values below one are treated as one.</param>
    /// <param name="color">The colour of the line, or null to inherit.</param>
    /// <param name="id">A stable id, or null to generate one.</param>
    public DividerComponent( int length = 26, string? color = "#FFFFFF", string? id = null ) : base(id)
    {
        Length = length;
        Color = color;
    }

    /// <summary>
    /// How many <c>─</c> characters this divider draws. Never less than one.
    /// </summary>
    public int Length {
        get => length;
        set => length = Math.Max(1, value);
    }

    /// <summary>
    /// The colour of the line, or null to inherit the renderer's default text colour.
    /// </summary>
    public string? Color { get; set; }

    /// <inheritdoc/>
    public override MenuNode Render( IMenuComponentRenderContext context )
    {
        var style = MenuTextStyle.Default.WithSize(MenuTextSize.Small).WithColor(Color);
        return new MenuTextNode(new string(DividerChar, Length), style);
    }
}
