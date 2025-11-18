
using SwiftlyS2.Core.Natives;
using SwiftlyS2.Core.NetMessages;
using SwiftlyS2.Shared.Natives;
using SwiftlyS2.Shared.NetMessages;
using SwiftlyS2.Shared.ProtobufDefinitions;

namespace SwiftlyS2.Core.ProtobufDefinitions;

internal class CMsgGCCStrike15_v2_MatchmakingGC2ClientReserveImpl : TypedProtobuf<CMsgGCCStrike15_v2_MatchmakingGC2ClientReserve>, CMsgGCCStrike15_v2_MatchmakingGC2ClientReserve
{
  public CMsgGCCStrike15_v2_MatchmakingGC2ClientReserveImpl(nint handle, bool isManuallyAllocated): base(handle)
  {
  }


  public ulong Serverid
  { get => Accessor.GetUInt64("serverid"); set => Accessor.SetUInt64("serverid", value); }


  public uint DirectUdpIp
  { get => Accessor.GetUInt32("direct_udp_ip"); set => Accessor.SetUInt32("direct_udp_ip", value); }


  public uint DirectUdpPort
  { get => Accessor.GetUInt32("direct_udp_port"); set => Accessor.SetUInt32("direct_udp_port", value); }


  public ulong Reservationid
  { get => Accessor.GetUInt64("reservationid"); set => Accessor.SetUInt64("reservationid", value); }


  public CMsgGCCStrike15_v2_MatchmakingGC2ServerReserve Reservation
  { get => new CMsgGCCStrike15_v2_MatchmakingGC2ServerReserveImpl(NativeNetMessages.GetNestedMessage(Address, "reservation"), false); }


  public string Map
  { get => Accessor.GetString("map"); set => Accessor.SetString("map", value); }


  public string ServerAddress
  { get => Accessor.GetString("server_address"); set => Accessor.SetString("server_address", value); }


  public DataCenterPing GsPing
  { get => new DataCenterPingImpl(NativeNetMessages.GetNestedMessage(Address, "gs_ping"), false); }


  public uint GsLocationId
  { get => Accessor.GetUInt32("gs_location_id"); set => Accessor.SetUInt32("gs_location_id", value); }

}
