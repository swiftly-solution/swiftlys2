using System.Collections.Concurrent;

namespace SwiftlyS2.Core.Misc;

internal static class FileLogger
{
    private static StreamWriter? _fileStream;
    private static readonly ConcurrentQueue<string> _queue = new();
    private static Lock _lock = new();
    private static bool _enabled = true;
    private static Thread? _flushThread;
    private static volatile bool _running = false;
    private static string _logDirectory = string.Empty;
    private static DateTime _currentDate;

    public static void Initialize( string basePath )
    {
        var enabledStr = Environment.GetEnvironmentVariable("SWIFTLY_MANAGED_LOG_ENABLE");
        _enabled = enabledStr != "0";
        if (!_enabled)
            return;

        var intervalStr = Environment.GetEnvironmentVariable("SWIFTLY_MANAGED_LOG_INTERVAL_MS");
        var intervalMs = int.TryParse(intervalStr, out var parsed) && parsed > 0 ? parsed : 2000;

        _logDirectory = Path.Combine(basePath, "managed");
        if (!Directory.Exists(_logDirectory))
            _ = Directory.CreateDirectory(_logDirectory);

        _currentDate = DateTime.Now;
        _fileStream = new StreamWriter(Path.Combine(_logDirectory, $"{_currentDate:yyyy-MM-dd_HH-mm-ss}.log"), append: true);

        _running = true;
        _flushThread = new Thread(() =>
        {
            while (_running)
            {
                Thread.Sleep(intervalMs);
                RollDateIfNeeded();
                Flush();
            }
        }) {
            IsBackground = true,
            Name = "FileLogger.Flush"
        };
        _flushThread.Start();
    }

    private static void RollDateIfNeeded()
    {
        var now = DateTime.Now;
        if (now.Date == _currentDate.Date) return;
        lock (_lock)
        {
            if (now.Date == _currentDate.Date) return;
            _fileStream?.Flush();
            _fileStream?.Dispose();
            _currentDate = now;
            _fileStream = new StreamWriter(Path.Combine(_logDirectory, $"{now:yyyy-MM-dd_HH-mm-ss}.log"), append: true);
        }
    }

    public static void Log( string message )
    {
        if (!_enabled) return;
        _queue.Enqueue(message);
    }

    public static void LogException( Exception exception, string message )
    {
        if (!_enabled) return;
        _queue.Enqueue(message);
        _queue.Enqueue(exception.Message);
        _queue.Enqueue(exception.StackTrace ?? string.Empty);
    }

    public static void Flush()
    {
        if (_fileStream == null || _queue.IsEmpty) return;
        lock (_lock)
        {
            while (_queue.TryDequeue(out var line))
                _fileStream.WriteLine(line);
            _fileStream.Flush();
        }
    }

    public static void Dispose()
    {
        _running = false;
        Flush();
        lock (_lock)
        {
            _fileStream?.Dispose();
            _fileStream = null;
        }
    }
}
