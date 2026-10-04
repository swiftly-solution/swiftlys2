using SwiftlyS2.Shared.Menu;

namespace SwiftlyS2.Core.Menu;

internal sealed class MenuKeymap(
    string menuScope,
    MenuActionRegistry actions,
    MenuKeybindResolver resolver,
    IReadOnlyList<IMenuKeybindSource> menuSources ) : IMenuKeymap
{
    private readonly Lock cacheLock = new();

    private Snapshot? snapshot;

    public IReadOnlyList<MenuActionDescriptor> Actions => GetSnapshot().Actions;

    public void ResolveAll( Span<MenuKey> keys )
    {
        var current = GetSnapshot();

        for (var index = 0; index < current.Actions.Count; index++)
        {
            keys[index] = resolver.Resolve(current.Actions[index], menuScope, current.Sources);
        }
    }

    public MenuKey GetKey( MenuActionId id )
    {
        var current = GetSnapshot();

        foreach (var descriptor in current.Actions)
        {
            if (string.Equals(descriptor.Id.Name, id.Name, StringComparison.OrdinalIgnoreCase))
            {
                return resolver.Resolve(descriptor, menuScope, current.Sources);
            }
        }

        return MenuKey.None;
    }

    public bool TryResolve( MenuKey key, out MenuActionId action )
    {
        var current = GetSnapshot();

        foreach (var descriptor in current.Actions)
        {
            var bound = resolver.Resolve(descriptor, menuScope, current.Sources);

            if (bound != MenuKey.None && (bound & key) != MenuKey.None)
            {
                action = descriptor.Id;
                return true;
            }
        }

        action = default;
        return false;
    }

    private Snapshot GetSnapshot()
    {
        var actionVersion = actions.Version;
        var sourceVersion = resolver.Version;

        if (snapshot is { } current && current.ActionVersion == actionVersion && current.SourceVersion == sourceVersion)
        {
            return current;
        }

        lock (cacheLock)
        {
            if (snapshot is { } existing && existing.ActionVersion == actionVersion && existing.SourceVersion == sourceVersion)
            {
                return existing;
            }

            var own = actions.GetScope(menuScope);
            var inherited = actions.GetScope(MenuActions.CoreScope);
            var merged = new List<MenuActionDescriptor>(own);

            foreach (var descriptor in inherited)
            {
                if (!own.Any(existingDescriptor => string.Equals(existingDescriptor.Id.Name, descriptor.Id.Name, StringComparison.OrdinalIgnoreCase)))
                {
                    merged.Add(descriptor);
                }
            }

            var built = new Snapshot(
                actionVersion,
                sourceVersion,
                merged.OrderBy(descriptor => descriptor.Order).ToList(),
                resolver.Merge(menuSources));

            snapshot = built;
            return built;
        }
    }

    private sealed record Snapshot(
        int ActionVersion,
        int SourceVersion,
        IReadOnlyList<MenuActionDescriptor> Actions,
        IMenuKeybindSource[] Sources );
}
