using SwiftlyS2.Core.GameEvents;
using SwiftlyS2.Shared.GameEvents;
using SwiftlyS2.Shared.SchemaDefinitions;
using SwiftlyS2.Shared.GameEventDefinitions;
using SwiftlyS2.Shared.Players;

namespace SwiftlyS2.Core.GameEventDefinitions;

// generated
/// <summary> 
/// Event "survival_paradrop_spawn"
/// </summary>
internal class EventSurvivalParadropSpawnImpl : GameEvent<EventSurvivalParadropSpawn>, EventSurvivalParadropSpawn
{

  public EventSurvivalParadropSpawnImpl(nint address) : base(address)
  {
  }

  public short EntityID
  { get => (short)Accessor.GetInt32("entityid"); set => Accessor.SetInt32("entityid", value); }
}
