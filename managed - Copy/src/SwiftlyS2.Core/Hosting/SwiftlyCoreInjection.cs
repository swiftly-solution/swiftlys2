using Microsoft.Extensions.DependencyInjection;
using SwiftlyS2.Core.Services;
using SwiftlyS2.Shared;

namespace SwiftlyS2.Core.Hosting;

internal static class SwiftlyCoreInjection {
  public static IServiceCollection AddSwiftlyCore(this IServiceCollection self, string basePath) {
    return self.AddSingleton<ISwiftlyCore, SwiftlyCore>((provider) => new SwiftlyCore(
      "SwiftlyS2",
      basePath,
      null,
      typeof(SwiftlyCore),
      provider,
      basePath
    ));
  }
}