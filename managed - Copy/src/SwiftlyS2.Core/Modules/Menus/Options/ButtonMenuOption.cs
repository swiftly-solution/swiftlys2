// using SwiftlyS2.Core.Menus;
// using SwiftlyS2.Shared.Menus;
// using SwiftlyS2.Shared.Players;

// namespace SwiftlyS2.Core.Menu.Options;

// [Obsolete("ButtonMenuOption will be deprecared at the release of SwiftlyS2.")]
// internal class ButtonMenuOption : IOption
// {
//     public string Text { get; set; }
//     public Action<IPlayer>? OnClick { get; set; }
//     public Action<IPlayer, IOption>? OnClickWithOption { get; set; }
//     public Func<IPlayer, bool>? VisibilityCheck { get; set; }
//     public Func<IPlayer, bool>? EnabledCheck { get; set; }
//     public Func<IPlayer, bool>? ValidationCheck { get; set; }
//     public Action<IPlayer>? OnValidationFailed { get; set; }
//     public IMenuTextSize Size { get; set; }
//     public bool CloseOnSelect { get; set; }
//     public IMenu? Menu { get; set; }
//     public MenuHorizontalStyle? OverflowStyle { get; init; }

//     public bool Visible => true;
//     public bool Enabled => true;

//     public ButtonMenuOption( string text, Action<IPlayer>? onClick = null, IMenuTextSize size = IMenuTextSize.Medium, MenuHorizontalStyle? overflowStyle = null )
//     {
//         Text = text;
//         OnClick = onClick;
//         Size = size;
//         OverflowStyle = overflowStyle;
//     }

//     public ButtonMenuOption( string text, Action<IPlayer, IOption>? onClick, IMenuTextSize size = IMenuTextSize.Medium, MenuHorizontalStyle? overflowStyle = null )
//     {
//         Text = text;
//         OnClickWithOption = onClick;
//         Size = size;
//         OverflowStyle = overflowStyle;
//     }

//     public bool ShouldShow( IPlayer player ) => VisibilityCheck?.Invoke(player) ?? true;

//     public bool CanInteract( IPlayer player ) => EnabledCheck?.Invoke(player) ?? true;

//     public bool HasSound() => true;

//     public string GetDisplayText( IPlayer player, bool updateHorizontalStyle = false )
//     {
//         var sizeClass = MenuSizeHelper.GetSizeClass(Size);

//         var text = (Menu as Menus.Menu)?.ApplyHorizontalStyle(Text, OverflowStyle, updateHorizontalStyle) ?? Text;
//         if (!CanInteract(player))
//         {
//             return $"<font class='{sizeClass}' color='grey'>{text}</font>";
//         }

//         return $"<font class='{sizeClass}'>{text}</font>";
//     }

//     public IMenuTextSize GetTextSize()
//     {
//         return Size;
//     }

// }