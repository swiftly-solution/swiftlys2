using System.Globalization;
using System.Collections.Concurrent;
using SwiftlyS2.Shared;
using SwiftlyS2.Core.Natives;
using SwiftlyS2.Shared.Menus;
using SwiftlyS2.Shared.Events;
using SwiftlyS2.Shared.Sounds;
using SwiftlyS2.Shared.Players;

namespace SwiftlyS2.Core.Menus;

internal sealed class MenuManagerAPI : IMenuManagerAPI
{
    /// <summary>
    /// The SwiftlyS2 core instance.
    /// </summary>
    public ISwiftlyCore Core { get; internal set; } = default!;

    /// <summary>
    /// Global Configuration settings for all menus.
    /// </summary>
    public MenuManagerConfiguration Configuration { get; init; }

    /// <summary>
    /// Fired when a menu is closed for a player.
    /// </summary>
    public event EventHandler<MenuManagerEventArgs>? MenuClosed;

    /// <summary>
    /// Fired when a menu is opened for a player.
    /// </summary>
    public event EventHandler<MenuManagerEventArgs>? MenuOpened;

    private readonly ConcurrentDictionary<int, IMenuAPI> openMenus = new();
    private readonly ConcurrentDictionary<(int, IMenuAPI), Action<IPlayer, IMenuAPI>?> onClosedCallbacks = new();
    private readonly SoundEvent useSound = new();
    private readonly SoundEvent exitSound = new();
    private readonly SoundEvent scrollSound = new();
    private readonly KeyBind buttonsScroll;
    private readonly KeyBind buttonsScrollBack;
    private readonly KeyBind buttonsExit;
    private readonly KeyBind buttonsUse;

    private static readonly Dictionary<string, KeyBind> StringToKeyBind = new() {
        ["mouse1"] = KeyBind.Mouse1,
        ["mouse2"] = KeyBind.Mouse2,
        ["space"] = KeyBind.Space,
        ["ctrl"] = KeyBind.Ctrl,
        ["w"] = KeyBind.W,
        ["a"] = KeyBind.A,
        ["s"] = KeyBind.S,
        ["d"] = KeyBind.D,
        ["e"] = KeyBind.E,
        ["esc"] = KeyBind.Esc,
        ["r"] = KeyBind.R,
        ["alt"] = KeyBind.Alt,
        ["shift"] = KeyBind.Shift,
        ["weapon1"] = KeyBind.Weapon1,
        ["weapon2"] = KeyBind.Weapon2,
        ["grenade1"] = KeyBind.Grenade1,
        ["grenade2"] = KeyBind.Grenade2,
        ["tab"] = KeyBind.Tab,
        ["f"] = KeyBind.F,
    };

    public MenuManagerAPI()
    {
        var settings = NativeEngineHelpers.GetMenuSettings().Trim().Split('\x01');
        Configuration = new MenuManagerConfiguration {
            NavigationPrefix = settings[0],
            InputMode = settings[1],
            ButtonsUse = settings[2],
            ButtonsScroll = settings[3],
            ButtonsScrollBack = settings[4],
            ButtonsExit = settings[5],
            SoundUseName = settings[6],
            SoundUseVolume = float.Parse(settings[7], CultureInfo.InvariantCulture),
            SoundScrollName = settings[8],
            SoundScrollVolume = float.Parse(settings[9], CultureInfo.InvariantCulture),
            SoundExitName = settings[10],
            SoundExitVolume = float.Parse(settings[11], CultureInfo.InvariantCulture),
            ItemsPerPage = int.Parse(settings[12]),
        };

        scrollSound.Name = Configuration.SoundScrollName;
        scrollSound.Volume = Configuration.SoundScrollVolume;
        useSound.Name = Configuration.SoundUseName;
        useSound.Volume = Configuration.SoundUseVolume;
        exitSound.Name = Configuration.SoundExitName;
        exitSound.Volume = Configuration.SoundExitVolume;
        buttonsScroll = StringToKeyBind.GetValueOrDefault(Configuration.ButtonsScroll.Trim().ToLower());
        buttonsScrollBack = StringToKeyBind.GetValueOrDefault(Configuration.ButtonsScrollBack.Trim().ToLower());
        buttonsExit = StringToKeyBind.GetValueOrDefault(Configuration.ButtonsExit.Trim().ToLower());
        buttonsUse = StringToKeyBind.GetValueOrDefault(Configuration.ButtonsUse.Trim().ToLower());

        openMenus.Clear();
        onClosedCallbacks.Clear();
    }

    ~MenuManagerAPI()
    {
        CloseAllMenus();

        openMenus.Clear();
        onClosedCallbacks.Clear();
    }

    internal void OnClientKeyStateChanged( IOnClientKeyStateChangedEvent @event )
    {
        if (openMenus.IsEmpty)
        {
            return;
        }

        var player = Core.PlayerManager.GetPlayer(@event.PlayerId);
        var menu = GetCurrentMenu(player);

        if (menu == null || !player.IsValid || player.IsFakeClient || !@event.Pressed)
        {
            return;
        }

        if (Configuration.InputMode.Trim().Equals("button", StringComparison.CurrentCultureIgnoreCase))
        {
            var scrollKey = menu.KeybindOverrides.Move ?? buttonsScroll;
            var scrollBackKey = menu.KeybindOverrides.MoveBack ?? buttonsScrollBack;
            var exitKey = menu.KeybindOverrides.Exit ?? buttonsExit;
            var useKey = menu.KeybindOverrides.Select ?? buttonsUse;

            if (scrollKey.HasFlag(@event.Key.ToKeyBind()))
            {
                _ = menu.MoveToOptionIndex(player, menu.GetCurrentOptionIndex(player) + 1);

                if (menu.Configuration.PlaySound)
                {
                    scrollSound.Recipients.AddRecipient(@event.PlayerId);
                    _ = scrollSound.Emit();
                    scrollSound.Recipients.RemoveRecipient(@event.PlayerId);
                }
            }
            else if (scrollBackKey.HasFlag(@event.Key.ToKeyBind()))
            {
                _ = menu.MoveToOptionIndex(player, menu.GetCurrentOptionIndex(player) - 1);

                if (menu.Configuration.PlaySound)
                {
                    scrollSound.Recipients.AddRecipient(@event.PlayerId);
                    _ = scrollSound.Emit();
                    scrollSound.Recipients.RemoveRecipient(@event.PlayerId);
                }
            }
            else if (exitKey.HasFlag(@event.Key.ToKeyBind()))
            {
                if (!menu.Configuration.DisableExit)
                {
                    CloseMenuForPlayerInternal(player, menu, true);

                    if (menu.Configuration.PlaySound)
                    {
                        exitSound.Recipients.AddRecipient(@event.PlayerId);
                        _ = exitSound.Emit();
                        exitSound.Recipients.RemoveRecipient(@event.PlayerId);
                    }
                }
            }
            else if (useKey.HasFlag(@event.Key.ToKeyBind()))
            {
                var option = menu.GetCurrentOption(player);
                if (option != null && option.Enabled && option.GetEnabled(player) && option.IsClickTaskCompleted(player))
                {
                    _ = Task.Run(async () => await option.OnClickAsync(player));

                    if (menu.Configuration.PlaySound && option.PlaySound)
                    {
                        useSound.Recipients.AddRecipient(@event.PlayerId);
                        _ = useSound.Emit();
                        useSound.Recipients.RemoveRecipient(@event.PlayerId);
                    }
                }
            }
        }
        else if (Configuration.InputMode.Trim().Equals("wasd", StringComparison.CurrentCultureIgnoreCase))
        {
            if (KeyBind.W.HasFlag(@event.Key.ToKeyBind()))
            {
                _ = menu.MoveToOptionIndex(player, menu.GetCurrentOptionIndex(player) - 1);

                if (menu.Configuration.PlaySound)
                {
                    scrollSound.Recipients.AddRecipient(@event.PlayerId);
                    _ = scrollSound.Emit();
                    scrollSound.Recipients.RemoveRecipient(@event.PlayerId);
                }
            }
            else if (KeyBind.S.HasFlag(@event.Key.ToKeyBind()))
            {
                _ = menu.MoveToOptionIndex(player, menu.GetCurrentOptionIndex(player) + 1);

                if (menu.Configuration.PlaySound)
                {
                    scrollSound.Recipients.AddRecipient(@event.PlayerId);
                    _ = scrollSound.Emit();
                    scrollSound.Recipients.RemoveRecipient(@event.PlayerId);
                }
            }
            else if (KeyBind.A.HasFlag(@event.Key.ToKeyBind()))
            {
                if (!menu.Configuration.DisableExit)
                {
                    CloseMenuForPlayerInternal(player, menu, true);

                    if (menu.Configuration.PlaySound)
                    {
                        exitSound.Recipients.AddRecipient(@event.PlayerId);
                        _ = exitSound.Emit();
                        exitSound.Recipients.RemoveRecipient(@event.PlayerId);
                    }
                }
            }
            else if (KeyBind.D.HasFlag(@event.Key.ToKeyBind()))
            {
                var option = menu.GetCurrentOption(player);
                if (option != null && option.Enabled && option.GetEnabled(player) && option.IsClickTaskCompleted(player))
                {
                    _ = Task.Run(async () => await option.OnClickAsync(player));

                    if (menu.Configuration.PlaySound && option.PlaySound)
                    {
                        useSound.Recipients.AddRecipient(@event.PlayerId);
                        _ = useSound.Emit();
                        useSound.Recipients.RemoveRecipient(@event.PlayerId);
                    }
                }
            }
        }
    }

    internal void OnClientDisconnected( IOnClientDisconnectedEvent @event )
    {
        var stackTrace = new System.Diagnostics.StackTrace(true);
        var player = Core.PlayerManager.GetPlayer(@event.PlayerId);
        if (player != null)
        {
            openMenus
                .Where(kvp => kvp.Key == player.PlayerID)
                .ToList()
                .ForEach(kvp => CloseMenuForPlayerInternal(player, kvp.Value, false));
        }
    }

    internal void OnMapUnload( IOnMapUnloadEvent _ )
    {
        CloseAllMenus();
    }

    public IMenuBuilderAPI CreateBuilder()
    {
        return new MenuBuilderAPI(Core);
    }

    public IMenuAPI CreateMenu( MenuConfiguration configuration, MenuKeybindOverrides keybindOverrides, IMenuAPI? parent = null, MenuOptionScrollStyle optionScrollStyle = MenuOptionScrollStyle.CenterFixed, MenuOptionTextStyle optionTextStyle = MenuOptionTextStyle.TruncateEnd )
    {
        var bindingList = new Dictionary<string, KeyBind> {
            ["Scroll"] = keybindOverrides.Move ?? buttonsScroll,
            ["ScrollBack"] = keybindOverrides.MoveBack ?? buttonsScrollBack,
            ["Exit"] = keybindOverrides.Exit ?? buttonsExit,
            ["Use"] = keybindOverrides.Select ?? buttonsUse
        }.ToList();

        for (var i = 0; i < bindingList.Count; i++)
        {
            for (var j = i + 1; j < bindingList.Count; j++)
            {
                var binding1 = bindingList[i];
                var binding2 = bindingList[j];
                var overlap = binding1.Value & binding2.Value;

                if (overlap != 0)
                {
                    Spectre.Console.AnsiConsole.WriteException(
                        new InvalidOperationException(
                            $"Key binding conflict detected in menu '{configuration.Title}': '{binding1.Key}' and '{binding2.Key}' share overlapping keys: {overlap}"
                        )
                    );
                }
            }
        }

        return new MenuAPI(Core, configuration, keybindOverrides, null/*, parent*/, optionScrollStyle/*, optionTextStyle*/) { Parent = (parent, null) };
    }

    public IMenuAPI? GetCurrentMenu( IPlayer player )
    {
        return openMenus.TryGetValue(player.PlayerID, out var menu) ? menu : null;
    }

    public void OpenMenu( IMenuAPI menu )
    {
        Core.PlayerManager
            .GetAllPlayers()
            .Where(player => player.IsValid && !player.IsFakeClient && (player.Controller?.IsValid ?? false))
            .ToList()
            .ForEach(player => OpenMenuForPlayer(player, menu));
    }

    public void OpenMenu( IMenuAPI menu, Action<IPlayer, IMenuAPI> onClosed )
    {
        Core.PlayerManager
            .GetAllPlayers()
            .Where(player => player.IsValid && !player.IsFakeClient && (player.Controller?.IsValid ?? false))
            .ToList()
            .ForEach(player => OpenMenuForPlayer(player, menu, onClosed));
    }

    public void OpenMenuForPlayer( IPlayer player, IMenuAPI menu )
    {
        if (!player.IsValid || player.IsFakeClient || !(player.Controller?.IsValid ?? false))
        {
            return;
        }

        if (openMenus.TryGetValue(player.PlayerID, out var currentMenu) && currentMenu != null)
        {
            if (menu.Parent.ParentMenu == currentMenu)
            {
                // We are transitioning from the current menu to one of its submenus.
                // To show the submenu, we first need to close the current (parent) menu.
                // The parent menu may have an onClosed callback registered in onClosedCallbacks.
                // If we do not remove that callback temporarily, closing the parent menu here
                // would incorrectly invoke the callback even though the user is only navigating
                // deeper into the menu hierarchy (parent -> submenu), not actually finishing the overall menu flow.
                // Therefore we:
                //   1. Temporarily remove the callback associated with the current menu.
                //   2. Close the current (parent) menu as part of the navigation process.
                //   3. Re-register the callback so it will only be invoked later, when the
                //      logical end of the menu flow is reached and the menu is truly closed.
                _ = onClosedCallbacks.TryRemove((player.PlayerID, currentMenu), out var callback);
                CloseMenuForPlayerInternal(player, currentMenu, false);
                _ = onClosedCallbacks.AddOrUpdate((player.PlayerID, currentMenu), callback, ( _, _ ) => callback);
            }
            else
            {
                CloseMenuForPlayerInternal(player, currentMenu, false);
            }
        }

        _ = openMenus.AddOrUpdate(player.PlayerID, menu, ( _, _ ) => menu);
        menu.ShowForPlayer(player);
        MenuOpened?.Invoke(this, new MenuManagerEventArgs { Player = player, Menu = menu });
    }

    public void OpenMenuForPlayer( IPlayer player, IMenuAPI menu, Action<IPlayer, IMenuAPI> onClosed )
    {
        OpenMenuForPlayer(player, menu);
        _ = onClosedCallbacks.AddOrUpdate((player.PlayerID, menu), onClosed, ( _, _ ) => onClosed);
    }

    public void CloseMenu( IMenuAPI menu )
    {
        Core.PlayerManager
            .GetAllPlayers()
            .ToList()
            .ForEach(player => CloseMenuForPlayerInternal(player, menu, true));
    }

    public void CloseMenuForPlayer( IPlayer player, IMenuAPI menu )
    {
        CloseMenuForPlayerInternal(player, menu, true);
    }

    public void CloseAllMenus()
    {
        openMenus.ToList().ForEach(kvp =>
        {
            var currentMenu = kvp.Value;
            while (currentMenu != null)
            {
                var player = Core.PlayerManager.GetPlayer(kvp.Key);
                currentMenu.HideForPlayer(player);
                MenuClosed?.Invoke(this, new MenuManagerEventArgs { Player = player, Menu = currentMenu });
                currentMenu = currentMenu.Parent.ParentMenu;
            }
            _ = openMenus.TryRemove(kvp.Key, out _);
        });
    }

    private void CloseMenuForPlayerInternal( IPlayer player, IMenuAPI menu, bool reopenParent )
    {
        if (player.IsFakeClient)
        {
            return;
        }

        if (!openMenus.TryGetValue(player.PlayerID, out var currentMenu) || currentMenu != menu)
        {
            return;
        }

        if (onClosedCallbacks.TryRemove((player.PlayerID, menu), out var onClosed) && onClosed != null)
        {
            onClosed(player, menu);
        }

        if (openMenus.TryRemove(player.PlayerID, out _))
        {
            menu.HideForPlayer(player);
            MenuClosed?.Invoke(this, new MenuManagerEventArgs { Player = player, Menu = menu });

            if (reopenParent && menu.Parent.ParentMenu != null)
            {
                OpenMenuForPlayer(player, menu.Parent.ParentMenu);
            }
        }
    }
}