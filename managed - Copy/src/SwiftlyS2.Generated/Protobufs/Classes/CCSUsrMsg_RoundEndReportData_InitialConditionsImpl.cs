
using SwiftlyS2.Core.Natives;
using SwiftlyS2.Core.NetMessages;
using SwiftlyS2.Shared.Natives;
using SwiftlyS2.Shared.NetMessages;
using SwiftlyS2.Shared.ProtobufDefinitions;

namespace SwiftlyS2.Core.ProtobufDefinitions;

internal class CCSUsrMsg_RoundEndReportData_InitialConditionsImpl : TypedProtobuf<CCSUsrMsg_RoundEndReportData_InitialConditions>, CCSUsrMsg_RoundEndReportData_InitialConditions
{
  public CCSUsrMsg_RoundEndReportData_InitialConditionsImpl(nint handle, bool isManuallyAllocated): base(handle)
  {
  }


  public int CtEquipValue
  { get => Accessor.GetInt32("ct_equip_value"); set => Accessor.SetInt32("ct_equip_value", value); }


  public int TEquipValue
  { get => Accessor.GetInt32("t_equip_value"); set => Accessor.SetInt32("t_equip_value", value); }


  public int TerroristOdds
  { get => Accessor.GetInt32("terrorist_odds"); set => Accessor.SetInt32("terrorist_odds", value); }

}
