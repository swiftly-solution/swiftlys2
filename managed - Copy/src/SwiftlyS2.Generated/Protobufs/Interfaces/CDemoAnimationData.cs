
using SwiftlyS2.Core.ProtobufDefinitions;
using SwiftlyS2.Shared.Natives;
using SwiftlyS2.Shared.NetMessages;

namespace SwiftlyS2.Shared.ProtobufDefinitions;

public interface CDemoAnimationData : ITypedProtobuf<CDemoAnimationData>
{
  static CDemoAnimationData ITypedProtobuf<CDemoAnimationData>.Wrap(nint handle, bool isManuallyAllocated) => new CDemoAnimationDataImpl(handle, isManuallyAllocated);


  public int EntityId { get; set; }


  public int StartTick { get; set; }


  public int EndTick { get; set; }


  public byte[] Data { get; set; }


  public long DataChecksum { get; set; }

}
