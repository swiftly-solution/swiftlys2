using SwiftlyS2.Core.GameEventDefinitions;
using SwiftlyS2.Shared.GameEvents;
using SwiftlyS2.Shared.Players;
using SwiftlyS2.Shared.SchemaDefinitions;

namespace SwiftlyS2.Shared.GameEventDefinitions;

/// <summary> 
/// Event "enter_rescue_zone"
/// </summary>
public interface EventEnterRescueZone : IGameEvent<EventEnterRescueZone>
{

    static EventEnterRescueZone IGameEvent<EventEnterRescueZone>.Create( nint address ) => new EventEnterRescueZoneImpl(address);

    static string IGameEvent<EventEnterRescueZone>.GetName() => "enter_rescue_zone";

    static uint IGameEvent<EventEnterRescueZone>.GetHash() => 0xA10C79CAu;
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

}
