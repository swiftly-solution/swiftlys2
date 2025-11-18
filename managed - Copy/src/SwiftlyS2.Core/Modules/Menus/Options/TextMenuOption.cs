// using SwiftlyS2.Core.Menus;
// using SwiftlyS2.Shared.Menus;
// using SwiftlyS2.Shared.Players;

// namespace SwiftlyS2.Core.Menu.Options;

// [Obsolete("TextMenuOption will be deprecared at the release of SwiftlyS2.")]
// internal class TextMenuOption : IOption
// {
//     public string Text { get; set; }
//     public ITextAlign Alignment { get; set; }
//     public IMenuTextSize Size { get; set; }
//     public Func<IPlayer, bool>? VisibilityCheck { get; set; }
//     public Func<string>? DynamicText { get; set; }
//     public IMenu? Menu { get; set; }
//     public MenuHorizontalStyle? OverflowStyle { get; init; }

//     public bool Visible => true;
//     public bool Enabled => false;

//     public TextMenuOption( string text, ITextAlign alignment = ITextAlign.Left, IMenuTextSize size = IMenuTextSize.Medium, MenuHorizontalStyle? overflowStyle = null )
//     {
//         Text = text;
//         Alignment = alignment;
//         Size = size;
//         OverflowStyle = overflowStyle;
//     }

//     public TextMenuOption( Func<string> dynamicText, ITextAlign alignment = ITextAlign.Left, IMenuTextSize size = IMenuTextSize.Medium, MenuHorizontalStyle? overflowStyle = null )
//     {
//         Text = string.Empty;
//         DynamicText = dynamicText;
//         Alignment = alignment;
//         Size = size;
//         OverflowStyle = overflowStyle;
//     }

//     public bool ShouldShow( IPlayer player ) => VisibilityCheck?.Invoke(player) ?? true;

//     public bool CanInteract( IPlayer player ) => true;

//     public bool HasSound() => false;

//     public string GetDisplayText( IPlayer player, bool updateHorizontalStyle )
//     {
//         var text = DynamicText?.Invoke() ?? Text;


//         text = (Menu as Menus.Menu)?.ApplyHorizontalStyle(text, OverflowStyle, updateHorizontalStyle) ?? text;

//         var sizeClass = MenuSizeHelper.GetSizeClass(Size);

//         text = $"<font class='{sizeClass}'>{text}</font>";

//         return Alignment switch {
//             ITextAlign.Center => $"<center>{text}</center>",
//             ITextAlign.Right => $"<div align='right'>{text}</div>",
//             _ => text
//         };
//     }

//     public IMenuTextSize GetTextSize()
//     {
//         return Size;
//     }
// }

// internal enum TextAlign
// {
//     Left,
//     Center,
//     Right
// }