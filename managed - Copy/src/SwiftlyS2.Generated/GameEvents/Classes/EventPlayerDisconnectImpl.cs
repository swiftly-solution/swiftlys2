using SwiftlyS2.Core.GameEvents;
using SwiftlyS2.Shared.GameEvents;
using SwiftlyS2.Shared.SchemaDefinitions;
using SwiftlyS2.Shared.GameEventDefinitions;
using SwiftlyS2.Shared.Players;

namespace SwiftlyS2.Core.GameEventDefinitions;

// generated
/// <summary> 
/// Event "player_disconnect"
/// a client was disconnected
/// </summary>
internal class EventPlayerDisconnectImpl : GameEvent<EventPlayerDisconnect>, EventPlayerDisconnect
{

  public EventPlayerDisconnectImpl(nint address) : base(address)
  {
  }

  // user ID on server
  public CCSPlayerController UserIdController
  { get => Accessor.GetPlayerController("userid"); }

  // user ID on server
  public CCSPlayerPawn UserIdPawn
  { get => Accessor.GetPlayerPawn("userid"); }

  // user ID on server
  public IPlayer UserIdPlayer
  { get => Accessor.GetPlayer("userid"); }

  // user ID on server
  public int UserId
  { get => Accessor.GetInt32("userid"); set => Accessor.SetInt32("userid", value); }

  // see networkdisconnect enum protobuf
  public short Reason
  { get => (short)Accessor.GetInt32("reason"); set => Accessor.SetInt32("reason", value); }

  // player name
  public string Name
  { get => Accessor.GetString("name"); set => Accessor.SetString("name", value); }

  // player network (i.e steam) id
  public string NetworkID
  { get => Accessor.GetString("networkid"); set => Accessor.SetString("networkid", value); }

  // steam id
  public ulong XuID
  { get => Accessor.GetUInt64("xuid"); set => Accessor.SetUInt64("xuid", value); }

  public short PlayerID
  { get => (short)Accessor.GetInt32("PlayerID"); set => Accessor.SetInt32("PlayerID", value); }
}
