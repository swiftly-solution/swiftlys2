using SwiftlyS2.Core.GameEvents;
using SwiftlyS2.Shared.GameEvents;
using SwiftlyS2.Shared.SchemaDefinitions;
using SwiftlyS2.Shared.GameEventDefinitions;
using SwiftlyS2.Shared.Players;

namespace SwiftlyS2.Core.GameEventDefinitions;

// generated
/// <summary> 
/// Event "player_connect"
/// a new client connected
/// </summary>
internal class EventPlayerConnectImpl : GameEvent<EventPlayerConnect>, EventPlayerConnect
{

  public EventPlayerConnectImpl(nint address) : base(address)
  {
  }

  // player name
  public string Name
  { get => Accessor.GetString("name"); set => Accessor.SetString("name", value); }

  // user ID on server (unique on server)
  public CCSPlayerController UserIdController
  { get => Accessor.GetPlayerController("userid"); }

  // user ID on server (unique on server)
  public CCSPlayerPawn UserIdPawn
  { get => Accessor.GetPlayerPawn("userid"); }

  // user ID on server (unique on server)
  public IPlayer UserIdPlayer
  { get => Accessor.GetPlayer("userid"); }

  // user ID on server (unique on server)
  public int UserId
  { get => Accessor.GetInt32("userid"); set => Accessor.SetInt32("userid", value); }

  // player network (i.e steam) id
  public string NetworkID
  { get => Accessor.GetString("networkid"); set => Accessor.SetString("networkid", value); }

  // steam id
  public ulong XuID
  { get => Accessor.GetUInt64("xuid"); set => Accessor.SetUInt64("xuid", value); }

  public bool Bot
  { get => Accessor.GetBool("bot"); set => Accessor.SetBool("bot", value); }
}
