using SwiftlyS2.Shared;

namespace SwiftlyS2.Shared.Menu.Components;

/// <summary>
/// Best-fit hex equivalents for <c>Helper.ChatColors</c>' tags, for components (like
/// <see cref="ChatColoredTextComponent"/>) that want to degrade gracefully on a hex-based renderer
/// instead of dropping colour entirely.
/// </summary>
/// <remarks>
/// The engine never exposes the exact RGB behind a chat colour code, so these are approximations
/// picked to match what the name suggests, not a verified in-game sample.
/// </remarks>
internal static class ChatColorPalette
{
    private static readonly Dictionary<string, string> HexByTag = new(StringComparer.OrdinalIgnoreCase) {
        [Helper.ChatColors.Default] = "#FFFFFF",
        [Helper.ChatColors.White] = "#FFFFFF",
        [Helper.ChatColors.DarkRed] = "#8B0000",
        [Helper.ChatColors.LightPurple] = "#D8A0D8",
        [Helper.ChatColors.Green] = "#008000",
        [Helper.ChatColors.Olive] = "#808000",
        [Helper.ChatColors.Lime] = "#00FF00",
        [Helper.ChatColors.Red] = "#FF0000",
        [Helper.ChatColors.Grey] = "#808080",
        [Helper.ChatColors.LightYellow] = "#FFFFE0",
        [Helper.ChatColors.Yellow] = "#FFD700",
        [Helper.ChatColors.Silver] = "#C0C0C0",
        [Helper.ChatColors.BlueGrey] = "#6699CC",
        [Helper.ChatColors.LightBlue] = "#ADD8E6",
        [Helper.ChatColors.Blue] = "#0000FF",
        [Helper.ChatColors.DarkBlue] = "#00008B",
        [Helper.ChatColors.Purple] = "#800080",
        [Helper.ChatColors.Magenta] = "#FF00FF",
        [Helper.ChatColors.LightRed] = "#FF6666",
        [Helper.ChatColors.Gold] = "#FFD700",
        [Helper.ChatColors.Orange] = "#FFA500",
    };

    /// <summary>
    /// Finds the best-fit hex colour for a <c>Helper.ChatColors</c> tag.
    /// </summary>
    /// <param name="chatColor">A tag from <see cref="Helper.ChatColors"/>, e.g. <c>"[green]"</c>.</param>
    /// <param name="hex">The matching hex colour, when recognised.</param>
    /// <returns><see langword="true"/> when the tag was recognised.</returns>
    public static bool TryGetHex( string chatColor, out string hex ) => HexByTag.TryGetValue(chatColor, out hex!);
}
