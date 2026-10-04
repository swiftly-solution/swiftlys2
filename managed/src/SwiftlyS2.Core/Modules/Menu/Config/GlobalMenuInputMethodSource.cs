using Microsoft.Extensions.Configuration;
using SwiftlyS2.Shared.Menu;

namespace SwiftlyS2.Core.Menu.Config;

internal sealed class GlobalMenuInputMethodSource( IConfiguration configuration ) : IMenuInputMethodSource
{
    private const string InputMethodKey = "InputMethod";

    public int Priority => MenuInputMethodPriority.Global;

    public bool TryGetInputMethod( string menuId, out MenuInputMethod inputMethod )
    {
        var menus = configuration.GetSection(MenuConfigFile.SectionName);

        return TryParse(menus.GetSection(menuId).GetSection(InputMethodKey).Value, out inputMethod)
            || TryParse(menus.GetSection(InputMethodKey).Value, out inputMethod);
    }

    private static bool TryParse( string? value, out MenuInputMethod inputMethod )
    {
        inputMethod = default;

        return !string.IsNullOrWhiteSpace(value)
            && Enum.TryParse(value.Trim(), ignoreCase: true, out inputMethod)
            && Enum.IsDefined(inputMethod);
    }
}
