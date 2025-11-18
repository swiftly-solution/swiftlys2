
using SwiftlyS2.Core.Natives;
using SwiftlyS2.Core.NetMessages;
using SwiftlyS2.Shared.Natives;
using SwiftlyS2.Shared.NetMessages;
using SwiftlyS2.Shared.ProtobufDefinitions;

namespace SwiftlyS2.Core.ProtobufDefinitions;

internal class CMsgGCCStrike15_v2_PlayerOverwatchCaseUpdateImpl : TypedProtobuf<CMsgGCCStrike15_v2_PlayerOverwatchCaseUpdate>, CMsgGCCStrike15_v2_PlayerOverwatchCaseUpdate
{
  public CMsgGCCStrike15_v2_PlayerOverwatchCaseUpdateImpl(nint handle, bool isManuallyAllocated): base(handle)
  {
  }


  public ulong Caseid
  { get => Accessor.GetUInt64("caseid"); set => Accessor.SetUInt64("caseid", value); }


  public uint Suspectid
  { get => Accessor.GetUInt32("suspectid"); set => Accessor.SetUInt32("suspectid", value); }


  public uint Fractionid
  { get => Accessor.GetUInt32("fractionid"); set => Accessor.SetUInt32("fractionid", value); }


  public uint RptAimbot
  { get => Accessor.GetUInt32("rpt_aimbot"); set => Accessor.SetUInt32("rpt_aimbot", value); }


  public uint RptWallhack
  { get => Accessor.GetUInt32("rpt_wallhack"); set => Accessor.SetUInt32("rpt_wallhack", value); }


  public uint RptSpeedhack
  { get => Accessor.GetUInt32("rpt_speedhack"); set => Accessor.SetUInt32("rpt_speedhack", value); }


  public uint RptTeamharm
  { get => Accessor.GetUInt32("rpt_teamharm"); set => Accessor.SetUInt32("rpt_teamharm", value); }


  public uint Reason
  { get => Accessor.GetUInt32("reason"); set => Accessor.SetUInt32("reason", value); }

}
