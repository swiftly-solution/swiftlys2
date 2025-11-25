using SwiftlyS2.Shared.Menus;

namespace SwiftlyS2.Core.Menus;

internal sealed class MenuBuilderAPI : IMenuBuilderAPI
{
    /// <summary>
    /// Gets the design interface for this menu.
    /// </summary>
    public IMenuDesignAPI Design { get => design ??= new MenuDesignAPI(configuration, this, style => optionScrollStyle = style/*, style => optionTextStyle = style*/); }

    private readonly MenuManagerAPI menuManager;
    private readonly MenuConfiguration configuration = new();
    private readonly List<IMenuOption> options = [];
    private MenuKeybindOverrides keybindOverrides = new();
    private MenuOptionScrollStyle optionScrollStyle = MenuOptionScrollStyle.CenterFixed;
    // private MenuOptionTextStyle optionTextStyle = MenuOptionTextStyle.TruncateEnd;
    private IMenuAPI? parent = null;
    private IMenuDesignAPI? design = null;

    public MenuBuilderAPI( MenuManagerAPI menuManager )
    {
        this.menuManager = menuManager;
        options.Clear();
    }

    public IMenuBuilderAPI BindToParent( IMenuAPI parent )
    {
        this.parent = parent;
        return this;
    }

    public IMenuBuilderAPI AddOption( IMenuOption option )
    {
        options.Add(option);
        return this;
    }

    public IMenuBuilderAPI EnableSound()
    {
        configuration.PlaySound = true;
        return this;
    }

    public IMenuBuilderAPI DisableSound()
    {
        configuration.PlaySound = false;
        return this;
    }

    public IMenuBuilderAPI SetPlayerFrozen( bool frozen = false )
    {
        configuration.FreezePlayer = frozen;
        return this;
    }

    public IMenuBuilderAPI SetAutoCloseDelay( float seconds = 0f )
    {
        configuration.AutoCloseAfter = seconds;
        return this;
    }

    public IMenuBuilderAPI SetSelectButton( KeyBind keyBind )
    {
        keybindOverrides = keybindOverrides with { Select = keyBind };
        return this;
    }

    public IMenuBuilderAPI SetMoveForwardButton( KeyBind keyBind )
    {
        keybindOverrides = keybindOverrides with { Move = keyBind };
        return this;
    }

    public IMenuBuilderAPI SetMoveBackwardButton( KeyBind keyBind )
    {
        keybindOverrides = keybindOverrides with { MoveBack = keyBind };
        return this;
    }

    public IMenuBuilderAPI SetExitButton( KeyBind keyBind )
    {
        keybindOverrides = keybindOverrides with { Exit = keyBind };
        return this;
    }

    public IMenuAPI Build()
    {
        MenuAPI menu;
        if (menuManager.IsInitialized)
        {
            // Core is initialized, create menu normally
            menu = new MenuAPI(menuManager.Core, configuration, keybindOverrides, this/*, parent*/, optionScrollStyle/*, optionTextStyle*/) { Parent = (parent, null) };
        }
        else
        {
            // Core is not initialized yet, create menu with deferred initialization
            menu = new MenuAPI(configuration, keybindOverrides, this, optionScrollStyle) { Parent = (parent, null) };
            menuManager.RegisterPendingBuild(menu);
        }
        options.ForEach(option => menu.AddOption(option));
        return menu;
    }
}