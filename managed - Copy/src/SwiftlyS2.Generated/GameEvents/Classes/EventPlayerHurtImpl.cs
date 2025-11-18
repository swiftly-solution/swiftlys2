using SwiftlyS2.Core.GameEvents;
using SwiftlyS2.Shared.GameEvents;
using SwiftlyS2.Shared.SchemaDefinitions;
using SwiftlyS2.Shared.GameEventDefinitions;
using SwiftlyS2.Shared.Players;

namespace SwiftlyS2.Core.GameEventDefinitions;

// generated
/// <summary> 
/// Event "player_hurt"
/// </summary>
internal class EventPlayerHurtImpl : GameEvent<EventPlayerHurt>, EventPlayerHurt
{

  public EventPlayerHurtImpl(nint address) : base(address)
  {
  }

  // player who was hurt
  public CCSPlayerController UserIdController
  { get => Accessor.GetPlayerController("userid"); }

  // player who was hurt
  public CCSPlayerPawn UserIdPawn
  { get => Accessor.GetPlayerPawn("userid"); }

  // player who was hurt
  public IPlayer UserIdPlayer
  { get => Accessor.GetPlayer("userid"); }

  // player who was hurt
  public int UserId
  { get => Accessor.GetInt32("userid"); set => Accessor.SetInt32("userid", value); }

  // player who attacked
  public int Attacker
  { get => Accessor.GetPlayerSlot("attacker"); set => Accessor.SetPlayerSlot("attacker", value); }

  // remaining health points
  public byte Health
  { get => (byte)Accessor.GetInt32("health"); set => Accessor.SetInt32("health", value); }

  // remaining armor points
  public byte Armor
  { get => (byte)Accessor.GetInt32("armor"); set => Accessor.SetInt32("armor", value); }

  // weapon name attacker used, if not the world
  public string Weapon
  { get => Accessor.GetString("weapon"); set => Accessor.SetString("weapon", value); }

  // damage done to health
  public short DmgHealth
  { get => (short)Accessor.GetInt32("dmg_health"); set => Accessor.SetInt32("dmg_health", value); }

  // damage done to armor
  public byte DmgArmor
  { get => (byte)Accessor.GetInt32("dmg_armor"); set => Accessor.SetInt32("dmg_armor", value); }

  // hitgroup that was damaged
  public byte HitGroup
  { get => (byte)Accessor.GetInt32("hitgroup"); set => Accessor.SetInt32("hitgroup", value); }
}
