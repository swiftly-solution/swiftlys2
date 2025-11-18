using Microsoft.Extensions.DependencyInjection;
using SwiftlyS2.Core.Services;

namespace SwiftlyS2.Core.Hosting;

internal static class ProfileServiceInjection
{
  public static IServiceCollection AddProfileService(this IServiceCollection self)
  {
    return self.AddSingleton<ProfileService>();
  }
}