using Microsoft.Extensions.Hosting;
using SwiftlyS2.Core.Misc;
using SwiftlyS2.Core.Hosting;

namespace SwiftlyS2.Core.Services;

internal class StartupService : IHostedService
{
    // private readonly IServiceProvider provider;

    public StartupService( IServiceProvider provider )
    {
        // this.provider = provider;
        provider.UseCoreCommandService();
        provider.UseCoreHookService();
        provider.UsePermissionManager();
        provider.UsePluginManager();
        provider.UseCommandTrackerService();
        provider.UseMenuManagerAPIService();
        // provider.UseTestService();
    }

    public Task StartAsync( CancellationToken cancellationToken )
    {
        return Task.CompletedTask;
    }

    public Task StopAsync( CancellationToken cancellationToken )
    {
        FileLogger.Dispose();
        return Task.CompletedTask;
    }
}