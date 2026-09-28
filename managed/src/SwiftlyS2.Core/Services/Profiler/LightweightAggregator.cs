using System.Collections.Concurrent;
using System.Diagnostics;

namespace SwiftlyS2.Core.Services.Profiler;

internal sealed class LightweightAggregator
{
    private readonly ConcurrentDictionary<string, ConcurrentDictionary<string, LightweightRecordingNode>> _groups = new();
    private long _sessionStartTicks = Stopwatch.GetTimestamp();

    public void Record( string identifier, string operation, double durationMs )
    {
        var timestampMs = Stopwatch.GetElapsedTime(_sessionStartTicks).TotalMilliseconds;
        var ops = _groups.GetOrAdd(identifier, static _ => new());
        var node = ops.GetOrAdd(operation, static ( op, ident ) => new LightweightRecordingNode { Identifier = ident, Operation = op }, identifier);
        node.Record(durationMs, timestampMs);
    }

    public double SessionElapsedMs() => Stopwatch.GetElapsedTime(_sessionStartTicks).TotalMilliseconds;

    public Dictionary<string, Dictionary<string, LightweightRecordingSnapshot>> Snapshot()
    {
        var result = new Dictionary<string, Dictionary<string, LightweightRecordingSnapshot>>();
        foreach (var (identifier, ops) in _groups)
        {
            var inner = new Dictionary<string, LightweightRecordingSnapshot>();
            foreach (var (operation, node) in ops)
                inner[operation] = node.Snapshot();
            result[identifier] = inner;
        }
        return result;
    }

    public void Reset()
    {
        _groups.Clear();
        _sessionStartTicks = Stopwatch.GetTimestamp();
    }
}
