using SwiftlyS2.Core.GameEventDefinitions;
using SwiftlyS2.Shared.GameEvents;
using SwiftlyS2.Shared.Players;
using SwiftlyS2.Shared.SchemaDefinitions;

namespace SwiftlyS2.Shared.GameEventDefinitions;

/// <summary> 
/// Event "player_score"
/// players scores changed
/// </summary>
public interface EventPlayerScore : IGameEvent<EventPlayerScore>
{

    static EventPlayerScore IGameEvent<EventPlayerScore>.Create( nint address ) => new EventPlayerScoreImpl(address);

    static string IGameEvent<EventPlayerScore>.GetName() => "player_score";

    static uint IGameEvent<EventPlayerScore>.GetHash() => 0xAF712F7Du;
    /// <summary>
    /// user ID on server
    /// <br/>
    /// type: player_controller
    /// </summary>
    public CCSPlayerController UserIdController { get; }

    /// <summary>
    /// user ID on server
    /// <br/>
    /// type: player_controller
    /// </summary>
    public CCSPlayerPawn UserIdPawn { get; }


    // user ID on server
    public IPlayer UserIdPlayer { get => Accessor.GetPlayer("userid"); }
    /// <summary>
    /// user ID on server
    /// <br/>
    /// type: player_controller
    /// </summary>
    public int UserId { get; set; }

    /// <summary>
    /// # of kills
    /// <br/>
    /// type: short
    /// </summary>
    public short Kills { get; set; }

    /// <summary>
    /// # of deaths
    /// <br/>
    /// type: short
    /// </summary>
    public short Deaths { get; set; }

    /// <summary>
    /// total game score
    /// <br/>
    /// type: short
    /// </summary>
    public short Score { get; set; }

}
