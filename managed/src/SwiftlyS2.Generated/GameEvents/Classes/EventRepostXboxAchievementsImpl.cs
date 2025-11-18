using SwiftlyS2.Core.GameEvents;
using SwiftlyS2.Shared.GameEventDefinitions;

namespace SwiftlyS2.Core.GameEventDefinitions;

// generated
/// <summary> 
/// Event "repost_xbox_achievements"
/// </summary>
internal class EventRepostXboxAchievementsImpl : GameEvent<EventRepostXboxAchievements>, EventRepostXboxAchievements
{

    public EventRepostXboxAchievementsImpl( nint address ) : base(address)
    {
    }

    // splitscreen ID
    public short SplitScreenPlayer { get => (short)Accessor.GetInt32("splitscreenplayer"); set => Accessor.SetInt32("splitscreenplayer", value); }
}
