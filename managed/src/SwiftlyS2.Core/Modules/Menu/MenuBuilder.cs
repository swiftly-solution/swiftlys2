using SwiftlyS2.Shared.Menu;
using SwiftlyS2.Shared.Menu.Components;

namespace SwiftlyS2.Core.Menu;

internal sealed class MenuBuilder(
    string menuId,
    string owner,
    MenuRuntime runtime,
    MenuActionRegistry actions,
    MenuRendererRegistry renderers,
    MenuKeybindResolver resolver,
    IMenuActionRegistry ownedActions ) : IMenuBuilder
{
    private readonly List<MenuActionDescriptor> declaredActions = [];
    private readonly List<IMenuKeybindSource> keybindSources = [];
    private readonly List<IMenuComponent> header = [];
    private readonly List<IMenuComponent> body = [];
    private readonly List<IMenuComponent> footer = [];

    private string rendererId = MenuRendererIds.CenterHtml;
    private IMenu? parent;
    private int itemsPerPage = 5;

    public IMenuBuilder WithRenderer( string rendererId )
    {
        this.rendererId = rendererId;
        return this;
    }

    public IMenuBuilder WithParent( IMenu parent )
    {
        this.parent = parent;
        return this;
    }

    public IMenuBuilder WithItemsPerPage( int itemsPerPage )
    {
        this.itemsPerPage = Math.Max(1, itemsPerPage);
        return this;
    }

    public IMenuBuilder WithAction( MenuActionDescriptor descriptor )
    {
        declaredActions.Add(descriptor);
        return this;
    }

    public IMenuBuilder WithAction( string name, MenuKey defaultKey, string? label = null )
    {
        return WithAction(new MenuActionDescriptor {
            Id = new MenuActionId(menuId, name),
            DefaultKey = defaultKey,
            Label = label
        });
    }

    public IMenuBuilder WithKeybindSource( IMenuKeybindSource source )
    {
        keybindSources.Add(source);
        return this;
    }

    public IMenuBuilder WithTitle( string title )
    {
        header.Add(new TitleComponent(title));
        return this;
    }

    public IMenuBuilder AddHeader( IMenuComponent component )
    {
        header.Add(component);
        return this;
    }

    public IMenuBuilder Add( IMenuComponent component )
    {
        body.Add(component);
        return this;
    }

    public IMenuBuilder AddFooter( IMenuComponent component )
    {
        footer.Add(component);
        return this;
    }

    public IMenu Build()
    {
        if (!renderers.TryGet(rendererId, out var renderer))
        {
            throw new InvalidOperationException(
                $"Menu '{menuId}' requested renderer '{rendererId}', which is not registered. Registered renderers: {string.Join(", ", renderers.RendererIds)}.");
        }

        foreach (var descriptor in declaredActions)
        {
            _ = ownedActions.Register(descriptor);
        }

        var keymap = new MenuKeymap(menuId, actions, resolver, keybindSources);
        var menu = new MenuInstance(menuId, owner, runtime, renderer, keymap, parent, itemsPerPage);

        foreach (var component in header)
        {
            menu.Add(MenuRegion.Header, component);
        }

        foreach (var component in body)
        {
            menu.Add(MenuRegion.Body, component);
        }

        foreach (var component in footer)
        {
            menu.Add(MenuRegion.Footer, component);
        }

        return menu;
    }
}
