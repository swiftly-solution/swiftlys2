
using SwiftlyS2.Core.ProtobufDefinitions;
using SwiftlyS2.Shared.Natives;
using SwiftlyS2.Shared.NetMessages;

namespace SwiftlyS2.Shared.ProtobufDefinitions;

public interface CMsgGCCStrike15_v2_MatchmakingGC2ClientReserve : ITypedProtobuf<CMsgGCCStrike15_v2_MatchmakingGC2ClientReserve>
{
  static CMsgGCCStrike15_v2_MatchmakingGC2ClientReserve ITypedProtobuf<CMsgGCCStrike15_v2_MatchmakingGC2ClientReserve>.Wrap(nint handle, bool isManuallyAllocated) => new CMsgGCCStrike15_v2_MatchmakingGC2ClientReserveImpl(handle, isManuallyAllocated);


  public ulong Serverid { get; set; }


  public uint DirectUdpIp { get; set; }


  public uint DirectUdpPort { get; set; }


  public ulong Reservationid { get; set; }


  public CMsgGCCStrike15_v2_MatchmakingGC2ServerReserve Reservation { get; }


  public string Map { get; set; }


  public string ServerAddress { get; set; }


  public DataCenterPing GsPing { get; }


  public uint GsLocationId { get; set; }

}
