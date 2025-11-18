namespace SwiftlyS2.Shared;

/// <summary>
/// Provides utility methods for generating HTML text with gradient color effects.
/// </summary>
public static class HtmlGradient
{
    /// <summary>
    /// Generates gradient colored text by interpolating between two colors.
    /// </summary>
    /// <param name="text">The text to apply gradient to.</param>
    /// <param name="startColor">The starting color in hex format (e.g., "#FF0000").</param>
    /// <param name="endColor">The ending color in hex format (e.g., "#0000FF").</param>
    /// <returns>HTML string with each character wrapped in a colored font tag.</returns>
    public static string GenerateGradientText( string text, string startColor, string endColor )
    {
        if (string.IsNullOrWhiteSpace(text))
        {
            return text;
        }

        var (startR, startG, startB) = ParseHexColor(startColor);
        var (endR, endG, endB) = ParseHexColor(endColor);
        var length = text.Length;

        return string.Concat(text.Select(( ch, i ) =>
        {
            var ratio = length > 1 ? (float)i / (length - 1) : 0f;
            var r = (int)(startR + ((endR - startR) * ratio));
            var g = (int)(startG + ((endG - startG) * ratio));
            var b = (int)(startB + ((endB - startB) * ratio));
            return $"<font color='#{r:X2}{g:X2}{b:X2}'>{ch}</font>";
        }));
    }

    /// <summary>
    /// Generates gradient colored text by interpolating across multiple color stops.
    /// </summary>
    /// <param name="text">The text to apply gradient to.</param>
    /// <param name="colors">Array of color stops in hex format (e.g., "#FF0000", "#00FF00", "#0000FF").</param>
    /// <returns>HTML string with each character wrapped in a colored font tag.</returns>
    public static string GenerateGradientText( string text, params string[] colors ) => (text, colors) switch {
        (null or "", _) => string.Empty,
        (_, []) => text,
        (_, [var single]) => $"<font color='{single}'>{text}</font>",
        _ => GenerateMultiColorGradient(text, colors)
    };

    /// <summary>
    /// Generates gradient colored text by interpolating between two SwiftlyS2 native colors.
    /// </summary>
    /// <param name="text">The text to apply gradient to.</param>
    /// <param name="startColor">The starting SwiftlyS2 native color.</param>
    /// <param name="endColor">The ending SwiftlyS2 native color.</param>
    /// <returns>HTML string with each character wrapped in a colored font tag.</returns>
    public static string GenerateGradientText( string text, Natives.Color startColor, Natives.Color endColor )
    {
        return GenerateGradientText(text, startColor.ToHex(), endColor.ToHex());
    }

    /// <summary>
    /// Generates gradient colored text by interpolating between two System.Drawing colors.
    /// </summary>
    /// <param name="text">The text to apply gradient to.</param>
    /// <param name="startColor">The starting System.Drawing color.</param>
    /// <param name="endColor">The ending System.Drawing color.</param>
    /// <returns>HTML string with each character wrapped in a colored font tag.</returns>
    public static string GenerateGradientText( string text, System.Drawing.Color startColor, System.Drawing.Color endColor )
    {
        return GenerateGradientText(text, $"#{startColor.R:X2}{startColor.G:X2}{startColor.B:X2}", $"#{endColor.R:X2}{endColor.G:X2}{endColor.B:X2}");
    }

    /// <summary>
    /// Generates gradient colored text by interpolating across multiple SwiftlyS2 native color stops.
    /// </summary>
    /// <param name="text">The text to apply gradient to.</param>
    /// <param name="colors">Array of SwiftlyS2 native color stops.</param>
    /// <returns>HTML string with each character wrapped in a colored font tag.</returns>
    public static string GenerateGradientText( string text, params Natives.Color[] colors )
    {
        return GenerateGradientText(text, colors.Select(c => c.ToHex()).ToArray());
    }

    /// <summary>
    /// Generates gradient colored text by interpolating across multiple System.Drawing color stops.
    /// </summary>
    /// <param name="text">The text to apply gradient to.</param>
    /// <param name="colors">Array of System.Drawing color stops.</param>
    /// <returns>HTML string with each character wrapped in a colored font tag.</returns>
    public static string GenerateGradientText( string text, params System.Drawing.Color[] colors )
    {
        return GenerateGradientText(text, colors.Select(c => $"#{c.R:X2}{c.G:X2}{c.B:X2}").ToArray());
    }

    private static string GenerateMultiColorGradient( string text, string[] colors )
    {
        var parsedColors = colors.Select(ParseHexColor).ToArray();
        var length = text.Length;

        return string.Concat(text.Select(( ch, i ) =>
        {
            var position = length > 1 ? (float)i / (length - 1) : 0f;
            var segmentIndex = position * (parsedColors.Length - 1);
            var startIdx = (int)Math.Floor(segmentIndex);
            var endIdx = Math.Min(startIdx + 1, parsedColors.Length - 1);
            var ratio = segmentIndex - startIdx;

            var (startR, startG, startB) = parsedColors[startIdx];
            var (endR, endG, endB) = parsedColors[endIdx];

            var r = (int)(startR + (endR - startR) * ratio);
            var g = (int)(startG + (endG - startG) * ratio);
            var b = (int)(startB + (endB - startB) * ratio);

            return $"<font color='#{r:X2}{g:X2}{b:X2}'>{ch}</font>";
        }));
    }

    private static (int R, int G, int B) ParseHexColor( string hex ) =>
        Helper.ParseHexColor(hex) is var (r, g, b, _) ? (r ?? 255, g ?? 255, b ?? 255) : (255, 255, 255);
}