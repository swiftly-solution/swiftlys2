using Microsoft.Extensions.Hosting;
using SwiftlyS2.Core.Misc;
using SwiftlyS2.Core.Hosting;
using SwiftlyS2.Core.SchemaDefinitions;

namespace SwiftlyS2.Core.Services;

internal class StartupService : IHostedService
{
    // private readonly IServiceProvider provider;

    public StartupService( IServiceProvider provider )
    {
        // JIT warmup
        var _ = ClassConvertor.ConvertEntityByDesignerName(0, "abc");

        // this.provider = provider;
        provider.StartCoreServices();
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
