
using SwiftlyS2.Core.Natives;
using SwiftlyS2.Core.NetMessages;
using SwiftlyS2.Shared.Natives;
using SwiftlyS2.Shared.NetMessages;
using SwiftlyS2.Shared.ProtobufDefinitions;

namespace SwiftlyS2.Core.ProtobufDefinitions;

internal class ServerHltvInfoImpl : TypedProtobuf<ServerHltvInfo>, ServerHltvInfo
{
  public ServerHltvInfoImpl(nint handle, bool isManuallyAllocated): base(handle)
  {
  }


  public uint TvUdpPort
  { get => Accessor.GetUInt32("tv_udp_port"); set => Accessor.SetUInt32("tv_udp_port", value); }


  public ulong TvWatchKey
  { get => Accessor.GetUInt64("tv_watch_key"); set => Accessor.SetUInt64("tv_watch_key", value); }


  public uint TvSlots
  { get => Accessor.GetUInt32("tv_slots"); set => Accessor.SetUInt32("tv_slots", value); }


  public uint TvClients
  { get => Accessor.GetUInt32("tv_clients"); set => Accessor.SetUInt32("tv_clients", value); }


  public uint TvProxies
  { get => Accessor.GetUInt32("tv_proxies"); set => Accessor.SetUInt32("tv_proxies", value); }


  public uint TvTime
  { get => Accessor.GetUInt32("tv_time"); set => Accessor.SetUInt32("tv_time", value); }


  public uint GameType
  { get => Accessor.GetUInt32("game_type"); set => Accessor.SetUInt32("game_type", value); }


  public string GameMapgroup
  { get => Accessor.GetString("game_mapgroup"); set => Accessor.SetString("game_mapgroup", value); }


  public string GameMap
  { get => Accessor.GetString("game_map"); set => Accessor.SetString("game_map", value); }


  public ulong TvMasterSteamid
  { get => Accessor.GetUInt64("tv_master_steamid"); set => Accessor.SetUInt64("tv_master_steamid", value); }


  public uint TvLocalSlots
  { get => Accessor.GetUInt32("tv_local_slots"); set => Accessor.SetUInt32("tv_local_slots", value); }


  public uint TvLocalClients
  { get => Accessor.GetUInt32("tv_local_clients"); set => Accessor.SetUInt32("tv_local_clients", value); }


  public uint TvLocalProxies
  { get => Accessor.GetUInt32("tv_local_proxies"); set => Accessor.SetUInt32("tv_local_proxies", value); }


  public uint TvRelaySlots
  { get => Accessor.GetUInt32("tv_relay_slots"); set => Accessor.SetUInt32("tv_relay_slots", value); }


  public uint TvRelayClients
  { get => Accessor.GetUInt32("tv_relay_clients"); set => Accessor.SetUInt32("tv_relay_clients", value); }


  public uint TvRelayProxies
  { get => Accessor.GetUInt32("tv_relay_proxies"); set => Accessor.SetUInt32("tv_relay_proxies", value); }


  public uint TvRelayAddress
  { get => Accessor.GetUInt32("tv_relay_address"); set => Accessor.SetUInt32("tv_relay_address", value); }


  public uint TvRelayPort
  { get => Accessor.GetUInt32("tv_relay_port"); set => Accessor.SetUInt32("tv_relay_port", value); }


  public ulong TvRelaySteamid
  { get => Accessor.GetUInt64("tv_relay_steamid"); set => Accessor.SetUInt64("tv_relay_steamid", value); }


  public uint Flags
  { get => Accessor.GetUInt32("flags"); set => Accessor.SetUInt32("flags", value); }

}
