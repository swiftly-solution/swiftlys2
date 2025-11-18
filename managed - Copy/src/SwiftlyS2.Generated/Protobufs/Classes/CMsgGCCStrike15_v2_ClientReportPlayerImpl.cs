
using SwiftlyS2.Core.Natives;
using SwiftlyS2.Core.NetMessages;
using SwiftlyS2.Shared.Natives;
using SwiftlyS2.Shared.NetMessages;
using SwiftlyS2.Shared.ProtobufDefinitions;

namespace SwiftlyS2.Core.ProtobufDefinitions;

internal class CMsgGCCStrike15_v2_ClientReportPlayerImpl : TypedProtobuf<CMsgGCCStrike15_v2_ClientReportPlayer>, CMsgGCCStrike15_v2_ClientReportPlayer
{
  public CMsgGCCStrike15_v2_ClientReportPlayerImpl(nint handle, bool isManuallyAllocated): base(handle)
  {
  }


  public uint AccountId
  { get => Accessor.GetUInt32("account_id"); set => Accessor.SetUInt32("account_id", value); }


  public uint RptAimbot
  { get => Accessor.GetUInt32("rpt_aimbot"); set => Accessor.SetUInt32("rpt_aimbot", value); }


  public uint RptWallhack
  { get => Accessor.GetUInt32("rpt_wallhack"); set => Accessor.SetUInt32("rpt_wallhack", value); }


  public uint RptSpeedhack
  { get => Accessor.GetUInt32("rpt_speedhack"); set => Accessor.SetUInt32("rpt_speedhack", value); }


  public uint RptTeamharm
  { get => Accessor.GetUInt32("rpt_teamharm"); set => Accessor.SetUInt32("rpt_teamharm", value); }


  public uint RptTextabuse
  { get => Accessor.GetUInt32("rpt_textabuse"); set => Accessor.SetUInt32("rpt_textabuse", value); }


  public uint RptVoiceabuse
  { get => Accessor.GetUInt32("rpt_voiceabuse"); set => Accessor.SetUInt32("rpt_voiceabuse", value); }


  public ulong MatchId
  { get => Accessor.GetUInt64("match_id"); set => Accessor.SetUInt64("match_id", value); }


  public bool ReportFromDemo
  { get => Accessor.GetBool("report_from_demo"); set => Accessor.SetBool("report_from_demo", value); }

}
