using SwiftlyS2.Core.GameEvents;
using SwiftlyS2.Shared.GameEvents;
using SwiftlyS2.Shared.SchemaDefinitions;
using SwiftlyS2.Shared.GameEventDefinitions;
using SwiftlyS2.Shared.Players;

namespace SwiftlyS2.Core.GameEventDefinitions;

// generated
/// <summary>
/// Event "achievement_earned"
/// </summary>
internal class EventAchievementEarnedImpl : GameEvent<EventAchievementEarned>, EventAchievementEarned
{

    public EventAchievementEarnedImpl(nint address) : base(address)
    {
    }

    // entindex of the player
    public CCSPlayerController PlayerController
    { get => Accessor.GetPlayerController("player"); }

    // entindex of the player
    public CCSPlayerPawn PlayerPawn
    { get => Accessor.GetPlayerPawn("player"); }

    // entindex of the player
    public IPlayer? PlayerPlayer
    { get => Accessor.GetPlayer("player"); }

    // entindex of the player
    public int Player
    { get => Accessor.GetInt32("player"); set => Accessor.SetInt32("player", value); }

    // achievement ID
    public short Achievement
    { get => (short)Accessor.GetInt32("achievement"); set => Accessor.SetInt32("achievement", value); }
}
