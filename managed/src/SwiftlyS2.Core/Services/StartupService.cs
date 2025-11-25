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
        // provider.UseTestService();
        provider.UseCoreHookService();
        provider.UsePermissionManager();
        provider.UseCommandTrackerService();
        provider.UseMenuManagerAPIService();
        // UseCoreCommandService must be the second to last one
        provider.UseCoreCommandService();
        // Initialize PluginManager after everything is ready
        provider.UsePluginManager();
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