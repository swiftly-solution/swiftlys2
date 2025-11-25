using Microsoft.Extensions.DependencyInjection;
using SwiftlyS2.Core.Services;

namespace SwiftlyS2.Core.Hosting;

internal static class MenuManagerAPIServiceInjection
{
    public static IServiceCollection AddMenuManagerAPIService( this IServiceCollection self )
    {
        _ = self.AddSingleton<MenuManagerAPIService>();
        return self;
    }

    public static void UseMenuManagerAPIService( this IServiceProvider self )
    {
        _ = self.GetRequiredService<MenuManagerAPIService>();
    }
}