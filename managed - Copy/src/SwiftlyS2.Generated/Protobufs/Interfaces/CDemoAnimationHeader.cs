
using SwiftlyS2.Core.ProtobufDefinitions;
using SwiftlyS2.Shared.Natives;
using SwiftlyS2.Shared.NetMessages;

namespace SwiftlyS2.Shared.ProtobufDefinitions;

public interface CDemoAnimationHeader : ITypedProtobuf<CDemoAnimationHeader>
{
  static CDemoAnimationHeader ITypedProtobuf<CDemoAnimationHeader>.Wrap(nint handle, bool isManuallyAllocated) => new CDemoAnimationHeaderImpl(handle, isManuallyAllocated);


  public int EntityId { get; set; }


  public int Tick { get; set; }


  public byte[] Data { get; set; }

}
