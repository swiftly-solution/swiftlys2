
using SwiftlyS2.Core.Natives;
using SwiftlyS2.Core.NetMessages;
using SwiftlyS2.Shared.Natives;
using SwiftlyS2.Shared.NetMessages;
using SwiftlyS2.Shared.ProtobufDefinitions;

namespace SwiftlyS2.Core.ProtobufDefinitions;

internal class CCSUsrMsg_RoundEndReportData_RerEventImpl : TypedProtobuf<CCSUsrMsg_RoundEndReportData_RerEvent>, CCSUsrMsg_RoundEndReportData_RerEvent
{
  public CCSUsrMsg_RoundEndReportData_RerEventImpl(nint handle, bool isManuallyAllocated): base(handle)
  {
  }


  public float Timestamp
  { get => Accessor.GetFloat("timestamp"); set => Accessor.SetFloat("timestamp", value); }


  public int TerroristOdds
  { get => Accessor.GetInt32("terrorist_odds"); set => Accessor.SetInt32("terrorist_odds", value); }


  public int CtAlive
  { get => Accessor.GetInt32("ct_alive"); set => Accessor.SetInt32("ct_alive", value); }


  public int TAlive
  { get => Accessor.GetInt32("t_alive"); set => Accessor.SetInt32("t_alive", value); }


  public CCSUsrMsg_RoundEndReportData_RerEvent_Victim VictimData
  { get => new CCSUsrMsg_RoundEndReportData_RerEvent_VictimImpl(NativeNetMessages.GetNestedMessage(Address, "victim_data"), false); }


  public CCSUsrMsg_RoundEndReportData_RerEvent_Objective ObjectiveData
  { get => new CCSUsrMsg_RoundEndReportData_RerEvent_ObjectiveImpl(NativeNetMessages.GetNestedMessage(Address, "objective_data"), false); }


  public IProtobufRepeatedFieldSubMessageType<CCSUsrMsg_RoundEndReportData_RerEvent_Damage> AllDamageData
  { get => new ProtobufRepeatedFieldSubMessageType<CCSUsrMsg_RoundEndReportData_RerEvent_Damage>(Accessor, "all_damage_data"); }

}
