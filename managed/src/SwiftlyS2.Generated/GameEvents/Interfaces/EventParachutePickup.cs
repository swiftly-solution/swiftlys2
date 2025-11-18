using SwiftlyS2.Core.GameEventDefinitions;
using SwiftlyS2.Shared.GameEvents;
using SwiftlyS2.Shared.Players;
using SwiftlyS2.Shared.SchemaDefinitions;

namespace SwiftlyS2.Shared.GameEventDefinitions;

/// <summary> 
/// Event "parachute_pickup"
/// </summary>
public interface EventParachutePickup : IGameEvent<EventParachutePickup>
{

    static EventParachutePickup IGameEvent<EventParachutePickup>.Create( nint address ) => new EventParachutePickupImpl(address);

    static string IGameEvent<EventParachutePickup>.GetName() => "parachute_pickup";

    static uint IGameEvent<EventParachutePickup>.GetHash() => 0x9A331261u;
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
