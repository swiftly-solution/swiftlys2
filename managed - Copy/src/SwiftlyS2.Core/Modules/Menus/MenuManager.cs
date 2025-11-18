// using System.Collections.Concurrent;
// using System.Globalization;
// using SwiftlyS2.Core.Menu.Options;
// using SwiftlyS2.Core.Natives;
// using SwiftlyS2.Shared;
// using SwiftlyS2.Shared.Events;
// using SwiftlyS2.Shared.Menus;
// using SwiftlyS2.Shared.Players;
// using SwiftlyS2.Shared.Sounds;

// namespace SwiftlyS2.Core.Menus;

// [Obsolete("MenuManager will be deprecared at the release of SwiftlyS2. Please use MenuManagerAPI instead")]
// internal class MenuManager : IMenuManager
// {
//     public MenuSettings Settings { get; set; } = new MenuSettings();

//     public event Action<IPlayer, IMenu>? OnMenuClosed;
//     public event Action<IPlayer, IMenu>? OnMenuOpened;
//     public event Action<IPlayer, IMenu>? OnMenuRendered;

//     private static readonly Dictionary<string, KeyKind> StringToKeyKind = new()
//     {
//         { "mouse1", KeyKind.Mouse1 },
//         { "mouse2", KeyKind.Mouse2 },
//         { "space", KeyKind.Space },
//         { "ctrl", KeyKind.Ctrl },
//         { "w", KeyKind.W },
//         { "a", KeyKind.A },
//         { "s", KeyKind.S },
//         { "d", KeyKind.D },
//         { "e", KeyKind.E },
//         { "esc", KeyKind.Esc },
//         { "r", KeyKind.R },
//         { "alt", KeyKind.Alt },
//         { "shift", KeyKind.Shift },
//         { "weapon1", KeyKind.Weapon1 },
//         { "weapon2", KeyKind.Weapon2 },
//         { "grenade1", KeyKind.Grenade1 },
//         { "grenade2", KeyKind.Grenade2 },
//         { "tab", KeyKind.Tab },
//         { "f", KeyKind.F },
//     };

//     private ConcurrentDictionary<IPlayer, IMenu> OpenMenus { get; set; } = new();
//     private ISwiftlyCore _Core { get; set; }

//     private SoundEvent _useSound = new();
//     private SoundEvent _exitSound = new();
//     private SoundEvent _scrollSound = new();

//     public MenuManager( ISwiftlyCore core )
//     {
//         _Core = core;
//         var settings = NativeEngineHelpers.GetMenuSettings();
//         var parts = settings.Split('\x01');
//         Settings = new MenuSettings {
//             NavigationPrefix = parts[0],
//             InputMode = parts[1],
//             ButtonsUse = parts[2],
//             ButtonsScroll = parts[3],
//             ButtonsScrollBack = parts[4],
//             ButtonsExit = parts[5],
//             SoundUseName = parts[6],
//             SoundUseVolume = float.Parse(parts[7], CultureInfo.InvariantCulture),
//             SoundScrollName = parts[8],
//             SoundScrollVolume = float.Parse(parts[9], CultureInfo.InvariantCulture),
//             SoundExitName = parts[10],
//             SoundExitVolume = float.Parse(parts[11], CultureInfo.InvariantCulture),
//             ItemsPerPage = int.Parse(parts[12]),
//         };

//         _scrollSound.Name = Settings.SoundScrollName;
//         _scrollSound.Volume = Settings.SoundScrollVolume;

//         _useSound.Name = Settings.SoundUseName;
//         _useSound.Volume = Settings.SoundUseVolume;

//         _exitSound.Name = Settings.SoundExitName;
//         _exitSound.Volume = Settings.SoundExitVolume;

//         _Core.Event.OnClientKeyStateChanged += KeyStateChange;
//         _Core.Event.OnClientDisconnected += OnClientDisconnected;
//         _Core.Event.OnMapUnload += OnMapUnload;
//     }

//     ~MenuManager()
//     {
//         CloseAllMenus();

//         _Core.Event.OnClientKeyStateChanged -= KeyStateChange;
//         _Core.Event.OnClientDisconnected -= OnClientDisconnected;
//         _Core.Event.OnMapUnload -= OnMapUnload;
//     }

//     void KeyStateChange( IOnClientKeyStateChangedEvent @event )
//     {
//         var player = _Core.PlayerManager.GetPlayer(@event.PlayerId);
//         var menu = GetMenu(player);
//         if (menu == null) return;
//         if (!@event.Pressed) return;

//         if (Settings.InputMode == "button")
//         {
//             var scrollKey = menu.ButtonOverrides?.Move ?? StringToKeyKind.GetValueOrDefault(Settings.ButtonsScroll);
//             var scrollBackKey = menu.ButtonOverrides?.MoveBack ?? StringToKeyKind.GetValueOrDefault(Settings.ButtonsScrollBack);
//             var exitKey = menu.ButtonOverrides?.Exit ?? StringToKeyKind.GetValueOrDefault(Settings.ButtonsExit);
//             var useKey = menu.ButtonOverrides?.Select ?? StringToKeyKind.GetValueOrDefault(Settings.ButtonsUse);

//             new Dictionary<string, KeyKind> { { "Scroll", scrollKey }, { "ScrollBack", scrollBackKey }, { "Exit", exitKey }, { "Use", useKey } }
//                 .GroupBy(kvp => kvp.Value)
//                 .Where(g => g.Count() > 1 && @event.Key.HasFlag(g.Key))
//                 .ToList()
//                 .ForEach(group =>
//                 {
//                     Spectre.Console.AnsiConsole.WriteException(
//                         new InvalidOperationException($"Duplicate key binding detected in menu '{menu.Title}': Key '{group.Key}' is used by: {string.Join(", ", group.Select(kvp => kvp.Key))}")
//                     );
//                 });

//             if (@event.Key == scrollKey)
//             {
//                 menu.MoveSelection(player, 1);

//                 if (menu.HasSound)
//                 {
//                     _scrollSound.Recipients.AddRecipient(@event.PlayerId);
//                     _scrollSound.Emit();
//                     _scrollSound.Recipients.RemoveRecipient(@event.PlayerId);
//                 }
//             }
//             else if (@event.Key == scrollBackKey)
//             {
//                 menu.MoveSelection(player, -1);

//                 if (menu.HasSound)
//                 {
//                     _scrollSound.Recipients.AddRecipient(@event.PlayerId);
//                     _scrollSound.Emit();
//                     _scrollSound.Recipients.RemoveRecipient(@event.PlayerId);
//                 }
//             }
//             else if (@event.Key == exitKey)
//             {
//                 _Core.Menus.CloseMenuForPlayer(player);

//                 if (menu.HasSound)
//                 {
//                     _exitSound.Recipients.AddRecipient(@event.PlayerId);
//                     _exitSound.Emit();
//                     _exitSound.Recipients.RemoveRecipient(@event.PlayerId);
//                 }
//             }
//             else if (@event.Key == useKey)
//             {
//                 var option = menu.GetCurrentOption(player);
//                 if (option is SliderMenuButton || option is ChoiceMenuOption)
//                 {
//                     menu.UseSlideOption(player, true);
//                 }
//                 else
//                 {
//                     menu.UseSelection(player);
//                 }

//                 if (menu.HasSound && (option?.HasSound() ?? false))
//                 {
//                     _useSound.Recipients.AddRecipient(@event.PlayerId);
//                     _useSound.Emit();
//                     _useSound.Recipients.RemoveRecipient(@event.PlayerId);
//                 }
//             }
//         }
//         else if (Settings.InputMode == "wasd")
//         {
//             if (@event.Key == KeyKind.W)
//             {
//                 menu.MoveSelection(player, -1);

//                 if (menu.HasSound)
//                 {
//                     _scrollSound.Recipients.AddRecipient(@event.PlayerId);
//                     _scrollSound.Emit();
//                     _scrollSound.Recipients.RemoveRecipient(@event.PlayerId);
//                 }
//             }
//             else if (@event.Key == KeyKind.S)
//             {
//                 menu.MoveSelection(player, 1);

//                 if (menu.HasSound)
//                 {
//                     _scrollSound.Recipients.AddRecipient(@event.PlayerId);
//                     _scrollSound.Emit();
//                     _scrollSound.Recipients.RemoveRecipient(@event.PlayerId);
//                 }
//             }
//             else if (@event.Key == KeyKind.A)
//             {
//                 CloseMenuForPlayer(player);
//                 if (menu.HasSound)
//                 {
//                     _exitSound.Recipients.AddRecipient(@event.PlayerId);
//                     _exitSound.Emit();
//                     _exitSound.Recipients.RemoveRecipient(@event.PlayerId);
//                 }
//             }
//             else if (@event.Key == KeyKind.D)
//             {
//                 var option = menu.GetCurrentOption(player);
//                 if (option is SliderMenuButton || option is ChoiceMenuOption)
//                 {
//                     menu.UseSlideOption(player, true);
//                 }
//                 else
//                 {
//                     menu.UseSelection(player);
//                 }

//                 if (menu.HasSound && (option?.HasSound() ?? false))
//                 {
//                     _useSound.Recipients.AddRecipient(@event.PlayerId);
//                     _useSound.Emit();
//                     _useSound.Recipients.RemoveRecipient(@event.PlayerId);
//                 }
//             }
//         }
//     }

//     public void OnClientDisconnected( IOnClientDisconnectedEvent @event )
//     {
//         var player = _Core.PlayerManager.GetPlayer(@event.PlayerId);
//         if (player == null)
//         {
//             return;
//         }

//         if (OpenMenus.TryRemove(player, out var menu))
//         {
//             var currentMenu = menu;
//             while (currentMenu != null)
//             {
//                 currentMenu.Close(player);
//                 OnMenuClosed?.Invoke(player, currentMenu);
//                 currentMenu = currentMenu.Parent;
//             }
//         }
//     }

//     public void OnMapUnload( IOnMapUnloadEvent _ )
//     {
//         CloseAllMenus();
//     }

//     public void CloseAllMenus()
//     {
//         foreach (var kvp in OpenMenus)
//         {
//             var player = kvp.Key;
//             var currentMenu = kvp.Value;
//             while (currentMenu != null)
//             {
//                 currentMenu.Close(player);
//                 OnMenuClosed?.Invoke(player, currentMenu);
//                 currentMenu = currentMenu.Parent;
//             }
//         }
//         OpenMenus.Clear();
//     }

//     public void CloseMenu( IMenu menu )
//     {
//         foreach (var kvp in OpenMenus)
//         {
//             var player = kvp.Key;
//             var openMenu = kvp.Value;

//             if (openMenu == menu)
//             {
//                 CloseMenuForPlayer(player);
//             }
//         }
//     }

//     public void CloseMenuByTitle( string title, bool exact = false )
//     {
//         foreach (var kvp in OpenMenus)
//         {
//             var player = kvp.Key;
//             var menu = kvp.Value;

//             if ((exact && menu.Title == title) || (!exact && menu.Title.Contains(title)))
//             {
//                 CloseMenuForPlayer(player);
//             }
//         }
//     }

//     public void CloseMenuForPlayer( IPlayer player )
//     {
//         if (OpenMenus.TryRemove(player, out var menu))
//         {
//             menu.Close(player);
//             OnMenuClosed?.Invoke(player, menu);
//             if (menu.Parent != null)
//             {
//                 OpenMenu(player, menu.Parent);
//             }
//         }
//     }

//     public IMenu CreateMenu( string title )
//     {
//         return new Menu { Title = title, MenuManager = this, MaxVisibleOptions = Settings.ItemsPerPage, _Core = _Core };
//     }

//     public IMenu? GetMenu( IPlayer player )
//     {
//         return OpenMenus.TryGetValue(player, out var menu) ? menu : null;
//     }

//     public void OpenMenu( IPlayer player, IMenu menu )
//     {
//         if (OpenMenus.TryGetValue(player, out var currentMenu))
//         {
//             currentMenu.Close(player);
//             OnMenuClosed?.Invoke(player, currentMenu);
//         }

//         OpenMenus[player] = menu;
//         menu.Show(player);
//         OnMenuOpened?.Invoke(player, menu);
//     }

//     public bool HasMenuOpen( IPlayer player )
//     {
//         return NativePlayer.HasMenuShown(player.PlayerID);
//     }
// }