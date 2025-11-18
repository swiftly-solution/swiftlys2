using SwiftlyS2.Shared.Events;

namespace SwiftlyS2.Core.Events;

internal class OnMapLoadEvent : IOnMapLoadEvent
{

  public required string MapName { get; set; }
} 