using SwiftlyS2.Shared;
using Tester.Framework;
using static Tester.Framework.TestContext;

namespace Tester.Sections;

public sealed class ConsoleOutputSection( ISwiftlyCore core ) : Section(core)
{
    public override string Name => "consoleoutput";

    private static string Marker() => "tester-console-" + Guid.NewGuid().ToString("N")[..10];

    public override Task Test( TestContext t )
    {
        var c = Core.ConsoleOutput;

        t.Test("IsFilterEnabled is stable between reads", () => Equal(c.IsFilterEnabled(), c.IsFilterEnabled(), "IsFilterEnabled"));

        t.Test("ToggleFilter flips the state and a second toggle restores it", () =>
        {
            var original = c.IsFilterEnabled();
            try
            {
                c.ToggleFilter();
                Equal(!original, c.IsFilterEnabled(), "after first toggle");
                c.ToggleFilter();
                Equal(original, c.IsFilterEnabled(), "after second toggle");
            }
            finally
            {
                if (c.IsFilterEnabled() != original) c.ToggleFilter();
            }
        });

        t.Test("ReloadFilterConfiguration keeps the on/off state", () =>
        {
            var original = c.IsFilterEnabled();
            c.ReloadFilterConfiguration();
            Equal(original, c.IsFilterEnabled(), "state after reload");
        });

        t.Test("ReloadFilterConfiguration can be repeated", () =>
        {
            for (var i = 0; i < 5; i++) c.ReloadFilterConfiguration();
        });

        t.Test("An unremarkable message is not filtered", () =>
        {
            var m = Marker();
            Expect(!c.NeedsFiltering(m), $"'{m}' needs filtering");
        });

        t.Test("NeedsFiltering is deterministic for the same message", () =>
        {
            var m = Marker();
            Equal(c.NeedsFiltering(m), c.NeedsFiltering(m), "second call");
            Equal(c.NeedsFiltering("Server: shutdown"), c.NeedsFiltering("Server: shutdown"), "fixed message");
        });

        t.Test("NeedsFiltering accepts empty, long, multi-line and unicode text", () =>
        {
            _ = c.NeedsFiltering("");
            _ = c.NeedsFiltering(new string('x', 10_000));
            _ = c.NeedsFiltering("line1\nline2\r\nline3");
            _ = c.NeedsFiltering("zażółć gęślą jaźń テスト \U0001F600");
        });

        t.Test("GetCounterText returns text and is readable repeatedly", () =>
        {
            var a = c.GetCounterText();
            NotNull(a, "counter text");
            Equal(a, c.GetCounterText(), "second read");
        });

        t.Test("WriteToServerConsole accepts normal text", () => c.WriteToServerConsole($"[Tester] console write {Marker()}"));

        t.Test("WriteToServerConsole accepts printf-style and markup-like text", () =>
            c.WriteToServerConsole($"[Tester] {Marker()} 100% %s %d %n {{0}} [red]tag[/] [bold"));

        t.Test("WriteToServerConsole accepts empty, multi-line, long and unicode text", () =>
        {
            c.WriteToServerConsole("");
            c.WriteToServerConsole($"[Tester] {Marker()}\nsecond line\r\nthird line");
            c.WriteToServerConsole($"[Tester] {Marker()} {new string('y', 2_000)}");
            c.WriteToServerConsole($"[Tester] {Marker()} テスト \U0001F600");
        });

        t.Test("WriteToServerConsole works with the filter toggled on and off", () =>
        {
            var original = c.IsFilterEnabled();
            try
            {
                if (!original) c.ToggleFilter();
                c.WriteToServerConsole($"[Tester] {Marker()} filter on");
                c.ToggleFilter();
                c.WriteToServerConsole($"[Tester] {Marker()} filter off");
            }
            finally
            {
                if (c.IsFilterEnabled() != original) c.ToggleFilter();
            }
        });

        t.Test("Concurrent NeedsFiltering and GetCounterText from pool threads", () =>
        {
            var failures = new System.Collections.Concurrent.ConcurrentBag<string>();
            Task.WaitAll(Enumerable.Range(0, 4).Select(w => Task.Run(() =>
            {
                try
                {
                    for (var i = 0; i < 200; i++)
                    {
                        _ = c.NeedsFiltering($"worker {w} message {i}");
                        _ = c.GetCounterText();
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
        var c = Core.ConsoleOutput;
        var message = "tester-profile plain message";
        var longMessage = new string('x', 1_000);

        p.Profile("ConsoleOutput.IsFilterEnabled", () => _ = c.IsFilterEnabled(), 200_000);
        p.Profile("ConsoleOutput.NeedsFiltering (short)", () => _ = c.NeedsFiltering(message), 100_000);
        p.Profile("ConsoleOutput.NeedsFiltering (1 KB)", () => _ = c.NeedsFiltering(longMessage), 50_000);
        p.Profile("ConsoleOutput.GetCounterText", () => _ = c.GetCounterText(), 100_000);
        p.ProfileOnGame("ConsoleOutput.ReloadFilterConfiguration", () => c.ReloadFilterConfiguration(), 50);
        p.ProfileOnGame("ConsoleOutput.WriteToServerConsole (prints a line each)", () => c.WriteToServerConsole("[Tester] profile write"), 100);
        return Task.CompletedTask;
    }
}
