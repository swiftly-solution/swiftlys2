
using SwiftlyS2.Core.ProtobufDefinitions;
using SwiftlyS2.Shared.Natives;
using SwiftlyS2.Shared.NetMessages;

namespace SwiftlyS2.Shared.ProtobufDefinitions;

public interface CMsgGCCStrike15_v2_PlayerOverwatchCaseStatus : ITypedProtobuf<CMsgGCCStrike15_v2_PlayerOverwatchCaseStatus>
{
  static CMsgGCCStrike15_v2_PlayerOverwatchCaseStatus ITypedProtobuf<CMsgGCCStrike15_v2_PlayerOverwatchCaseStatus>.Wrap(nint handle, bool isManuallyAllocated) => new CMsgGCCStrike15_v2_PlayerOverwatchCaseStatusImpl(handle, isManuallyAllocated);


  public ulong Caseid { get; set; }


  public uint Statusid { get; set; }

}
