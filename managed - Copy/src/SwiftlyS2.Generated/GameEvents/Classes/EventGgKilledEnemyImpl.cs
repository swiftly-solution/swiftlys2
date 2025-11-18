using SwiftlyS2.Core.GameEvents;
using SwiftlyS2.Shared.GameEvents;
using SwiftlyS2.Shared.SchemaDefinitions;
using SwiftlyS2.Shared.GameEventDefinitions;
using SwiftlyS2.Shared.Players;

namespace SwiftlyS2.Core.GameEventDefinitions;

// generated
/// <summary> 
/// Event "gg_killed_enemy"
/// </summary>
internal class EventGgKilledEnemyImpl : GameEvent<EventGgKilledEnemy>, EventGgKilledEnemy
{

  public EventGgKilledEnemyImpl(nint address) : base(address)
  {
  }

  // user ID who died
  public int VictimID
  { get => Accessor.GetPlayerSlot("victimid"); set => Accessor.SetPlayerSlot("victimid", value); }

  // user ID who killed
  public int AttackerID
  { get => Accessor.GetPlayerSlot("attackerid"); set => Accessor.SetPlayerSlot("attackerid", value); }

  // did killer dominate victim with this kill
  public short Dominated
  { get => (short)Accessor.GetInt32("dominated"); set => Accessor.SetInt32("dominated", value); }

  // did killer get revenge on victim with this kill
  public short Revenge
  { get => (short)Accessor.GetInt32("revenge"); set => Accessor.SetInt32("revenge", value); }

  // did killer kill with a bonus weapon?
  public bool Bonus
  { get => Accessor.GetBool("bonus"); set => Accessor.SetBool("bonus", value); }
}
