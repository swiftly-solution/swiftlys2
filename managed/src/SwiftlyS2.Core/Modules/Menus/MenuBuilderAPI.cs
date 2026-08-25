using SwiftlyS2.Shared;
using SwiftlyS2.Shared.Menus;
using SwiftlyS2.Shared.Players;
using SwiftlyS2.Core.Translations;

namespace SwiftlyS2.Core.Menus;

[Obsolete("The classic menu system is deprecated and will be removed in future versions. Use Core.Menu (IMenuService) instead.")]
internal sealed class MenuBuilderAPI : IMenuBuilderAPI
{
    /// <summary>
    /// Gets the design interface for this menu.
    /// </summary>
    public IMenuDesignAPI Design { get => field ??= new MenuDesignAPI(configuration, this, style => optionScrollStyle = style/*, style => optionTextStyle = style*/); } = null;

    private readonly ISwiftlyCore core;
    private readonly MenuConfiguration configuration = new();
    private readonly List<IMenuOption> options = [];
    private MenuKeybindOverrides keybindOverrides = new();
    private MenuOptionScrollStyle optionScrollStyle = MenuOptionScrollStyle.CenterFixed;
    // private MenuOptionTextStyle optionTextStyle = MenuOptionTextStyle.TruncateEnd;
    private IMenuAPI? parent = null;

    public MenuBuilderAPI( ISwiftlyCore core )
    {
        this.core = core;
        configuration.Title = GlobalLocalization.MenuDefaultTitle();
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

    public IMenuBuilderAPI EnableExit()
    {
        configuration.DisableExit = false;
        return this;
    }

    public IMenuBuilderAPI DisableExit()
    {
        configuration.DisableExit = true;
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

    public IMenuBuilderAPI AddExtraButton( KeyBind keyBind, string label, Action<IPlayer, IMenuAPI> action )
    {
        configuration.ExtraButtons.Add(new MenuExtraButton {
            KeyBind = keyBind,
            Label = label,
            Action = action
        });
        return this;
    }

    public IMenuAPI Build()
    {
        var menu = new MenuAPI(core, configuration, keybindOverrides, this/*, parent*/, optionScrollStyle/*, optionTextStyle*/) { Parent = (parent, null) };

        if (options.Count > 0)
        {
            options.ForEach(menu.AddOption);
        }
        else
        {
            menu.AddOption(MenuAPI.defaultOption);
        }

        return menu;
    }
}