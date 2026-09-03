namespace SwiftlyS2.Shared.Schemas;

internal static class SchemaWrapperCache
{
    internal static int Generation;

    public static void ClearAll() => Interlocked.Increment(ref Generation);
}

internal static class SchemaWrapperCache<T> where T : ISchemaClass<T>
{
    [ThreadStatic] private static Dictionary<nint, T>? _cache;
    [ThreadStatic] private static int _generation;

    private const int MaxEntries = 16384;

    public static T Get( nint ptr )
    {
        var gen = Volatile.Read(ref SchemaWrapperCache.Generation);
        var cache = _cache;

        if (cache is null)
        {
            cache = _cache = new(256);
            _generation = gen;
        }
        else if (_generation != gen)
        {
            cache.Clear();
            _generation = gen;
        }

        if (cache.TryGetValue(ptr, out var wrapper))
        {
            return wrapper;
        }

        if (cache.Count >= MaxEntries)
        {
            cache.Clear();
        }

        wrapper = T.From(ptr);
        cache[ptr] = wrapper;
        return wrapper;
    }
}
