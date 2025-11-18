namespace SwiftlyS2.Shared.Events;

/// <summary>
/// Called when the map is loaded.
/// </summary>
public interface IOnMapLoadEvent {

  /// <summary>
  /// The name of the map.
  /// </summary>
  public string MapName { get; }
} 