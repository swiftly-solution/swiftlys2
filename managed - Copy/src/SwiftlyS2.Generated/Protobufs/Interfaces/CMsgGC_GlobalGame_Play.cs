
using SwiftlyS2.Core.ProtobufDefinitions;
using SwiftlyS2.Shared.Natives;
using SwiftlyS2.Shared.NetMessages;

namespace SwiftlyS2.Shared.ProtobufDefinitions;

public interface CMsgGC_GlobalGame_Play : ITypedProtobuf<CMsgGC_GlobalGame_Play>
{
  static CMsgGC_GlobalGame_Play ITypedProtobuf<CMsgGC_GlobalGame_Play>.Wrap(nint handle, bool isManuallyAllocated) => new CMsgGC_GlobalGame_PlayImpl(handle, isManuallyAllocated);


  public ulong Ticket { get; set; }


  public uint Gametimems { get; set; }


  public uint Msperpoint { get; set; }

}
