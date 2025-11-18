
using SwiftlyS2.Core.ProtobufDefinitions;
using SwiftlyS2.Shared.Natives;
using SwiftlyS2.Shared.NetMessages;

namespace SwiftlyS2.Shared.ProtobufDefinitions;

public interface CSGOInterpolationInfoPB : ITypedProtobuf<CSGOInterpolationInfoPB>
{
  static CSGOInterpolationInfoPB ITypedProtobuf<CSGOInterpolationInfoPB>.Wrap(nint handle, bool isManuallyAllocated) => new CSGOInterpolationInfoPBImpl(handle, isManuallyAllocated);


  public int SrcTick { get; set; }


  public int DstTick { get; set; }


  public float Frac { get; set; }

}
