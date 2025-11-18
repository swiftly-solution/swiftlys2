
using SwiftlyS2.Core.ProtobufDefinitions;
using SwiftlyS2.Shared.Natives;
using SwiftlyS2.Shared.NetMessages;

namespace SwiftlyS2.Shared.ProtobufDefinitions;

public interface CMsgGC_GlobalGame_Unsubscribe : ITypedProtobuf<CMsgGC_GlobalGame_Unsubscribe>
{
  static CMsgGC_GlobalGame_Unsubscribe ITypedProtobuf<CMsgGC_GlobalGame_Unsubscribe>.Wrap(nint handle, bool isManuallyAllocated) => new CMsgGC_GlobalGame_UnsubscribeImpl(handle, isManuallyAllocated);


  public int Timeleft { get; set; }

}
