using SwiftlyS2.Shared.Menus;

namespace SwiftlyS2.Core.Menus.OptionsBase;

/// <summary>
/// Represents a menu option that opens a submenu when clicked.
/// </summary>
public sealed class SubmenuMenuOption : MenuOptionBase
{
    private readonly Func<Task<IMenuAPI>>? submenuBuilderAsync;

    // /// <summary>
    // /// Occurs when the submenu is ready to be opened.
    // /// </summary>
    // public event EventHandler<MenuManagerEventArgs>? SubmenuRequested;

    /// <summary>
    /// Creates an instance of <see cref="SubmenuMenuOption"/> with a pre-built submenu.
    /// </summary>
    /// <param name="submenu">The submenu to open when this option is clicked.</param>
    /// <param name="updateIntervalMs">The interval in milliseconds between text updates. Defaults to 120ms.</param>
    /// <param name="pauseIntervalMs">The pause duration in milliseconds before starting the next text update cycle. Defaults to 1000ms.</param>
    /// <remarks>
    /// When using this constructor, the <see cref="MenuOptionBase.Text"/> property must be manually set to specify the initial text.
    /// </remarks>
    public SubmenuMenuOption(
        IMenuAPI submenu,
        int updateIntervalMs = 120,
        int pauseIntervalMs = 1000 ) : base(updateIntervalMs, pauseIntervalMs)
    {
        PlaySound = true;
        this.submenuBuilderAsync = () => Task.FromResult(submenu);
        Click += OnSubmenuClick;
    }

    /// <summary>
    /// Creates an instance of <see cref="SubmenuMenuOption"/> with a pre-built submenu.
    /// </summary>
    /// <param name="text">The text content to display.</param>
    /// <param name="submenu">The submenu to open when this option is clicked.</param>
    /// <param name="updateIntervalMs">The interval in milliseconds between text updates. Defaults to 120ms.</param>
    /// <param name="pauseIntervalMs">The pause duration in milliseconds before starting the next text update cycle. Defaults to 1000ms.</param>
    public SubmenuMenuOption(
        string text,
        IMenuAPI submenu,
        int updateIntervalMs = 120,
        int pauseIntervalMs = 1000 ) : this(submenu, updateIntervalMs, pauseIntervalMs)
    {
        Text = text;
    }

    /// <summary>
    /// Creates an instance of <see cref="SubmenuMenuOption"/> with a synchronous builder.
    /// </summary>
    /// <param name="submenuBuilder">Function that builds and returns the submenu.</param>
    /// <param name="updateIntervalMs">The interval in milliseconds between text updates. Defaults to 120ms.</param>
    /// <param name="pauseIntervalMs">The pause duration in milliseconds before starting the next text update cycle. Defaults to 1000ms.</param>
    /// <remarks>
    /// When using this constructor, the <see cref="MenuOptionBase.Text"/> property must be manually set to specify the initial text.
    /// </remarks>
    public SubmenuMenuOption(
        Func<IMenuAPI> submenuBuilder,
        int updateIntervalMs = 120,
        int pauseIntervalMs = 1000 ) : base(updateIntervalMs, pauseIntervalMs)
    {
        PlaySound = true;
        this.submenuBuilderAsync = () => Task.FromResult(submenuBuilder());
        Click += OnSubmenuClick;
    }

    /// <summary>
    /// Creates an instance of <see cref="SubmenuMenuOption"/> with a synchronous builder.
    /// </summary>
    /// <param name="text">The text content to display.</param>
    /// <param name="submenuBuilder">Function that builds and returns the submenu.</param>
    /// <param name="updateIntervalMs">The interval in milliseconds between text updates. Defaults to 120ms.</param>
    /// <param name="pauseIntervalMs">The pause duration in milliseconds before starting the next text update cycle. Defaults to 1000ms.</param>
    public SubmenuMenuOption(
        string text,
        Func<IMenuAPI> submenuBuilder,
        int updateIntervalMs = 120,
        int pauseIntervalMs = 1000 ) : this(submenuBuilder, updateIntervalMs, pauseIntervalMs)
    {
        Text = text;
    }

    /// <summary>
    /// Creates an instance of <see cref="SubmenuMenuOption"/> with an asynchronous builder.
    /// </summary>
    /// <param name="submenuBuilderAsync">Async function that builds and returns the submenu.</param>
    /// <param name="updateIntervalMs">The interval in milliseconds between text updates. Defaults to 120ms.</param>
    /// <param name="pauseIntervalMs">The pause duration in milliseconds before starting the next text update cycle. Defaults to 1000ms.</param>
    /// <remarks>
    /// When using this constructor, the <see cref="MenuOptionBase.Text"/> property must be manually set to specify the initial text.
    /// </remarks>
    public SubmenuMenuOption(
        Func<Task<IMenuAPI>> submenuBuilderAsync,
        int updateIntervalMs = 120,
        int pauseIntervalMs = 1000 ) : base(updateIntervalMs, pauseIntervalMs)
    {
        PlaySound = true;
        this.submenuBuilderAsync = submenuBuilderAsync;
        Click += OnSubmenuClick;
    }

    /// <summary>
    /// Creates an instance of <see cref="SubmenuMenuOption"/> with an asynchronous builder.
    /// </summary>
    /// <param name="text">The text content to display.</param>
    /// <param name="submenuBuilderAsync">Async function that builds and returns the submenu.</param>
    /// <param name="updateIntervalMs">The interval in milliseconds between text updates. Defaults to 120ms.</param>
    /// <param name="pauseIntervalMs">The pause duration in milliseconds before starting the next text update cycle. Defaults to 1000ms.</param>
    public SubmenuMenuOption(
        string text,
        Func<Task<IMenuAPI>> submenuBuilderAsync,
        int updateIntervalMs = 120,
        int pauseIntervalMs = 1000 ) : this(submenuBuilderAsync, updateIntervalMs, pauseIntervalMs)
    {
        Text = text;
    }

    private async ValueTask OnSubmenuClick( object? sender, MenuOptionClickEventArgs args )
    {
        if (submenuBuilderAsync == null || Menu == null)
        {
            return;
        }

        var menu = await submenuBuilderAsync.Invoke();
        if (menu is not MenuAPI submenu)
        {
            return;
        }

        var currentMenu = Menu.MenuManager.GetCurrentMenu(args.Player);
        if (Menu != currentMenu || currentMenu == null)
        {
            return;
        }

        // SubmenuRequested?.Invoke(this, new MenuManagerEventArgs {
        //     Player = args.Player,
        //     Menu = menu
        // });
        submenu.Parent = (Menu, this);
        Menu.MenuManager.OpenMenuForPlayer(args.Player, submenu);
    }
}