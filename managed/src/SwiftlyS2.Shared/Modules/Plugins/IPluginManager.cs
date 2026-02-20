namespace SwiftlyS2.Shared.Plugins;

public enum PluginStatus
{
    Loaded,
    Unloaded,
    Loading,
    Error,
    Indeterminate
}

public interface IPluginManager
{
    /// <summary>
    /// Loads the specified plugin.
    /// </summary>
    /// <param name="pluginId">The ID of the plugin.</param>
    /// <param name="silent">If true, suppresses any error messages.</param>
    /// <returns>True if the plugin was loaded successfully, false otherwise.</returns>
    public bool LoadPlugin( string pluginId, bool silent = false );
    /// <summary>
    /// Unloads the specified plugin.
    /// </summary>
    /// <param name="pluginId">The ID of the plugin.</param>
    /// <param name="silent">If true, suppresses any error messages.</param>
    /// <returns>True if the plugin was unloaded successfully, false otherwise.</returns>
    public bool UnloadPlugin( string pluginId, bool silent = false );
    /// <summary>
    /// Reloads the specified plugin.
    /// </summary>
    /// <param name="pluginId">The ID of the plugin.</param>
    /// <param name="silent">If true, suppresses any error messages.</param>
    /// <returns>True if the plugin was reloaded successfully, false otherwise.</returns>
    public bool ReloadPlugin( string pluginId, bool silent = false );
    /// <summary>
    /// Gets the status of the specified plugin.
    /// </summary>
    /// <param name="pluginId">The ID of the plugin.</param>
    public PluginStatus? GetPluginStatus( string pluginId );
    /// <summary>
    /// Gets the metadata of the specified plugin.
    /// </summary>
    /// <param name="pluginId">The ID of the plugin.</param>
    public PluginMetadata? GetPluginMetadata( string pluginId );
    /// <summary>
    /// Gets the path of the specified plugin.
    /// </summary>
    /// <param name="pluginId">The ID of the plugin.</param>
    public string? GetPluginPath( string pluginId );
    /// <summary>
    /// Gets a dictionary of all plugin paths, keyed by plugin ID.
    /// </summary>
    public Dictionary<string, string> GetPluginPaths();
    /// <summary>
    /// Gets a dictionary of all plugin statuses, keyed by plugin ID.
    /// </summary>
    public Dictionary<string, PluginStatus> GetAllPluginStatuses();
    /// <summary>
    /// Gets a dictionary of all plugin metadata, keyed by plugin ID.
    /// </summary>
    public Dictionary<string, PluginMetadata> GetAllPluginMetadata();
    /// <summary>
    /// Gets a list of all plugin IDs.
    /// </summary>
    public IEnumerable<string> GetAllPlugins();

    /// <summary>
    /// Returns true if the specified plugin is blocked from loading.
    /// </summary>
    /// <param name="pluginId">The ID of the plugin.</param>
    public bool IsPluginBlocked( string pluginId );

    /// <summary>
    /// Adds a plugin to the blocklist, preventing it from loading.
    /// If the plugin is already loaded, it will NOT be unloaded — only future loads are prevented.
    /// </summary>
    /// <param name="pluginId">The ID of the plugin to block.</param>
    public void BlockPlugin( string pluginId );

    /// <summary>
    /// Removes a plugin from the blocklist, allowing it to load again.
    /// </summary>
    /// <param name="pluginId">The ID of the plugin to unblock.</param>
    public void UnblockPlugin( string pluginId );

    /// <summary>
    /// Gets the set of all blocked plugin IDs.
    /// </summary>
    public IReadOnlySet<string> GetBlockedPlugins();
}