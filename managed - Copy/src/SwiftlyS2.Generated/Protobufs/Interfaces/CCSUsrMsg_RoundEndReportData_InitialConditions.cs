
using SwiftlyS2.Core.ProtobufDefinitions;
using SwiftlyS2.Shared.Natives;
using SwiftlyS2.Shared.NetMessages;

namespace SwiftlyS2.Shared.ProtobufDefinitions;

public interface CCSUsrMsg_RoundEndReportData_InitialConditions : ITypedProtobuf<CCSUsrMsg_RoundEndReportData_InitialConditions>
{
  static CCSUsrMsg_RoundEndReportData_InitialConditions ITypedProtobuf<CCSUsrMsg_RoundEndReportData_InitialConditions>.Wrap(nint handle, bool isManuallyAllocated) => new CCSUsrMsg_RoundEndReportData_InitialConditionsImpl(handle, isManuallyAllocated);


  public int CtEquipValue { get; set; }


  public int TEquipValue { get; set; }


  public int TerroristOdds { get; set; }

}
