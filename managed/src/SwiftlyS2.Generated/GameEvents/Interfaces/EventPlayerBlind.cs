using SwiftlyS2.Core.GameEventDefinitions;
using SwiftlyS2.Shared.GameEvents;
using SwiftlyS2.Shared.Players;
using SwiftlyS2.Shared.SchemaDefinitions;

namespace SwiftlyS2.Shared.GameEventDefinitions;

/// <summary> 
/// Event "player_blind"
/// </summary>
public interface EventPlayerBlind : IGameEvent<EventPlayerBlind>
{

    static EventPlayerBlind IGameEvent<EventPlayerBlind>.Create( nint address ) => new EventPlayerBlindImpl(address);

    static string IGameEvent<EventPlayerBlind>.GetName() => "player_blind";

    static uint IGameEvent<EventPlayerBlind>.GetHash() => 0x4D79D81Cu;
    /// <summary>
    /// <br/>
    /// type: player_controller
    /// </summary>
    public CCSPlayerController UserIdController { get; }

    /// <summary>
    /// <br/>
    /// type: player_controller
    /// </summary>
    public CCSPlayerPawn UserIdPawn { get; }


    public IPlayer UserIdPlayer { get => Accessor.GetPlayer("userid"); }
    /// <summary>
    /// <br/>
    /// type: player_controller
    /// </summary>
    public int UserId { get; set; }

    /// <summary>
    /// user ID who threw the flash
    /// <br/>
    /// type: player_controller
    /// </summary>
    public int Attacker { get; set; }

    /// <summary>
    /// the flashbang going off
    /// <br/>
    /// type: short
    /// </summary>
    public short EntityID { get; set; }

    /// <summary>
    /// type: float
    /// </summary>
    public float BlindDuration { get; set; }

}
