
using SwiftlyS2.Core.Natives;
using SwiftlyS2.Core.NetMessages;
using SwiftlyS2.Shared.Natives;
using SwiftlyS2.Shared.NetMessages;
using SwiftlyS2.Shared.ProtobufDefinitions;

namespace SwiftlyS2.Core.ProtobufDefinitions;

internal class CNETMsg_SignonStateImpl : NetMessage<CNETMsg_SignonState>, CNETMsg_SignonState
{
  public CNETMsg_SignonStateImpl(nint handle, bool isManuallyAllocated): base(handle, isManuallyAllocated)
  {
  }


  public SignonState_t SignonState
  { get => (SignonState_t)Accessor.GetInt32("signon_state"); set => Accessor.SetInt32("signon_state", (int)value); }


  public uint SpawnCount
  { get => Accessor.GetUInt32("spawn_count"); set => Accessor.SetUInt32("spawn_count", value); }


  public uint NumServerPlayers
  { get => Accessor.GetUInt32("num_server_players"); set => Accessor.SetUInt32("num_server_players", value); }


  public IProtobufRepeatedFieldValueType<string> PlayersNetworkids
  { get => new ProtobufRepeatedFieldValueType<string>(Accessor, "players_networkids"); }


  public string MapName
  { get => Accessor.GetString("map_name"); set => Accessor.SetString("map_name", value); }


  public string Addons
  { get => Accessor.GetString("addons"); set => Accessor.SetString("addons", value); }

}
