
using SwiftlyS2.Core.Natives;
using SwiftlyS2.Core.NetMessages;
using SwiftlyS2.Shared.Natives;
using SwiftlyS2.Shared.NetMessages;
using SwiftlyS2.Shared.ProtobufDefinitions;

namespace SwiftlyS2.Core.ProtobufDefinitions;

internal class CMsgGameServerInfoImpl : TypedProtobuf<CMsgGameServerInfo>, CMsgGameServerInfo
{
  public CMsgGameServerInfoImpl(nint handle, bool isManuallyAllocated): base(handle)
  {
  }


  public uint ServerPublicIpAddr
  { get => Accessor.GetUInt32("server_public_ip_addr"); set => Accessor.SetUInt32("server_public_ip_addr", value); }


  public uint ServerPrivateIpAddr
  { get => Accessor.GetUInt32("server_private_ip_addr"); set => Accessor.SetUInt32("server_private_ip_addr", value); }


  public uint ServerPort
  { get => Accessor.GetUInt32("server_port"); set => Accessor.SetUInt32("server_port", value); }


  public uint ServerTvPort
  { get => Accessor.GetUInt32("server_tv_port"); set => Accessor.SetUInt32("server_tv_port", value); }


  public string ServerKey
  { get => Accessor.GetString("server_key"); set => Accessor.SetString("server_key", value); }


  public bool ServerHibernation
  { get => Accessor.GetBool("server_hibernation"); set => Accessor.SetBool("server_hibernation", value); }


  public CMsgGameServerInfo_ServerType ServerType
  { get => (CMsgGameServerInfo_ServerType)Accessor.GetInt32("server_type"); set => Accessor.SetInt32("server_type", (int)value); }


  public uint ServerRegion
  { get => Accessor.GetUInt32("server_region"); set => Accessor.SetUInt32("server_region", value); }


  public float ServerLoadavg
  { get => Accessor.GetFloat("server_loadavg"); set => Accessor.SetFloat("server_loadavg", value); }


  public float ServerTvBroadcastTime
  { get => Accessor.GetFloat("server_tv_broadcast_time"); set => Accessor.SetFloat("server_tv_broadcast_time", value); }


  public float ServerGameTime
  { get => Accessor.GetFloat("server_game_time"); set => Accessor.SetFloat("server_game_time", value); }


  public ulong ServerRelayConnectedSteamId
  { get => Accessor.GetUInt64("server_relay_connected_steam_id"); set => Accessor.SetUInt64("server_relay_connected_steam_id", value); }


  public uint RelaySlotsMax
  { get => Accessor.GetUInt32("relay_slots_max"); set => Accessor.SetUInt32("relay_slots_max", value); }


  public int RelaysConnected
  { get => Accessor.GetInt32("relays_connected"); set => Accessor.SetInt32("relays_connected", value); }


  public int RelayClientsConnected
  { get => Accessor.GetInt32("relay_clients_connected"); set => Accessor.SetInt32("relay_clients_connected", value); }


  public ulong RelayedGameServerSteamId
  { get => Accessor.GetUInt64("relayed_game_server_steam_id"); set => Accessor.SetUInt64("relayed_game_server_steam_id", value); }


  public uint ParentRelayCount
  { get => Accessor.GetUInt32("parent_relay_count"); set => Accessor.SetUInt32("parent_relay_count", value); }


  public ulong TvSecretCode
  { get => Accessor.GetUInt64("tv_secret_code"); set => Accessor.SetUInt64("tv_secret_code", value); }

}
