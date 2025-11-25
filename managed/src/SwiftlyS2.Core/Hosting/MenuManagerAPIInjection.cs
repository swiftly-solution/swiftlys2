using Microsoft.Extensions.DependencyInjection;
using SwiftlyS2.Core.Menus;

namespace SwiftlyS2.Core.Hosting;

internal static class MenuManagerAPIInjection
{
    public static IServiceCollection AddMenuManagerAPI( this IServiceCollection self )
    {
        _ = self.AddSingleton<MenuManagerAPI>();
        return self;
    }
}