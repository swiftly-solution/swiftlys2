namespace SwiftlyS2.Shared.Menu;

/// <summary>
/// Relative text sizes a renderer maps onto its own output.
/// </summary>
public enum MenuTextSize
{
    /// <summary>Extra small.</summary>
    ExtraSmall,

    /// <summary>Small.</summary>
    Small,

    /// <summary>Between small and medium.</summary>
    SmallMedium,

    /// <summary>Medium. The default.</summary>
    Medium,

    /// <summary>Between medium and large.</summary>
    MediumLarge,

    /// <summary>Large.</summary>
    Large,

    /// <summary>Extra large.</summary>
    ExtraLarge
}

/// <summary>
/// Renderer-agnostic presentation hints for a run of text.
/// </summary>
/// <remarks>
/// These are hints, not commands. A renderer honours what its output medium supports and ignores
/// the rest, which is what lets one component render on any renderer.
/// </remarks>
public readonly record struct MenuTextStyle
{
    /// <summary>
    /// The text colour as a hex string such as <c>"#FFFFFF"</c>, or null to inherit.
    /// </summary>
    public string? Color { get; init; }

    /// <summary>
    /// The relative text size.
    /// </summary>
    public MenuTextSize Size { get; init; }

    /// <summary>
    /// Whether the text should be emphasised.
    /// </summary>
    public bool Bold { get; init; }

    /// <summary>
    /// The default style: medium size, inherited colour, no emphasis.
    /// </summary>
    public static MenuTextStyle Default => new() { Size = MenuTextSize.Medium };

    /// <summary>
    /// Returns a copy of this style with a different colour.
    /// </summary>
    /// <param name="color">The hex colour to apply, or null to inherit.</param>
    /// <returns>The adjusted style.</returns>
    public MenuTextStyle WithColor( string? color ) => this with { Color = color };

    /// <summary>
    /// Returns a copy of this style with a different size.
    /// </summary>
    /// <param name="size">The size to apply.</param>
    /// <returns>The adjusted style.</returns>
    public MenuTextStyle WithSize( MenuTextSize size ) => this with { Size = size };
}
