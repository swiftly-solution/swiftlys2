namespace SwiftlyS2.Core.Services;

internal class RootDirService {
  public string GetRoot() {
    if (Environment.GetEnvironmentVariable("SWIFTLY_MANAGED_ROOT") is { } root) {
      return root;
    }
    return AppContext.BaseDirectory;
  }

  public string CombineRoot(string path) {
    return Path.Combine(GetRoot(), path);
  }

  public string GetPluginsRoot() {
    return CombineRoot("plugins");
  }

  public string GetConfigRoot() {
    return CombineRoot("configs");
  }

  public string GetDataRoot() {
    return CombineRoot("data");
  }
}