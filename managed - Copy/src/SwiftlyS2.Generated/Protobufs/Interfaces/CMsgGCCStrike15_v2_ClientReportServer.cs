
using SwiftlyS2.Core.ProtobufDefinitions;
using SwiftlyS2.Shared.Natives;
using SwiftlyS2.Shared.NetMessages;

namespace SwiftlyS2.Shared.ProtobufDefinitions;

public interface CMsgGCCStrike15_v2_ClientReportServer : ITypedProtobuf<CMsgGCCStrike15_v2_ClientReportServer>
{
  static CMsgGCCStrike15_v2_ClientReportServer ITypedProtobuf<CMsgGCCStrike15_v2_ClientReportServer>.Wrap(nint handle, bool isManuallyAllocated) => new CMsgGCCStrike15_v2_ClientReportServerImpl(handle, isManuallyAllocated);


  public uint RptPoorperf { get; set; }


  public uint RptAbusivemodels { get; set; }


  public uint RptBadmotd { get; set; }


  public uint RptListingabuse { get; set; }


  public uint RptInventoryabuse { get; set; }


  public ulong MatchId { get; set; }

}
