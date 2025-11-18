using SwiftlyS2.Core.GameEventDefinitions;
using SwiftlyS2.Shared.GameEvents;
using SwiftlyS2.Shared.Players;
using SwiftlyS2.Shared.SchemaDefinitions;

namespace SwiftlyS2.Shared.GameEventDefinitions;

/// <summary> 
/// Event "hltv_cameraman"
/// a spectator/player is a cameraman
/// </summary>
public interface EventHltvCameraman : IGameEvent<EventHltvCameraman>
{

    static EventHltvCameraman IGameEvent<EventHltvCameraman>.Create( nint address ) => new EventHltvCameramanImpl(address);

    static string IGameEvent<EventHltvCameraman>.GetName() => "hltv_cameraman";

    static uint IGameEvent<EventHltvCameraman>.GetHash() => 0xD54932D5u;
    /// <summary>
    /// camera man entity index
    /// <br/>
    /// type: player_controller
    /// </summary>
    public CCSPlayerController UserIdController { get; }

    /// <summary>
    /// camera man entity index
    /// <br/>
    /// type: player_controller
    /// </summary>
    public CCSPlayerPawn UserIdPawn { get; }


    // camera man entity index
    public IPlayer UserIdPlayer { get => Accessor.GetPlayer("userid"); }
    /// <summary>
    /// camera man entity index
    /// <br/>
    /// type: player_controller
    /// </summary>
    public int UserId { get; set; }

}
