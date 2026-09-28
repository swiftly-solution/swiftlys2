using SwiftlyS2.Shared.Menu;

namespace SwiftlyS2.Core.Menu;

internal sealed class MenuRendererResolver
{
    private readonly List<IMenuRendererSource> sources = [];
    private readonly Lock sourceLock = new();

    public void AddSource( IMenuRendererSource source )
    {
        lock (sourceLock)
        {
            sources.Add(source);
            sources.Sort(( left, right ) => right.Priority.CompareTo(left.Priority));
        }
    }

    public void RemoveSource( IMenuRendererSource source )
    {
        lock (sourceLock)
        {
            _ = sources.Remove(source);
        }
    }

    public string Resolve( string menuId, string codeRendererId, IReadOnlyList<IMenuRendererSource> menuSources )
    {
        foreach (var source in Ordered(menuSources))
        {
            if (source.TryGetRenderer(menuId, out var rendererId))
            {
                return rendererId;
            }
        }

        return codeRendererId;
    }

    private IEnumerable<IMenuRendererSource> Ordered( IReadOnlyList<IMenuRendererSource> menuSources )
    {
        List<IMenuRendererSource> combined;

        lock (sourceLock)
        {
            combined = new(sources.Count + menuSources.Count);
            combined.AddRange(sources);
        }

        combined.AddRange(menuSources);
        combined.Sort(( left, right ) => right.Priority.CompareTo(left.Priority));

        return combined;
    }
}
