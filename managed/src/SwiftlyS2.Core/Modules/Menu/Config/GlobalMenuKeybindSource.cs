using Microsoft.Extensions.Configuration;
using SwiftlyS2.Shared.Menu;

namespace SwiftlyS2.Core.Menu.Config;

internal sealed class GlobalMenuKeybindSource( IConfiguration configuration ) : IMenuKeybindSource
{
    public int Priority => MenuKeybindPriority.Global;

    public bool TryGetKey( MenuActionId id, out MenuKey key )
    {
        key = MenuKey.None;

        var section = configuration.GetSection(MenuConfigFile.SectionName)
            .GetSection(id.Scope)
            .GetSection(id.Name);

        if (!section.Exists())
        {
            return false;
        }

        if (section.Value is { } single)
        {
            return MenuKeys.TryParse(single, out key);
        }

        var names = section.GetChildren()
            .Select(child => child.Value)
            .Where(value => !string.IsNullOrWhiteSpace(value))
            .ToList();

        if (names.Count == 0)
        {
            return false;
        }

        key = MenuKeys.ParseAll(names);
        return key != MenuKey.None;
    }
}
