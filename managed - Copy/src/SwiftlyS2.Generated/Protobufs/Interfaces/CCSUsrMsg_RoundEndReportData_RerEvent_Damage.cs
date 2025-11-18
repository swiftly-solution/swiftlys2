
using SwiftlyS2.Core.ProtobufDefinitions;
using SwiftlyS2.Shared.Natives;
using SwiftlyS2.Shared.NetMessages;

namespace SwiftlyS2.Shared.ProtobufDefinitions;

public interface CCSUsrMsg_RoundEndReportData_RerEvent_Damage : ITypedProtobuf<CCSUsrMsg_RoundEndReportData_RerEvent_Damage>
{
  static CCSUsrMsg_RoundEndReportData_RerEvent_Damage ITypedProtobuf<CCSUsrMsg_RoundEndReportData_RerEvent_Damage>.Wrap(nint handle, bool isManuallyAllocated) => new CCSUsrMsg_RoundEndReportData_RerEvent_DamageImpl(handle, isManuallyAllocated);


  public int OtherPlayerslot { get; set; }


  public ulong OtherXuid { get; set; }


  public int HealthRemoved { get; set; }


  public int NumHits { get; set; }


  public int ReturnHealthRemoved { get; set; }


  public int ReturnNumHits { get; set; }

}
