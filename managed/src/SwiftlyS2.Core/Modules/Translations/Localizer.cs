using SwiftlyS2.Shared.Translation;

namespace SwiftlyS2.Core.Translations;

internal class Localizer : ILocalizer
{
    private Dictionary<string, string> _Resource { get; init; }
    private Dictionary<string, string> _DefaultResource { get; init; }



    public Localizer( Dictionary<string, string> resource, Dictionary<string, string> defaultResource )
    {
        _Resource = resource;
        _DefaultResource = defaultResource;
    }

    public string this[string key] => Get(key);

    public string this[string key, params object[] args] => string.Format(this[key], args);

    public string Get( string key )
    {
        return _Resource.ContainsKey(key)
            ? _Resource[key]
            : _DefaultResource.ContainsKey(key) ? _DefaultResource[key] : throw new Exception($"Translation key {key} not found.");
    }

}