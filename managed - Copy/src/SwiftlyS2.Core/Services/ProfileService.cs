using SwiftlyS2.Core.Natives;
using System.Text.Json;
using System.Text.Json.Serialization;
using System.Linq;
using System.Diagnostics;

namespace SwiftlyS2.Core.Services;

internal class ProfileService
{

  private readonly Lock _lock = new();
  private bool _enabled = false;

  private readonly Dictionary<string, Dictionary<string, Stat>> _statsTable = new();
  private readonly Dictionary<string, Dictionary<string, ulong>> _activeStartsUs = new();

  // High-precision timestamping via Stopwatch, mapped to epoch micros
  private readonly long _swBaseTicks;
  private readonly ulong _epochBaseMicros;
  private readonly double _ticksToMicro;

  private sealed class Stat
  {
    public ulong Count;
    public ulong TotalUs;
    public ulong MinUs = ulong.MaxValue;
    public ulong MaxUs = 0UL;
  }

  public ProfileService()
  {
    _swBaseTicks = Stopwatch.GetTimestamp();
    // Capture epoch micros at approximately the same moment as base ticks
    var epochTicks = DateTimeOffset.UtcNow.Ticks - DateTimeOffset.UnixEpoch.Ticks; // 100ns ticks
    _epochBaseMicros = (ulong)(epochTicks / 10);
    _ticksToMicro = 1_000_000.0 / Stopwatch.Frequency;
  }

  private ulong NowMicrosecondsSinceUnixEpoch()
  {
    var deltaTicks = Stopwatch.GetTimestamp() - _swBaseTicks;
    var micros = (ulong)(deltaTicks * _ticksToMicro);
    return _epochBaseMicros + micros;
  }

  public void Enable()
  {
    lock (_lock)
    {
      _statsTable.Clear();
      _activeStartsUs.Clear();
      _enabled = true;
    }
  }

  public void Disable()
  {
    lock (_lock)
    {
      _enabled = false;
      _statsTable.Clear();
      _activeStartsUs.Clear();
    }
  }

  public bool IsEnabled()
  {
    lock (_lock)
    {
      return _enabled;
    }
  }

  public void StartRecordingWithIdentifier( string identifier, string name )
  {
    if (!_enabled) return;
    lock (_lock)
    {
      if (!_activeStartsUs.TryGetValue(identifier, out var startMap))
      {
        startMap = new Dictionary<string, ulong>(StringComparer.Ordinal);
        _activeStartsUs[identifier] = startMap;
      }
      startMap[name] = NowMicrosecondsSinceUnixEpoch();
    }
  }
  public void StopRecordingWithIdentifier( string identifier, string name )
  {
    if (!_enabled) return;
    lock (_lock)
    {
      if (!_activeStartsUs.TryGetValue(identifier, out var startMap)) return;
      if (!startMap.TryGetValue(name, out var startUs)) return;
      var endUs = NowMicrosecondsSinceUnixEpoch();
      var durUs = endUs > startUs ? endUs - startUs : 0UL;
      startMap.Remove(name);

      if (!_statsTable.TryGetValue(identifier, out var nameToStat))
      {
        nameToStat = new Dictionary<string, Stat>(StringComparer.Ordinal);
        _statsTable[identifier] = nameToStat;
      }
      if (!nameToStat.TryGetValue(name, out var stat))
      {
        stat = new Stat();
        nameToStat[name] = stat;
      }

      stat.Count++;
      stat.TotalUs += durUs;
      if (durUs < stat.MinUs) stat.MinUs = durUs;
      if (durUs > stat.MaxUs) stat.MaxUs = durUs;
    }
  }
  public void RecordTimeWithIdentifier( string identifier, string name, double duration )
  {
    lock (_lock)
    {
      if (!_enabled) return;

      var durUs = duration <= 0 ? 0UL : (ulong)duration;

      if (!_statsTable.TryGetValue(identifier, out var nameToStat))
      {
        nameToStat = new Dictionary<string, Stat>(StringComparer.Ordinal);
        _statsTable[identifier] = nameToStat;
      }
      if (!nameToStat.TryGetValue(name, out var stat))
      {
        stat = new Stat();
        nameToStat[name] = stat;
      }

      stat.Count++;
      stat.TotalUs += durUs;
      if (durUs < stat.MinUs) stat.MinUs = durUs;
      if (durUs > stat.MaxUs) stat.MaxUs = durUs;
    }
  }

  public void StartRecording( string name )
  {
    StartRecordingWithIdentifier("SwiftlyS2", name);
  }

  public void StopRecording( string name )
  {
    StopRecordingWithIdentifier("SwiftlyS2", name);
  }

  public void RecordTime( string name, double duration )
  {
    RecordTimeWithIdentifier("SwiftlyS2", name, duration);
  }

  public string GenerateJSONPerformance( string pluginId )
  {
    // snapshot stats
    Dictionary<string, Dictionary<string, Stat>> stats;
    lock (_lock)
    {
      stats = _statsTable.ToDictionary(
        kv => kv.Key,
        kv => kv.Value.ToDictionary(inner => inner.Key, inner => new Stat {
          Count = inner.Value.Count,
          TotalUs = inner.Value.TotalUs,
          MinUs = inner.Value.Count == 0 ? 0UL : inner.Value.MinUs,
          MaxUs = inner.Value.MaxUs,
        }, StringComparer.Ordinal), StringComparer.Ordinal);
    }

    var traceEvents = new List<Dictionary<string, object?>>();

    // Metadata events
    traceEvents.Add(new Dictionary<string, object?> {
      { "args", new Dictionary<string, object?> { { "name", "Swiftly" } } },
      { "cat", "__metadata" },
      { "name", "process_name" },
      { "ph", "M" },
      { "pid", 1 },
      { "tid", 1 },
      { "ts", 0UL },
    });
    traceEvents.Add(new Dictionary<string, object?> {
      { "args", new Dictionary<string, object?> { { "name", "Swiftly Main" } } },
      { "cat", "__metadata" },
      { "name", "thread_name" },
      { "ph", "M" },
      { "pid", 1 },
      { "tid", 1 },
      { "ts", 0UL },
    });
    traceEvents.Add(new Dictionary<string, object?> {
      { "args", new Dictionary<string, object?> { { "name", "Swiftly Profiler" } } },
      { "cat", "__metadata" },
      { "name", "thread_name" },
      { "ph", "M" },
      { "pid", 1 },
      { "tid", 2 },
      { "ts", 0UL },
    });

    string FormatUs( float us )
    {
      // switch to ms when duration reaches 0.01 ms (10 microseconds)
      if (us >= 10f)
      {
        var ms = us / 1000f;
        return $"{ms:F2}ms";
      }
      var ius = (ulong)System.MathF.Max(0f, us);
      return $"{ius:d}.00μs";
    }

    foreach (var (plugin, nameMap) in stats)
    {
      if (!string.IsNullOrEmpty(pluginId) && !string.Equals(pluginId, plugin, StringComparison.Ordinal))
      {
        continue;
      }
      foreach (var (name, stat) in nameMap)
      {
        var count = stat.Count;
        float minUs = count == 0 ? 0f : stat.MinUs;
        float maxUs = stat.MaxUs;
        float avgUs = count == 0 ? 0f : (float)(stat.TotalUs / (double)count);
        var eventName = $"{name} [{plugin}] (min={FormatUs(minUs)},avg={FormatUs(avgUs)},max={FormatUs(maxUs)},count={(ulong)count})";

        traceEvents.Add(new Dictionary<string, object?> {
          { "name", eventName },
          { "ph", "X" },
          { "tid", 2 },
          { "pid", 1 },
          { "ts", 0UL },
          { "dur", stat.TotalUs },
        });
      }
    }

    var root = new Dictionary<string, object?> {
      { "traceEvents", traceEvents }
    };

    var options = new JsonSerializerOptions {
      PropertyNamingPolicy = null,
      WriteIndented = false,
      DefaultIgnoreCondition = JsonIgnoreCondition.Never,
    };

    return JsonSerializer.Serialize(root, options);
  }
}
