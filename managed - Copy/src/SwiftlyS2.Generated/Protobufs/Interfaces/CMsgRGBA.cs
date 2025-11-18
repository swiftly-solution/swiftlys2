
using SwiftlyS2.Core.ProtobufDefinitions;
using SwiftlyS2.Shared.Natives;
using SwiftlyS2.Shared.NetMessages;

namespace SwiftlyS2.Shared.ProtobufDefinitions;

public interface CMsgRGBA : ITypedProtobuf<CMsgRGBA>
{
  static CMsgRGBA ITypedProtobuf<CMsgRGBA>.Wrap(nint handle, bool isManuallyAllocated) => new CMsgRGBAImpl(handle, isManuallyAllocated);


  public int R { get; set; }


  public int G { get; set; }


  public int B { get; set; }


  public int A { get; set; }

}
