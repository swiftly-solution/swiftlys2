namespace SwiftlyS2.Core.Services;

internal class DataDirectoryService
{

    private RootDirService RootDirService { get; init; }

    private string DataRoot => RootDirService.GetDataRoot();
    public DataDirectoryService( RootDirService rootDirService )
    {
        RootDirService = rootDirService;

        if (!Directory.Exists(DataRoot))
        {
            _ = Directory.CreateDirectory(DataRoot);
        }
    }

    public void EnsurePluginDataDirectory( string pluginId )
    {
        var pluginDataDirectory = GetPluginDataDirectory(pluginId);
        if (!Directory.Exists(pluginDataDirectory))
        {
            _ = Directory.CreateDirectory(pluginDataDirectory);
        }
    }

    public string GetPluginDataDirectory( string pluginId ) => Path.Combine(DataRoot, pluginId);
}