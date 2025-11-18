using SwiftlyS2.Core.GameEventDefinitions;
using SwiftlyS2.Shared.GameEvents;
using SwiftlyS2.Shared.Players;
using SwiftlyS2.Shared.SchemaDefinitions;

namespace SwiftlyS2.Shared.GameEventDefinitions;

/// <summary> 
/// Event "smokegrenade_expired"
/// </summary>
public interface EventSmokegrenadeExpired : IGameEvent<EventSmokegrenadeExpired>
{

    static EventSmokegrenadeExpired IGameEvent<EventSmokegrenadeExpired>.Create( nint address ) => new EventSmokegrenadeExpiredImpl(address);

    static string IGameEvent<EventSmokegrenadeExpired>.GetName() => "smokegrenade_expired";

    static uint IGameEvent<EventSmokegrenadeExpired>.GetHash() => 0x45406DF6u;
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
