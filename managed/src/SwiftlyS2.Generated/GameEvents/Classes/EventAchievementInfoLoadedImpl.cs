using SwiftlyS2.Core.GameEvents;
using SwiftlyS2.Shared.GameEventDefinitions;

namespace SwiftlyS2.Core.GameEventDefinitions;

// generated
/// <summary> 
/// Event "achievement_info_loaded"
/// </summary>
internal class EventAchievementInfoLoadedImpl : GameEvent<EventAchievementInfoLoaded>, EventAchievementInfoLoaded
{

    public EventAchievementInfoLoadedImpl( nint address ) : base(address)
    {
    }
}
