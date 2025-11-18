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
}