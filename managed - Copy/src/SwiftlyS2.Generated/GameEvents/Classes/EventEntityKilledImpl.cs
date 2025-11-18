using SwiftlyS2.Core.GameEvents;
using SwiftlyS2.Shared.GameEvents;
using SwiftlyS2.Shared.SchemaDefinitions;
using SwiftlyS2.Shared.GameEventDefinitions;
using SwiftlyS2.Shared.Players;

namespace SwiftlyS2.Core.GameEventDefinitions;

// generated
/// <summary> 
/// Event "entity_killed"
/// </summary>
internal class EventEntityKilledImpl : GameEvent<EventEntityKilled>, EventEntityKilled
{

  public EventEntityKilledImpl(nint address) : base(address)
  {
  }

  public int EntindexKilled
  { get => Accessor.GetInt32("entindex_killed"); set => Accessor.SetInt32("entindex_killed", value); }

  public int EntindexAttacker
  { get => Accessor.GetInt32("entindex_attacker"); set => Accessor.SetInt32("entindex_attacker", value); }

  public int EntindexInflictor
  { get => Accessor.GetInt32("entindex_inflictor"); set => Accessor.SetInt32("entindex_inflictor", value); }

  public int DamageBits
  { get => Accessor.GetInt32("damagebits"); set => Accessor.SetInt32("damagebits", value); }
}
