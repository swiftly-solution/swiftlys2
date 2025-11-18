// using SwiftlyS2.Shared.Players;
// using SwiftlyS2.Shared.Menus;

// namespace SwiftlyS2.Core.Menu.Options;

// [Obsolete("SeparatorMenuOption will be deprecared at the release of SwiftlyS2.")]
// internal class SeparatorMenuOption : IOption
// {
//     public string Text { get; set; }
//     public bool Visible => true;
//     public bool Enabled => false;

//     public IMenu? Menu { get; set; }
//     public MenuHorizontalStyle? OverflowStyle { get; init; }

//     public SeparatorMenuOption()
//     {
//         Text = "─────────────────────";
//     }

//     public bool ShouldShow( IPlayer player ) => true;
//     public bool CanInteract( IPlayer player ) => false;
//     public bool HasSound() => false;

//     public string GetDisplayText( IPlayer player, bool updateHorizontalStyle = false )
//     {
//         return $"<font color='{Menu!.RenderColor.ToHex(true)}'>{Text}</font>";
//     }

//     public IMenuTextSize GetTextSize()
//     {
//         return IMenuTextSize.Small;
//     }
// }