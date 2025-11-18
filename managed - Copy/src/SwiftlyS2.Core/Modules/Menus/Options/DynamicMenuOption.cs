// using SwiftlyS2.Core.Menus;
// using SwiftlyS2.Shared.Menus;
// using SwiftlyS2.Shared.Players;
// using SwiftlyS2.Shared.SchemaDefinitions;

// namespace SwiftlyS2.Core.Menu.Options;

// [Obsolete("DynamicMenuOption will be deprecared at the release of SwiftlyS2.")]
// internal class DynamicMenuOption : IOption
// {
//     private readonly Func<IPlayer, string> _textProvider;
//     private readonly Action<IPlayer>? _onClick;
//     private Func<IPlayer, bool>? _visibilityCheck;
//     private Func<IPlayer, bool>? _enabledCheck;
//     private Func<IPlayer, bool>? _validationCheck;
//     private Action<IPlayer>? _onValidationFailed;
//     private readonly TimeSpan _updateInterval;
//     private DateTime _lastUpdate = DateTime.MinValue;
//     private string _cachedText = "";
//     private IMenuTextSize _size;
//     private bool _closeOnSelect;
//     public IMenu? Menu { get; set; }
//     public MenuHorizontalStyle? OverflowStyle { get; init; }

//     public string Text {
//         get => _cachedText;
//         set => _cachedText = value;
//     }

//     public bool Visible => true;
//     public bool Enabled => true;

//     public DynamicMenuOption( Func<IPlayer, string> textProvider, TimeSpan updateInterval, Action<IPlayer>? onClick = null, IMenuTextSize size = IMenuTextSize.Medium )
//     {
//         _textProvider = textProvider;
//         _updateInterval = updateInterval;
//         _onClick = onClick;
//         _size = size;
//     }

//     public DynamicMenuOption( Func<string> textProvider, TimeSpan updateInterval, Action<IPlayer>? onClick = null, IMenuTextSize size = IMenuTextSize.Medium )
//     {
//         _textProvider = _ => textProvider();
//         _updateInterval = updateInterval;
//         _onClick = onClick;
//         _size = size;
//     }

//     public bool ShouldShow( IPlayer player ) => _visibilityCheck?.Invoke(player) ?? true;

//     public bool CanInteract( IPlayer player ) => _onClick != null && (_enabledCheck?.Invoke(player) ?? true);

//     public bool HasSound() => false;

//     public string GetDisplayText( IPlayer player, bool updateHorizontalStyle = false )
//     {
//         var sizeClass = MenuSizeHelper.GetSizeClass(_size);

//         var needsUpdate = DateTime.Now - _lastUpdate > _updateInterval;
//         if (needsUpdate)
//         {
//             var oldText = _cachedText;
//             _cachedText = _textProvider(player);
//             _lastUpdate = DateTime.Now;

//             if (oldText != _cachedText && Menu != null)
//             {
//                 Menu.Rerender(player, false);
//             }
//         }

//         if (!CanInteract(player) && _onClick != null)
//         {
//             return $"<font class='{sizeClass}' color='grey'>{_cachedText}</font>";
//         }

//         return $"<font class='{sizeClass}'>{_cachedText}</font>";
//     }

//     public IMenuTextSize GetTextSize()
//     {
//         return _size;
//     }

//     public void Click( IPlayer player )
//     {
//         if (CanInteract(player))
//         {
//             if (_validationCheck != null && !_validationCheck(player))
//             {
//                 _onValidationFailed?.Invoke(player);
//                 return;
//             }
//             _onClick?.Invoke(player);
//         }
//     }

//     public DynamicMenuOption WithVisibilityCheck( Func<IPlayer, bool> check )
//     {
//         _visibilityCheck = check;
//         return this;
//     }

//     public DynamicMenuOption WithEnabledCheck( Func<IPlayer, bool> check )
//     {
//         _enabledCheck = check;
//         return this;
//     }

//     public DynamicMenuOption WithValidation( Func<IPlayer, bool> check, Action<IPlayer>? onFailed = null )
//     {
//         _validationCheck = check;
//         _onValidationFailed = onFailed;
//         return this;
//     }

//     public DynamicMenuOption WithCloseOnSelect( bool close = true )
//     {
//         _closeOnSelect = close;
//         return this;
//     }

//     public bool ShouldCloseOnSelect() => _closeOnSelect;
// }