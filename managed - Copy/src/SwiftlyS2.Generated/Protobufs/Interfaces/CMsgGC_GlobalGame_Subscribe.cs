
using SwiftlyS2.Core.ProtobufDefinitions;
using SwiftlyS2.Shared.Natives;
using SwiftlyS2.Shared.NetMessages;

namespace SwiftlyS2.Shared.ProtobufDefinitions;

public interface CMsgGC_GlobalGame_Subscribe : ITypedProtobuf<CMsgGC_GlobalGame_Subscribe>
{
  static CMsgGC_GlobalGame_Subscribe ITypedProtobuf<CMsgGC_GlobalGame_Subscribe>.Wrap(nint handle, bool isManuallyAllocated) => new CMsgGC_GlobalGame_SubscribeImpl(handle, isManuallyAllocated);


  public ulong Ticket { get; set; }

}
