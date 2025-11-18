
using SwiftlyS2.Core.Natives;
using SwiftlyS2.Core.NetMessages;
using SwiftlyS2.Shared.Natives;
using SwiftlyS2.Shared.NetMessages;
using SwiftlyS2.Shared.ProtobufDefinitions;

namespace SwiftlyS2.Core.ProtobufDefinitions;

internal class CMsgGCCStrike15_v2_ClientReportServerImpl : TypedProtobuf<CMsgGCCStrike15_v2_ClientReportServer>, CMsgGCCStrike15_v2_ClientReportServer
{
  public CMsgGCCStrike15_v2_ClientReportServerImpl(nint handle, bool isManuallyAllocated): base(handle)
  {
  }


  public uint RptPoorperf
  { get => Accessor.GetUInt32("rpt_poorperf"); set => Accessor.SetUInt32("rpt_poorperf", value); }


  public uint RptAbusivemodels
  { get => Accessor.GetUInt32("rpt_abusivemodels"); set => Accessor.SetUInt32("rpt_abusivemodels", value); }


  public uint RptBadmotd
  { get => Accessor.GetUInt32("rpt_badmotd"); set => Accessor.SetUInt32("rpt_badmotd", value); }


  public uint RptListingabuse
  { get => Accessor.GetUInt32("rpt_listingabuse"); set => Accessor.SetUInt32("rpt_listingabuse", value); }


  public uint RptInventoryabuse
  { get => Accessor.GetUInt32("rpt_inventoryabuse"); set => Accessor.SetUInt32("rpt_inventoryabuse", value); }


  public ulong MatchId
  { get => Accessor.GetUInt64("match_id"); set => Accessor.SetUInt64("match_id", value); }

}
