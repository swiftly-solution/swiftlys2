using System.Collections.Concurrent;
using System.Diagnostics;
using System.Reflection;

namespace SwiftlyS2.Core.Services.Profiler;

internal static class ProfilerPatches
{
    private static readonly ConcurrentDictionary<Type, MethodInfo?> ValueTaskAsTaskCache = new();

    public static void Prefix( out long __state )
    {
        __state = Stopwatch.GetTimestamp();
    }

    public static void FinalizerVoid( MethodBase __originalMethod, long __state )
    {
        LightweightProfilerService.Current?.RecordCompletion(__originalMethod, __state, null);
    }

    public static void FinalizerResult( MethodBase __originalMethod, long __state, object? __result )
    {
        LightweightProfilerService.Current?.RecordCompletion(__originalMethod, __state, __result);
    }

    internal static MethodInfo? GetValueTaskAsTask( Type resultType )
    {
        return ValueTaskAsTaskCache.GetOrAdd(resultType, static t =>
            t.IsGenericType && t.GetGenericTypeDefinition() == typeof(ValueTask<>) ? t.GetMethod("AsTask") : null);
    }
}
