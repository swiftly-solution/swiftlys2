
using SwiftlyS2.Core.Natives;
using SwiftlyS2.Core.NetMessages;
using SwiftlyS2.Shared.Natives;
using SwiftlyS2.Shared.NetMessages;
using SwiftlyS2.Shared.ProtobufDefinitions;

namespace SwiftlyS2.Core.ProtobufDefinitions;

internal class CMsgGCCStrike15_v2_BetaEnrollmentImpl : TypedProtobuf<CMsgGCCStrike15_v2_BetaEnrollment>, CMsgGCCStrike15_v2_BetaEnrollment
{
  public CMsgGCCStrike15_v2_BetaEnrollmentImpl(nint handle, bool isManuallyAllocated): base(handle)
  {
  }


  public uint Eresult
  { get => Accessor.GetUInt32("eresult"); set => Accessor.SetUInt32("eresult", value); }

}
