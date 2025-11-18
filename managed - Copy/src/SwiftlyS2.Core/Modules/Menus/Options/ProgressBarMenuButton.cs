// using SwiftlyS2.Shared.Players;
// using SwiftlyS2.Shared.Menus;
// using SwiftlyS2.Core.Menus;

// namespace SwiftlyS2.Core.Menu.Options;

// [Obsolete("ProgressBarMenuOption will be deprecared at the release of SwiftlyS2.")]
// internal class ProgressBarMenuOption( string text, Func<float> progressProvider, int barWidth = 20, IMenuTextSize size = IMenuTextSize.Medium, MenuHorizontalStyle? overflowStyle = null ) : IOption
// {
//     public string Text { get; set; } = text;
//     public Func<float> ProgressProvider { get; set; } = progressProvider;
//     public int BarWidth { get; set; } = barWidth;
//     public string FilledChar { get; set; } = "█";
//     public string EmptyChar { get; set; } = "░";
//     public bool ShowPercentage { get; set; } = true;
//     public IMenuTextSize Size { get; set; } = size;
//     public IMenu? Menu { get; set; }
//     public MenuHorizontalStyle? OverflowStyle { get; init; } = overflowStyle;

//     public bool Visible => true;
//     public bool Enabled => false;

//     public bool ShouldShow( IPlayer player ) => true;
//     public bool CanInteract( IPlayer player ) => false;
//     public bool HasSound() => false;

//     public string GetDisplayText( IPlayer player, bool updateHorizontalStyle = false )
//     {
//         var sizeClass = MenuSizeHelper.GetSizeClass(Size);

//         var progress = Math.Clamp(ProgressProvider(), 0f, 1f);
//         var filledCount = (int)(progress * BarWidth);
//         var emptyCount = BarWidth - filledCount;

//         var bar = "";
//         for (int i = 0; i < filledCount; i++)
//             bar += $"<font color='{Menu!.RenderColor.ToHex(true)}'>{FilledChar}</font>";
//         for (int i = 0; i < emptyCount; i++)
//             bar += $"<font color='#666666'>{EmptyChar}</font>";

//         var percentage = ShowPercentage ? $" {(int)(progress * 100)}%" : "";
//         return $"<font class='{sizeClass}'>{((Menu as Menus.Menu)?.ApplyHorizontalStyle(Text, OverflowStyle, updateHorizontalStyle) ?? Text)}: {bar}{percentage}</font>";
//     }

//     public IMenuTextSize GetTextSize()
//     {
//         return Size;
//     }
// }