
using SwiftlyS2.Core.Natives;
using SwiftlyS2.Core.NetMessages;
using SwiftlyS2.Shared.Natives;
using SwiftlyS2.Shared.NetMessages;
using SwiftlyS2.Shared.ProtobufDefinitions;

namespace SwiftlyS2.Core.ProtobufDefinitions;

internal class CCSUsrMsg_RoundEndReportData_RerEvent_DamageImpl : TypedProtobuf<CCSUsrMsg_RoundEndReportData_RerEvent_Damage>, CCSUsrMsg_RoundEndReportData_RerEvent_Damage
{
  public CCSUsrMsg_RoundEndReportData_RerEvent_DamageImpl(nint handle, bool isManuallyAllocated): base(handle)
  {
  }


  public int OtherPlayerslot
  { get => Accessor.GetInt32("other_playerslot"); set => Accessor.SetInt32("other_playerslot", value); }


  public ulong OtherXuid
  { get => Accessor.GetUInt64("other_xuid"); set => Accessor.SetUInt64("other_xuid", value); }


  public int HealthRemoved
  { get => Accessor.GetInt32("health_removed"); set => Accessor.SetInt32("health_removed", value); }


  public int NumHits
  { get => Accessor.GetInt32("num_hits"); set => Accessor.SetInt32("num_hits", value); }


  public int ReturnHealthRemoved
  { get => Accessor.GetInt32("return_health_removed"); set => Accessor.SetInt32("return_health_removed", value); }


  public int ReturnNumHits
  { get => Accessor.GetInt32("return_num_hits"); set => Accessor.SetInt32("return_num_hits", value); }

}
