namespace SwiftlyS2.Core.Services;

internal class RootDirService
{
    private static string? _root = null;

    public static string GetRoot()
    {
        _root ??= Environment.GetEnvironmentVariable("SWIFTLY_MANAGED_ROOT") is { } root ? root : AppContext.BaseDirectory;
        return _root;
    }

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