using SwiftlyS2.Core.GameEventDefinitions;
using SwiftlyS2.Shared.GameEvents;

namespace SwiftlyS2.Shared.GameEventDefinitions;

/// <summary> 
/// Event "achievement_earned"
/// </summary>
public interface EventAchievementEarned : IGameEvent<EventAchievementEarned>
{

    static EventAchievementEarned IGameEvent<EventAchievementEarned>.Create( nint address ) => new EventAchievementEarnedImpl(address);

    static string IGameEvent<EventAchievementEarned>.GetName() => "achievement_earned";

    static uint IGameEvent<EventAchievementEarned>.GetHash() => 0xDA3B5BA6u;
    /// <summary>
    /// entindex of the player
    /// <br/>
    /// type: player_controller
    /// </summary>
    public int Player { get; set; }

    /// <summary>
    /// achievement ID
    /// <br/>
    /// type: short
    /// </summary>
    public short Achievement { get; set; }

}
