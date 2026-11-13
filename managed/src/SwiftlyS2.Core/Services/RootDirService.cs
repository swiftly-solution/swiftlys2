using SwiftlyS2.Core.Misc;

namespace SwiftlyS2.Core.Services;

internal class RootDirService
{
    public static string GetRoot() => BootstrapInfo.RootPath;

    public string CombineRoot( string path )
    {
        return Path.Combine(GetRoot(), path);
    }

    public string GetPluginsRoot()
    {
        return CombineRoot("plugins");
    }

    public string GetConfigRoot()
    {
        return CombineRoot("configs");
    }

    public string GetDataRoot()
    {
        return CombineRoot("data");
    }
}