using Microsoft.Extensions.Logging;
using SwiftlyS2.Shared;
using Tester.Framework;
using Tester.Sections.Support;
using static Tester.Framework.TestContext;

namespace Tester.Sections;

public sealed class LoggerFactorySection( ISwiftlyCore core ) : Section(core)
{
    public override string Name => "loggerfactory";

    private ILoggerFactory Factory => Core.LoggerFactory;

    private static string Marker() => "tester-" + Guid.NewGuid().ToString("N")[..10];

    private static LogCapture Capture( ILoggerFactory factory )
    {
        LogCapture.Attach(factory);
        return LogCapture.Instance;
    }

    public override Task Test( TestContext t )
    {
        var f = Factory;

        t.Test("Core.LoggerFactory exists and is stable", () =>
        {
            NotNull(Core.LoggerFactory, "LoggerFactory");
            Expect(ReferenceEquals(Core.LoggerFactory, Core.LoggerFactory), "different instances");
        });

        t.Test("CreateLogger(category) returns the same logger for the same category", () =>
        {
            var a = f.CreateLogger("Tester.Same");
            var b = f.CreateLogger("Tester.Same");
            NotNull(a, "logger");
            Expect(ReferenceEquals(a, b), "different instances for one category");
        });

        t.Test("Different categories give different loggers", () =>
            Expect(!ReferenceEquals(f.CreateLogger("Tester.A"), f.CreateLogger("Tester.B")), "same instance"));

        t.Test("CreateLogger<T>() uses the type's full name as the category", () =>
        {
            var cap = Capture(f);
            var m = Marker();
            f.CreateLogger<LoggerFactorySection>().LogInformation("{M} generic", m);
            Equal(typeof(LoggerFactorySection).FullName!, cap.With(m).Single().Category, "category");
        });

        t.Test("CreateLogger(Type) uses the type's full name as the category", () =>
        {
            var cap = Capture(f);
            var m = Marker();
            f.CreateLogger(typeof(TesterPlugin)).LogInformation("{M} by type", m);
            Equal(typeof(TesterPlugin).FullName!, cap.With(m).Single().Category, "category");
        });

        t.Test("Custom category reaches providers", () =>
        {
            var cap = Capture(f);
            var m = Marker();
            f.CreateLogger("Tester.Custom").LogWarning("{M} custom", m);
            var e = cap.With(m).Single();
            Equal("Tester.Custom", e.Category, "category");
            Equal(LogLevel.Warning, e.Level, "level");
        });

        t.Test("A provider added after a logger exists still receives its messages", () =>
        {
            var early = f.CreateLogger("Tester.Early");
            var cap = Capture(f);
            var m = Marker();
            early.LogInformation("{M} early logger", m);
            Equal(1, cap.With(m).Count, "entries");
        });

        t.Test("Core.Logger and factory loggers feed the same provider", () =>
        {
            var cap = Capture(f);
            var m = Marker();
            Core.Logger.LogInformation("{M} from core logger", m);
            f.CreateLogger("Tester.Other").LogInformation("{M} from factory logger", m);
            Equal(2, cap.With(m).Count, "entries");
        });

        t.Test("Empty and markup-like category names are accepted", () =>
        {
            var m = Marker();
            f.CreateLogger("").LogInformation("{M} empty category", m);
            f.CreateLogger("[red]cat[/]").LogInformation("{M} markup category", m);
        });

        t.Test("Many distinct categories", () =>
        {
            for (var i = 0; i < 200; i++) NotNull(f.CreateLogger("Tester.Bulk." + i), "logger");
        });

        t.Test("Concurrent CreateLogger for one category returns one instance", () =>
        {
            var loggers = new ILogger[16];
            Task.WaitAll(Enumerable.Range(0, loggers.Length).Select(i => Task.Run(() => loggers[i] = f.CreateLogger("Tester.Race"))).ToArray());
            Expect(loggers.All(l => ReferenceEquals(l, loggers[0])), "more than one instance created");
        });

        t.Test("AddProvider(null) is rejected", () => Throws<ArgumentNullException>(() => f.AddProvider(null!)));

        return Task.CompletedTask;
    }

    public override bool ProfileInBackground => true;

    public override Task Profile( ProfileContext p )
    {
        var f = Factory;
        var counter = 0;

        p.Profile("LoggerFactory.CreateLogger(cached category)", () => _ = f.CreateLogger("Tester.Profile"), 200_000);
        p.Profile("LoggerFactory.CreateLogger<T>() (cached)", () => _ = f.CreateLogger<LoggerFactorySection>(), 200_000);
        p.Profile("LoggerFactory.CreateLogger(Type) (cached)", () => _ = f.CreateLogger(typeof(TesterPlugin)), 200_000);
        p.Profile("LoggerFactory.CreateLogger(new category)", () => _ = f.CreateLogger("Tester.Unique." + counter++), 500);
        return Task.CompletedTask;
    }
}
