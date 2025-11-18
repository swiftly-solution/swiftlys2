
using SwiftlyS2.Core.ProtobufDefinitions;
using SwiftlyS2.Shared.Natives;
using SwiftlyS2.Shared.NetMessages;

namespace SwiftlyS2.Shared.ProtobufDefinitions;

public interface CMsgGCRequestSessionIP : ITypedProtobuf<CMsgGCRequestSessionIP>
{
  static CMsgGCRequestSessionIP ITypedProtobuf<CMsgGCRequestSessionIP>.Wrap(nint handle, bool isManuallyAllocated) => new CMsgGCRequestSessionIPImpl(handle, isManuallyAllocated);


  public ulong Steamid { get; set; }

}
