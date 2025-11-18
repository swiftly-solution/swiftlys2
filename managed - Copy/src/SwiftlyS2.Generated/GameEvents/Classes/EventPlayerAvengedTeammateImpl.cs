using SwiftlyS2.Core.GameEvents;
using SwiftlyS2.Shared.GameEvents;
using SwiftlyS2.Shared.SchemaDefinitions;
using SwiftlyS2.Shared.GameEventDefinitions;
using SwiftlyS2.Shared.Players;

namespace SwiftlyS2.Core.GameEventDefinitions;

// generated
/// <summary> 
/// Event "player_avenged_teammate"
/// </summary>
internal class EventPlayerAvengedTeammateImpl : GameEvent<EventPlayerAvengedTeammate>, EventPlayerAvengedTeammate
{

  public EventPlayerAvengedTeammateImpl(nint address) : base(address)
  {
  }

  public int AvengerId
  { get => Accessor.GetPlayerSlot("avenger_id"); set => Accessor.SetPlayerSlot("avenger_id", value); }

  public int AvengedPlayerId
  { get => Accessor.GetPlayerSlot("avenged_player_id"); set => Accessor.SetPlayerSlot("avenged_player_id", value); }
}
