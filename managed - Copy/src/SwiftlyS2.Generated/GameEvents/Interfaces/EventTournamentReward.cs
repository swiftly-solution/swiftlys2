using SwiftlyS2.Shared.SchemaDefinitions;
using SwiftlyS2.Shared.GameEvents;
using SwiftlyS2.Core.GameEventDefinitions;
using SwiftlyS2.Shared.Players;

namespace SwiftlyS2.Shared.GameEventDefinitions;

/// <summary> 
/// Event "tournament_reward"
/// </summary>
public interface EventTournamentReward : IGameEvent<EventTournamentReward> {

  static EventTournamentReward IGameEvent<EventTournamentReward>.Create(nint address) => new EventTournamentRewardImpl(address);

  static string IGameEvent<EventTournamentReward>.GetName() => "tournament_reward";

  static uint IGameEvent<EventTournamentReward>.GetHash() => 0x1FF0AA30u;
  /// <summary>
  /// type: long
  /// </summary>
  int DefIndex { get; set; }

  /// <summary>
  /// type: long
  /// </summary>
  int TotalRewards { get; set; }

  /// <summary>
  /// type: long
  /// </summary>
  int AccountID { get; set; }

}
