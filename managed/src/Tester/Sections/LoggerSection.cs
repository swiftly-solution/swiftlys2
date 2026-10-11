using Microsoft.Extensions.Logging;
using SwiftlyS2.Shared;
using Tester.Framework;
using Tester.Sections.Support;
using static Tester.Framework.TestContext;

namespace Tester.Sections;

public sealed class LoggerSection( ISwiftlyCore core ) : Section(core)
{
    public override string Name => "logger";

    private ILogger Log => Core.Logger;

    private static string Marker() => "tester-" + Guid.NewGuid().ToString("N")[..10];

    private LogCapture Capture()
    {
        LogCapture.Attach(Core.LoggerFactory);
        return LogCapture.Instance;
    }

    public override Task Test( TestContext t )
    {
        t.Test("Core.Logger exists and is stable", () =>
        {
            NotNull(Core.Logger, "Logger");
            Expect(ReferenceEquals(Core.Logger, Core.Logger), "different instances");
        });

        t.Test("IsEnabled(None) is false", () => Expect(!Log.IsEnabled(LogLevel.None), "None enabled"));

        t.Test("IsEnabled is monotonic across levels", () =>
        {
            var levels = new[] { LogLevel.Trace, LogLevel.Debug, LogLevel.Information, LogLevel.Warning, LogLevel.Error, LogLevel.Critical };
            for (var i = 0; i < levels.Length - 1; i++)
                if (Log.IsEnabled(levels[i])) Expect(Log.IsEnabled(levels[i + 1]), $"{levels[i]} enabled but {levels[i + 1]} is not");
        });

        t.Test("Error and Critical are enabled", () =>
            Expect(Log.IsEnabled(LogLevel.Error) && Log.IsEnabled(LogLevel.Critical), "error levels disabled"));

        t.Test("Every level can be logged without throwing", () =>
        {
            var m = Marker();
            Log.LogTrace("{Marker} trace", m);
            Log.LogDebug("{Marker} debug", m);
            Log.LogInformation("{Marker} information", m);
            Log.LogWarning("{Marker} warning", m);
            Log.LogError("{Marker} error", m);
            Log.LogCritical("{Marker} critical", m);
        });

        t.Test("Structured arguments are formatted", () =>
        {
            var cap = Capture();
            var m = Marker();
            Log.LogInformation("{Marker} X={X} Y={Y}", m, 5, "abc");
            var e = cap.With(m).SingleOrDefault();
            NotNull(e, "entry");
            Equal($"{m} X=5 Y=abc", e!.Message, "message");
            Equal(LogLevel.Information, e.Level, "level");
        });

        t.Test("Level is preserved for each severity", () =>
        {
            var cap = Capture();
            var m = Marker();
            Log.LogWarning("{M} w", m);
            Log.LogError("{M} e", m);
            Log.LogCritical("{M} c", m);
            var levels = cap.With(m).Select(e => e.Level).ToList();
            Expect(levels.SequenceEqual([LogLevel.Warning, LogLevel.Error, LogLevel.Critical]), $"got {string.Join(",", levels)}");
        });

        t.Test("Exception overload delivers the same exception", () =>
        {
            var cap = Capture();
            var m = Marker();
            var ex = new InvalidOperationException("boom");
            Log.LogError(ex, "{M} failed", m);
            var e = cap.With(m).SingleOrDefault();
            NotNull(e, "entry");
            Expect(ReferenceEquals(ex, e!.Exception), "exception instance differs");
        });

        t.Test("EventId is delivered", () =>
        {
            var cap = Capture();
            var m = Marker();
            Log.LogInformation(new EventId(77, "tester"), "{M} with id", m);
            var e = cap.With(m).SingleOrDefault();
            NotNull(e, "entry");
            Equal(77, e!.EventId.Id, "EventId");
        });

        t.Test("Category is the plugin type", () =>
        {
            var cap = Capture();
            var m = Marker();
            Log.LogInformation("{M} category", m);
            var e = cap.With(m).SingleOrDefault();
            NotNull(e, "entry");
            Equal(typeof(TesterPlugin).FullName!, e!.Category, "category");
        });

        t.Test("Null argument is rendered as (null)", () =>
        {
            var cap = Capture();
            var m = Marker();
            Log.LogInformation("{M} value={V}", m, (object?)null);
            Equal($"{m} value=(null)", cap.With(m).Single().Message, "message");
        });

        t.Test("Braces without arguments do not throw", () =>
        {
            var m = Marker();
            Log.LogInformation(m + " {{escaped}} and {0}");
            Log.LogInformation(m + " text {");
        });

        t.Test("Console markup characters in a message do not throw", () =>
        {
            var m = Marker();
            Log.LogInformation("{M} [red]unclosed [/] [[ ]] [/red] [bold", m);
        });

        t.Test("Console markup characters in an exception message do not throw", () =>
        {
            var m = Marker();
            Log.LogError(new Exception("bad [markup] [/] [red tag"), "{M} failed", m);
        });

        t.Test("Multi-line, long and empty messages do not throw", () =>
        {
            var m = Marker();
            Log.LogInformation("{M}\nline two\r\nline three\n\n", m);
            Log.LogInformation("{M} {Long}", m, new string('x', 20_000));
            Log.LogInformation("");
        });

        t.Test("BeginScope returns a disposable, nested scopes dispose cleanly", () =>
        {
            using var a = Log.BeginScope("outer");
            using var b = Log.BeginScope(new Dictionary<string, object> { ["k"] = 1 });
            Log.LogInformation("{M} inside scopes", Marker());
        });

        t.Test("Scope values reach providers that support scopes", () =>
        {
            var cap = Capture();
            if (!cap.ReceivedScopeProvider) Skip("factory did not hand out a scope provider");
            var m = Marker();
            using (Log.BeginScope("scope-" + m)) Log.LogInformation("{M} scoped", m);
            var e = cap.With(m).SingleOrDefault();
            NotNull(e, "entry");
            Expect(e!.Scopes.Any(s => s.Contains("scope-" + m)), $"scopes: [{string.Join(", ", e.Scopes)}]");
        });

        t.Test("Concurrent logging from pool threads loses nothing", () =>
        {
            var cap = Capture();
            var m = Marker();
            Task.WaitAll(Enumerable.Range(0, 8).Select(w => Task.Run(() =>
            {
                for (var i = 0; i < 5; i++) Log.LogInformation("{M} worker={W} i={I}", m, w, i);
            })).ToArray());
            Equal(40, cap.With(m).Count, "captured entries");
        });

        return Task.CompletedTask;
    }

    public override bool ProfileInBackground => true;

    public override Task Profile( ProfileContext p )
    {
        var log = Log;

        p.Profile("Logger.IsEnabled(Information)", () => _ = log.IsEnabled(LogLevel.Information), 200_000);
        p.Profile("Logger.IsEnabled(Trace)", () => _ = log.IsEnabled(LogLevel.Trace), 200_000);
        if (!log.IsEnabled(LogLevel.Trace))
            p.Profile("Logger.LogTrace (level filtered out)", () => log.LogTrace("filtered {X}", 1), 200_000);
        p.Profile("Logger.BeginScope + Dispose", () => log.BeginScope("s")?.Dispose(), 100_000);

        using var factory = LoggerFactory.Create(b => b.AddProvider(new SinkProvider()));
        var sink = factory.CreateLogger("Tester.Sink");
        var ex = new InvalidOperationException("x");
        p.Profile("ILogger.LogInformation (no args, sink)", () => sink.LogInformation("plain message"), 200_000);
        p.Profile("ILogger.LogInformation (2 args, sink)", () => sink.LogInformation("player {Name} has {Points}", "Bob", 7), 200_000);
        p.Profile("ILogger.LogError (exception, sink)", () => sink.LogError(ex, "failed {X}", 1), 100_000);
        return Task.CompletedTask;
    }
}
