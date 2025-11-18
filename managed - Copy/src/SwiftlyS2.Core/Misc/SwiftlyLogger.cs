using Microsoft.Extensions.Logging;
using Spectre.Console;

namespace SwiftlyS2.Core.Misc;

internal class SwiftlyLoggerProvider : ILoggerProvider
{
  private readonly string _contextName;

  public SwiftlyLoggerProvider(string contextName)
  {
    _contextName = contextName;
  }

  public ILogger CreateLogger(string categoryName)
  {
    return new SwiftlyLogger(categoryName, _contextName);
  }

  public void Dispose()
  {
  }
}

internal class SwiftlyLogger : ILogger
{
  private readonly string _categoryName;
  private readonly string _contextName;

  public SwiftlyLogger(string categoryName, string contextName)
  {
    _categoryName = categoryName;
    _contextName = contextName;
  }

  public IDisposable? BeginScope<TState>(TState state) where TState : notnull
  {
    return NullScope.Instance;
  }

  public bool IsEnabled(LogLevel logLevel)
  {
    return logLevel != LogLevel.None;
  }

  public void Log<TState>(LogLevel logLevel, EventId eventId, TState state, Exception? exception, Func<TState, Exception?, string> formatter)
  {
    if (!IsEnabled(logLevel)) return;

    var timestamp = DateTime.Now.ToString("MM/dd HH:mm:ss");
    var level = GetLogLevelString(logLevel);
    var id = $"[{eventId.ToString()}]";
    var color = GetLogLevelColor(logLevel);

    AnsiConsole.MarkupLineInterpolated($"[lightsteelblue1 bold]{_contextName}[/] [lightsteelblue]|[/] [grey42]{timestamp}[/] [lightsteelblue]|[/] [{color}]{level}[/] [lightsteelblue]|[/] [lightsteelblue]{_categoryName}{id}[/]");

    string? message = formatter != null ? formatter(state, exception) : state?.ToString();
    if (!string.IsNullOrEmpty(message))
    {
      FileLogger.Log($"{_contextName} | {timestamp} | {level} | {_categoryName}{id} | {message}");
      var lines = message.Split('\n');
      for (int i = 0; i < lines.Length; i++)
      {
        var line = lines[i];
        if (i == lines.Length - 1 && line == "") break;
        AnsiConsole.MarkupLineInterpolated($"[lightsteelblue1 bold]{_contextName}[/] [lightsteelblue]|[/] [grey85]{line}[/]");
      }
    }

    if (exception != null)
    {
      FileLogger.LogException(exception, $"{exception.Message}");
      AnsiConsole.WriteException(exception);
    }
    AnsiConsole.Reset();
  }

  private static string GetLogLevelString(LogLevel logLevel)
  {
    return logLevel switch
    {
      LogLevel.Trace => "Trace      ",
      LogLevel.Debug => "Debug      ",
      LogLevel.Information => "Information",
      LogLevel.Warning => "Warning    ",
      LogLevel.Error => "Error      ",
      LogLevel.Critical => "Critical   ",
      _ => "Unknown    "
    };
  }

  private static string GetLogLevelColor(LogLevel logLevel)
  {
    return logLevel switch
    {
      LogLevel.Trace => "grey42",
      LogLevel.Debug => "grey42",
      LogLevel.Information => "silver",
      LogLevel.Warning => "yellow1",
      LogLevel.Error => "red3",
      LogLevel.Critical => "red3",
      _ => "grey42"
    };
  }

  private sealed class NullScope : IDisposable
  {
    public static readonly NullScope Instance = new NullScope();
    public void Dispose() { }
  }
}