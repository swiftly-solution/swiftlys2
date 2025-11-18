using SwiftlyS2.Core.GameEvents;
using SwiftlyS2.Shared.GameEvents;
using SwiftlyS2.Shared.SchemaDefinitions;
using SwiftlyS2.Shared.GameEventDefinitions;
using SwiftlyS2.Shared.Players;

namespace SwiftlyS2.Core.GameEventDefinitions;

// generated
/// <summary> 
/// Event "achievement_info_loaded"
/// </summary>
internal class EventAchievementInfoLoadedImpl : GameEvent<EventAchievementInfoLoaded>, EventAchievementInfoLoaded
{

  public EventAchievementInfoLoadedImpl(nint address) : base(address)
  {
  }
}
