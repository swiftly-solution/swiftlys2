using SwiftlyS2.Shared.Events;

namespace SwiftlyS2.Core.Events;

internal class OnClientPutInServerEvent : IOnClientPutInServerEvent
{

  public int PlayerId { get; set; }

  public ClientKind Kind { get; set; }
} 