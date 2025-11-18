using SwiftlyS2.Shared.SchemaDefinitions;
using SwiftlyS2.Shared.GameEvents;
using SwiftlyS2.Core.GameEventDefinitions;
using SwiftlyS2.Shared.Players;

namespace SwiftlyS2.Shared.GameEventDefinitions;

/// <summary> 
/// Event "achievement_info_loaded"
/// </summary>
public interface EventAchievementInfoLoaded : IGameEvent<EventAchievementInfoLoaded> {

  static EventAchievementInfoLoaded IGameEvent<EventAchievementInfoLoaded>.Create(nint address) => new EventAchievementInfoLoadedImpl(address);

  static string IGameEvent<EventAchievementInfoLoaded>.GetName() => "achievement_info_loaded";

  static uint IGameEvent<EventAchievementInfoLoaded>.GetHash() => 0x724EE32Fu;
}
