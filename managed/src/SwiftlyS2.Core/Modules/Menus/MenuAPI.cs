using System.Collections.Concurrent;
using SwiftlyS2.Shared;
using SwiftlyS2.Shared.Menus;
using SwiftlyS2.Core.Natives;
using SwiftlyS2.Core.Menus.OptionsBase;
using SwiftlyS2.Shared.Players;
using SwiftlyS2.Shared.SchemaDefinitions;

namespace SwiftlyS2.Core.Menus;

internal sealed class MenuAPI : IMenuAPI, IDisposable
{
    private (IMenuAPI? ParentMenu, IMenuOption? TriggerOption) parent;

    internal static readonly IMenuOption noOptionsOption = new TextMenuOption("No options");

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
    /// Gets or sets the default comment text to use when a menu option's Comment is not set.
    /// </summary>
    public string DefaultComment { get; set; } = $"Powered by <font color='#ff3c00'>❤️</font> {HtmlGradient.GenerateGradientText("SwiftlyS2", "#ffffff", "#96d5ff")}";

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
    /// <remarks>
    /// This event is fired once per render frame.
    /// </remarks>
    public event EventHandler<MenuEventArgs>? OptionHovering;

    /// <summary>
    /// Fired when a different option is hovered.
    /// </summary>
    /// <remarks>
    /// This event is only fired when the hovered option changes.
    /// </remarks>
    public event EventHandler<MenuEventArgs>? OptionHovered;

    /// <summary>
    /// Fired when a menu option is selected (activated) by the player.
    /// </summary>
    public event EventHandler<MenuEventArgs>? OptionSelected;

    // /// <summary>
    // /// Fired when an option is about to enter the visible viewport.
    // /// </summary>
    // public event EventHandler<MenuEventArgs>? OptionEntering;

    // /// <summary>
    // /// Fired when an option is about to leave the visible viewport.
    // /// </summary>
    // public event EventHandler<MenuEventArgs>? OptionLeaving;

    private readonly ISwiftlyCore core;
    private readonly List<IMenuOption> options = [];
    private readonly Lock optionsLock = new(); // Lock for synchronizing modifications to the `options`
    private readonly ConcurrentDictionary<int, int> selectedOptionIndex = new(); // Stores the currently selected option index for each player
    // NOTE: Menu selection movement is entirely driven by changes to `desiredOptionIndex` (independent of any other variables)
    private readonly ConcurrentDictionary<int, int> desiredOptionIndex = new(); // Stores the desired option index for each player
    private int maxOptions = 0;
    // private readonly ConcurrentDictionary<int, int> selectedDisplayLine = new(); // Stores the currently selected display line index for each player (some options may span multiple lines)
    // private int maxDisplayLines = 0;
    // private readonly ConcurrentDictionary<int, IReadOnlyList<IMenuOption>> visibleOptionsCache = new();
    private readonly ConcurrentDictionary<int, CancellationTokenSource> autoCloseCancelTokens = new();

    // private readonly ConcurrentDictionary<int, string> renderCache = new();
    private readonly ConcurrentDictionary<Task, CancellationTokenSource> renderLoopTasks = new();
    private readonly Lock viewersLock = new();
    private readonly HashSet<int> viewers = [];

    private volatile bool disposed = false;

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
        disposed = true;

        core?.PlayerManager
            .GetAllPlayers()
            .Where(player => player.IsValid && (selectedOptionIndex.TryGetValue(player.PlayerID, out var _) || desiredOptionIndex.TryGetValue(player.PlayerID, out var _)))
            .ToList()
            .ForEach(player =>
            {
                NativePlayer.ClearCenterMenuRender(player.PlayerID);
                SetFreezeState(player, false);
                if (autoCloseCancelTokens.TryGetValue(player.PlayerID, out var token))
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

        renderLoopTasks.Keys.ToList().ForEach(task =>
        {
            if (renderLoopTasks.TryRemove(task, out var cts))
            {
                cts.Cancel();
                cts.Dispose();
            }
        });
        renderLoopTasks.Clear();

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

        var now = DateTime.UtcNow;
        lock (optionsLock)
        {
            try
            {
                // const string category = "MenuAPI::UpdateDynamicText";
                // core.Profiler.StartRecording(category);

                foreach (var option in options)
                {
                    if (option is MenuOptionBase optionBase)
                    {
                        optionBase.UpdateDynamicText(now);
                        optionBase.UpdateCustomAnimations(now);
                    }
                }

                // core.Profiler.StopRecording(category);
            }
            catch
            { }
        }

        var playerStates = core.PlayerManager
            .GetAllPlayers()
            .Where(player => player.IsValid && !player.IsFakeClient)
            .Select(player => (
                Player: player,
                DesiredIndex: desiredOptionIndex.TryGetValue(player.PlayerID, out var desired) ? desired : -1,
                SelectedIndex: selectedOptionIndex.TryGetValue(player.PlayerID, out var selected) ? selected : -1
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
                var updateResult = selectedOptionIndex.TryUpdate(player.PlayerID, currentOriginalIndex, selectedIndex);
                if (updateResult && currentOriginalIndex != desiredIndex)
                {
                    _ = desiredOptionIndex.TryUpdate(player.PlayerID, currentOriginalIndex, desiredIndex);
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
                .AsReadOnly();

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
                ? string.Concat(Configuration.HideTitleItemCount ? "<br>" : $"<font class='fontSize-s' color='#FFFFFF'> [{selectedIndex + 1}/{maxOptions}]</font><br>", guideLine, "<br>")
                : string.Concat("<br>", guideLine, "<br>")
        );

        var menuItems = string.Join("<br>", visibleOptions.Select(( option, index ) => string.Concat(
            index == arrowPosition
                ? $"<font color='{navigationColor}' class='fontSize-sm'>{core.MenusAPI.Configuration.NavigationPrefix} </font>"
                : "\u00A0\u00A0\u00A0 ",
            option.GetDisplayText(player, 0)
        )));

        var currentOption = visibleOptions.Count > 0 ? visibleOptions[arrowPosition] : null;
        var optionBase = currentOption as MenuOptionBase;

        var comment = !string.IsNullOrWhiteSpace(optionBase?.Comment)
            ? string.Concat(
                "<br>",
                guideLine,
                "<br>",
                $"<font class='fontSize-s'>{optionBase.Comment}</font><br>"
            )
            : string.Concat(
                "<br>",
                guideLine,
                "<br>",
                $"<font class='fontSize-s'>{DefaultComment}</font><br>"
            );

        var claimInfo = optionBase?.InputClaimInfo ?? MenuInputClaimInfo.Empty;

        var footerSection = Configuration.HideFooter ? string.Empty :
            core.MenusAPI.Configuration.InputMode switch {
                "wasd" => string.Concat(
                    "<font class='fontSize-s' color='#FFFFFF'>",
                    $"<font color='{footerColor}'>Move:</font> W/S",
                    claimInfo.ClaimsUse
                        ? $" | <font color='{footerColor}'>{claimInfo.UseLabel ?? "Use"}:</font> D"
                        : $" | <font color='{footerColor}'>Use:</font> D",
                    claimInfo.ClaimsExit
                        ? $" | <font color='{footerColor}'>{claimInfo.ExitLabel ?? "Exit"}:</font> A"
                        : (Configuration.DisableExit ? string.Empty : $" | <font color='{footerColor}'>Exit:</font> A"),
                    "</font>"
                ),
                _ => string.Concat(
                    "<font class='fontSize-s' color='#FFFFFF'>",
                    $"<font color='{footerColor}'>Move:</font> {KeybindOverrides.Move?.ToString() ?? core.MenusAPI.Configuration.ButtonsScroll.ToUpper()}/{KeybindOverrides.MoveBack?.ToString() ?? core.MenusAPI.Configuration.ButtonsScrollBack.ToUpper()}",
                    claimInfo.ClaimsUse
                        ? $" | <font color='{footerColor}'>{claimInfo.UseLabel ?? "Use"}:</font> {KeybindOverrides.Select?.ToString() ?? core.MenusAPI.Configuration.ButtonsUse.ToUpper()}"
                        : $" | <font color='{footerColor}'>Use:</font> {KeybindOverrides.Select?.ToString() ?? core.MenusAPI.Configuration.ButtonsUse.ToUpper()}",
                    claimInfo.ClaimsExit
                        ? $" | <font color='{footerColor}'>{claimInfo.ExitLabel ?? "Exit"}:</font> {KeybindOverrides.Exit?.ToString() ?? core.MenusAPI.Configuration.ButtonsExit.ToUpper()}"
                        : (Configuration.DisableExit ? string.Empty : $" | <font color='{footerColor}'>Exit:</font> {KeybindOverrides.Exit?.ToString() ?? core.MenusAPI.Configuration.ButtonsExit.ToUpper()}"),
                    "</font>"
                )
            };

        return string.Concat(
            titleSection,
            "<font color='#FFFFFF' class='fontSize-sm'>",
            menuItems,
            "</font>",
            comment,
            footerSection
        );
    }

    public void ShowForPlayer( IPlayer player )
    {
        _ = selectedOptionIndex.AddOrUpdate(player.PlayerID, 0, ( _, _ ) => 0);
        _ = desiredOptionIndex.AddOrUpdate(player.PlayerID, 0, ( _, _ ) => 0);
        // _ = selectedDisplayLine.AddOrUpdate(player, 0, ( _, _ ) => 0);

        if (!player.IsValid || player.IsFakeClient)
        {
            return;
        }

        _ = MoveToOptionIndex(player, 0);

        // Add viewer, resume animations if first viewer
        lock (viewersLock)
        {
            _ = viewers.Add(player.PlayerID);

            if (viewers.Count == 1)
            {
                renderLoopTasks.Keys.ToList().ForEach(task =>
                {
                    if (renderLoopTasks.TryRemove(task, out var cts))
                    {
                        cts.Cancel();
                        cts.Dispose();
                    }
                });

                var cts = new CancellationTokenSource();
                var token = cts.Token;
                var delayMilliseconds = (int)(1000f / 64f);
                var task = Task.Run(async () =>
                {
                    while (!token.IsCancellationRequested && !disposed)
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
                }, token);
                _ = renderLoopTasks.TryAdd(task, cts);

                lock (optionsLock)
                {
                    options.OfType<MenuOptionBase>().ToList().ForEach(option => option.ResumeTextAnimation());
                }
            }
        }

        SetFreezeState(player, Configuration.FreezePlayer);

        if (Configuration.AutoCloseAfter > 0)
        {
            _ = autoCloseCancelTokens.AddOrUpdate(
                player.PlayerID,
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
        var removedFromSelected = selectedOptionIndex.TryRemove(player.PlayerID, out _);
        var removedFromDesired = desiredOptionIndex.TryRemove(player.PlayerID, out _);
        // var removedFromDisplayLine = selectedDisplayLine.TryRemove(player, out _);
        var keyExists = removedFromSelected || removedFromDesired/* || removedFromDisplayLine*/;

        if (player.IsFakeClient || !(player.Controller?.IsValid ?? false) || !(player.PlayerPawn?.IsValid ?? false))
        {
            return;
        }

        if (keyExists)
        {
            NativePlayer.ClearCenterMenuRender(player.PlayerID);
            core.Scheduler.NextTick(() => NativePlayer.ClearCenterMenuRender(player.PlayerID));

            // Remove viewer, pause animations if no viewers left
            lock (viewersLock)
            {
                _ = viewers.Remove(player.PlayerID);

                if (viewers.Count == 0)
                {
                    renderLoopTasks.Keys.ToList().ForEach(task =>
                    {
                        if (renderLoopTasks.TryRemove(task, out var cts))
                        {
                            cts.Cancel();
                            cts.Dispose();
                        }
                    });

                    lock (optionsLock)
                    {
                        options.OfType<MenuOptionBase>().ToList().ForEach(option => option.PauseTextAnimation());
                    }
                }
            }
        }

        SetFreezeState(player, false);

        // _ = renderCache.TryRemove(player, out _);

        if (autoCloseCancelTokens.TryRemove(player.PlayerID, out var token))
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
            if (option is MenuOptionBase baseOption)
            {
                baseOption.Menu = this;
            }
            if (option != noOptionsOption && maxOptions == 1) _ = RemoveOption(noOptionsOption);
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
            if (option != noOptionsOption && maxOptions == 1) AddOption(noOptionsOption);
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
        if (maxOptions == 0 || !desiredOptionIndex.TryGetValue(player.PlayerID, out var oldIndex))
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

            if (visibleIndex >= 0 && desiredOptionIndex.TryUpdate(player.PlayerID, visibleIndex, oldIndex))
            {
                OptionHovered?.Invoke(this, new MenuEventArgs {
                    Player = player,
                    Options = new List<IMenuOption> { options[visibleIndex] }.AsReadOnly()
                });

                return true;
            }

            return false;
        }
    }

    public IMenuOption? GetCurrentOption( IPlayer player )
    {
        lock (optionsLock)
        {
            return selectedOptionIndex.TryGetValue(player.PlayerID, out var index) ? options[index] : null;
        }
    }

    public int GetCurrentOptionIndex( IPlayer player )
    {
        return selectedOptionIndex.TryGetValue(player.PlayerID, out var index) ? index : -1;
    }

    // public int GetCurrentOptionDisplayLine( IPlayer player )
    // {
    //     return selectedDisplayLine.TryGetValue(player, out var line) ? line : -1;
    // }

    internal void InvokeOptionSelected( IPlayer player, IMenuOption option )
    {
        OptionSelected?.Invoke(this, new MenuEventArgs {
            Player = player,
            Options = new List<IMenuOption> { option }.AsReadOnly()
        });
    }

    private void SetFreezeState( IPlayer player, bool freeze )
    {
        if (!player.IsValid || player.IsFakeClient || !(player.PlayerPawn?.IsValid ?? false))
        {
            return;
        }

        core.Scheduler.NextTick(() =>
        {
            var moveType = freeze ? MoveType_t.MOVETYPE_NONE : MoveType_t.MOVETYPE_WALK;
            player.PlayerPawn.MoveType = moveType;
            player.PlayerPawn.ActualMoveType = moveType;
            player.PlayerPawn.MoveTypeUpdated();
        });
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