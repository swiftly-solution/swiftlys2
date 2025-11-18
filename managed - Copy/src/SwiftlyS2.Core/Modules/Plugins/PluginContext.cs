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

    public void Dispose()
    {
        Plugin?.Unload();
        Loader?.Dispose();
        // Core?.MenuManager?.CloseAllMenus();
        Core?.MenuManagerAPI?.CloseAllMenus();
        Core?.Dispose();
    }
}