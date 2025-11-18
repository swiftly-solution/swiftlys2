
using SwiftlyS2.Core.Natives;
using SwiftlyS2.Core.NetMessages;
using SwiftlyS2.Shared.Natives;
using SwiftlyS2.Shared.NetMessages;
using SwiftlyS2.Shared.ProtobufDefinitions;

namespace SwiftlyS2.Core.ProtobufDefinitions;

internal class CCSUsrMsg_RoundEndReportDataImpl : NetMessage<CCSUsrMsg_RoundEndReportData>, CCSUsrMsg_RoundEndReportData
{
  public CCSUsrMsg_RoundEndReportDataImpl(nint handle, bool isManuallyAllocated): base(handle, isManuallyAllocated)
  {
  }


  public CCSUsrMsg_RoundEndReportData_InitialConditions InitConditions
  { get => new CCSUsrMsg_RoundEndReportData_InitialConditionsImpl(NativeNetMessages.GetNestedMessage(Address, "init_conditions"), false); }


  public IProtobufRepeatedFieldSubMessageType<CCSUsrMsg_RoundEndReportData_RerEvent> AllRerEventData
  { get => new ProtobufRepeatedFieldSubMessageType<CCSUsrMsg_RoundEndReportData_RerEvent>(Accessor, "all_rer_event_data"); }

}
