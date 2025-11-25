using Microsoft.Extensions.DependencyInjection;
using SwiftlyS2.Core.Models;
using SwiftlyS2.Core.Permissions;

namespace SwiftlyS2.Core.Hosting;

internal static class PermissionManagerInjection
{
    public static IServiceCollection AddPermissionManager( this IServiceCollection self )
    {
        _ = self.AddSingleton<PermissionManager>()
            .AddOptions<PermissionConfig>()
            .BindConfiguration("Permissions")
            .ValidateOnStart();

        return self;
    }

    public static void UsePermissionManager( this IServiceProvider self )
    {
        _ = self.GetRequiredService<PermissionManager>();
    }
}