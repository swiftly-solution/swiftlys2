using Microsoft.Extensions.Hosting;
using SwiftlyS2.Core.Hosting;
using SwiftlyS2.Core.Misc;

namespace SwiftlyS2.Core.Services;

internal class StartupService : IHostedService
{

  private readonly IServiceProvider _provider;

  public StartupService(IServiceProvider provider)
  {
    _provider = provider;
    provider.UseCoreCommandService();
    provider.UseCoreHookService();
    provider.UsePermissionManager();
    provider.UsePluginManager();
    provider.UseCommandTrackerService();
    // provider.UseTestService();
  }

  public Task StartAsync(CancellationToken cancellationToken)
  {
    return Task.CompletedTask;
  }

  public Task StopAsync(CancellationToken cancellationToken)
  {
    FileLogger.Dispose();
    return Task.CompletedTask;
  }
}