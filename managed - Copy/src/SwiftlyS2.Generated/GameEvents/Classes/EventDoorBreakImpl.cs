using SwiftlyS2.Core.GameEvents;
using SwiftlyS2.Shared.GameEvents;
using SwiftlyS2.Shared.SchemaDefinitions;
using SwiftlyS2.Shared.GameEventDefinitions;
using SwiftlyS2.Shared.Players;

namespace SwiftlyS2.Core.GameEventDefinitions;

// generated
/// <summary> 
/// Event "door_break"
/// </summary>
internal class EventDoorBreakImpl : GameEvent<EventDoorBreak>, EventDoorBreak
{

  public EventDoorBreakImpl(nint address) : base(address)
  {
  }

  public int EntIndex
  { get => Accessor.GetInt32("entindex"); set => Accessor.SetInt32("entindex", value); }

  public int DMgState
  { get => Accessor.GetInt32("dmgstate"); set => Accessor.SetInt32("dmgstate", value); }
}
