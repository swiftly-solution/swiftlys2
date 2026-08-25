using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Primitives;

namespace SwiftlyS2.Core.Menu.Config;

internal sealed class MenuLayoutOptions
{
    public const string SectionName = "MenuLayout";

    private const int DefaultMaxVisibleLines = 5;
    private const int LowestMaxVisibleLines = 1;
    private const int HighestMaxVisibleLines = 32;

    private readonly IConfiguration configuration;

    private volatile int maxVisibleLines;

    public MenuLayoutOptions( IConfiguration configuration )
    {
        this.configuration = configuration;

        Reload();

        _ = ChangeToken.OnChange(configuration.GetReloadToken, Reload);
    }

    public int MaxVisibleLines => maxVisibleLines;

    private void Reload()
    {
        var value = configuration.GetSection(SectionName)["MaxVisibleLines"];

        maxVisibleLines = int.TryParse(value, out var parsed)
            ? Math.Clamp(parsed, LowestMaxVisibleLines, HighestMaxVisibleLines)
            : DefaultMaxVisibleLines;
    }
}
