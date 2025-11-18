
using SwiftlyS2.Core.Natives;
using SwiftlyS2.Core.NetMessages;
using SwiftlyS2.Shared.Natives;
using SwiftlyS2.Shared.NetMessages;
using SwiftlyS2.Shared.ProtobufDefinitions;

namespace SwiftlyS2.Core.ProtobufDefinitions;

internal class CCSUsrMsg_RoundEndReportData_RerEvent_ObjectiveImpl : TypedProtobuf<CCSUsrMsg_RoundEndReportData_RerEvent_Objective>, CCSUsrMsg_RoundEndReportData_RerEvent_Objective
{
  public CCSUsrMsg_RoundEndReportData_RerEvent_ObjectiveImpl(nint handle, bool isManuallyAllocated): base(handle)
  {
  }


  public int Type
  { get => Accessor.GetInt32("type"); set => Accessor.SetInt32("type", value); }

}
