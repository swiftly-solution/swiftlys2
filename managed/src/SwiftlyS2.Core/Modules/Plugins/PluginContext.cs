using System.Diagnostics;
using McMaster.NETCore.Plugins;
using SwiftlyS2.Core.Services;
using SwiftlyS2.Shared;
using SwiftlyS2.Shared.Plugins;

namespace SwiftlyS2.Core.Plugins;

internal class PluginContext : IDisposable
{
    public SwiftlyCore? Core { get; set; }

    public PluginMetadata? Metadata { get; set; }

    public string? PluginDirectory { get; set; }

    public PluginStatus? Status { get; set; }
    public BasePlugin? Plugin { get; set; }

    public PluginLoader? Loader { get; set; }

    public TimeSpan LoadTime { get; set; }

    public TimeSpan UnloadTime { get; private set; }

    public void Dispose()
    {
        var startedAt = Stopwatch.GetTimestamp();
        Plugin?.Unload();
        UnloadTime = Stopwatch.GetElapsedTime(startedAt);
        Loader?.Dispose();
        // Core?.MenuManager?.CloseAllMenus();
        Core?.MenuManagerAPI?.CloseAllMenus();
        Core?.Dispose();
    }
}