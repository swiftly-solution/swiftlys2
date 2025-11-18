
using SwiftlyS2.Core.ProtobufDefinitions;
using SwiftlyS2.Shared.Natives;
using SwiftlyS2.Shared.NetMessages;

namespace SwiftlyS2.Shared.ProtobufDefinitions;

public interface CCSUsrMsg_RoundEndReportData_RerEvent_Objective : ITypedProtobuf<CCSUsrMsg_RoundEndReportData_RerEvent_Objective>
{
  static CCSUsrMsg_RoundEndReportData_RerEvent_Objective ITypedProtobuf<CCSUsrMsg_RoundEndReportData_RerEvent_Objective>.Wrap(nint handle, bool isManuallyAllocated) => new CCSUsrMsg_RoundEndReportData_RerEvent_ObjectiveImpl(handle, isManuallyAllocated);


  public int Type { get; set; }

}
