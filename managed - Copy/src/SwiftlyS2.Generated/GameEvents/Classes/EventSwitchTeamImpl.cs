using SwiftlyS2.Core.GameEvents;
using SwiftlyS2.Shared.GameEvents;
using SwiftlyS2.Shared.SchemaDefinitions;
using SwiftlyS2.Shared.GameEventDefinitions;
using SwiftlyS2.Shared.Players;

namespace SwiftlyS2.Core.GameEventDefinitions;

// generated
/// <summary> 
/// Event "switch_team"
/// </summary>
internal class EventSwitchTeamImpl : GameEvent<EventSwitchTeam>, EventSwitchTeam
{

  public EventSwitchTeamImpl(nint address) : base(address)
  {
  }

  // number of active players on both T and CT
  public short NumPlayers
  { get => (short)Accessor.GetInt32("numPlayers"); set => Accessor.SetInt32("numPlayers", value); }

  // number of spectators
  public short NumSpectators
  { get => (short)Accessor.GetInt32("numSpectators"); set => Accessor.SetInt32("numSpectators", value); }

  // average rank of human players
  public short AvgRank
  { get => (short)Accessor.GetInt32("avg_rank"); set => Accessor.SetInt32("avg_rank", value); }

  public short NumTSlotsFree
  { get => (short)Accessor.GetInt32("numTSlotsFree"); set => Accessor.SetInt32("numTSlotsFree", value); }

  public short NumCTSlotsFree
  { get => (short)Accessor.GetInt32("numCTSlotsFree"); set => Accessor.SetInt32("numCTSlotsFree", value); }
}
