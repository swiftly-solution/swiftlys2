using SwiftlyS2.Core.GameEvents;
using SwiftlyS2.Shared.GameEvents;
using SwiftlyS2.Shared.SchemaDefinitions;
using SwiftlyS2.Shared.GameEventDefinitions;
using SwiftlyS2.Shared.Players;

namespace SwiftlyS2.Core.GameEventDefinitions;

// generated
/// <summary> 
/// Event "vip_killed"
/// </summary>
internal class EventVipKilledImpl : GameEvent<EventVipKilled>, EventVipKilled
{

  public EventVipKilledImpl(nint address) : base(address)
  {
  }

  // player who was the VIP
  public CCSPlayerController UserIdController
  { get => Accessor.GetPlayerController("userid"); }

  // player who was the VIP
  public CCSPlayerPawn UserIdPawn
  { get => Accessor.GetPlayerPawn("userid"); }

  // player who was the VIP
  public IPlayer UserIdPlayer
  { get => Accessor.GetPlayer("userid"); }

  // player who was the VIP
  public int UserId
  { get => Accessor.GetInt32("userid"); set => Accessor.SetInt32("userid", value); }

  // user ID who killed the VIP
  public int Attacker
  { get => Accessor.GetPlayerSlot("attacker"); set => Accessor.SetPlayerSlot("attacker", value); }
}
