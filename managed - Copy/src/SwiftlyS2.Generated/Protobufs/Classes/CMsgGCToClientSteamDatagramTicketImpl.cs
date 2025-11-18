
using SwiftlyS2.Core.Natives;
using SwiftlyS2.Core.NetMessages;
using SwiftlyS2.Shared.Natives;
using SwiftlyS2.Shared.NetMessages;
using SwiftlyS2.Shared.ProtobufDefinitions;

namespace SwiftlyS2.Core.ProtobufDefinitions;

internal class CMsgGCToClientSteamDatagramTicketImpl : TypedProtobuf<CMsgGCToClientSteamDatagramTicket>, CMsgGCToClientSteamDatagramTicket
{
  public CMsgGCToClientSteamDatagramTicketImpl(nint handle, bool isManuallyAllocated): base(handle)
  {
  }


  public byte[] SerializedTicket
  { get => Accessor.GetBytes("serialized_ticket"); set => Accessor.SetBytes("serialized_ticket", value); }

}
