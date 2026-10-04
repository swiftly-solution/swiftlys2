using SwiftlyS2.Shared.Menu;

namespace SwiftlyS2.Core.Menu;

internal sealed class MenuKeybindResolver
{
    private readonly Lock sourceLock = new();

    private volatile IMenuKeybindSource[] sources = [];
    private volatile int version;
    public int Version => version;

    public void AddSource( IMenuKeybindSource source )
    {
        lock (sourceLock)
        {
            var next = new List<IMenuKeybindSource>(sources) { source };
            next.Sort(( left, right ) => right.Priority.CompareTo(left.Priority));
            sources = [.. next];
            version++;
        }
    }

    public void RemoveSource( IMenuKeybindSource source )
    {
        lock (sourceLock)
        {
            var next = new List<IMenuKeybindSource>(sources);

            if (next.Remove(source))
            {
                sources = [.. next];
                version++;
            }
        }
    }

    public IMenuKeybindSource[] Merge( IReadOnlyList<IMenuKeybindSource> menuSources )
    {
        var global = sources;

        if (menuSources.Count == 0)
        {
            return global;
        }

        var combined = new List<IMenuKeybindSource>(global.Length + menuSources.Count);
        combined.AddRange(global);
        combined.AddRange(menuSources);
        combined.Sort(( left, right ) => right.Priority.CompareTo(left.Priority));

        return [.. combined];
    }

    public MenuKey Resolve( MenuActionDescriptor descriptor, string menuScope, IMenuKeybindSource[] ordered )
    {
        var scoped = new MenuActionId(menuScope, descriptor.Id.Name);

        foreach (var source in ordered)
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
}
