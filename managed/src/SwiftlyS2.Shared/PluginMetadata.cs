namespace SwiftlyS2.Shared;

public class PluginMetadata : Attribute
{
    public required string Id { get; set; }
    private string? _displayName;
    public string Name {
        get => _displayName ?? Id;
        set => _displayName = value;
    }
    public required string Version { get; set; }
    public string Author { get; set; } = "Anonymous";
    public string Description { get; set; } = "No further description.";
    public string Website { get; set; } = string.Empty;

    /// <summary>
    /// Controls plugin load order. Lower values load first. Default is 1000.
    /// Use values below 1000 for plugins that need to load early (e.g. plugin managers, core APIs).
    /// </summary>
    public int LoadPriority { get; set; } = 1000;
}