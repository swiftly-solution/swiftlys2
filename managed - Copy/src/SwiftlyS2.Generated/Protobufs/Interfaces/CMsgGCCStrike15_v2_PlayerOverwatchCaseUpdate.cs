
using SwiftlyS2.Core.ProtobufDefinitions;
using SwiftlyS2.Shared.Natives;
using SwiftlyS2.Shared.NetMessages;

namespace SwiftlyS2.Shared.ProtobufDefinitions;

public interface CMsgGCCStrike15_v2_PlayerOverwatchCaseUpdate : ITypedProtobuf<CMsgGCCStrike15_v2_PlayerOverwatchCaseUpdate>
{
  static CMsgGCCStrike15_v2_PlayerOverwatchCaseUpdate ITypedProtobuf<CMsgGCCStrike15_v2_PlayerOverwatchCaseUpdate>.Wrap(nint handle, bool isManuallyAllocated) => new CMsgGCCStrike15_v2_PlayerOverwatchCaseUpdateImpl(handle, isManuallyAllocated);


  public ulong Caseid { get; set; }


  public uint Suspectid { get; set; }


  public uint Fractionid { get; set; }


  public uint RptAimbot { get; set; }


  public uint RptWallhack { get; set; }


  public uint RptSpeedhack { get; set; }


  public uint RptTeamharm { get; set; }


  public uint Reason { get; set; }

}
