using SwiftlyS2.Core.GameEventDefinitions;
using SwiftlyS2.Shared.GameEvents;
using SwiftlyS2.Shared.Players;
using SwiftlyS2.Shared.SchemaDefinitions;

namespace SwiftlyS2.Shared.GameEventDefinitions;

/// <summary> 
/// Event "decoy_started"
/// </summary>
public interface EventDecoyStarted : IGameEvent<EventDecoyStarted>
{

    static EventDecoyStarted IGameEvent<EventDecoyStarted>.Create( nint address ) => new EventDecoyStartedImpl(address);

    static string IGameEvent<EventDecoyStarted>.GetName() => "decoy_started";

    static uint IGameEvent<EventDecoyStarted>.GetHash() => 0xD1159B75u;
    /// <summary>
    /// <br/>
    /// type: player_pawn
    /// </summary>
    public CCSPlayerController UserIdController { get; }

    /// <summary>
    /// <br/>
    /// type: player_pawn
    /// </summary>
    public CCSPlayerPawn UserIdPawn { get; }


    public IPlayer UserIdPlayer { get => Accessor.GetPlayer("userid"); }
    /// <summary>
    /// <br/>
    /// type: player_pawn
    /// </summary>
    public int UserId { get; set; }

    /// <summary>
    /// type: short
    /// </summary>
    public short EntityID { get; set; }

    /// <summary>
    /// type: float
    /// </summary>
    public float X { get; set; }

    /// <summary>
    /// type: float
    /// </summary>
    public float Y { get; set; }

    /// <summary>
    /// type: float
    /// </summary>
    public float Z { get; set; }

}
