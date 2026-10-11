using System.Collections.Concurrent;
using Microsoft.Extensions.Logging;

namespace Tester.Sections.Support;

public sealed record LogEntry( string Category, LogLevel Level, EventId EventId, string Message, Exception? Exception, string[] Scopes );

public sealed class LogCapture : ILoggerProvider, ISupportExternalScope
{
    public static readonly LogCapture Instance = new();

    private static readonly object Gate = new();
    private static bool attached;

    private readonly ConcurrentQueue<LogEntry> entries = new();
    private IExternalScopeProvider? scopes;

    public bool ReceivedScopeProvider => scopes is not null;

    public static void Attach( ILoggerFactory factory )
    {
        lock (Gate)
        {
            if (attached) return;
            factory.AddProvider(Instance);
            attached = true;
        }
    }

    public IReadOnlyList<LogEntry> With( string marker ) => entries.Where(e => e.Message.Contains(marker)).ToList();

    public ILogger CreateLogger( string categoryName ) => new CaptureLogger(categoryName, this);

    public void SetScopeProvider( IExternalScopeProvider scopeProvider ) => scopes = scopeProvider;

    public void Dispose() { }

    private sealed class CaptureLogger( string category, LogCapture owner ) : ILogger
    {
        public IDisposable? BeginScope<TState>( TState state ) where TState : notnull => owner.scopes?.Push(state);

        public bool IsEnabled( LogLevel logLevel ) => logLevel != LogLevel.None;

        public void Log<TState>( LogLevel logLevel, EventId eventId, TState state, Exception? exception, Func<TState, Exception?, string> formatter )
        {
            var active = new List<string>();
            owner.scopes?.ForEachScope(static ( s, list ) => list.Add(s?.ToString() ?? ""), active);
            owner.entries.Enqueue(new LogEntry(category, logLevel, eventId, formatter(state, exception), exception, [.. active]));
        }
    }
}

public sealed class SinkProvider : ILoggerProvider
{
    public ILogger CreateLogger( string categoryName ) => new Sink();

    public void Dispose() { }

    private sealed class Sink : ILogger
    {
        public IDisposable? BeginScope<TState>( TState state ) where TState : notnull => null;

        public bool IsEnabled( LogLevel logLevel ) => true;

        public void Log<TState>( LogLevel logLevel, EventId eventId, TState state, Exception? exception, Func<TState, Exception?, string> formatter )
            => _ = formatter(state, exception);
    }
}
