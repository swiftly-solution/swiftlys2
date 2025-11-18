
using SwiftlyS2.Core.Natives;
using SwiftlyS2.Core.NetMessages;
using SwiftlyS2.Shared.Natives;
using SwiftlyS2.Shared.NetMessages;
using SwiftlyS2.Shared.ProtobufDefinitions;

namespace SwiftlyS2.Core.ProtobufDefinitions;

internal class CMsgGCCStrike15_v2_Client2GCEconPreviewDataBlockRequestImpl : TypedProtobuf<CMsgGCCStrike15_v2_Client2GCEconPreviewDataBlockRequest>, CMsgGCCStrike15_v2_Client2GCEconPreviewDataBlockRequest
{
  public CMsgGCCStrike15_v2_Client2GCEconPreviewDataBlockRequestImpl(nint handle, bool isManuallyAllocated): base(handle)
  {
  }


  public ulong ParamS
  { get => Accessor.GetUInt64("param_s"); set => Accessor.SetUInt64("param_s", value); }


  public ulong ParamA
  { get => Accessor.GetUInt64("param_a"); set => Accessor.SetUInt64("param_a", value); }


  public ulong ParamD
  { get => Accessor.GetUInt64("param_d"); set => Accessor.SetUInt64("param_d", value); }


  public ulong ParamM
  { get => Accessor.GetUInt64("param_m"); set => Accessor.SetUInt64("param_m", value); }

}
