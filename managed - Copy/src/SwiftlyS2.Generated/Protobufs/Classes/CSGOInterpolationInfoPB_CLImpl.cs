
using SwiftlyS2.Core.Natives;
using SwiftlyS2.Core.NetMessages;
using SwiftlyS2.Shared.Natives;
using SwiftlyS2.Shared.NetMessages;
using SwiftlyS2.Shared.ProtobufDefinitions;

namespace SwiftlyS2.Core.ProtobufDefinitions;

internal class CSGOInterpolationInfoPB_CLImpl : TypedProtobuf<CSGOInterpolationInfoPB_CL>, CSGOInterpolationInfoPB_CL
{
  public CSGOInterpolationInfoPB_CLImpl(nint handle, bool isManuallyAllocated): base(handle)
  {
  }


  public float Frac
  { get => Accessor.GetFloat("frac"); set => Accessor.SetFloat("frac", value); }

}
