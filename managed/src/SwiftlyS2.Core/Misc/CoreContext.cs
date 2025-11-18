using SwiftlyS2.Shared;

namespace SwiftlyS2.Core.Services;

internal class CoreContext
{
    public string Name { get; init; }

    public string BaseDirectory { get; init; }

    public PluginMetadata? PluginMetadata { get; init; }

    public CoreContext( string name, string baseDirectory, PluginMetadata? pluginManifest )
    {
        Name = name;
        BaseDirectory = baseDirectory;
        PluginMetadata = pluginManifest;
    }
}