using SwiftlyS2.Core.GameEvents;
using SwiftlyS2.Shared.GameEvents;
using SwiftlyS2.Shared.SchemaDefinitions;
using SwiftlyS2.Shared.GameEventDefinitions;
using SwiftlyS2.Shared.Players;

namespace SwiftlyS2.Core.GameEventDefinitions;

// generated
/// <summary> 
/// Event "bonus_updated"
/// </summary>
internal class EventBonusUpdatedImpl : GameEvent<EventBonusUpdated>, EventBonusUpdated
{

  public EventBonusUpdatedImpl(nint address) : base(address)
  {
  }

  public short NumAdvanced
  { get => (short)Accessor.GetInt32("numadvanced"); set => Accessor.SetInt32("numadvanced", value); }

  public short NumBronze
  { get => (short)Accessor.GetInt32("numbronze"); set => Accessor.SetInt32("numbronze", value); }

  public short NumSilver
  { get => (short)Accessor.GetInt32("numsilver"); set => Accessor.SetInt32("numsilver", value); }

  public short NumGold
  { get => (short)Accessor.GetInt32("numgold"); set => Accessor.SetInt32("numgold", value); }
}
