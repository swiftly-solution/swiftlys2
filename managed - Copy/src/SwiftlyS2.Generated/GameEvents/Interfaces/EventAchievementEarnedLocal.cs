using SwiftlyS2.Shared.SchemaDefinitions;
using SwiftlyS2.Shared.GameEvents;
using SwiftlyS2.Core.GameEventDefinitions;
using SwiftlyS2.Shared.Players;

namespace SwiftlyS2.Shared.GameEventDefinitions;

/// <summary> 
/// Event "achievement_earned_local"
/// </summary>
public interface EventAchievementEarnedLocal : IGameEvent<EventAchievementEarnedLocal> {

  static EventAchievementEarnedLocal IGameEvent<EventAchievementEarnedLocal>.Create(nint address) => new EventAchievementEarnedLocalImpl(address);

  static string IGameEvent<EventAchievementEarnedLocal>.GetName() => "achievement_earned_local";

  static uint IGameEvent<EventAchievementEarnedLocal>.GetHash() => 0x106FCE0Au;
  /// <summary>
  /// achievement ID
  /// <br/>
  /// type: short
  /// </summary>
  short Achievement { get; set; }

  /// <summary>
  /// splitscreen ID
  /// <br/>
  /// type: short
  /// </summary>
  short SplitScreenPlayer { get; set; }

}
