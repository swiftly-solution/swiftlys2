using SwiftlyS2.Core.GameEvents;
using SwiftlyS2.Shared.GameEvents;
using SwiftlyS2.Shared.SchemaDefinitions;
using SwiftlyS2.Shared.GameEventDefinitions;
using SwiftlyS2.Shared.Players;

namespace SwiftlyS2.Core.GameEventDefinitions;

// generated
/// <summary> 
/// Event "bot_takeover"
/// </summary>
internal class EventBotTakeoverImpl : GameEvent<EventBotTakeover>, EventBotTakeover
{

  public EventBotTakeoverImpl(nint address) : base(address)
  {
  }

  public CCSPlayerController UserIdController
  { get => Accessor.GetPlayerController("userid"); }

  public CCSPlayerPawn UserIdPawn
  { get => Accessor.GetPlayerPawn("userid"); }

  public IPlayer UserIdPlayer
  { get => Accessor.GetPlayer("userid"); }

  public int UserId
  { get => Accessor.GetInt32("userid"); set => Accessor.SetInt32("userid", value); }

  public int BotID
  { get => Accessor.GetPlayerSlot("botid"); set => Accessor.SetPlayerSlot("botid", value); }

  public float P
  { get => Accessor.GetFloat("p"); set => Accessor.SetFloat("p", value); }

  public float Y
  { get => Accessor.GetFloat("y"); set => Accessor.SetFloat("y", value); }

  public float R
  { get => Accessor.GetFloat("r"); set => Accessor.SetFloat("r", value); }
}
