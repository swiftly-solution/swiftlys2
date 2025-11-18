using SwiftlyS2.Core.GameEventDefinitions;
using SwiftlyS2.Shared.GameEvents;
using SwiftlyS2.Shared.Players;
using SwiftlyS2.Shared.SchemaDefinitions;

namespace SwiftlyS2.Shared.GameEventDefinitions;

/// <summary> 
/// Event "add_player_sonar_icon"
/// </summary>
public interface EventAddPlayerSonarIcon : IGameEvent<EventAddPlayerSonarIcon>
{

    static EventAddPlayerSonarIcon IGameEvent<EventAddPlayerSonarIcon>.Create( nint address ) => new EventAddPlayerSonarIconImpl(address);

    static string IGameEvent<EventAddPlayerSonarIcon>.GetName() => "add_player_sonar_icon";

    static uint IGameEvent<EventAddPlayerSonarIcon>.GetHash() => 0x7B807538u;
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
    /// type: float
    /// </summary>
    public float PosX { get; set; }

    /// <summary>
    /// type: float
    /// </summary>
    public float PosY { get; set; }

    /// <summary>
    /// type: float
    /// </summary>
    public float PosZ { get; set; }

}
