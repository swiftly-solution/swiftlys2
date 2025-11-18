using SwiftlyS2.Shared.SchemaDefinitions;

namespace SwiftlyS2.Shared.Events;

/// <summary>
/// Called when an entity is spawned.
/// </summary>
public interface IOnEntitySpawnedEvent {

  /// <summary>
  /// The entity that was spawned.
  /// </summary>
  public CEntityInstance Entity { get; }
} 