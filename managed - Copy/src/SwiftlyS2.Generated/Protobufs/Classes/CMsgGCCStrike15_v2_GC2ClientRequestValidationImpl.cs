
using SwiftlyS2.Core.Natives;
using SwiftlyS2.Core.NetMessages;
using SwiftlyS2.Shared.Natives;
using SwiftlyS2.Shared.NetMessages;
using SwiftlyS2.Shared.ProtobufDefinitions;

namespace SwiftlyS2.Core.ProtobufDefinitions;

internal class CMsgGCCStrike15_v2_GC2ClientRequestValidationImpl : TypedProtobuf<CMsgGCCStrike15_v2_GC2ClientRequestValidation>, CMsgGCCStrike15_v2_GC2ClientRequestValidation
{
  public CMsgGCCStrike15_v2_GC2ClientRequestValidationImpl(nint handle, bool isManuallyAllocated): base(handle)
  {
  }


  public bool FullReport
  { get => Accessor.GetBool("full_report"); set => Accessor.SetBool("full_report", value); }


  public string Module
  { get => Accessor.GetString("module"); set => Accessor.SetString("module", value); }

}
