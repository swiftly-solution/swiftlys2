using Microsoft.Extensions.DependencyInjection;
using SwiftlyS2.Core.Services;

namespace SwiftlyS2.Core.Hosting;

internal static class TestServiceInjection
{
    public static IServiceCollection AddTestService( this IServiceCollection self )
    {
        _ = self.AddSingleton<TestService>();
        return self;
    }

    public static void UseTestService( this IServiceProvider self )
    {
        _ = self.GetRequiredService<TestService>();
    }
}