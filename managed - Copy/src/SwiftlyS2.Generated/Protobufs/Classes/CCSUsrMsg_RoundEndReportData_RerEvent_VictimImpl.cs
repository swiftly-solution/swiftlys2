
using SwiftlyS2.Core.Natives;
using SwiftlyS2.Core.NetMessages;
using SwiftlyS2.Shared.Natives;
using SwiftlyS2.Shared.NetMessages;
using SwiftlyS2.Shared.ProtobufDefinitions;

namespace SwiftlyS2.Core.ProtobufDefinitions;

internal class CCSUsrMsg_RoundEndReportData_RerEvent_VictimImpl : TypedProtobuf<CCSUsrMsg_RoundEndReportData_RerEvent_Victim>, CCSUsrMsg_RoundEndReportData_RerEvent_Victim
{
  public CCSUsrMsg_RoundEndReportData_RerEvent_VictimImpl(nint handle, bool isManuallyAllocated): base(handle)
  {
  }


  public int TeamNumber
  { get => Accessor.GetInt32("team_number"); set => Accessor.SetInt32("team_number", value); }


  public int Playerslot
  { get => Accessor.GetInt32("playerslot"); set => Accessor.SetInt32("playerslot", value); }


  public ulong Xuid
  { get => Accessor.GetUInt64("xuid"); set => Accessor.SetUInt64("xuid", value); }


  public int Color
  { get => Accessor.GetInt32("color"); set => Accessor.SetInt32("color", value); }


  public bool IsBot
  { get => Accessor.GetBool("is_bot"); set => Accessor.SetBool("is_bot", value); }


  public bool IsDead
  { get => Accessor.GetBool("is_dead"); set => Accessor.SetBool("is_dead", value); }

}
