using Microsoft.Extensions.DependencyInjection;
using SwiftlyS2.Core.Database;

namespace SwiftlyS2.Core.Hosting;

internal static class DatabaseConnectionManagerInjection
{
    public static IServiceCollection AddDatabaseConnectionManager( this IServiceCollection self )
    {
        _ = self.AddSingleton<DatabaseConnectionManager>();
        return self;
    }
}
