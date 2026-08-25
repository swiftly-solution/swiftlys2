using Microsoft.Extensions.DependencyInjection;
using SwiftlyS2.Core.Services;

namespace SwiftlyS2.Core.Hosting;

internal static class MenuRuntimeServiceInjection
{
    public static IServiceCollection AddMenuRuntimeService( this IServiceCollection self )
    {
        _ = self.AddSingleton<MenuRuntimeService>();
        return self;
    }

    public static void UseMenuRuntimeService( this IServiceProvider self )
    {
        _ = self.GetRequiredService<MenuRuntimeService>();
    }
}
