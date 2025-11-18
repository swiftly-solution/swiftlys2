using System.Data.Common;
using System.Text.Json;
using Microsoft.Extensions.Configuration;
using SwiftlyS2.Core.Natives;
using SwiftlyS2.Core.Services;
using SwiftlyS2.Shared.Services;
using Tomlyn;

namespace SwiftlyS2.Core.Services;

internal class PluginConfigurationService : IPluginConfigurationService
{

  private ConfigurationService _ConfigurationService { get; init; }
  private CoreContext _Id { get; init; }
  private IConfigurationManager? _Manager { get; set; }

  public bool BasePathExists
  {
    get => Path.Exists(BasePath);
  }

  public PluginConfigurationService(CoreContext id, ConfigurationService configurationService)
  {
    _Id = id;
    _ConfigurationService = configurationService;
  }

  public string BasePath => Path.Combine(_ConfigurationService.GetConfigRoot(), "plugins", _Id.Name);

  public string GetRoot()
  {
    var dir = Path.Combine(_ConfigurationService.GetConfigRoot(), "plugins", _Id.Name);
    if (!Directory.Exists(dir))
    {
      Directory.CreateDirectory(dir);
    }
    return dir;
  }

  public string GetConfigPath(string name)
  {
    return Path.Combine(GetRoot(), name);
  }

  public IPluginConfigurationService InitializeWithTemplate(string name, string templatePath)
  {

    var configPath = GetConfigPath(name);

    if (File.Exists(configPath))
    {
      return this;
    }

    var dir = Path.GetDirectoryName(configPath);
    if (dir is not null)
    {
      Directory.CreateDirectory(dir);
    }
    File.Create(configPath).Close();

    var templateAbsPath = Path.Combine(_Id.BaseDirectory, "resources", "templates", templatePath);

    if (!File.Exists(templateAbsPath))
    {
      throw new FileNotFoundException($"Template file not found: {templateAbsPath}");
    }

    File.Copy(templateAbsPath, configPath);
    return this;
  }

  public IPluginConfigurationService InitializeJsonWithModel<T>(string name, string sectionName) where T : class, new()
  {

    var configPath = GetConfigPath(name);

    if (File.Exists(configPath))
    {
      return this;
    }

    var dir = Path.GetDirectoryName(configPath);
    if (dir is not null)
    {
      Directory.CreateDirectory(dir);
    }

    var config = new T();

    var wrapped = new Dictionary<string, object?>
    {
      [sectionName] = config
    };

    var options = new JsonSerializerOptions
    {
      WriteIndented = true,
      IncludeFields = true,
      PropertyNamingPolicy = null
    };

    var configJson = JsonSerializer.Serialize(wrapped, options);
    File.WriteAllText(configPath, configJson);

    return this;
  }

  public IPluginConfigurationService InitializeTomlWithModel<T>(string name, string sectionName) where T : class, new()
  {

    var configPath = GetConfigPath(name);

    if (File.Exists(configPath))
    {
      return this;
    }

    var dir = Path.GetDirectoryName(configPath);
    if (dir is not null)
    {
      Directory.CreateDirectory(dir);
    }

    var config = new T();

    var wrapped = new Dictionary<string, object?>
    {
      [sectionName] = config
    };

    var tomlString = Toml.FromModel(wrapped);
    File.WriteAllText(configPath, tomlString);

    return this;
  }

  public IPluginConfigurationService Configure(Action<IConfigurationBuilder> configure)
  {
    configure(Manager);
    return this;
  }

  public IConfigurationManager Manager
  {
    get
    {
      if (!BasePathExists)
      {
        throw new Exception("Base path does not exist in file system. Please call InitializeWithTemplate, InitializeJsonWithModel or InitializeTomlWithModel before using the Manager.");
      }
      if (_Manager is null)
      {
        _Manager = new ConfigurationManager();
        _Manager.SetBasePath(BasePath);
      }
      return _Manager;
    }
  }
}
