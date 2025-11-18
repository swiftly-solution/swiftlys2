using System.Globalization;
using SwiftlyS2.Shared.Schemas;

namespace SwiftlyS2.Shared;

public static class Helper
{
    private static readonly Dictionary<string, string> ColorCodes = new()
    {
        { "[default]", "\x01" },
        { "[/]", "\x01" },
        { "[white]", "\x01" },
        { "[darkred]", "\x02" },
        { "[lightpurple]", "\x03" },
        { "[green]", "\x04" },
        { "[olive]", "\x05" },
        { "[lime]", "\x06" },
        { "[red]", "\x07" },
        { "[gray]", "\x08" },
        { "[grey]", "\x08" },
        { "[lightyellow]", "\x09" },
        { "[yellow]", "\x09" },
        { "[silver]", "\x0A" },
        { "[bluegrey]", "\x0A" },
        { "[lightblue]", "\x0B" },
        { "[blue]", "\x0B" },
        { "[darkblue]", "\x0C" },
        { "[purple]", "\x0E" },
        { "[magenta]", "\x0E" },
        { "[lightred]", "\x0F" },
        { "[gold]", "\x10" },
        { "[orange]", "\x10" }
    };

    public static class ChatColors
    {
        public static string Default = "[/]";
        public static string White = "[white]";
        public static string DarkRed = "[darkred]";
        public static string Green = "[green]";
        public static string LightYellow = "[lightyellow]";
        public static string LightBlue = "[lightblue]";
        public static string Olive = "[olive]";
        public static string Lime = "[lime]";
        public static string Red = "[red]";
        public static string LightPurple = "[lightpurple]";
        public static string Purple = "[purple]";
        public static string Grey = "[grey]";
        public static string Yellow = "[yellow]";
        public static string Gold = "[gold]";
        public static string Silver = "[silver]";
        public static string Blue = "[blue]";
        public static string DarkBlue = "[darkblue]";
        public static string BlueGrey = "[bluegrey]";
        public static string Magenta = "[magenta]";
        public static string LightRed = "[lightred]";
        public static string Orange = "[orange]";
    }

    /// <summary>
    /// Replace the color codes in the text with the corresponding color codes.
    /// </summary>
    /// <param name="text">The text to replace the color codes in.</param>
    /// <returns>The text with the color codes replaced.</returns>
    public static string Colored( this string text )
    {
        if (text.StartsWith("["))
        {
            text = " " + text;
        }

        foreach (var color in ColorCodes)
        {
            text = text.Replace(color.Key, color.Value);
        }

        return text;
    }

    /// <summary>
    /// Convert the pointer to the schema class.
    /// </summary>
    /// <typeparam name="T">The schema class to convert to.</typeparam>
    /// <param name="ptr">The pointer to the schema class.</param>
    /// <returns>The schema class.</returns>
    public static T AsSchema<T>( nint ptr ) where T : ISchemaClass<T>
    {
        return T.From(ptr);
    }

    /// <summary>
    /// Estimates the display width of a character based on its type.
    /// Inspired by: https://github.com/spectreconsole/wcwidth
    /// </summary>
    /// <param name="c">The character to measure.</param>
    /// <returns>The estimated display width in relative units.</returns>
    public static float GetCharWidth( char c ) => c switch {
        >= '\u4E00' and <= '\u9FFF' => 2.0f, // CJK Unified Ideographs
        >= '\u3000' and <= '\u303F' => 2.0f, // CJK Symbols and Punctuation
        >= '\uFF00' and <= '\uFFEF' => 2.0f, // Halfwidth and Fullwidth Forms
        >= '\uAC00' and <= '\uD7AF' => 2.2f, // Hangul Syllables
        >= '\u1100' and <= '\u11FF' => 2.2f, // Hangul Jamo
        >= '\u3130' and <= '\u318F' => 2.2f, // Hangul Compatibility Jamo
        >= '\u3040' and <= '\u309F' => 2.05f, // Hiragana
        >= '\u30A0' and <= '\u30FF' => 2.05f, // Katakana
        >= '\u31F0' and <= '\u31FF' => 2.05f, // Katakana Phonetic Extensions
        >= 'A' and <= 'Z' => 1.2f,
        >= 'a' and <= 'z' => 1.0f,
        >= '0' and <= '9' => 1.0f,
        ' ' => 0.5f,
        >= '!' and <= '/' => 0.8f,
        >= ':' and <= '@' => 0.8f,
        >= '[' and <= '`' => 0.8f,
        >= '{' and <= '~' => 0.8f,
        _ => 1.0f
    };

    /// <summary>
    /// Estimates the display width of a text string based on character types.
    /// </summary>
    /// <param name="text">The text string to measure.</param>
    /// <returns>The estimated display width in relative units.</returns>
    public static float EstimateTextWidth( string text ) => text.Sum(GetCharWidth);

    /// <summary>
    /// Parses a hex color string and returns the RGBA color components.
    /// </summary>
    /// <param name="hexColor">The hex color string to parse.</param>
    /// <param name="alphaFirst">Whether alpha component comes first (ARGB format). Default is false (RGBA format).</param>
    /// <returns>Color components (R, G, B, A) or nulls if invalid. Alpha defaults to 255 for RGB format.</returns>
    public static (int? r, int? g, int? b, int? a) ParseHexColor( string hexColor, bool alphaFirst = false )
    {
        if (string.IsNullOrWhiteSpace(hexColor) || !hexColor.StartsWith('#'))
        {
            return (null, null, null, null);
        }

        var hexPart = hexColor[1..];
        if (hexPart.Length == 0 || !hexPart.All(char.IsAsciiHexDigit))
        {
            return (null, null, null, null);
        }

        foreach (var groupCount in new[] { 3, 4 })
        {
            if (hexPart.Length % groupCount != 0)
            {
                continue;
            }

            var groupSize = hexPart.Length / groupCount;
            var groups = Enumerable.Range(0, groupCount)
                .Select(i => hexPart.Substring(i * groupSize, groupSize))
                .Select(g => int.TryParse(g, NumberStyles.HexNumber, null, out var v) && v is >= 0 and <= 255 ? v : (int?)null)
                .ToArray();

            if (groups.All(g => g.HasValue))
            {
                return groupCount switch {
                    3 => (groups[0], groups[1], groups[2], 255),                         // RGB
                    4 when alphaFirst => (groups[1], groups[2], groups[3], groups[0]),   // ARGB → RGBA
                    4 => (groups[0], groups[1], groups[2], groups[3]),                   // RGBA
                    _ => (null, null, null, null)
                };
            }
        }

        return (null, null, null, null);
    }
}