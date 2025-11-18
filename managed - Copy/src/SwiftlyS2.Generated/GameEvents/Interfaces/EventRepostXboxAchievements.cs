using SwiftlyS2.Shared.SchemaDefinitions;
using SwiftlyS2.Shared.GameEvents;
using SwiftlyS2.Core.GameEventDefinitions;
using SwiftlyS2.Shared.Players;

namespace SwiftlyS2.Shared.GameEventDefinitions;

/// <summary> 
/// Event "repost_xbox_achievements"
/// </summary>
public interface EventRepostXboxAchievements : IGameEvent<EventRepostXboxAchievements> {

  static EventRepostXboxAchievements IGameEvent<EventRepostXboxAchievements>.Create(nint address) => new EventRepostXboxAchievementsImpl(address);

  static string IGameEvent<EventRepostXboxAchievements>.GetName() => "repost_xbox_achievements";

  static uint IGameEvent<EventRepostXboxAchievements>.GetHash() => 0x7D188D23u;
  /// <summary>
  /// splitscreen ID
  /// <br/>
  /// type: short
  /// </summary>
  short SplitScreenPlayer { get; set; }

}
