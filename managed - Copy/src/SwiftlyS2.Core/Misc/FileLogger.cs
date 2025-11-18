using System.Text;
using Spectre.Console;

namespace SwiftlyS2.Core.Misc;

internal static class FileLogger
{

  private static StreamWriter? _fileStream;
  private static Lock _lock = new();

  public static void Initialize( string basePath )
  {
    var directory = Path.Combine(basePath, "logs");

    if (!Directory.Exists(directory))
    {
      Directory.CreateDirectory(directory);
    }

    var time = DateTime.Now.ToString("yyyy-MM-dd_HH-mm-ss");

    var filePath = Path.Combine(directory, $"swiftlys2_managed_{time}.log");

    if (File.Exists(filePath))
    {
      File.Delete(filePath);
    }

    _fileStream = new StreamWriter(filePath, true);
  }

  public static void Log( string message )
  {
    lock (_lock)
    {
      if (_fileStream == null)
      {
        return;
      }

      _fileStream.WriteLine(message);
      _fileStream.Flush();
    }
  }

  public static void LogException( Exception exception, string message )
  {
    lock (_lock)
    {
      if (_fileStream == null)
      {
        return;
      }
      _fileStream.WriteLine(message);
      _fileStream.WriteLine(exception.Message);
      _fileStream.WriteLine(exception.StackTrace);
      _fileStream.Flush();
    }
  }
  public static void Dispose()
  {
    _fileStream?.Dispose();
    _fileStream = null;
  }
}