using SwiftlyS2.Shared.Menu;

namespace SwiftlyS2.Core.Menu;

internal sealed class MenuKeymap(
    string menuScope,
    MenuActionRegistry actions,
    MenuKeybindResolver resolver,
    IReadOnlyList<IMenuKeybindSource> menuSources ) : IMenuKeymap
{
    public IReadOnlyList<MenuActionDescriptor> Actions => BuildActions();

    public MenuKey GetKey( MenuActionId id )
    {
        foreach (var descriptor in BuildActions())
        {
            if (string.Equals(descriptor.Id.Name, id.Name, StringComparison.OrdinalIgnoreCase))
            {
                return resolver.Resolve(descriptor, menuScope, menuSources);
            }
        }

        return MenuKey.None;
    }

    public bool TryResolve( MenuKey key, out MenuActionId action )
    {
        foreach (var descriptor in BuildActions())
        {
            var bound = resolver.Resolve(descriptor, menuScope, menuSources);

            if (bound != MenuKey.None && (bound & key) != MenuKey.None)
            {
                action = descriptor.Id;
                return true;
            }
        }

        action = default;
        return false;
    }

    private List<MenuActionDescriptor> BuildActions()
    {
        var own = actions.GetScope(menuScope);
        var inherited = actions.GetScope(MenuActions.CoreScope);
        var result = new List<MenuActionDescriptor>(own);

        foreach (var descriptor in inherited)
        {
            if (!own.Any(existing => string.Equals(existing.Id.Name, descriptor.Id.Name, StringComparison.OrdinalIgnoreCase)))
            {
                result.Add(descriptor);
            }
        }

        return result.OrderBy(descriptor => descriptor.Order).ToList();
    }
}
