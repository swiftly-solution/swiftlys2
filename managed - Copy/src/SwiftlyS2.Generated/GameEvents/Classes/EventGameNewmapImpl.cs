using SwiftlyS2.Core.GameEvents;
using SwiftlyS2.Shared.GameEvents;
using SwiftlyS2.Shared.SchemaDefinitions;
using SwiftlyS2.Shared.GameEventDefinitions;
using SwiftlyS2.Shared.Players;

namespace SwiftlyS2.Core.GameEventDefinitions;

// generated
/// <summary> 
/// Event "game_newmap"
/// send when new map is completely loaded
/// </summary>
internal class EventGameNewmapImpl : GameEvent<EventGameNewmap>, EventGameNewmap
{

  public EventGameNewmapImpl(nint address) : base(address)
  {
  }

  // map name
  public string MapName
  { get => Accessor.GetString("mapname"); set => Accessor.SetString("mapname", value); }

  // true if this is a transition from one map to another
  public bool Transition
  { get => Accessor.GetBool("transition"); set => Accessor.SetBool("transition", value); }
}
