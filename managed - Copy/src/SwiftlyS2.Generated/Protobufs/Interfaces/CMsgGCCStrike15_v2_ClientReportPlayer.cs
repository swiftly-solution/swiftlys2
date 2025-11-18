
using SwiftlyS2.Core.ProtobufDefinitions;
using SwiftlyS2.Shared.Natives;
using SwiftlyS2.Shared.NetMessages;

namespace SwiftlyS2.Shared.ProtobufDefinitions;

public interface CMsgGCCStrike15_v2_ClientReportPlayer : ITypedProtobuf<CMsgGCCStrike15_v2_ClientReportPlayer>
{
  static CMsgGCCStrike15_v2_ClientReportPlayer ITypedProtobuf<CMsgGCCStrike15_v2_ClientReportPlayer>.Wrap(nint handle, bool isManuallyAllocated) => new CMsgGCCStrike15_v2_ClientReportPlayerImpl(handle, isManuallyAllocated);


  public uint AccountId { get; set; }


  public uint RptAimbot { get; set; }


  public uint RptWallhack { get; set; }


  public uint RptSpeedhack { get; set; }


  public uint RptTeamharm { get; set; }


  public uint RptTextabuse { get; set; }


  public uint RptVoiceabuse { get; set; }


  public ulong MatchId { get; set; }


  public bool ReportFromDemo { get; set; }

}
