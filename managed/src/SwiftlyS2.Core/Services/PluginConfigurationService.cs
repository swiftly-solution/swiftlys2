using System.Data.Common;
using System.Text.Json;
using System.Text.Json.Serialization.Metadata;
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
    return InitializeJsonWithModel<T>(name, sectionName, false);
  }

  public IPluginConfigurationService InitializeJsonWithModel<T>( string name, string sectionName, bool addMissingKeys ) where T : class, new()
  {

    var configPath = GetConfigPath(name);
    var exists = File.Exists(configPath);

    if (exists && !addMissingKeys)
    {
      return this;
    }

    var config = new T();

    var wrapped = new Dictionary<string, object?> {
      [sectionName] = config
    };

    var options = new JsonSerializerOptions {
      WriteIndented = true,
      IncludeFields = true,
      PropertyNamingPolicy = null,
      TypeInfoResolver = new DefaultJsonTypeInfoResolver {
        Modifiers = { ConfigurationCommentService.ApplyConfigurationKeyNames }
      }
    };

    var writeComments = Path.GetExtension(name).Equals(".jsonc", StringComparison.OrdinalIgnoreCase) && ConfigurationCommentService.HasDescriptions(typeof(T));

    if (exists)
    {
      var defaults = JsonSerializer.SerializeToNode(wrapped, options)!.AsObject();
      var updated = ConfigurationMissingJsonKeyService.AddMissingKeys(File.ReadAllText(configPath), defaults, sectionName, typeof(T), options, writeComments);
      if (updated is not null)
      {
        File.WriteAllText(configPath, updated);
      }
      return this;
    }

    var dir = Path.GetDirectoryName(configPath);
    if (dir is not null)
    {
      Directory.CreateDirectory(dir);
    }

    var configJson = JsonSerializer.Serialize(wrapped, options);
    if (writeComments)
    {
      configJson = ConfigurationCommentService.WriteJson(configJson, sectionName, typeof(T), options);
    }
    File.WriteAllText(configPath, configJson);

    return this;
  }

  public IPluginConfigurationService InitializeTomlWithModel<T>( string name, string sectionName ) where T : class, new()
  {
    return InitializeTomlWithModel<T>(name, sectionName, false);
  }

  public IPluginConfigurationService InitializeTomlWithModel<T>( string name, string sectionName, bool addMissingKeys ) where T : class, new()
  {

    var configPath = GetConfigPath(name);
    var exists = File.Exists(configPath);

    if (exists && !addMissingKeys)
    {
      return this;
    }

    var config = new T();

    var wrapped = new Dictionary<string, object?> {
      [sectionName] = config
    };

    var tomlModelOptions = new TomlModelOptions {
      ConvertPropertyName = name => name,
      IgnoreMissingProperties = true
    };
    var getPropertyName = tomlModelOptions.GetPropertyName;
    var getFieldName = tomlModelOptions.GetFieldName;
    tomlModelOptions.GetPropertyName = property => ConfigurationCommentService.GetConfigurationKeyName(property) ?? getPropertyName(property);
    tomlModelOptions.GetFieldName = field => ConfigurationCommentService.GetConfigurationKeyName(field) ?? getFieldName(field);

    var tomlString = Toml.FromModel(wrapped, tomlModelOptions);
    if (ConfigurationCommentService.HasDescriptions(typeof(T)))
    {
      tomlString = ConfigurationCommentService.WriteToml(tomlString, sectionName, typeof(T));
    }

    if (exists)
    {
      var updated = ConfigurationMissingTomlKeyService.AddMissingKeys(File.ReadAllText(configPath), tomlString);
      if (updated is not null)
      {
        File.WriteAllText(configPath, updated);
      }
      return this;
    }

    var dir = Path.GetDirectoryName(configPath);
    if (dir is not null)
    {
      Directory.CreateDirectory(dir);
    }

    File.WriteAllText(configPath, tomlString);

    return this;
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
      if (_Manager == null)
      {
        _Manager = new ConfigurationManager();
        _ = _Manager.SetBasePath(BasePath);
      }
      return _Manager;
    }
  }
}
