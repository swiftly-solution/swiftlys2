using SwiftlyS2.Shared.SchemaDefinitions;

namespace SwiftlyS2.Shared.Events;

/// <summary>
/// Called when an entity is created.
/// </summary>
public interface IOnEntityCreatedEvent {

  /// <summary>
  /// The entity that was created.
  /// The entity is not fully initialized when this event is called, better do things on next tick and also add a validity check there.
  /// </summary>
  public CEntityInstance Entity { get; }
} 