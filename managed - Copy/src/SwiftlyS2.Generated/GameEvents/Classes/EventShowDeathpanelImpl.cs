using SwiftlyS2.Core.GameEvents;
using SwiftlyS2.Shared.GameEvents;
using SwiftlyS2.Shared.SchemaDefinitions;
using SwiftlyS2.Shared.GameEventDefinitions;
using SwiftlyS2.Shared.Players;

namespace SwiftlyS2.Core.GameEventDefinitions;

// generated
/// <summary> 
/// Event "show_deathpanel"
/// </summary>
internal class EventShowDeathpanelImpl : GameEvent<EventShowDeathpanel>, EventShowDeathpanel
{

  public EventShowDeathpanelImpl(nint address) : base(address)
  {
  }

  // endindex of the one who was killed
  public int Victim
  { get => Accessor.GetPlayerSlot("victim"); set => Accessor.SetPlayerSlot("victim", value); }

  // entindex of the killer entity
  public nint Killer
  { get => Accessor.GetPtr("killer"); set => Accessor.SetPtr("killer", value); }

  public int KillerController
  { get => Accessor.GetPlayerSlot("killer_controller"); set => Accessor.SetPlayerSlot("killer_controller", value); }

  public short HitsTaken
  { get => (short)Accessor.GetInt32("hits_taken"); set => Accessor.SetInt32("hits_taken", value); }

  public short DamageTaken
  { get => (short)Accessor.GetInt32("damage_taken"); set => Accessor.SetInt32("damage_taken", value); }

  public short HitsGiven
  { get => (short)Accessor.GetInt32("hits_given"); set => Accessor.SetInt32("hits_given", value); }

  public short DamageGiven
  { get => (short)Accessor.GetInt32("damage_given"); set => Accessor.SetInt32("damage_given", value); }
}
