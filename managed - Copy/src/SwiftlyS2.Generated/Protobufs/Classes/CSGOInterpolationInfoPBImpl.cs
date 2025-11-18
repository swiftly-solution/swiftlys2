
using SwiftlyS2.Core.Natives;
using SwiftlyS2.Core.NetMessages;
using SwiftlyS2.Shared.Natives;
using SwiftlyS2.Shared.NetMessages;
using SwiftlyS2.Shared.ProtobufDefinitions;

namespace SwiftlyS2.Core.ProtobufDefinitions;

internal class CSGOInterpolationInfoPBImpl : TypedProtobuf<CSGOInterpolationInfoPB>, CSGOInterpolationInfoPB
{
  public CSGOInterpolationInfoPBImpl(nint handle, bool isManuallyAllocated): base(handle)
  {
  }


  public int SrcTick
  { get => Accessor.GetInt32("src_tick"); set => Accessor.SetInt32("src_tick", value); }


  public int DstTick
  { get => Accessor.GetInt32("dst_tick"); set => Accessor.SetInt32("dst_tick", value); }


  public float Frac
  { get => Accessor.GetFloat("frac"); set => Accessor.SetFloat("frac", value); }

}
