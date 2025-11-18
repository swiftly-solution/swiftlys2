// using SwiftlyS2.Core.Menus;
// using SwiftlyS2.Shared.Menus;
// using SwiftlyS2.Shared.Players;
// using SwiftlyS2.Shared.SchemaDefinitions;

// namespace SwiftlyS2.Core.Menu.Options;

// [Obsolete("ChoiceMenuOption will be deprecared at the release of SwiftlyS2.")]
// internal class ChoiceMenuOption : IOption
// {
//     public string Text { get; set; }
//     public List<string> Choices { get; set; }
//     public int SelectedIndex { get; set; }
//     public Action<IPlayer, string>? OnChange { get; set; }
//     public Action<IPlayer, IOption, string>? OnChangeWithOption { get; set; }
//     public Func<IPlayer, bool>? VisibilityCheck { get; set; }
//     public Func<IPlayer, bool>? EnabledCheck { get; set; }
//     public IMenuTextSize Size { get; set; }
//     public IMenu? Menu { get; set; }
//     public MenuHorizontalStyle? OverflowStyle { get; init; }

//     public bool Visible => true;
//     public bool Enabled => true;

//     public string SelectedChoice => Choices.Count > 0 ? Choices[SelectedIndex] : "";

//     public ChoiceMenuOption( string text, IEnumerable<string> choices, string? defaultChoice = null, Action<IPlayer, string>? onChange = null, IMenuTextSize size = IMenuTextSize.Medium, MenuHorizontalStyle? overflowStyle = null )
//     {
//         Text = text;
//         Choices = [.. choices];
//         SelectedIndex = 0;
//         Size = size;

//         if (defaultChoice != null)
//         {
//             var index = Choices.IndexOf(defaultChoice);
//             if (index >= 0) SelectedIndex = index;
//         }

//         OnChange = onChange;
//         OverflowStyle = overflowStyle;
//     }

//     public ChoiceMenuOption( string text, IEnumerable<string> choices, string? defaultChoice, Action<IPlayer, IOption, string>? onChange, IMenuTextSize size = IMenuTextSize.Medium, MenuHorizontalStyle? overflowStyle = null )
//     {
//         Text = text;
//         Choices = [.. choices];
//         SelectedIndex = 0;
//         Size = size;

//         if (defaultChoice != null)
//         {
//             var index = Choices.IndexOf(defaultChoice);
//             if (index >= 0) SelectedIndex = index;
//         }

//         OnChangeWithOption = onChange;
//         OverflowStyle = overflowStyle;
//     }

//     public bool ShouldShow( IPlayer player ) => VisibilityCheck?.Invoke(player) ?? true;

//     public bool CanInteract( IPlayer player ) => EnabledCheck?.Invoke(player) ?? true;

//     public bool HasSound() => true;

//     public string GetDisplayText( IPlayer player, bool updateHorizontalStyle = false )
//     {
//         var sizeClass = MenuSizeHelper.GetSizeClass(Size);

//         var choice = $"<font color='{Menu!.RenderColor.ToHex(true)}'>[</font>{SelectedChoice}<font color='{Menu.RenderColor.ToHex(true)}'>]</font>";

//         var text = (Menu as Menus.Menu)?.ApplyHorizontalStyle(Text, OverflowStyle, updateHorizontalStyle) ?? Text;
//         if (!CanInteract(player))
//         {
//             return $"<font class='{sizeClass}' color='grey'>{text}: {choice}</font>";
//         }
//         return $"<font class='{sizeClass}'>{text}: {choice}</font>";
//     }

//     public IMenuTextSize GetTextSize()
//     {
//         return Size;
//     }

//     public void Next( IPlayer player )
//     {
//         if (!CanInteract(player) || Choices.Count == 0)
//         {
//             return;
//         }
//         SelectedIndex = (SelectedIndex + 1) % Choices.Count;
//         OnChange?.Invoke(player, SelectedChoice);
//         OnChangeWithOption?.Invoke(player, this, SelectedChoice);
//     }
//     public void Previous( IPlayer player )
//     {
//         if (!CanInteract(player) || Choices.Count == 0)
//         {
//             return;
//         }
//         SelectedIndex = (SelectedIndex - 1 + Choices.Count) % Choices.Count;
//         OnChange?.Invoke(player, SelectedChoice);
//         OnChangeWithOption?.Invoke(player, this, SelectedChoice);
//     }
// }