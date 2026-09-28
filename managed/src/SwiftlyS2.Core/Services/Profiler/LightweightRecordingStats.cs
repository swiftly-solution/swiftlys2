namespace SwiftlyS2.Core.Services.Profiler;

internal sealed class LightweightRecordingNode
{
    private const int ReservoirCapacity = 4096;
    private const double TickRateHz = 64.0;
    private const double BudgetMs = 1000.0 / TickRateHz;

    public required string Identifier { get; init; }
    public required string Operation { get; init; }

    private readonly object _lock = new();
    private readonly double[] _reservoir = new double[ReservoirCapacity];
    private readonly Random _random = new();
    private int _reservoirCount;

    public long Count { get; private set; }
    public double TotalMs { get; private set; }
    public double MinMs { get; private set; }
    public double MaxMs { get; private set; }
    public double FirstMs { get; private set; }
    public double LastMs { get; private set; }
    public int ExcBudgetCount { get; private set; }

    private double _mean;
    private double _m2;

    public void Record( double durationMs, double timestampMs )
    {
        lock (_lock)
        {
            Count++;
            TotalMs += durationMs;

            if (Count == 1)
            {
                MinMs = durationMs;
                MaxMs = durationMs;
                FirstMs = timestampMs;
            }
            else
            {
                if (durationMs < MinMs) MinMs = durationMs;
                if (durationMs > MaxMs) MaxMs = durationMs;
            }
            LastMs = timestampMs;

            var delta = durationMs - _mean;
            _mean += delta / Count;
            var delta2 = durationMs - _mean;
            _m2 += delta * delta2;

            if (durationMs > BudgetMs) ExcBudgetCount++;

            if (_reservoirCount < ReservoirCapacity)
            {
                _reservoir[_reservoirCount++] = durationMs;
            }
            else
            {
                var j = _random.NextInt64(Count);
                if (j < ReservoirCapacity) _reservoir[(int)j] = durationMs;
            }
        }
    }

    public LightweightRecordingSnapshot Snapshot()
    {
        lock (_lock)
        {
            var sorted = _reservoir.AsSpan(0, _reservoirCount).ToArray();
            Array.Sort(sorted);

            var stdDev = Count > 0 ? Math.Sqrt(_m2 / Count) : 0.0;

            return new LightweightRecordingSnapshot(
                Identifier,
                Operation,
                Count,
                TotalMs,
                Count > 0 ? TotalMs / Count : 0.0,
                stdDev,
                Percentile(sorted, 50),
                Percentile(sorted, 75),
                Percentile(sorted, 95),
                Percentile(sorted, 99),
                FirstMs,
                LastMs,
                ExcBudgetCount);
        }
    }

    private static double Percentile( double[] sorted, int p )
    {
        if (sorted.Length == 0) return 0.0;
        var idx = Math.Clamp((int)Math.Ceiling(p / 100.0 * sorted.Length) - 1, 0, sorted.Length - 1);
        return sorted[idx];
    }
}

internal readonly record struct LightweightRecordingSnapshot(
    string Identifier,
    string Operation,
    long Count,
    double TotalMs,
    double MeanMs,
    double StdDevMs,
    double P50,
    double P75,
    double P95,
    double P99,
    double FirstMs,
    double LastMs,
    int ExcBudgetCount
);
