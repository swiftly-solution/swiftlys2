using SwiftlyS2.Shared.Events;
using SwiftlyS2.Shared.SchemaDefinitions;

namespace SwiftlyS2.Core.Events;

internal class OnEntityCreatedEvent : IOnEntityCreatedEvent
{

  public required CEntityInstance Entity { get; set; }
} 