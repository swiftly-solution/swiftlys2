
using SwiftlyS2.Core.Natives;
using SwiftlyS2.Core.NetMessages;
using SwiftlyS2.Shared.Natives;
using SwiftlyS2.Shared.NetMessages;
using SwiftlyS2.Shared.ProtobufDefinitions;

namespace SwiftlyS2.Core.ProtobufDefinitions;

internal class CMsgGCCStrike15_v2_PlayerOverwatchCaseAssignmentImpl : TypedProtobuf<CMsgGCCStrike15_v2_PlayerOverwatchCaseAssignment>, CMsgGCCStrike15_v2_PlayerOverwatchCaseAssignment
{
  public CMsgGCCStrike15_v2_PlayerOverwatchCaseAssignmentImpl(nint handle, bool isManuallyAllocated): base(handle)
  {
  }


  public ulong Caseid
  { get => Accessor.GetUInt64("caseid"); set => Accessor.SetUInt64("caseid", value); }


  public string Caseurl
  { get => Accessor.GetString("caseurl"); set => Accessor.SetString("caseurl", value); }


  public uint Verdict
  { get => Accessor.GetUInt32("verdict"); set => Accessor.SetUInt32("verdict", value); }


  public uint Timestamp
  { get => Accessor.GetUInt32("timestamp"); set => Accessor.SetUInt32("timestamp", value); }


  public uint Throttleseconds
  { get => Accessor.GetUInt32("throttleseconds"); set => Accessor.SetUInt32("throttleseconds", value); }


  public uint Suspectid
  { get => Accessor.GetUInt32("suspectid"); set => Accessor.SetUInt32("suspectid", value); }


  public uint Fractionid
  { get => Accessor.GetUInt32("fractionid"); set => Accessor.SetUInt32("fractionid", value); }


  public uint Numrounds
  { get => Accessor.GetUInt32("numrounds"); set => Accessor.SetUInt32("numrounds", value); }


  public uint Fractionrounds
  { get => Accessor.GetUInt32("fractionrounds"); set => Accessor.SetUInt32("fractionrounds", value); }


  public int Streakconvictions
  { get => Accessor.GetInt32("streakconvictions"); set => Accessor.SetInt32("streakconvictions", value); }


  public uint Reason
  { get => Accessor.GetUInt32("reason"); set => Accessor.SetUInt32("reason", value); }

}
