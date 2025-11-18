
using SwiftlyS2.Core.Natives;
using SwiftlyS2.Core.NetMessages;
using SwiftlyS2.Shared.Natives;
using SwiftlyS2.Shared.NetMessages;
using SwiftlyS2.Shared.ProtobufDefinitions;

namespace SwiftlyS2.Core.ProtobufDefinitions;

internal class CSVCMsg_GameSessionConfigurationImpl : TypedProtobuf<CSVCMsg_GameSessionConfiguration>, CSVCMsg_GameSessionConfiguration
{
  public CSVCMsg_GameSessionConfigurationImpl(nint handle, bool isManuallyAllocated): base(handle)
  {
  }


  public bool IsMultiplayer
  { get => Accessor.GetBool("is_multiplayer"); set => Accessor.SetBool("is_multiplayer", value); }


  public bool IsLoadsavegame
  { get => Accessor.GetBool("is_loadsavegame"); set => Accessor.SetBool("is_loadsavegame", value); }


  public bool IsBackgroundMap
  { get => Accessor.GetBool("is_background_map"); set => Accessor.SetBool("is_background_map", value); }


  public bool IsHeadless
  { get => Accessor.GetBool("is_headless"); set => Accessor.SetBool("is_headless", value); }


  public uint MinClientLimit
  { get => Accessor.GetUInt32("min_client_limit"); set => Accessor.SetUInt32("min_client_limit", value); }


  public uint MaxClientLimit
  { get => Accessor.GetUInt32("max_client_limit"); set => Accessor.SetUInt32("max_client_limit", value); }


  public uint MaxClients
  { get => Accessor.GetUInt32("max_clients"); set => Accessor.SetUInt32("max_clients", value); }


  public uint TickInterval
  { get => Accessor.GetUInt32("tick_interval"); set => Accessor.SetUInt32("tick_interval", value); }


  public string Hostname
  { get => Accessor.GetString("hostname"); set => Accessor.SetString("hostname", value); }


  public string Savegamename
  { get => Accessor.GetString("savegamename"); set => Accessor.SetString("savegamename", value); }


  public string S1Mapname
  { get => Accessor.GetString("s1_mapname"); set => Accessor.SetString("s1_mapname", value); }


  public string Gamemode
  { get => Accessor.GetString("gamemode"); set => Accessor.SetString("gamemode", value); }


  public string ServerIpAddress
  { get => Accessor.GetString("server_ip_address"); set => Accessor.SetString("server_ip_address", value); }


  public byte[] Data
  { get => Accessor.GetBytes("data"); set => Accessor.SetBytes("data", value); }


  public bool IsLocalonly
  { get => Accessor.GetBool("is_localonly"); set => Accessor.SetBool("is_localonly", value); }


  public bool NoSteamServer
  { get => Accessor.GetBool("no_steam_server"); set => Accessor.SetBool("no_steam_server", value); }


  public bool IsTransition
  { get => Accessor.GetBool("is_transition"); set => Accessor.SetBool("is_transition", value); }


  public string Previouslevel
  { get => Accessor.GetString("previouslevel"); set => Accessor.SetString("previouslevel", value); }


  public string Landmarkname
  { get => Accessor.GetString("landmarkname"); set => Accessor.SetString("landmarkname", value); }

}
