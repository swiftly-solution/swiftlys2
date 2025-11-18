
using SwiftlyS2.Core.Natives;
using SwiftlyS2.Core.NetMessages;
using SwiftlyS2.Shared.Natives;
using SwiftlyS2.Shared.NetMessages;
using SwiftlyS2.Shared.ProtobufDefinitions;

namespace SwiftlyS2.Core.ProtobufDefinitions;

internal class CMsgGCCStrike15_v2_PlayerOverwatchCaseStatusImpl : TypedProtobuf<CMsgGCCStrike15_v2_PlayerOverwatchCaseStatus>, CMsgGCCStrike15_v2_PlayerOverwatchCaseStatus
{
  public CMsgGCCStrike15_v2_PlayerOverwatchCaseStatusImpl(nint handle, bool isManuallyAllocated): base(handle)
  {
  }


  public ulong Caseid
  { get => Accessor.GetUInt64("caseid"); set => Accessor.SetUInt64("caseid", value); }


  public uint Statusid
  { get => Accessor.GetUInt32("statusid"); set => Accessor.SetUInt32("statusid", value); }

}
