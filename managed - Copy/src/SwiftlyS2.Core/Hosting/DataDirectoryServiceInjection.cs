using Microsoft.Extensions.DependencyInjection;
using SwiftlyS2.Core.Services;

namespace SwiftlyS2.Core.Hosting;

internal static class DataDirectoryServiceInjection {
  public static IServiceCollection AddDataDirectoryService(this IServiceCollection self) {
    self.AddSingleton<DataDirectoryService>();
    return self;
  }
}