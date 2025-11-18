using SwiftlyS2.Core.GameEventDefinitions;
using SwiftlyS2.Shared.GameEvents;
using SwiftlyS2.Shared.Players;
using SwiftlyS2.Shared.SchemaDefinitions;

namespace SwiftlyS2.Shared.GameEventDefinitions;

/// <summary> 
/// Event "player_sound"
/// </summary>
public interface EventPlayerSound : IGameEvent<EventPlayerSound>
{

    static EventPlayerSound IGameEvent<EventPlayerSound>.Create( nint address ) => new EventPlayerSoundImpl(address);

    static string IGameEvent<EventPlayerSound>.GetName() => "player_sound";

    static uint IGameEvent<EventPlayerSound>.GetHash() => 0xE562BC6Cu;
    /// <summary>
    /// <br/>
    /// type: player_controller_and_pawn
    /// </summary>
    public CCSPlayerController UserIdController { get; }

    /// <summary>
    /// <br/>
    /// type: player_controller_and_pawn
    /// </summary>
    public CCSPlayerPawn UserIdPawn { get; }


    public IPlayer UserIdPlayer { get => Accessor.GetPlayer("userid"); }
    /// <summary>
    /// <br/>
    /// type: player_controller_and_pawn
    /// </summary>
    public int UserId { get; set; }

    /// <summary>
    /// type: int
    /// </summary>
    public int Radius { get; set; }

    /// <summary>
    /// type: float
    /// </summary>
    public float Duration { get; set; }

    /// <summary>
    /// type: bool
    /// </summary>
    public bool Step { get; set; }

}
