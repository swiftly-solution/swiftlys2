namespace SwiftlyS2.Shared.Services;

public interface IRegistratorService {

  /// <summary>
  /// Register a object that contains attributes for listeners.
  /// </summary>
  /// <param name="instance">Any object that contains attributes for listeners.</param>
  void Register(object instance);
}