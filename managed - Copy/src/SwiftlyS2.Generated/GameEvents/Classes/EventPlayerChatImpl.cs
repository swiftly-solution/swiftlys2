using SwiftlyS2.Core.GameEvents;
using SwiftlyS2.Shared.GameEvents;
using SwiftlyS2.Shared.SchemaDefinitions;
using SwiftlyS2.Shared.GameEventDefinitions;
using SwiftlyS2.Shared.Players;

namespace SwiftlyS2.Core.GameEventDefinitions;

// generated
/// <summary> 
/// Event "player_chat"
/// a public player chat
/// </summary>
internal class EventPlayerChatImpl : GameEvent<EventPlayerChat>, EventPlayerChat
{

  public EventPlayerChatImpl(nint address) : base(address)
  {
  }

  // true if team only chat
  public bool TeamOnly
  { get => Accessor.GetBool("teamonly"); set => Accessor.SetBool("teamonly", value); }

  // chatting player
  public CCSPlayerController UserIdController
  { get => Accessor.GetPlayerController("userid"); }

  // chatting player
  public CCSPlayerPawn UserIdPawn
  { get => Accessor.GetPlayerPawn("userid"); }

  // chatting player
  public IPlayer UserIdPlayer
  { get => Accessor.GetPlayer("userid"); }

  // chatting player
  public int UserId
  { get => Accessor.GetInt32("userid"); set => Accessor.SetInt32("userid", value); }

  // chatting player ID
  public short Playerid
  { get => (short)Accessor.GetInt32("playerid"); set => Accessor.SetInt32("playerid", value); }

  // chat text
  public string Text
  { get => Accessor.GetString("text"); set => Accessor.SetString("text", value); }
}
