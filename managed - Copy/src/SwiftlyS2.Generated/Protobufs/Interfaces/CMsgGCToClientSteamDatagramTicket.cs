
using SwiftlyS2.Core.ProtobufDefinitions;
using SwiftlyS2.Shared.Natives;
using SwiftlyS2.Shared.NetMessages;

namespace SwiftlyS2.Shared.ProtobufDefinitions;

public interface CMsgGCToClientSteamDatagramTicket : ITypedProtobuf<CMsgGCToClientSteamDatagramTicket>
{
  static CMsgGCToClientSteamDatagramTicket ITypedProtobuf<CMsgGCToClientSteamDatagramTicket>.Wrap(nint handle, bool isManuallyAllocated) => new CMsgGCToClientSteamDatagramTicketImpl(handle, isManuallyAllocated);


  public byte[] SerializedTicket { get; set; }

}
