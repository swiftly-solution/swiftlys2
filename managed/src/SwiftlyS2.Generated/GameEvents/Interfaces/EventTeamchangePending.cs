using SwiftlyS2.Core.GameEventDefinitions;
using SwiftlyS2.Shared.GameEvents;
using SwiftlyS2.Shared.Players;
using SwiftlyS2.Shared.SchemaDefinitions;

namespace SwiftlyS2.Shared.GameEventDefinitions;

/// <summary> 
/// Event "teamchange_pending"
/// </summary>
public interface EventTeamchangePending : IGameEvent<EventTeamchangePending>
{

    static EventTeamchangePending IGameEvent<EventTeamchangePending>.Create( nint address ) => new EventTeamchangePendingImpl(address);

    static string IGameEvent<EventTeamchangePending>.GetName() => "teamchange_pending";

    static uint IGameEvent<EventTeamchangePending>.GetHash() => 0x53F97450u;
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
    /// type: byte
    /// </summary>
    public byte ToTeam { get; set; }

}
