// using SwiftlyS2.Core.Menus;
// using SwiftlyS2.Shared.Menus;
// using SwiftlyS2.Shared.Players;

// namespace SwiftlyS2.Core.Menu.Options;

// [Obsolete("SubmenuMenuOption will be deprecared at the release of SwiftlyS2.")]
// internal class SubmenuMenuOption : IOption
// {
//     public string Text { get; set; }
//     public IMenu? Submenu { get; set; }
//     public Func<Task<IMenu>>? SubmenuBuilderAsync { get; set; }
//     public Func<IPlayer, bool>? VisibilityCheck { get; set; }
//     public Func<IPlayer, bool>? EnabledCheck { get; set; }
//     public IMenuTextSize Size { get; set; }
//     public IMenu? Menu { get; set; }
//     public MenuHorizontalStyle? OverflowStyle { get; init; }

//     public bool Visible => true;
//     public bool Enabled => true;

//     public SubmenuMenuOption( string text, IMenu? submenu = null, IMenuTextSize size = IMenuTextSize.Medium )
//     {
//         Text = text;
//         Submenu = submenu;
//         Size = size;
//     }

//     public SubmenuMenuOption( string text, Func<IMenu> submenuBuilder, IMenuTextSize size = IMenuTextSize.Medium )
//     {
//         Text = text;
//         SubmenuBuilderAsync = () => Task.FromResult(submenuBuilder());
//         Size = size;
//     }

//     public SubmenuMenuOption( string text, Func<Task<IMenu>> asyncSubmenuBuilder, IMenuTextSize size = IMenuTextSize.Medium )
//     {
//         Text = text;
//         SubmenuBuilderAsync = asyncSubmenuBuilder;
//         Size = size;
//     }

//     public bool ShouldShow( IPlayer player ) => VisibilityCheck?.Invoke(player) ?? true;

//     public bool CanInteract( IPlayer player ) => EnabledCheck?.Invoke(player) ?? true;

//     public bool HasSound() => true;

//     public string GetDisplayText( IPlayer player, bool updateHorizontalStyle = false )
//     {
//         var sizeClass = MenuSizeHelper.GetSizeClass(Size);

//         var arrow = $" <font color='{Menu!.RenderColor.ToHex(true)}' class='{sizeClass}'>{Menu!.MenuManager.Settings.NavigationPrefix}</font>";

//         if (!CanInteract(player))
//         {
//             return $"<font class='{sizeClass}' color='grey'>{Text}{arrow}</font>";
//         }

//         return $"<font class='{sizeClass}'>{Text}{arrow}</font>";
//     }

//     public IMenuTextSize GetTextSize()
//     {
//         return Size;
//     }

//     public async Task<IMenu?> GetSubmenuAsync()
//     {
//         if (Submenu != null)
//         {
//             return Submenu;
//         }

//         if (SubmenuBuilderAsync != null)
//         {
//             return await SubmenuBuilderAsync.Invoke();
//         }

//         return null;
//     }
// }