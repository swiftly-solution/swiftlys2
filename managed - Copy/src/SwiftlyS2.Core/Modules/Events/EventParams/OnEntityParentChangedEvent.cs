using SwiftlyS2.Shared.Events;
using SwiftlyS2.Shared.SchemaDefinitions;

namespace SwiftlyS2.Core.Events;

internal class OnEntityParentChangedEvent : IOnEntityParentChangedEvent
{

  public required CEntityInstance Entity { get; set; }

  public CEntityInstance? NewParent { get; set; }
} 