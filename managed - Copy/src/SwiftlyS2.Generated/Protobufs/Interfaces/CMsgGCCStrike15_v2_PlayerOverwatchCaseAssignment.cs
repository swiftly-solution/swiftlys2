
using SwiftlyS2.Core.ProtobufDefinitions;
using SwiftlyS2.Shared.Natives;
using SwiftlyS2.Shared.NetMessages;

namespace SwiftlyS2.Shared.ProtobufDefinitions;

public interface CMsgGCCStrike15_v2_PlayerOverwatchCaseAssignment : ITypedProtobuf<CMsgGCCStrike15_v2_PlayerOverwatchCaseAssignment>
{
  static CMsgGCCStrike15_v2_PlayerOverwatchCaseAssignment ITypedProtobuf<CMsgGCCStrike15_v2_PlayerOverwatchCaseAssignment>.Wrap(nint handle, bool isManuallyAllocated) => new CMsgGCCStrike15_v2_PlayerOverwatchCaseAssignmentImpl(handle, isManuallyAllocated);


  public ulong Caseid { get; set; }


  public string Caseurl { get; set; }


  public uint Verdict { get; set; }


  public uint Timestamp { get; set; }


  public uint Throttleseconds { get; set; }


  public uint Suspectid { get; set; }


  public uint Fractionid { get; set; }


  public uint Numrounds { get; set; }


  public uint Fractionrounds { get; set; }


  public int Streakconvictions { get; set; }


  public uint Reason { get; set; }

}
