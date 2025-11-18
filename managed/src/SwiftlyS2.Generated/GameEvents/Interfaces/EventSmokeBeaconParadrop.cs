using SwiftlyS2.Core.GameEventDefinitions;
using SwiftlyS2.Shared.GameEvents;
using SwiftlyS2.Shared.Players;
using SwiftlyS2.Shared.SchemaDefinitions;

namespace SwiftlyS2.Shared.GameEventDefinitions;

/// <summary> 
/// Event "smoke_beacon_paradrop"
/// </summary>
public interface EventSmokeBeaconParadrop : IGameEvent<EventSmokeBeaconParadrop>
{

    static EventSmokeBeaconParadrop IGameEvent<EventSmokeBeaconParadrop>.Create( nint address ) => new EventSmokeBeaconParadropImpl(address);

    static string IGameEvent<EventSmokeBeaconParadrop>.GetName() => "smoke_beacon_paradrop";

    static uint IGameEvent<EventSmokeBeaconParadrop>.GetHash() => 0xA68BF79Bu;
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
    /// type: short
    /// </summary>
    public short ParaDrop { get; set; }

}
