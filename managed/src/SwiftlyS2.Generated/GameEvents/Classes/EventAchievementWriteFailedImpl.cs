using SwiftlyS2.Core.GameEvents;
using SwiftlyS2.Shared.GameEventDefinitions;

namespace SwiftlyS2.Core.GameEventDefinitions;

// generated
/// <summary> 
/// Event "achievement_write_failed"
/// </summary>
internal class EventAchievementWriteFailedImpl : GameEvent<EventAchievementWriteFailed>, EventAchievementWriteFailed
{

    public EventAchievementWriteFailedImpl( nint address ) : base(address)
    {
    }
}
