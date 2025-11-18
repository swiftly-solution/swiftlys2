using SwiftlyS2.Core.GameEvents;
using SwiftlyS2.Shared.GameEvents;
using SwiftlyS2.Shared.SchemaDefinitions;
using SwiftlyS2.Shared.GameEventDefinitions;
using SwiftlyS2.Shared.Players;

namespace SwiftlyS2.Core.GameEventDefinitions;

// generated
/// <summary> 
/// Event "tournament_reward"
/// </summary>
internal class EventTournamentRewardImpl : GameEvent<EventTournamentReward>, EventTournamentReward
{

  public EventTournamentRewardImpl(nint address) : base(address)
  {
  }

  public int DefIndex
  { get => Accessor.GetInt32("defindex"); set => Accessor.SetInt32("defindex", value); }

  public int TotalRewards
  { get => Accessor.GetInt32("totalrewards"); set => Accessor.SetInt32("totalrewards", value); }

  public int AccountID
  { get => Accessor.GetInt32("accountid"); set => Accessor.SetInt32("accountid", value); }
}
