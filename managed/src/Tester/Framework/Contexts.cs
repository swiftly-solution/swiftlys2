using System.Diagnostics;
using SwiftlyS2.Shared;
using SwiftlyS2.Shared.Players;

namespace Tester.Framework;

public sealed class SkipException( string reason ) : Exception(reason);

public abstract class Ctx( ISwiftlyCore core, IPlayer caller )
{
    private readonly List<string> _lines = [];

    public ISwiftlyCore Core { get; } = core;

    public IPlayer Caller { get; } = caller;

    internal IReadOnlyList<string> Lines => _lines;

    internal Action<string>? Live { get; set; }

    protected void Add( string line )
    {
        _lines.Add(line);
        Live?.Invoke(line);
    }

    private readonly List<(string Name, string? Detail)> _failures = [];

    internal IReadOnlyList<(string Name, string? Detail)> Failures => _failures;

    protected void AddFailure( string name, string? detail ) => _failures.Add((name, detail));

    public Task NextTick()
    {
        var tcs = new TaskCompletionSource();
        Core.Scheduler.NextTick(() => tcs.SetResult());
        return tcs.Task;
    }

    public async Task Ticks( int count )
    {
        for (var i = 0; i < count; i++) await NextTick();
    }

    public async Task<bool> Until( Func<bool> condition, int maxTicks = 128 )
    {
        for (var i = 0; i < maxTicks; i++)
        {
            if (condition()) return true;
            await NextTick();
        }
        return condition();
    }
}

public sealed class TestContext( ISwiftlyCore core, IPlayer caller ) : Ctx(core, caller)
{
    public int Passed { get; private set; }
    public int Failed { get; private set; }
    public int Skipped { get; private set; }

    public static void Expect( bool condition, string message = "expectation failed" )
    {
        if (!condition) throw new Exception(message);
    }

    public static void Equal<T>( T expected, T actual, string? what = null )
    {
        if (!EqualityComparer<T>.Default.Equals(expected, actual))
            throw new Exception($"{what ?? "value"}: expected <{expected}> but was <{actual}>");
    }

    public static void NotNull( object? value, string? what = null )
    {
        if (value is null) throw new Exception($"{what ?? "value"} was null");
    }

    public static void Throws<TEx>( Action body ) where TEx : Exception
    {
        try { body(); }
        catch (TEx) { return; }
        catch (Exception e) { throw new Exception($"expected {typeof(TEx).Name} but got {e.GetType().Name}: {e.Message}"); }
        throw new Exception($"expected {typeof(TEx).Name} but nothing was thrown");
    }

    public static void Skip( string reason ) => throw new SkipException(reason);

    public void Test( string name, Action body )
    {
        var t0 = Stopwatch.GetTimestamp();
        try { body(); Record("PASS", name, t0, null); Passed++; }
        catch (SkipException s) { Record("SKIP", name, t0, s.Message); Skipped++; }
        catch (Exception e) { Record("FAIL", name, t0, e.Message); AddFailure(name, e.Message); Failed++; }
    }

    public async Task TestAsync( string name, Func<Task> body )
    {
        var t0 = Stopwatch.GetTimestamp();
        try { await body(); Record("PASS", name, t0, null); Passed++; }
        catch (SkipException s) { Record("SKIP", name, t0, s.Message); Skipped++; }
        catch (Exception e) { Record("FAIL", name, t0, e.Message); AddFailure(name, e.Message); Failed++; }
    }

    private void Record( string status, string name, long t0, string? detail )
        => Add($"  {status} {name} ({Stopwatch.GetElapsedTime(t0).TotalMilliseconds:F2} ms){(detail is null ? "" : " - " + detail)}");
}

public sealed class ProfileContext( ISwiftlyCore core, IPlayer caller ) : Ctx(core, caller)
{
    public int Count { get; private set; }

    public void Profile( string name, Action body, int iterations = 10_000 )
    {
        try
        {
            const int batches = 5;
            var per = Math.Max(1, iterations / batches);
            for (var i = 0; i < Math.Min(per, 1000); i++) body();

            var ns = new double[batches];
            var bytes = new double[batches];
            for (var b = 0; b < batches; b++)
            {
                var a0 = GC.GetAllocatedBytesForCurrentThread();
                var t0 = Stopwatch.GetTimestamp();
                for (var i = 0; i < per; i++) body();
                ns[b] = Stopwatch.GetElapsedTime(t0).TotalNanoseconds / per;
                bytes[b] = (double)(GC.GetAllocatedBytesForCurrentThread() - a0) / per;
            }
            Array.Sort(ns);
            Array.Sort(bytes);
            Add($"  {name,-48} {ns[batches / 2],10:F1} ns/op {bytes[batches / 2],8:F1} B/op {per * batches,8} iters");
            Count++;
        }
        catch (Exception e)
        {
            Add($"  {name,-48} ERROR {e.Message}");
            AddFailure(name, e.Message);
        }
    }

    /// <summary>
    /// For calls of unknown cost (native scans). Times one call, then repeats only as many as fit in
    /// <paramref name="budgetMs"/> (at least 1, at most <paramref name="maxIterations"/>), so a slow call cannot
    /// stall the game thread for long. The first call is included in the average.
    /// </summary>
    public void ProfileBudget( string name, Action body, int maxIterations = 1_000, int budgetMs = 500 )
    {
        try
        {
            var a0 = GC.GetAllocatedBytesForCurrentThread();
            var t0 = Stopwatch.GetTimestamp();
            body();
            var firstMs = Stopwatch.GetElapsedTime(t0).TotalMilliseconds;
            var n = 1;
            var limit = (int)Math.Clamp(budgetMs / Math.Max(firstMs, 0.001), 1, maxIterations);
            for (; n < limit; n++) body();
            var elapsed = Stopwatch.GetElapsedTime(t0);
            var bytes = (double)(GC.GetAllocatedBytesForCurrentThread() - a0) / n;
            Add($"  {name,-48} {elapsed.TotalNanoseconds / n,10:F1} ns/op {bytes,8:F1} B/op {n,8} iters (budgeted, first call {firstMs:F2} ms)");
            Count++;
        }
        catch (Exception e)
        {
            Add($"  {name,-48} ERROR {e.Message}");
            AddFailure(name, e.Message);
        }
    }

    public T OnGame<T>( Func<T> body )
    {
        if (Core.IsGameThread) return body();
        T result = default!;
        Exception? error = null;
        using var done = new ManualResetEventSlim();
        Core.Scheduler.NextTick(() =>
        {
            try { result = body(); }
            catch (Exception e) { error = e; }
            finally { done.Set(); }
        });
        done.Wait();
        return error is null ? result : throw new InvalidOperationException("game-thread call failed: " + error.Message, error);
    }

    public void OnGame( Action body ) => OnGame<object?>(() => { body(); return null; });

    public void ProfileOnGame( string name, Action body, int iterations = 10_000, double sliceMs = 3 )
    {
        if (Core.IsGameThread)
        {
            Profile(name, body, iterations);
            return;
        }

        try
        {
            const int batches = 5;
            var per = Math.Max(1, iterations / batches);
            var warmup = Math.Min(per, 1000);
            var ns = new double[batches];
            var bytes = new double[batches];
            var phase = -1;
            var done = 0;
            var chunk = 1;
            double elapsedNs = 0, allocated = 0;
            Exception? error = null;
            using var finished = new ManualResetEventSlim();

            void Slice()
            {
                try
                {
                    var start = Stopwatch.GetTimestamp();
                    while (true)
                    {
                        var target = phase < 0 ? warmup : per;
                        var n = Math.Min(chunk, target - done);
                        var a0 = GC.GetAllocatedBytesForCurrentThread();
                        var t0 = Stopwatch.GetTimestamp();
                        for (var i = 0; i < n; i++) body();
                        var dt = Stopwatch.GetElapsedTime(t0);
                        done += n;
                        if (phase >= 0)
                        {
                            elapsedNs += dt.TotalNanoseconds;
                            allocated += GC.GetAllocatedBytesForCurrentThread() - a0;
                        }

                        if (dt.TotalMilliseconds > sliceMs / 2 && chunk > 1) chunk = Math.Max(1, chunk / 2);
                        else if (dt.TotalMilliseconds < sliceMs / 16 && chunk < 4096) chunk *= 2;

                        if (done >= target)
                        {
                            if (phase >= 0)
                            {
                                ns[phase] = elapsedNs / per;
                                bytes[phase] = allocated / per;
                                elapsedNs = 0;
                                allocated = 0;
                            }
                            phase++;
                            done = 0;
                            if (phase >= batches)
                            {
                                finished.Set();
                                return;
                            }
                        }

                        if (Stopwatch.GetElapsedTime(start).TotalMilliseconds >= sliceMs)
                        {
                            Core.Scheduler.NextTick(Slice);
                            return;
                        }
                    }
                }
                catch (Exception e)
                {
                    error = e;
                    finished.Set();
                }
            }

            Core.Scheduler.NextTick(Slice);
            finished.Wait();
            if (error is not null) throw error;

            Array.Sort(ns);
            Array.Sort(bytes);
            Add($"  {name,-48} {ns[batches / 2],10:F1} ns/op {bytes[batches / 2],8:F1} B/op {per * batches,8} iters (sliced on game thread)");
            Count++;
        }
        catch (Exception e)
        {
            Add($"  {name,-48} ERROR {e.Message}");
            AddFailure(name, e.Message);
        }
    }

    public async Task ProfileAsync( string name, Func<Task> body, int iterations = 500 )
    {
        try
        {
            const int batches = 5;
            var per = Math.Max(1, iterations / batches);
            for (var i = 0; i < Math.Min(per, 50); i++) await body();

            var ns = new double[batches];
            for (var b = 0; b < batches; b++)
            {
                var t0 = Stopwatch.GetTimestamp();
                for (var i = 0; i < per; i++) await body();
                ns[b] = Stopwatch.GetElapsedTime(t0).TotalNanoseconds / per;
            }
            Array.Sort(ns);
            Add($"  {name,-48} {ns[batches / 2],10:F1} ns/op {"-",8} B/op {per * batches,8} iters");
            Count++;
        }
        catch (Exception e)
        {
            Add($"  {name,-48} ERROR {e.Message}");
            AddFailure(name, e.Message);
        }
    }
}
