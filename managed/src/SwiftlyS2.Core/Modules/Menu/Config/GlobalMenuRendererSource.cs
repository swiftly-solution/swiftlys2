using Microsoft.Extensions.Configuration;
using SwiftlyS2.Shared.Menu;

namespace SwiftlyS2.Core.Menu.Config;

internal sealed class GlobalMenuRendererSource( IConfiguration configuration ) : IMenuRendererSource
{
    private const string RendererKey = "Renderer";

    public int Priority => MenuRendererPriority.Global;

    public bool TryGetRenderer( string menuId, out string rendererId )
    {
        var menus = configuration.GetSection(MenuConfigFile.SectionName);
        var scoped = menus.GetSection(menuId).GetSection(RendererKey);

        if (scoped.Value is { } scopedValue && !string.IsNullOrWhiteSpace(scopedValue))
        {
            rendererId = scopedValue;
            return true;
        }

        var global = menus.GetSection(RendererKey);

        if (global.Value is { } globalValue && !string.IsNullOrWhiteSpace(globalValue))
        {
            rendererId = globalValue;
            return true;
        }

        rendererId = "";
        return false;
    }
}
