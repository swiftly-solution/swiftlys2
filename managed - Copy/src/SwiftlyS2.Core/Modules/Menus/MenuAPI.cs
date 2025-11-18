using System.Collections.Concurrent;
using SwiftlyS2.Shared;
using SwiftlyS2.Shared.Menus;
using SwiftlyS2.Core.Natives;
using SwiftlyS2.Shared.Players;
using SwiftlyS2.Shared.SchemaDefinitions;

namespace SwiftlyS2.Core.Menus;

internal sealed class MenuAPI : IMenuAPI, IDisposable
{
    private (IMenuAPI? ParentMenu, IMenuOption? TriggerOption) parent;

    /// <summary>
    /// The menu manager that this menu belongs to.
    /// </summary>
    public IMenuManagerAPI MenuManager { get; init; }

    /// <summary>
    /// Configuration settings for this menu.
    /// </summary>
    public MenuConfiguration Configuration { get; init; }

    /// <summary>
    /// Keybind overrides for this menu.
    /// </summary>
    public MenuKeybindOverrides KeybindOverrides { get; init; }

    /// <summary>
    /// The scroll style for this menu options.
    /// </summary>
    public MenuOptionScrollStyle OptionScrollStyle { get; init; }

    // /// <summary>
    // /// The text overflow style for menu options.
    // /// </summary>
    // public MenuOptionTextStyle OptionTextStyle { get; init; }

    /// <summary>
    /// The builder used to construct and configure this menu.
    /// </summary>
    public IMenuBuilderAPI? Builder { get; init; }

    /// <summary>
    /// Gets or sets an object that contains data about this menu.
    /// </summary>
    public object? Tag { get; set; }

    /// <summary>
    /// The parent hierarchy information in a hierarchical menu structure.
    /// </summary>
    public (IMenuAPI? ParentMenu, IMenuOption? TriggerOption) Parent {
        get => parent;
        internal set {
            if (parent == value)
            {
                return;
            }

            if (value.ParentMenu == this)
            {
                Spectre.Console.AnsiConsole.WriteException(new ArgumentException($"Parent cannot be self.", nameof(value)));
            }
            else
            {
                parent = value;
            }
        }
    }

    /// <summary>
    /// Read-only collection of all options in this menu.
    /// </summary>
    public IReadOnlyList<IMenuOption> Options {
        get {
            lock (optionsLock)
            {
                return options.AsReadOnly();
            }
        }
    }

    // /// <summary>
    // /// Fired before a player navigates to a different menu option.
    // /// </summary>
    // public event EventHandler<MenuEventArgs>? BeforeSelectionMove;

    // /// <summary>
    // /// Fired after a player navigates to a different menu option.
    // /// </summary>
    // public event EventHandler<MenuEventArgs>? AfterSelectionMove;

    /// <summary>
    /// Fired when the selection pointer is hovering over an option.
    /// </summary>
    public event EventHandler<MenuEventArgs>? OptionHovering;

    // /// <summary>
    // /// Fired when an option is about to enter the visible viewport.
    // /// </summary>
    // public event EventHandler<MenuEventArgs>? OptionEntering;

    // /// <summary>
    // /// Fired when an option is about to leave the visible viewport.
    // /// </summary>
    // public event EventHandler<MenuEventArgs>? OptionLeaving;

    private readonly ISwiftlyCore core;
    private readonly List<IMenuOption> options = new();
    private readonly Lock optionsLock = new(); // Lock for synchronizing modifications to the `options`
    private readonly ConcurrentDictionary<IPlayer, int> selectedOptionIndex = new(); // Stores the currently selected option index for each player
    // NOTE: Menu selection movement is entirely driven by changes to `desiredOptionIndex` (independent of any other variables)
    private readonly ConcurrentDictionary<IPlayer, int> desiredOptionIndex = new(); // Stores the desired option index for each player
    private int maxOptions = 0;
    // private readonly ConcurrentDictionary<IPlayer, int> selectedDisplayLine = new(); // Stores the currently selected display line index for each player (some options may span multiple lines)
    // private int maxDisplayLines = 0;
    // private readonly ConcurrentDictionary<IPlayer, IReadOnlyList<IMenuOption>> visibleOptionsCache = new();
    private readonly ConcurrentDictionary<IPlayer, CancellationTokenSource> autoCloseCancelTokens = new();

    // private readonly ConcurrentDictionary<IPlayer, string> renderCache = new();
    private readonly CancellationTokenSource renderLoopCancellationTokenSource = new();

    private volatile bool disposed;

    // [SetsRequiredMembers]
    public MenuAPI( ISwiftlyCore core, MenuConfiguration configuration, MenuKeybindOverrides keybindOverrides, IMenuBuilderAPI? builder = null/*, IMenuAPI? parent = null*/, MenuOptionScrollStyle optionScrollStyle = MenuOptionScrollStyle.CenterFixed/*, MenuOptionTextStyle optionTextStyle = MenuOptionTextStyle.TruncateEnd*/ )
    {
        disposed = false;

        this.core = core;

        MenuManager = core.MenusAPI;
        Configuration = configuration;
        KeybindOverrides = keybindOverrides;
        OptionScrollStyle = optionScrollStyle;
        // OptionTextStyle = optionTextStyle;
        Builder = builder;
        // Parent = parent;

        lock (optionsLock)
        {
            options.Clear();
        }
        selectedOptionIndex.Clear();
        desiredOptionIndex.Clear();
        // selectedDisplayLine.Clear();
        autoCloseCancelTokens.Clear();
        // visibleOptionsCache.Clear();
        // renderCache.Clear();

        maxOptions = 0;
        // maxDisplayLines = 0;

        // core.Event.OnTick += OnTick;

        _ = Task.Run(async () =>
        {
            var token = renderLoopCancellationTokenSource.Token;
            var delayMilliseconds = (int)(1000f / 64f / 2f);
            while (!token.IsCancellationRequested || disposed)
            {
                try
                {
                    OnRender();
                    await Task.Delay(delayMilliseconds, token);
                }
                catch (OperationCanceledException)
                {
                    break;
                }
                catch
                {
                }
            }
        }, renderLoopCancellationTokenSource.Token);
    }

    ~MenuAPI()
    {
        Dispose();
    }

    public void Dispose()
    {
        if (disposed)
        {
            return;
        }

        // Console.WriteLine($"{GetType().Name} has been disposed.");
        core.PlayerManager
            .GetAllPlayers()
            .Where(player => player.IsValid && (selectedOptionIndex.TryGetValue(player, out var _) || desiredOptionIndex.TryGetValue(player, out var _)))
            .ToList()
            .ForEach(player =>
            {
                NativePlayer.ClearCenterMenuRender(player.PlayerID);
                SetFreezeState(player, false);
                if (autoCloseCancelTokens.TryGetValue(player, out var token))
                {
                    token.Cancel();
                    token.Dispose();
                }
            });

        lock (optionsLock)
        {
            // options.ForEach(option => option.Dispose());
            options.Clear();
        }
        selectedOptionIndex.Clear();
        desiredOptionIndex.Clear();
        // selectedDisplayLine.Clear();
        autoCloseCancelTokens.Clear();
        // visibleOptionsCache.Clear();
        // renderCache.Clear();

        maxOptions = 0;
        // maxDisplayLines = 0;

        // core.Event.OnTick -= OnTick;

        renderLoopCancellationTokenSource?.Cancel();
        renderLoopCancellationTokenSource?.Dispose();

        disposed = true;
        GC.SuppressFinalize(this);
    }

    // private void OnTick()
    // {
    //     if (maxOptions <= 0)
    //     {
    //         return;
    //     }

    //     foreach (var kvp in renderCache)
    //     {
    //         var player = kvp.Key;
    //         if (!player.IsValid || player.IsFakeClient)
    //         {
    //             continue;
    //         }

    //         NativePlayer.SetCenterMenuRender(player.PlayerID, kvp.Value);
    //     }
    // }

    private void OnRender()
    {
        if (maxOptions <= 0)
        {
            return;
        }

        var playerStates = core.PlayerManager
            .GetAllPlayers()
            .Where(player => player.IsValid && !player.IsFakeClient)
            .Select(player => (
                Player: player,
                DesiredIndex: desiredOptionIndex.TryGetValue(player, out var desired) ? desired : -1,
                SelectedIndex: selectedOptionIndex.TryGetValue(player, out var selected) ? selected : -1
            ))
            .Where(state => state.DesiredIndex >= 0 && state.SelectedIndex >= 0)
            .ToList();

        var baseMaxVisibleItems = Configuration.MaxVisibleItems < 1 ? core.MenusAPI.Configuration.ItemsPerPage : Configuration.MaxVisibleItems;
        var maxVisibleItems = Configuration.AutoIncreaseVisibleItems
            ? Math.Clamp(baseMaxVisibleItems + (Configuration.HideTitle ? 1 : 0) + (Configuration.HideFooter ? 1 : 0), 1, 7)
            : Math.Clamp(baseMaxVisibleItems, 1, 5);
        var halfVisible = maxVisibleItems / 2;

        foreach (var (player, desiredIndex, selectedIndex) in playerStates)
        {
            ProcessPlayerMenu(player, desiredIndex, selectedIndex, maxOptions, maxVisibleItems, halfVisible);
        }
    }

    private void ProcessPlayerMenu( IPlayer player, int desiredIndex, int selectedIndex, int maxOptions, int maxVisibleItems, int halfVisible )
    {
        var filteredOptions = new List<IMenuOption>();
        lock (optionsLock)
        {
            filteredOptions = options.Where(opt => opt.Visible && opt.GetVisible(player)).ToList();
        }

        if (filteredOptions.Count <= 0)
        {
            var emptyHtml = BuildMenuHtml(player, [], 0, 0, maxOptions, maxVisibleItems);
            // _ = renderCache.AddOrUpdate(player, emptyHtml, ( _, _ ) => emptyHtml);
            core.Scheduler.NextTick(() => NativePlayer.SetCenterMenuRender(player.PlayerID, emptyHtml));
            return;
        }

        var clampedDesiredIndex = Math.Clamp(desiredIndex, 0, maxOptions - 1);
        var (visibleOptions, arrowPosition) = GetVisibleOptionsAndArrowPosition(filteredOptions, clampedDesiredIndex, maxVisibleItems, halfVisible);
        var safeArrowPosition = Math.Clamp(arrowPosition, 0, visibleOptions.Count - 1);

        OptionHovering?.Invoke(this, new MenuEventArgs {
            Player = player,
            Options = new List<IMenuOption> { visibleOptions[safeArrowPosition] }.AsReadOnly()
        });

        var html = BuildMenuHtml(player, visibleOptions, safeArrowPosition, clampedDesiredIndex, maxOptions, maxVisibleItems);
        // _ = renderCache.AddOrUpdate(player, html, ( _, _ ) => html);
        core.Scheduler.NextTick(() => NativePlayer.SetCenterMenuRender(player.PlayerID, html));

        lock (optionsLock)
        {
            var currentOriginalIndex = options.IndexOf(visibleOptions[safeArrowPosition]);

            if (currentOriginalIndex != selectedIndex)
            {
                var updateResult = selectedOptionIndex.TryUpdate(player, currentOriginalIndex, selectedIndex);
                if (updateResult && currentOriginalIndex != desiredIndex)
                {
                    _ = desiredOptionIndex.TryUpdate(player, currentOriginalIndex, desiredIndex);
                }
            }
        }
    }

    private (IReadOnlyList<IMenuOption> VisibleOptions, int ArrowPosition) GetVisibleOptionsAndArrowPosition( List<IMenuOption> filteredOptions, int clampedDesiredIndex, int maxVisibleItems, int halfVisible )
    {
        var filteredMaxOptions = -1;
        var mappedDesiredIndex = -1;
        lock (optionsLock)
        {
            filteredMaxOptions = filteredOptions.Count;
            mappedDesiredIndex = filteredOptions.IndexOf(options[clampedDesiredIndex]);

            if (mappedDesiredIndex < 0)
            {
                mappedDesiredIndex = filteredOptions
                    .Select(( opt, idx ) => (Index: idx, Distance: Math.Abs(options.IndexOf(opt) - clampedDesiredIndex)))
                    .MinBy(x => x.Distance)
                    .Index;
            }
        }

        if (filteredMaxOptions <= maxVisibleItems)
        {
            return (filteredOptions.AsReadOnly(), mappedDesiredIndex);
        }

        var (startIndex, arrowPosition) = CalculateScrollPosition(mappedDesiredIndex, filteredMaxOptions, maxVisibleItems, halfVisible);

        var visibleOptions = OptionScrollStyle == MenuOptionScrollStyle.CenterFixed
            ? Enumerable.Range(0, maxVisibleItems)
                .Select(i => filteredOptions[(mappedDesiredIndex + i - halfVisible + filteredMaxOptions) % filteredMaxOptions])
                .ToList()
                .AsReadOnly()
            : filteredOptions
                .Skip(startIndex)
                .Take(maxVisibleItems)
                .ToList()
                .AsReadOnly()
            ;

        return (visibleOptions, arrowPosition);
    }

    private (int StartIndex, int ArrowPosition) CalculateScrollPosition( int clampedDesiredIndex, int maxOptions, int maxVisibleItems, int halfVisible )
    {
        return OptionScrollStyle switch {
            MenuOptionScrollStyle.WaitingCenter when clampedDesiredIndex < halfVisible
                => (0, clampedDesiredIndex),
            MenuOptionScrollStyle.WaitingCenter when clampedDesiredIndex >= maxOptions - halfVisible
                => (maxOptions - maxVisibleItems, maxVisibleItems - (maxOptions - clampedDesiredIndex)),
            MenuOptionScrollStyle.WaitingCenter
                => (clampedDesiredIndex - halfVisible, halfVisible),

            MenuOptionScrollStyle.LinearScroll when maxVisibleItems == 1
                => (clampedDesiredIndex, 0),
            MenuOptionScrollStyle.LinearScroll when clampedDesiredIndex < maxVisibleItems - 1
                => (0, clampedDesiredIndex),
            MenuOptionScrollStyle.LinearScroll when clampedDesiredIndex >= maxOptions - (maxVisibleItems - 1)
                => (maxOptions - maxVisibleItems, maxVisibleItems - (maxOptions - clampedDesiredIndex)),
            MenuOptionScrollStyle.LinearScroll
                => (clampedDesiredIndex - (maxVisibleItems - 1), maxVisibleItems - 1),

            MenuOptionScrollStyle.CenterFixed
                => (-1, halfVisible),

            _ => (0, 0)
        };
    }

    private string BuildMenuHtml( IPlayer player, IReadOnlyList<IMenuOption> visibleOptions, int arrowPosition, int selectedIndex, int maxOptions, int maxVisibleItems )
    {
        var guideLineColor = Configuration.VisualGuideLineColor ?? "#FFFFFF";
        var navigationColor = Configuration.NavigationMarkerColor ?? "#FFFFFF";
        var footerColor = Configuration.FooterColor ?? "#FF0000";
        var guideLine = $"<font class='fontSize-s' color='{guideLineColor}'>──────────────────────────</font>";

        var titleSection = Configuration.HideTitle ? string.Empty : string.Concat(
            $"<font class='fontSize-m' color='#FFFFFF'>{Configuration.Title}</font>",
            maxOptions > maxVisibleItems
                ? string.Concat($"<font class='fontSize-s' color='#FFFFFF'> [{selectedIndex + 1}/{maxOptions}]</font><br>", guideLine, "<br>")
                : string.Concat("<br>", guideLine, "<br>")
        );

        var menuItems = string.Join("<br>", visibleOptions.Select(( option, index ) => string.Concat(
            index == arrowPosition
                ? $"<font color='{navigationColor}' class='fontSize-sm'>{core.MenusAPI.Configuration.NavigationPrefix} </font>"
                : "\u00A0\u00A0\u00A0 ",
            option.GetDisplayText(player, 0)
        )));

        var footerSection = Configuration.HideFooter ? string.Empty :
            core.MenusAPI.Configuration.InputMode switch {
                "wasd" => string.Concat(
                    "<br>", guideLine, "<br>",
                    "<font class='fontSize-s' color='#FFFFFF'>",
                    $"<font color='{footerColor}'>Move:</font> W/S | ",
                    $"<font color='{footerColor}'>Use:</font> D | ",
                    $"<font color='{footerColor}'>Exit:</font> A",
                    "</font>"
                ),
                _ => string.Concat(
                    "<br>", guideLine, "<br>",
                    "<font class='fontSize-s' color='#FFFFFF'>",
                    $"<font color='{footerColor}'>Move:</font> {KeybindOverrides.Move?.ToString() ?? core.MenusAPI.Configuration.ButtonsScroll.ToUpper()}/{KeybindOverrides.MoveBack?.ToString() ?? core.MenusAPI.Configuration.ButtonsScrollBack.ToUpper()} | ",
                    $"<font color='{footerColor}'>Use:</font> {KeybindOverrides.Select?.ToString() ?? core.MenusAPI.Configuration.ButtonsUse.ToUpper()} | ",
                    $"<font color='{footerColor}'>Exit:</font> {KeybindOverrides.Exit?.ToString() ?? core.MenusAPI.Configuration.ButtonsExit.ToUpper()}",
                    "</font>"
                )
            };

        return string.Concat(
            titleSection,
            "<font color='#FFFFFF' class='fontSize-sm'>",
            menuItems,
            "</font>",
            footerSection
        );
    }

    public void ShowForPlayer( IPlayer player )
    {
        _ = selectedOptionIndex.AddOrUpdate(player, 0, ( _, _ ) => 0);
        _ = desiredOptionIndex.AddOrUpdate(player, 0, ( _, _ ) => 0);
        // _ = selectedDisplayLine.AddOrUpdate(player, 0, ( _, _ ) => 0);

        if (!player.IsValid || player.IsFakeClient)
        {
            return;
        }

        SetFreezeState(player, Configuration.FreezePlayer);

        if (Configuration.AutoCloseAfter > 0)
        {
            _ = autoCloseCancelTokens.AddOrUpdate(
                player,
                _ => core.Scheduler.DelayBySeconds(Configuration.AutoCloseAfter, () => core.MenusAPI.CloseMenuForPlayer(player, this)),
                ( _, oldToken ) =>
                {
                    oldToken.Cancel();
                    oldToken.Dispose();
                    return core.Scheduler.DelayBySeconds(Configuration.AutoCloseAfter, () => core.MenusAPI.CloseMenuForPlayer(player, this));
                }
            );
        }
    }

    public void HideForPlayer( IPlayer player )
    {
        var removedFromSelected = selectedOptionIndex.TryRemove(player, out _);
        var removedFromDesired = desiredOptionIndex.TryRemove(player, out _);
        // var removedFromDisplayLine = selectedDisplayLine.TryRemove(player, out _);
        var keyExists = removedFromSelected || removedFromDesired/* || removedFromDisplayLine*/;

        if (!player.IsValid || player.IsFakeClient)
        {
            return;
        }

        if (keyExists)
        {
            NativePlayer.ClearCenterMenuRender(player.PlayerID);
        }

        SetFreezeState(player, false);

        // _ = renderCache.TryRemove(player, out _);

        if (autoCloseCancelTokens.TryRemove(player, out var token))
        {
            token.Cancel();
            token.Dispose();
        }

        // if (!selectedOptionIndex.Any(kvp => !kvp.Key.IsFakeClient) && !desiredOptionIndex.Any(kvp => !kvp.Key.IsFakeClient))
        // {
        //     Dispose();
        // }
    }

    public void AddOption( IMenuOption option )
    {
        lock (optionsLock)
        {
            // option.Click += OnOptionClick;

            // if (option is OptionsBase.SubmenuMenuOption submenuOption)
            // {
            //     submenuOption.SubmenuRequested += OnSubmenuRequested;
            // }
            if (option is OptionsBase.MenuOptionBase baseOption)
            {
                baseOption.Menu = this;
            }
            options.Add(option);
            maxOptions = options.Count;
            // maxDisplayLines = options.Sum(option => option.LineCount);
        }
    }

    public bool RemoveOption( IMenuOption option )
    {
        lock (optionsLock)
        {
            // option.Click -= OnOptionClick;

            // if (option is OptionsBase.SubmenuMenuOption submenuOption)
            // {
            //     submenuOption.SubmenuRequested -= OnSubmenuRequested;
            // }

            var result = options.Remove(option);
            maxOptions = options.Count;
            // maxDisplayLines = options.Sum(option => option.LineCount);
            return result;
        }
    }

    public bool MoveToOption( IPlayer player, IMenuOption option )
    {
        lock (optionsLock)
        {
            return MoveToOptionIndex(player, options.IndexOf(option));
        }
    }

    public bool MoveToOptionIndex( IPlayer player, int index )
    {

        if (maxOptions == 0 || !desiredOptionIndex.TryGetValue(player, out var oldIndex))
        {
            return false;
        }

        var targetIndex = ((index % maxOptions) + maxOptions) % maxOptions;
        var direction = Math.Sign(targetIndex - oldIndex);
        if (direction == 0)
        {
            return true;
        }

        lock (optionsLock)
        {
            var visibleIndex = Enumerable.Range(0, maxOptions)
                .Select(i => (((targetIndex + (i * direction)) % maxOptions) + maxOptions) % maxOptions)
                .FirstOrDefault(idx => options[idx].Visible && options[idx].GetVisible(player), -1);

            return visibleIndex >= 0 && desiredOptionIndex.TryUpdate(player, visibleIndex, oldIndex);
        }
    }

    public IMenuOption? GetCurrentOption( IPlayer player )
    {
        lock (optionsLock)
        {
            return selectedOptionIndex.TryGetValue(player, out var index) ? options[index] : null;
        }
    }

    public int GetCurrentOptionIndex( IPlayer player )
    {
        return selectedOptionIndex.TryGetValue(player, out var index) ? index : -1;
    }

    // public int GetCurrentOptionDisplayLine( IPlayer player )
    // {
    //     return selectedDisplayLine.TryGetValue(player, out var line) ? line : -1;
    // }

    private static void SetFreezeState( IPlayer player, bool freeze )
    {
        if (!player.IsValid || player.IsFakeClient || !(player.PlayerPawn?.IsValid ?? false))
        {
            return;
        }

        var moveType = freeze ? MoveType_t.MOVETYPE_NONE : MoveType_t.MOVETYPE_WALK;
        player.PlayerPawn.MoveType = moveType;
        player.PlayerPawn.ActualMoveType = moveType;
        player.PlayerPawn.MoveTypeUpdated();
    }

    // private ValueTask OnOptionClick( object? sender, MenuOptionClickEventArgs args )
    // {
    //     if (args.CloseMenu)
    //     {
    //         CloseForPlayer(args.Player);
    //     }

    //     return ValueTask.CompletedTask;
    // }

    // private void OnSubmenuRequested( object? sender, MenuManagerEventArgs args )
    // {
    //     if (args.Player != null && args.Menu != null)
    //     {
    //         core.MenusAPI.OpenMenuForPlayer(args.Player, args.Menu);
    //     }
    // }
}