using SwiftlyS2.Shared.Menu;

namespace SwiftlyS2.Core.Menu;

internal sealed class MenuKeybindResolver
{
    private readonly List<IMenuKeybindSource> sources = [];
    private readonly Lock sourceLock = new();

    public void AddSource( IMenuKeybindSource source )
    {
        lock (sourceLock)
        {
            sources.Add(source);
            sources.Sort(( left, right ) => right.Priority.CompareTo(left.Priority));
        }
    }

    public void RemoveSource( IMenuKeybindSource source )
    {
        lock (sourceLock)
        {
            _ = sources.Remove(source);
        }
    }

    public MenuKey Resolve( MenuActionDescriptor descriptor, string menuScope, IReadOnlyList<IMenuKeybindSource> menuSources )
    {
        var scoped = new MenuActionId(menuScope, descriptor.Id.Name);

        foreach (var source in Ordered(menuSources))
        {
            if (source.TryGetKey(scoped, out var scopedKey) && scopedKey != MenuKey.None)
            {
                return scopedKey;
            }

            if (source.TryGetKey(descriptor.Id, out var key) && key != MenuKey.None)
            {
                return key;
            }
        }

        return descriptor.DefaultKey;
    }

    private IEnumerable<IMenuKeybindSource> Ordered( IReadOnlyList<IMenuKeybindSource> menuSources )
    {
        List<IMenuKeybindSource> combined;

        lock (sourceLock)
        {
            combined = new List<IMenuKeybindSource>(sources.Count + menuSources.Count);
            combined.AddRange(sources);
        }

        combined.AddRange(menuSources);
        combined.Sort(( left, right ) => right.Priority.CompareTo(left.Priority));

        return combined;
    }
}
