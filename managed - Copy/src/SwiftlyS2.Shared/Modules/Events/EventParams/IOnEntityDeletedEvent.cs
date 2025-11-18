using SwiftlyS2.Shared.SchemaDefinitions;

namespace SwiftlyS2.Shared.Events;

/// <summary>
/// Called when an entity is deleted.
/// </summary>
public interface IOnEntityDeletedEvent {

  /// <summary>
  /// The entity that was deleted.
  /// </summary>
  public CEntityInstance Entity { get; }
} 