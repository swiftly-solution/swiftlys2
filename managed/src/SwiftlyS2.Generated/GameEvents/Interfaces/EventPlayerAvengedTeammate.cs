using SwiftlyS2.Shared.SchemaDefinitions;
using SwiftlyS2.Shared.GameEvents;
using SwiftlyS2.Core.GameEventDefinitions;
using SwiftlyS2.Shared.Players;

namespace SwiftlyS2.Shared.GameEventDefinitions;

/// <summary>
/// Event "player_avenged_teammate"
/// </summary>
public interface EventPlayerAvengedTeammate : IGameEvent<EventPlayerAvengedTeammate>
{

    static EventPlayerAvengedTeammate IGameEvent<EventPlayerAvengedTeammate>.Create(nint address) => new EventPlayerAvengedTeammateImpl(address);

    static string IGameEvent<EventPlayerAvengedTeammate>.GetName() => "player_avenged_teammate";

    static uint IGameEvent<EventPlayerAvengedTeammate>.GetHash() => 0x8E286DACu;

    /// <summary>
    /// <br/>
    /// type: player_controller
    /// </summary>
    CCSPlayerController AvengerIdController { get; }

    /// <summary>
    /// <br/>
    /// type: player_controller
    /// </summary>
    CCSPlayerPawn AvengerIdPawn { get; }

    public IPlayer? AvengerIdPlayer
    { get => Accessor.GetPlayer("avenger_id"); }

    /// <summary>
    /// <br/>
    /// type: player_controller
    /// </summary>
    int AvengerId { get; set; }

    /// <summary>
    /// <br/>
    /// type: player_controller
    /// </summary>
    CCSPlayerController AvengedPlayerIdController { get; }

    /// <summary>
    /// <br/>
    /// type: player_controller
    /// </summary>
    CCSPlayerPawn AvengedPlayerIdPawn { get; }

    public IPlayer? AvengedPlayerIdPlayer
    { get => Accessor.GetPlayer("avenged_player_id"); }

    /// <summary>
    /// <br/>
    /// type: player_controller
    /// </summary>
    int AvengedPlayerId { get; set; }
}
