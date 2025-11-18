using Microsoft.Extensions.DependencyInjection;
using SwiftlyS2.Core.Services;

namespace SwiftlyS2.Core.Hosting;

internal static class CoreCommandServiceInjection
{
    public static IServiceCollection AddCoreCommandService( this IServiceCollection services )
    {
        _ = services.AddSingleton<CoreCommandService>();
        return services;
    }

    public static void UseCoreCommandService( this IServiceProvider provider )
    {
        _ = provider.GetRequiredService<CoreCommandService>();
    }
}