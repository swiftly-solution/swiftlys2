using SwiftlyS2.Shared.Menu;

namespace SwiftlyS2.Core.Menu;

internal sealed class MenuInputMethodResolver
{
    private readonly List<IMenuInputMethodSource> sources = [];
    private readonly Lock sourceLock = new();

    public void AddSource( IMenuInputMethodSource source )
    {
        lock (sourceLock)
        {
            sources.Add(source);
            sources.Sort(( left, right ) => right.Priority.CompareTo(left.Priority));
        }
    }

    public void RemoveSource( IMenuInputMethodSource source )
    {
        lock (sourceLock)
        {
            _ = sources.Remove(source);
        }
    }

    public MenuInputMethod Resolve( string menuId, MenuInputMethod codeInputMethod, IReadOnlyList<IMenuInputMethodSource> menuSources )
    {
        foreach (var source in Ordered(menuSources))
        {
            if (source.TryGetInputMethod(menuId, out var inputMethod))
            {
                return inputMethod;
            }
        }

        return codeInputMethod;
    }

    private IEnumerable<IMenuInputMethodSource> Ordered( IReadOnlyList<IMenuInputMethodSource> menuSources )
    {
        List<IMenuInputMethodSource> combined;

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
