using System.Data.Common;
using System.Text.Json;
using System.Text.Json.Nodes;
using Microsoft.Extensions.Configuration;
using SwiftlyS2.Core.Natives;
using SwiftlyS2.Core.Services;
using SwiftlyS2.Shared.Services;
using Tomlyn;
using Tomlyn.Model;

namespace SwiftlyS2.Core.Services;

internal class PluginConfigurationService : IPluginConfigurationService
{

  private ConfigurationService _ConfigurationService { get; init; }
  private CoreContext _Id { get; init; }
  private IConfigurationManager? _Manager { get; set; }

  public bool BasePathExists {
    get => Path.Exists(BasePath);
  }

  public PluginConfigurationService( CoreContext id, ConfigurationService configurationService )
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

  public string GetConfigPath( string name )
  {
    return Path.Combine(GetRoot(), name);
  }

  public IPluginConfigurationService InitializeWithTemplate( string name, string templatePath )
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

    var templateAbsPath = Path.Combine(_Id.BaseDirectory, "resources", "templates", templatePath);

    if (!File.Exists(templateAbsPath))
    {
      throw new FileNotFoundException($"Template file not found: {templateAbsPath}");
    }

    File.Copy(templateAbsPath, configPath);
    return this;
  }

  public IPluginConfigurationService InitializeJsonWithModel<T>( string name, string sectionName ) where T : class, new()
  {
    var configPath = GetConfigPath(name);

    var dir = Path.GetDirectoryName(configPath);
    if (dir is not null)
    {
      Directory.CreateDirectory(dir);
    }

    var options = new JsonSerializerOptions {
      WriteIndented = true,
      IncludeFields = true,
      PropertyNamingPolicy = null
    };

    var defaults = new Dictionary<string, object?> { [sectionName] = new T() };
    var defaultJson = JsonSerializer.SerializeToNode(defaults, options)!.AsObject();

    if (!File.Exists(configPath))
    {
      File.WriteAllText(configPath, defaultJson.ToJsonString(options));
      return this;
    }

    var existingText = File.ReadAllText(configPath);
    JsonNode? existingNode;
    try
    {
      existingNode = JsonNode.Parse(existingText);
    }
    catch (JsonException)
    {
      existingNode = null;
    }

    if (existingNode is not JsonObject existingObj)
    {
      File.WriteAllText(configPath, defaultJson.ToJsonString(options));
      return this;
    }

    if (MergeJsonObjects(existingObj, defaultJson))
    {
      File.WriteAllText(configPath, existingObj.ToJsonString(options));
    }

    return this;
  }

  private static bool MergeJsonObjects( JsonObject target, JsonObject defaults )
  {
    var changed = false;

    foreach (var (key, defaultValue) in defaults)
    {
      if (!target.ContainsKey(key))
      {
        target[key] = defaultValue?.DeepClone();
        changed = true;
      }
      else if (defaultValue is JsonObject defaultChild && target[key] is JsonObject targetChild)
      {
        if (MergeJsonObjects(targetChild, defaultChild))
        {
          changed = true;
        }
      }
    }

    return changed;
  }

  public IPluginConfigurationService InitializeTomlWithModel<T>( string name, string sectionName ) where T : class, new()
  {
    var configPath = GetConfigPath(name);

    var dir = Path.GetDirectoryName(configPath);
    if (dir is not null)
    {
      Directory.CreateDirectory(dir);
    }

    var tomlModelOptions = new TomlModelOptions {
      ConvertPropertyName = n => n,
      IgnoreMissingProperties = true
    };

    var defaults = new Dictionary<string, object?> { [sectionName] = new T() };
    var defaultToml = Toml.FromModel(defaults, tomlModelOptions);

    if (!File.Exists(configPath))
    {
      File.WriteAllText(configPath, defaultToml);
      return this;
    }

    var existingText = File.ReadAllText(configPath);
    TomlTable? existingTable;
    try
    {
      existingTable = Toml.ToModel(existingText);
    }
    catch
    {
      existingTable = null;
    }

    var defaultTable = Toml.ToModel(defaultToml);

    if (existingTable is null)
    {
      File.WriteAllText(configPath, defaultToml);
      return this;
    }

    if (MergeTomlTables(existingTable, defaultTable))
    {
      File.WriteAllText(configPath, Toml.FromModel(existingTable, tomlModelOptions));
    }

    return this;
  }

  private static bool MergeTomlTables( TomlTable target, TomlTable defaults )
  {
    var changed = false;

    foreach (var (key, defaultValue) in defaults)
    {
      if (!target.ContainsKey(key))
      {
        target[key] = defaultValue;
        changed = true;
      }
      else if (defaultValue is TomlTable defaultChild && target[key] is TomlTable targetChild)
      {
        if (MergeTomlTables(targetChild, defaultChild))
        {
          changed = true;
        }
      }
    }

    return changed;
  }

  public IPluginConfigurationService Configure( Action<IConfigurationBuilder> configure )
  {
    configure(Manager);
    return this;
  }

  public IConfigurationManager Manager {
    get {
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
