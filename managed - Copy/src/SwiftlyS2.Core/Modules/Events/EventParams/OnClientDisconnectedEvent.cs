using SwiftlyS2.Shared.Events;
using SwiftlyS2.Shared.ProtobufDefinitions;

namespace SwiftlyS2.Core.Events;

internal class OnClientDisconnectedEvent : IOnClientDisconnectedEvent
{

  public int PlayerId { get; set; }

  public ENetworkDisconnectionReason Reason { get; set; }
} 