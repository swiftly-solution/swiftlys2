
using SwiftlyS2.Core.Natives;
using SwiftlyS2.Core.NetMessages;
using SwiftlyS2.Shared.Natives;
using SwiftlyS2.Shared.NetMessages;
using SwiftlyS2.Shared.ProtobufDefinitions;

namespace SwiftlyS2.Core.ProtobufDefinitions;

internal class CSVCMsg_ServerInfoImpl : NetMessage<CSVCMsg_ServerInfo>, CSVCMsg_ServerInfo
{
  public CSVCMsg_ServerInfoImpl(nint handle, bool isManuallyAllocated): base(handle, isManuallyAllocated)
  {
  }


  public int Protocol
  { get => Accessor.GetInt32("protocol"); set => Accessor.SetInt32("protocol", value); }


  public int ServerCount
  { get => Accessor.GetInt32("server_count"); set => Accessor.SetInt32("server_count", value); }


  public bool IsDedicated
  { get => Accessor.GetBool("is_dedicated"); set => Accessor.SetBool("is_dedicated", value); }


  public bool IsHltv
  { get => Accessor.GetBool("is_hltv"); set => Accessor.SetBool("is_hltv", value); }


  public int COs
  { get => Accessor.GetInt32("c_os"); set => Accessor.SetInt32("c_os", value); }


  public int MaxClients
  { get => Accessor.GetInt32("max_clients"); set => Accessor.SetInt32("max_clients", value); }


  public int MaxClasses
  { get => Accessor.GetInt32("max_classes"); set => Accessor.SetInt32("max_classes", value); }


  public int PlayerSlot
  { get => Accessor.GetInt32("player_slot"); set => Accessor.SetInt32("player_slot", value); }


  public float TickInterval
  { get => Accessor.GetFloat("tick_interval"); set => Accessor.SetFloat("tick_interval", value); }


  public string GameDir
  { get => Accessor.GetString("game_dir"); set => Accessor.SetString("game_dir", value); }


  public string MapName
  { get => Accessor.GetString("map_name"); set => Accessor.SetString("map_name", value); }


  public string SkyName
  { get => Accessor.GetString("sky_name"); set => Accessor.SetString("sky_name", value); }


  public string HostName
  { get => Accessor.GetString("host_name"); set => Accessor.SetString("host_name", value); }


  public string AddonName
  { get => Accessor.GetString("addon_name"); set => Accessor.SetString("addon_name", value); }


  public CSVCMsg_GameSessionConfiguration GameSessionConfig
  { get => new CSVCMsg_GameSessionConfigurationImpl(NativeNetMessages.GetNestedMessage(Address, "game_session_config"), false); }


  public byte[] GameSessionManifest
  { get => Accessor.GetBytes("game_session_manifest"); set => Accessor.SetBytes("game_session_manifest", value); }

}
