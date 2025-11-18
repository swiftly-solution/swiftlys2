using SwiftlyS2.Core.GameEvents;
using SwiftlyS2.Shared.GameEvents;
using SwiftlyS2.Shared.SchemaDefinitions;
using SwiftlyS2.Shared.GameEventDefinitions;
using SwiftlyS2.Shared.Players;

namespace SwiftlyS2.Core.GameEventDefinitions;

// generated
/// <summary> 
/// Event "inventory_updated"
/// </summary>
internal class EventInventoryUpdatedImpl : GameEvent<EventInventoryUpdated>, EventInventoryUpdated
{

  public EventInventoryUpdatedImpl(nint address) : base(address)
  {
  }

  public short ItemDef
  { get => (short)Accessor.GetInt32("itemdef"); set => Accessor.SetInt32("itemdef", value); }

  public int Itemid
  { get => Accessor.GetInt32("itemid"); set => Accessor.SetInt32("itemid", value); }
}
