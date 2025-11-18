
using SwiftlyS2.Core.Natives;
using SwiftlyS2.Core.NetMessages;
using SwiftlyS2.Shared.Natives;
using SwiftlyS2.Shared.NetMessages;
using SwiftlyS2.Shared.ProtobufDefinitions;

namespace SwiftlyS2.Core.ProtobufDefinitions;

internal class CMsgGCCStrike15_v2_ClientToGCRequestTicketImpl : TypedProtobuf<CMsgGCCStrike15_v2_ClientToGCRequestTicket>, CMsgGCCStrike15_v2_ClientToGCRequestTicket
{
  public CMsgGCCStrike15_v2_ClientToGCRequestTicketImpl(nint handle, bool isManuallyAllocated): base(handle)
  {
  }


  public ulong AuthorizedSteamId
  { get => Accessor.GetUInt64("authorized_steam_id"); set => Accessor.SetUInt64("authorized_steam_id", value); }


  public uint AuthorizedPublicIp
  { get => Accessor.GetUInt32("authorized_public_ip"); set => Accessor.SetUInt32("authorized_public_ip", value); }


  public ulong GameserverSteamId
  { get => Accessor.GetUInt64("gameserver_steam_id"); set => Accessor.SetUInt64("gameserver_steam_id", value); }


  public string GameserverSdrRouting
  { get => Accessor.GetString("gameserver_sdr_routing"); set => Accessor.SetString("gameserver_sdr_routing", value); }

}
