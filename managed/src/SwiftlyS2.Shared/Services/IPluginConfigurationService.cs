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
  /// When the file name ends with <c>.jsonc</c>, properties and fields marked with <see cref="System.ComponentModel.DescriptionAttribute"/>
  /// are written with their description as <c>//</c> comment lines above the key, including members of nested objects, list items
  /// and dictionary values. A <c>.json</c> file is always written as plain JSON, since comments are not valid JSON.
  /// </summary>
  /// <typeparam name="T">The type of the configuration model.</typeparam>
  /// <param name="name">The name of the configuration file.</param>
  /// <param name="sectionName">The name of the section in the configuration file.</param>
  public IPluginConfigurationService InitializeJsonWithModel<T>(string name, string sectionName) where T : class, new();

  /// <summary>
  /// Initialize the json configuration file with a class as template.
  /// Behaves like <see cref="InitializeJsonWithModel{T}(string, string)"/> when the file does not exist yet.
  /// When the file exists and <paramref name="addMissingKeys"/> is <c>true</c>, keys present in the model but missing from the file
  /// are inserted with their default value, including keys of nested objects. In a <c>.jsonc</c> file each inserted key gets its
  /// <see cref="System.ComponentModel.DescriptionAttribute"/> as <c>//</c> comment lines above it. The rest of the file, including
  /// existing values, comments and formatting, is left untouched. Dictionary entries and list items are never added.
  /// Keys are matched case-insensitively, the same way the configuration binder reads them. A file that is not valid JSON is not changed.
  /// </summary>
  /// <typeparam name="T">The type of the configuration model.</typeparam>
  /// <param name="name">The name of the configuration file.</param>
  /// <param name="sectionName">The name of the section in the configuration file.</param>
  /// <param name="addMissingKeys">Whether to insert keys missing from an existing file.</param>
  public IPluginConfigurationService InitializeJsonWithModel<T>(string name, string sectionName, bool addMissingKeys) where T : class, new();

  /// <summary>
  /// Initialize the TOML configuration file with a class as template.
  /// Properties and fields marked with <see cref="System.ComponentModel.DescriptionAttribute"/> are written with their
  /// description as <c>#</c> comment lines above the key or above the table header. Members of inline tables cannot carry comments.
  /// </summary>
  /// <typeparam name="T">The type of the configuration model.</typeparam>
  /// <param name="name">The name of the configuration file.</param>
  /// <param name="sectionName">The name of the section in the configuration file.</param>
  public IPluginConfigurationService InitializeTomlWithModel<T>(string name, string sectionName) where T : class, new();

  /// <summary>
  /// Initialize the TOML configuration file with a class as template.
  /// Behaves like <see cref="InitializeTomlWithModel{T}(string, string)"/> when the file does not exist yet.
  /// When the file exists and <paramref name="addMissingKeys"/> is <c>true</c>, keys present in the model but missing from an existing table
  /// are inserted after the table's last value, and tables missing from the file are appended at its end, each with its
  /// <see cref="System.ComponentModel.DescriptionAttribute"/> as <c>#</c> comment lines. The rest of the file, including existing values,
  /// comments and formatting, is left untouched. Members of inline tables and entries of table arrays are never added.
  /// Keys are matched case-insensitively, the same way the configuration binder reads them. A file that is not valid TOML is not changed.
  /// </summary>
  /// <typeparam name="T">The type of the configuration model.</typeparam>
  /// <param name="name">The name of the configuration file.</param>
  /// <param name="sectionName">The name of the section in the configuration file.</param>
  /// <param name="addMissingKeys">Whether to insert keys missing from an existing file.</param>
  public IPluginConfigurationService InitializeTomlWithModel<T>(string name, string sectionName, bool addMissingKeys) where T : class, new();

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
