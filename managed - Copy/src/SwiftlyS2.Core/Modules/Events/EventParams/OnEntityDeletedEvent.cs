using SwiftlyS2.Shared.Events;
using SwiftlyS2.Shared.SchemaDefinitions;

namespace SwiftlyS2.Core.Events;

internal class OnEntityDeletedEvent : IOnEntityDeletedEvent
{

  public required CEntityInstance Entity { get; set; }
} 