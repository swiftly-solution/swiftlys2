namespace SwiftlyS2.Shared.Translation;

/// <summary>
/// Represents a localizer that can be used to get translations for a given key.
/// </summary>
public interface ILocalizer
{
  
  /// <summary>
  /// Gets the translation for the specified key.
  /// </summary>
  /// <param name="key">The key of the translation.</param>
  /// <returns>The translation for the specified key.</returns>
  string this[string key] { get; }

  /// <summary>
  /// Gets the translation for the specified key with the specified arguments.
  /// </summary>
  /// <param name="key">The key of the translation.</param>
  /// <param name="args">The arguments to format the translation with. Use <see cref="string.Format(string, object[])"/> to format the translation.</param>
  /// <returns>The translation for the specified key with the specified arguments.</returns>
  string this[string key, params object[] args] { get; }
}