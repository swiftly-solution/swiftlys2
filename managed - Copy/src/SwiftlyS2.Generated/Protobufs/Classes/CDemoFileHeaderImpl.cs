
using SwiftlyS2.Core.Natives;
using SwiftlyS2.Core.NetMessages;
using SwiftlyS2.Shared.Natives;
using SwiftlyS2.Shared.NetMessages;
using SwiftlyS2.Shared.ProtobufDefinitions;

namespace SwiftlyS2.Core.ProtobufDefinitions;

internal class CDemoFileHeaderImpl : TypedProtobuf<CDemoFileHeader>, CDemoFileHeader
{
  public CDemoFileHeaderImpl(nint handle, bool isManuallyAllocated): base(handle)
  {
  }


  public string DemoFileStamp
  { get => Accessor.GetString("demo_file_stamp"); set => Accessor.SetString("demo_file_stamp", value); }


  public int PatchVersion
  { get => Accessor.GetInt32("patch_version"); set => Accessor.SetInt32("patch_version", value); }


  public string ServerName
  { get => Accessor.GetString("server_name"); set => Accessor.SetString("server_name", value); }


  public string ClientName
  { get => Accessor.GetString("client_name"); set => Accessor.SetString("client_name", value); }


  public string MapName
  { get => Accessor.GetString("map_name"); set => Accessor.SetString("map_name", value); }


  public string GameDirectory
  { get => Accessor.GetString("game_directory"); set => Accessor.SetString("game_directory", value); }


  public int FullpacketsVersion
  { get => Accessor.GetInt32("fullpackets_version"); set => Accessor.SetInt32("fullpackets_version", value); }


  public bool AllowClientsideEntities
  { get => Accessor.GetBool("allow_clientside_entities"); set => Accessor.SetBool("allow_clientside_entities", value); }


  public bool AllowClientsideParticles
  { get => Accessor.GetBool("allow_clientside_particles"); set => Accessor.SetBool("allow_clientside_particles", value); }


  public string Addons
  { get => Accessor.GetString("addons"); set => Accessor.SetString("addons", value); }


  public string DemoVersionName
  { get => Accessor.GetString("demo_version_name"); set => Accessor.SetString("demo_version_name", value); }


  public string DemoVersionGuid
  { get => Accessor.GetString("demo_version_guid"); set => Accessor.SetString("demo_version_guid", value); }


  public int BuildNum
  { get => Accessor.GetInt32("build_num"); set => Accessor.SetInt32("build_num", value); }


  public string Game
  { get => Accessor.GetString("game"); set => Accessor.SetString("game", value); }


  public int ServerStartTick
  { get => Accessor.GetInt32("server_start_tick"); set => Accessor.SetInt32("server_start_tick", value); }

}
