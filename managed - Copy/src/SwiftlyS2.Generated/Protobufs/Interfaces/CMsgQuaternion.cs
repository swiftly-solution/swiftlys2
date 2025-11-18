
using SwiftlyS2.Core.ProtobufDefinitions;
using SwiftlyS2.Shared.Natives;
using SwiftlyS2.Shared.NetMessages;

namespace SwiftlyS2.Shared.ProtobufDefinitions;

public interface CMsgQuaternion : ITypedProtobuf<CMsgQuaternion>
{
  static CMsgQuaternion ITypedProtobuf<CMsgQuaternion>.Wrap(nint handle, bool isManuallyAllocated) => new CMsgQuaternionImpl(handle, isManuallyAllocated);


  public float X { get; set; }


  public float Y { get; set; }


  public float Z { get; set; }


  public float W { get; set; }

}
