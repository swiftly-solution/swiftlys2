namespace SwiftlyS2.Shared.Events;

/// <summary>
/// Called when the map is unloaded.
/// </summary>
public interface IOnMapUnloadEvent {

  /// <summary>
  /// The name of the map.
  /// </summary>
  public string MapName { get; }
} 