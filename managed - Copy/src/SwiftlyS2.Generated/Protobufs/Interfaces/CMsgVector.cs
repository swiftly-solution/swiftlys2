
using SwiftlyS2.Core.ProtobufDefinitions;
using SwiftlyS2.Shared.Natives;
using SwiftlyS2.Shared.NetMessages;

namespace SwiftlyS2.Shared.ProtobufDefinitions;

public interface CMsgVector : ITypedProtobuf<CMsgVector>
{
  static CMsgVector ITypedProtobuf<CMsgVector>.Wrap(nint handle, bool isManuallyAllocated) => new CMsgVectorImpl(handle, isManuallyAllocated);


  public float X { get; set; }


  public float Y { get; set; }


  public float Z { get; set; }


  public float W { get; set; }

}
