using Microsoft.Extensions.Configuration;

namespace SwiftlyS2.Shared.Services;

public interface IPluginConfigurationService
{

  /// <summary>
  /// Get the base path of plugin configuration.
  /// </summary>
  /// <returns>The base path of the plugin configuration.</returns>
  public string BasePath { get; }


  /// <summary>
  /// Get the path to the configuration file.
  /// </summary>
  /// <param name="name">The name of the configuration file, including the extension.</param>
  /// <returns>The path to the configuration file.</returns>
  public string GetConfigPath(string name);

  /// <summary>
  /// Initialize the configuration file with a template.
  /// To use this, you must package a templates folder in the plugin, with the template file in it.
  /// </summary>
  /// <param name="name">The name of the configuration file.</param>
  /// <param name="templateName">The name of the template file.</param>
  public IPluginConfigurationService InitializeWithTemplate(string name, string templateName);

  /// <summary>
  /// Initialize the json configuration file with a class as template.
  /// </summary>
  /// <typeparam name="T">The type of the configuration model.</typeparam>
  /// <param name="name">The name of the configuration file.</param>
  /// <param name="sectionName">The name of the section in the configuration file.</param>
  public IPluginConfigurationService InitializeJsonWithModel<T>(string name, string sectionName) where T : class, new();

  /// <summary>
  /// Initialize the TOML configuration file with a class as template.
  /// </summary>
  /// <typeparam name="T">The type of the configuration model.</typeparam>
  /// <param name="name">The name of the configuration file.</param>
  /// <param name="sectionName">The name of the section in the configuration file.</param>
  public IPluginConfigurationService InitializeTomlWithModel<T>(string name, string sectionName) where T : class, new();

  /// <summary>
  /// Configure the internal configuration manager.
  /// </summary>
  /// <param name="configure">The action to configure the configuration manager.</param>
  /// <returns>The plugin configuration service.</returns>
  public IPluginConfigurationService Configure(Action<IConfigurationBuilder> configure);


  /// <summary>
  /// Get the configuration root.
  /// </summary>
  public IConfigurationManager Manager { get; }

  /// <summary>
  /// Whether the base path exists in the file system.
  /// </summary>
  public bool BasePathExists { get; }

}
