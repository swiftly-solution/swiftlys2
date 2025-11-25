using Microsoft.Extensions.DependencyInjection;
using SwiftlyS2.Core.Services;

namespace SwiftlyS2.Core.Hosting;

internal static class CoreCommandServiceInjection
{
    public static IServiceCollection AddCoreCommandService( this IServiceCollection self )
    {
        _ = self.AddSingleton<CoreCommandService>();
        return self;
    }

    public static void UseCoreCommandService( this IServiceProvider self )
    {
        _ = self.GetRequiredService<CoreCommandService>();
    }
}