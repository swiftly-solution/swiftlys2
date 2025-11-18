using SwiftlyS2.Shared.SchemaDefinitions;
using SwiftlyS2.Shared.GameEvents;
using SwiftlyS2.Core.GameEventDefinitions;
using SwiftlyS2.Shared.Players;

namespace SwiftlyS2.Shared.GameEventDefinitions;

/// <summary> 
/// Event "achievement_earned"
/// </summary>
public interface EventAchievementEarned : IGameEvent<EventAchievementEarned> {

  static EventAchievementEarned IGameEvent<EventAchievementEarned>.Create(nint address) => new EventAchievementEarnedImpl(address);

  static string IGameEvent<EventAchievementEarned>.GetName() => "achievement_earned";

  static uint IGameEvent<EventAchievementEarned>.GetHash() => 0xDA3B5BA6u;
  /// <summary>
  /// entindex of the player
  /// <br/>
  /// type: player_controller
  /// </summary>
  int Player { get; set; }

  /// <summary>
  /// achievement ID
  /// <br/>
  /// type: short
  /// </summary>
  short Achievement { get; set; }

}
