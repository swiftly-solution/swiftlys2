
using SwiftlyS2.Core.ProtobufDefinitions;
using SwiftlyS2.Shared.Natives;
using SwiftlyS2.Shared.NetMessages;

namespace SwiftlyS2.Shared.ProtobufDefinitions;

public interface CCSUsrMsg_RoundEndReportData_RerEvent_Victim : ITypedProtobuf<CCSUsrMsg_RoundEndReportData_RerEvent_Victim>
{
  static CCSUsrMsg_RoundEndReportData_RerEvent_Victim ITypedProtobuf<CCSUsrMsg_RoundEndReportData_RerEvent_Victim>.Wrap(nint handle, bool isManuallyAllocated) => new CCSUsrMsg_RoundEndReportData_RerEvent_VictimImpl(handle, isManuallyAllocated);


  public int TeamNumber { get; set; }


  public int Playerslot { get; set; }


  public ulong Xuid { get; set; }


  public int Color { get; set; }


  public bool IsBot { get; set; }


  public bool IsDead { get; set; }

}
