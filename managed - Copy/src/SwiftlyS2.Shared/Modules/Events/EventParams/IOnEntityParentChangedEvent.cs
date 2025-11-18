using SwiftlyS2.Shared.SchemaDefinitions;

namespace SwiftlyS2.Shared.Events;

/// <summary>
/// Called when an entity's parent changes.
/// </summary>
public interface IOnEntityParentChangedEvent {

  /// <summary>
  /// The entity that had its parent changed.
  /// </summary>
  public CEntityInstance Entity { get; }

  /// <summary>
  /// The new parent of the entity.
  /// </summary>
  public CEntityInstance? NewParent { get; }
} 