using Microsoft.Extensions.Logging;
using Spectre.Console;

namespace SwiftlyS2.Core.Misc;

internal class SwiftlyLoggerProvider( string contextName ) : ILoggerProvider
{
    public ILogger CreateLogger( string categoryName ) => new SwiftlyLogger(categoryName, contextName);
    public void Dispose() { }
}

internal class SwiftlyLogger( string categoryName, string contextName ) : ILogger
{
    private static readonly LogLevel MinLogLevel = GetMinLogLevelFromEnv();
    private static readonly Dictionary<LogLevel, (string Text, string Color)> LogLevelConfig = new() {
        [LogLevel.Trace] = ("Debug", "grey42"),
        [LogLevel.Debug] = ("Debug", "grey42"),
        [LogLevel.Information] = ("Information", "silver"),
        [LogLevel.Warning] = ("Warning", "yellow1"),
        [LogLevel.Error] = ("Error", "red3"),
        [LogLevel.Critical] = ("Critical", "red3")
    };

    public IDisposable BeginScope<TState>( TState state ) where TState : notnull => NullScope.Instance;

    public bool IsEnabled( LogLevel logLevel ) => logLevel != LogLevel.None && logLevel >= MinLogLevel;

    public void Log<TState>( LogLevel logLevel, EventId eventId, TState state, Exception? exception, Func<TState, Exception?, string> formatter )
    {
        if (!IsEnabled(logLevel))
        {
            return;
        }

        var timestamp = DateTime.Now.ToString("MM/dd HH:mm:ss");
        var (levelText, color) = LogLevelConfig.TryGetValue(logLevel, out var config) ? config : ("Unknown", "grey42");
        var eventIdText = eventId.Id != 0 ? $"[{eventId.Id}]" : string.Empty;
        AnsiConsole.Profile.Width = 13337;

        // Console output
        AnsiConsole.MarkupLineInterpolated($"[lightsteelblue1 bold]{contextName}[/] [lightsteelblue]|[/] [grey42]{timestamp}[/] [lightsteelblue]|[/] [{color}]{levelText}[/] [lightsteelblue]|[/] [lightsteelblue]{categoryName}{eventIdText}[/]");

        // Message output
        var message = formatter?.Invoke(state, exception) ?? state?.ToString();
        if (!string.IsNullOrEmpty(message))
        {
            FileLogger.Log($"{contextName} | {timestamp} | {levelText} | {categoryName}{eventIdText} | {message}");
            OutputMessageLines(message);
        }

        // Exception output
        if (exception != null)
        {
            FileLogger.LogException(exception, exception.Message);
            AnsiConsole.WriteException(exception);
        }

        AnsiConsole.Reset();
    }

    private void OutputMessageLines( string message )
    {
        var lines = message.Split('\n', StringSplitOptions.RemoveEmptyEntries);
        foreach (var line in lines)
        {
            AnsiConsole.MarkupLineInterpolated($"[lightsteelblue1 bold]{contextName}[/] [lightsteelblue]|[/] [grey85]{line}[/]");
        }
    }

    private static LogLevel GetMinLogLevelFromEnv()
    {
        var level = Environment.GetEnvironmentVariable("SWIFTLY_LOG_LEVEL")?.ToUpperInvariant();
        return level switch {
            "DEBUG" => LogLevel.Debug,
            "INFO" => LogLevel.Information,
            "WARNING" => LogLevel.Warning,
            "ERROR" => LogLevel.Error,
            "OFF" => LogLevel.None,
            _ => LogLevel.Information
        };
    }

    private sealed class NullScope : IDisposable
    {
        public static readonly NullScope Instance = new();
        public void Dispose() { }
    }
}