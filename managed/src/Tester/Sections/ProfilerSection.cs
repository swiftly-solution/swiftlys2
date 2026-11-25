using System.Collections.Concurrent;
using SwiftlyS2.Shared;
using Tester.Framework;
using static Tester.Framework.TestContext;

namespace Tester.Sections;

public sealed class ProfilerSection( ISwiftlyCore core ) : Section(core)
{
    public override string Name => "profiler";

    private static string Marker() => "tester-prof-" + Guid.NewGuid().ToString("N")[..10];

    public override Task Test( TestContext t )
    {
        var pr = Core.Profiler;

        t.Test("Start/Stop never throw, whatever the profiler state", () =>
        {
            var m = Marker();
            pr.StartRecording(m);
            pr.StopRecording(m);
        });

        t.Test("Odd names are accepted (empty, long, unicode, markup, newline)", () =>
        {
            foreach (var name in new[] { "", new string('n', 5_000), "テスト \U0001F600", "[red]x[/] {0} %s", "line1\nline2" })
            {
                pr.StartRecording(name);
                pr.StopRecording(name);
            }
        });

        t.Test("Stop without Start never throws", () => pr.StopRecording(Marker()));

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
                        pr.StopRecording(m);
                    }
                }
                catch (Exception e) { failures.Add($"worker {w}: {e.Message}"); }
            })).ToArray());
            Expect(failures.IsEmpty, string.Join("; ", failures));
        });

        return Task.CompletedTask;
    }

    public override bool ProfileInBackground => true;

    public override Task Profile( ProfileContext p )
    {
        var pr = Core.Profiler;

        p.Profile("Profiler.StartRecording + StopRecording", () => { pr.StartRecording("tester-profile"); pr.StopRecording("tester-profile"); }, 100_000);
        p.Profile("Profiler.StopRecording without Start", () => pr.StopRecording("tester-profile-none"), 200_000);
        return Task.CompletedTask;
    }
}
