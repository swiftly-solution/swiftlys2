using SwiftlyS2.Shared.Events;
using SwiftlyS2.Shared.SchemaDefinitions;

namespace SwiftlyS2.Core.Events;

internal class OnEntitySpawnedEvent : IOnEntitySpawnedEvent
{

  public required CEntityInstance Entity { get; set; }
} 