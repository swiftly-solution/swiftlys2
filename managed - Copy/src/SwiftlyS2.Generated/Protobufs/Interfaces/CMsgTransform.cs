
using SwiftlyS2.Core.ProtobufDefinitions;
using SwiftlyS2.Shared.Natives;
using SwiftlyS2.Shared.NetMessages;

namespace SwiftlyS2.Shared.ProtobufDefinitions;

public interface CMsgTransform : ITypedProtobuf<CMsgTransform>
{
  static CMsgTransform ITypedProtobuf<CMsgTransform>.Wrap(nint handle, bool isManuallyAllocated) => new CMsgTransformImpl(handle, isManuallyAllocated);


  public Vector Position { get; set; }


  public float Scale { get; set; }


  public CMsgQuaternion Orientation { get; }

}
