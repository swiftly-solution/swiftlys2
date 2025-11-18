using SwiftlyS2.Core.GameEvents;
using SwiftlyS2.Shared.GameEvents;
using SwiftlyS2.Shared.SchemaDefinitions;
using SwiftlyS2.Shared.GameEventDefinitions;
using SwiftlyS2.Shared.Players;

namespace SwiftlyS2.Core.GameEventDefinitions;

// generated
/// <summary> 
/// Event "flare_ignite_npc"
/// </summary>
internal class EventFlareIgniteNpcImpl : GameEvent<EventFlareIgniteNpc>, EventFlareIgniteNpc
{

  public EventFlareIgniteNpcImpl(nint address) : base(address)
  {
  }

  // entity ignited
  public int EntIndex
  { get => Accessor.GetInt32("entindex"); set => Accessor.SetInt32("entindex", value); }
}
