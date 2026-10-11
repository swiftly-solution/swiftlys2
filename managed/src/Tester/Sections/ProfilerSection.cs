using System.Collections.Concurrent;
using System.Diagnostics;
using System.Diagnostics.Tracing;
using System.Text.RegularExpressions;
using SwiftlyS2.Shared;
using Tester.Framework;
using static Tester.Framework.TestContext;

namespace Tester.Sections;

public sealed class ProfilerSection( ISwiftlyCore core ) : Section(core)
{
    private const string SourceName = "SwiftlyS2-Profiler";

    public override string Name => "profiler";

    private sealed record Entry( string Event, string Name, double? Duration );

    private sealed class Listener : EventListener
    {
        private readonly ConcurrentQueue<Entry> entries = new();

        protected override void OnEventSourceCreated( EventSource source )
        {
            if (source.Name == SourceName) EnableEvents(source, EventLevel.Informational);
        }

        protected override void OnEventWritten( EventWrittenEventArgs e )
        {
            if (e.EventSource.Name != SourceName || e.Payload is null) return;
            entries.Enqueue(new Entry(e.EventName ?? "", e.Payload[0]?.ToString() ?? "", e.Payload.Count > 1 ? Convert.ToDouble(e.Payload[1]) : null));
        }

        public List<Entry> With( string marker ) => entries.Where(x => x.Name.Contains(marker)).ToList();
    }

    private static string Marker() => "tester-prof-" + Guid.NewGuid().ToString("N")[..10];

    private bool ProfilerIsOn()
    {
        using var probe = new Listener();
        var m = Marker();
        Core.Profiler.RecordTime(m, 1);
        return probe.With(m).Count > 0;
    }

    public override Task Test( TestContext t )
    {
        var pr = Core.Profiler;

        t.Test("Start/Stop/Record never throw, whatever the profiler state", () =>
        {
            var m = Marker();
            pr.StartRecording(m);
            pr.StopRecording(m);
            pr.RecordTime(m, 1.5);
        });

        t.Test("Odd names are accepted (empty, long, unicode, markup, newline)", () =>
        {
            foreach (var name in new[] { "", new string('n', 5_000), "テスト \U0001F600", "[red]x[/] {0} %s", "line1\nline2" })
            {
                pr.StartRecording(name);
                pr.StopRecording(name);
                pr.RecordTime(name, 0.25);
            }
        });

        t.Test("Stop without Start never throws", () => pr.StopRecording(Marker()));

        t.Test("RecordTime accepts zero, negative, huge and non-finite durations", () =>
        {
            var m = Marker();
            foreach (var d in new[] { 0d, -1d, double.MaxValue, double.NaN, double.PositiveInfinity })
                pr.RecordTime(m, d);
        });

        t.Test("Concurrent use from pool threads never throws", () =>
        {
            var failures = new ConcurrentBag<string>();
            Task.WaitAll(Enumerable.Range(0, 8).Select(w => Task.Run(() =>
            {
                try
                {
                    for (var i = 0; i < 500; i++)
                    {
                        var m = $"tester-prof-conc-{w}-{i % 7}";
                        pr.StartRecording(m);
                        pr.RecordTime(m, i);
                        pr.StopRecording(m);
                    }
                }
                catch (Exception e) { failures.Add($"worker {w}: {e.Message}"); }
            })).ToArray());
            Expect(failures.IsEmpty, string.Join("; ", failures));
        });

        t.Test("Start then Stop emits a start and a stop event with a duration", () =>
        {
            if (!ProfilerIsOn()) Skip("profiler is disabled (sw profiler enable 1)");
            using var l = new Listener();
            var m = Marker();
            pr.StartRecording(m);
            Thread.Sleep(30);
            pr.StopRecording(m);
            var e = l.With(m);
            Equal(2, e.Count, "event count");
            Equal("RecordingStart", e[0].Event, "first event");
            Equal("RecordingStop", e[1].Event, "second event");
            Expect(e[1].Duration is >= 25 and < 1000, $"duration {e[1].Duration} ms for a 30 ms sleep");
        });

        t.Test("Recorded names are prefixed with the plugin identifier", () =>
        {
            if (!ProfilerIsOn()) Skip("profiler is disabled (sw profiler enable 1)");
            using var l = new Listener();
            var m = Marker();
            pr.RecordTime(m, 1);
            var name = l.With(m).Single().Name;
            Expect(Regex.IsMatch(name, @"^\[.+\] " + Regex.Escape(m) + "$"), $"name '{name}'");
        });

        t.Test("RecordTime emits the given duration unchanged", () =>
        {
            if (!ProfilerIsOn()) Skip("profiler is disabled (sw profiler enable 1)");
            using var l = new Listener();
            var m = Marker();
            pr.RecordTime(m, 12.5);
            var e = l.With(m).Single();
            Equal("RecordTime", e.Event, "event");
            Equal(12.5, e.Duration ?? double.NaN, "duration");
        });

        t.Test("Stop without Start emits nothing", () =>
        {
            if (!ProfilerIsOn()) Skip("profiler is disabled (sw profiler enable 1)");
            using var l = new Listener();
            var m = Marker();
            pr.StopRecording(m);
            Equal(0, l.With(m).Count, "events");
        });

        t.Test("A second Stop after Stop emits nothing", () =>
        {
            if (!ProfilerIsOn()) Skip("profiler is disabled (sw profiler enable 1)");
            using var l = new Listener();
            var m = Marker();
            pr.StartRecording(m);
            pr.StopRecording(m);
            pr.StopRecording(m);
            Equal(1, l.With(m).Count(x => x.Event == "RecordingStop"), "stop events");
        });

        t.Test("Starting the same name twice restarts the timer", () =>
        {
            if (!ProfilerIsOn()) Skip("profiler is disabled (sw profiler enable 1)");
            using var l = new Listener();
            var m = Marker();
            pr.StartRecording(m);
            Thread.Sleep(60);
            pr.StartRecording(m);
            Thread.Sleep(10);
            pr.StopRecording(m);
            var stop = l.With(m).Single(x => x.Event == "RecordingStop");
            Expect(stop.Duration is >= 8 and < 50, $"duration {stop.Duration} ms (expected the second Start to win)");
        });

        t.Test("Overlapping recordings with different names are timed independently", () =>
        {
            if (!ProfilerIsOn()) Skip("profiler is disabled (sw profiler enable 1)");
            using var l = new Listener();
            var a = Marker();
            var b = Marker();
            pr.StartRecording(a);
            Thread.Sleep(40);
            pr.StartRecording(b);
            Thread.Sleep(10);
            pr.StopRecording(b);
            pr.StopRecording(a);
            var da = l.With(a).Single(x => x.Event == "RecordingStop").Duration;
            var db = l.With(b).Single(x => x.Event == "RecordingStop").Duration;
            Expect(da > db, $"outer {da} ms should be longer than inner {db} ms");
            Expect(db is >= 8 and < 40, $"inner {db} ms");
        });

        t.Test("Recording times a span measured by the caller within tolerance", () =>
        {
            if (!ProfilerIsOn()) Skip("profiler is disabled (sw profiler enable 1)");
            using var l = new Listener();
            var m = Marker();
            var sw = Stopwatch.StartNew();
            pr.StartRecording(m);
            Thread.Sleep(50);
            pr.StopRecording(m);
            sw.Stop();
            var d = l.With(m).Single(x => x.Event == "RecordingStop").Duration ?? -1;
            Expect(d <= sw.Elapsed.TotalMilliseconds + 1 && d >= 45, $"reported {d} ms, measured {sw.Elapsed.TotalMilliseconds:F1} ms");
        });

        t.Test("Concurrent recordings emit one start and one stop each", () =>
        {
            if (!ProfilerIsOn()) Skip("profiler is disabled (sw profiler enable 1)");
            using var l = new Listener();
            var m = Marker();
            Task.WaitAll(Enumerable.Range(0, 8).Select(w => Task.Run(() =>
            {
                for (var i = 0; i < 50; i++)
                {
                    var name = $"{m}-{w}-{i}";
                    pr.StartRecording(name);
                    pr.StopRecording(name);
                }
            })).ToArray());
            var e = l.With(m);
            Equal(400, e.Count(x => x.Event == "RecordingStart"), "starts");
            Equal(400, e.Count(x => x.Event == "RecordingStop"), "stops");
        });

        return Task.CompletedTask;
    }

    public override bool ProfileInBackground => true;

    public override Task Profile( ProfileContext p )
    {
        var pr = Core.Profiler;
        var state = ProfilerIsOn() ? "profiler on" : "profiler off";

        p.Profile($"Profiler.StartRecording + StopRecording ({state})", () => { pr.StartRecording("tester-profile"); pr.StopRecording("tester-profile"); }, 100_000);
        p.Profile($"Profiler.RecordTime ({state})", () => pr.RecordTime("tester-profile", 1.0), 200_000);
        p.Profile($"Profiler.StopRecording without Start ({state})", () => pr.StopRecording("tester-profile-none"), 200_000);
        return Task.CompletedTask;
    }
}
