using System.Collections.Concurrent;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Primitives;
using SwiftlyS2.Shared.Menu;

namespace SwiftlyS2.Core.Menu.Config;

internal sealed class GlobalMenuKeybindSource : IMenuKeybindSource
{
    private readonly IConfiguration configuration;

    private readonly ConcurrentDictionary<MenuActionId, MenuKey> cache = new();

    public GlobalMenuKeybindSource( IConfiguration configuration )
    {
        this.configuration = configuration;

        _ = ChangeToken.OnChange(configuration.GetReloadToken, cache.Clear);
    }

    public int Priority => MenuKeybindPriority.Global;

    public bool TryGetKey( MenuActionId id, out MenuKey key )
    {
        if (!cache.TryGetValue(id, out key))
        {
            key = Read(id);
            cache[id] = key;
        }

        return key != MenuKey.None;
    }

    private MenuKey Read( MenuActionId id )
    {
        var section = configuration.GetSection(MenuConfigFile.SectionName)
            .GetSection(id.Scope)
            .GetSection(id.Name);

        if (!section.Exists())
        {
            return MenuKey.None;
        }

        if (section.Value is { } single)
        {
            return MenuKeys.TryParse(single, out var parsed) ? parsed : MenuKey.None;
        }

        var names = section.GetChildren()
            .Select(child => child.Value)
            .Where(value => !string.IsNullOrWhiteSpace(value))
            .ToList();

        return names.Count == 0 ? MenuKey.None : MenuKeys.ParseAll(names);
    }
}
