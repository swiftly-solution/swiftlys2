
using SwiftlyS2.Core.ProtobufDefinitions;
using SwiftlyS2.Shared.Natives;
using SwiftlyS2.Shared.NetMessages;

namespace SwiftlyS2.Shared.ProtobufDefinitions;

public interface CCSUsrMsg_RoundEndReportData_RerEvent : ITypedProtobuf<CCSUsrMsg_RoundEndReportData_RerEvent>
{
  static CCSUsrMsg_RoundEndReportData_RerEvent ITypedProtobuf<CCSUsrMsg_RoundEndReportData_RerEvent>.Wrap(nint handle, bool isManuallyAllocated) => new CCSUsrMsg_RoundEndReportData_RerEventImpl(handle, isManuallyAllocated);


  public float Timestamp { get; set; }


  public int TerroristOdds { get; set; }


  public int CtAlive { get; set; }


  public int TAlive { get; set; }


  public CCSUsrMsg_RoundEndReportData_RerEvent_Victim VictimData { get; }


  public CCSUsrMsg_RoundEndReportData_RerEvent_Objective ObjectiveData { get; }


  public IProtobufRepeatedFieldSubMessageType<CCSUsrMsg_RoundEndReportData_RerEvent_Damage> AllDamageData { get; }

}
