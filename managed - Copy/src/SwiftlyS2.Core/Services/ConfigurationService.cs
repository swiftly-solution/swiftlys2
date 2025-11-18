using Microsoft.Extensions.Configuration;

namespace SwiftlyS2.Core.Services;

internal class ConfigurationService {
  private readonly RootDirService _rootDirService;

  public ConfigurationService(RootDirService rootDirService) {
    _rootDirService = rootDirService;
  }

  public string GetConfigRoot() {
    return _rootDirService.GetConfigRoot();
  }

  public string CombineConfigPath(string configPath) {
    return _rootDirService.CombineRoot(configPath);
  }

  public void InitializeConfig(string configPath) {
    if (!File.Exists(CombineConfigPath(configPath))) {
      
    }
  }

  public IConfiguration GetConfig(string configPath) {
    return new ConfigurationBuilder()
      .AddJsonFile(CombineConfigPath(configPath))
      .Build();
  }
}