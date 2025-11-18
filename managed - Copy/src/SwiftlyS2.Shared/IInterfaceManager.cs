namespace SwiftlyS2.Shared;

public interface IInterfaceManager {

  /// <summary>
  /// Add a keyed shared interface for other plugin to use.
  /// The interface must be defined in the contracts dll.
  /// </summary>
  /// <typeparam name="TInterface">The interface to add.</typeparam>
  /// <typeparam name="TImpl">The implementation of the interface.</typeparam>
  /// <param name="key">The key of the interface.</param>
  /// <param name="implInstance">The implementation of the interface.</param>
  public void AddSharedInterface<TInterface, TImpl>(string key, TImpl implInstance)
            where TInterface : class
            where TImpl : class, TInterface;

  /// <summary>
  /// Check if a shared interface exists.
  /// </summary>
  /// <param name="key">The key of the interface.</param>
  /// <returns>True if the interface exists, false otherwise.</returns>
  public bool HasSharedInterface(string key);

  /// <summary>
  /// Get a shared interface.
  /// </summary>
  /// <typeparam name="TInterface">The interface to get.</typeparam>
  /// <param name="key">The key of the interface.</param>
  /// <returns>The implementation of the interface.</returns>
  public TInterface GetSharedInterface<TInterface>(string key) where TInterface : class;

}