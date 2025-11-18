using SwiftlyS2.Shared.Events;

namespace SwiftlyS2.Core.Events;

internal class OnClientKeyStateChangedEvent : IOnClientKeyStateChangedEvent
{

  public int PlayerId { get; set; }

  public KeyKind Key { get; set; }

  public bool Pressed { get; set; }
} 