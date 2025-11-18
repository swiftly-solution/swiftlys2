
using SwiftlyS2.Core.ProtobufDefinitions;
using SwiftlyS2.Shared.Natives;
using SwiftlyS2.Shared.NetMessages;

namespace SwiftlyS2.Shared.ProtobufDefinitions;

public interface CMsgGCRequestSessionIPResponse : ITypedProtobuf<CMsgGCRequestSessionIPResponse>
{
  static CMsgGCRequestSessionIPResponse ITypedProtobuf<CMsgGCRequestSessionIPResponse>.Wrap(nint handle, bool isManuallyAllocated) => new CMsgGCRequestSessionIPResponseImpl(handle, isManuallyAllocated);


  public uint Ip { get; set; }

}
