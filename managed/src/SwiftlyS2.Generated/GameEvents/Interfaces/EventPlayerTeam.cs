using SwiftlyS2.Core.GameEventDefinitions;
using SwiftlyS2.Shared.GameEvents;
using SwiftlyS2.Shared.Players;
using SwiftlyS2.Shared.SchemaDefinitions;

namespace SwiftlyS2.Shared.GameEventDefinitions;

/// <summary> 
/// Event "player_team"
/// </summary>
public interface EventPlayerTeam : IGameEvent<EventPlayerTeam>
{

    static EventPlayerTeam IGameEvent<EventPlayerTeam>.Create( nint address ) => new EventPlayerTeamImpl(address);

    static string IGameEvent<EventPlayerTeam>.GetName() => "player_team";

    static uint IGameEvent<EventPlayerTeam>.GetHash() => 0xD57549C4u;
    /// <summary>
    /// player
    /// <br/>
    /// type: player_controller_and_pawn
    /// </summary>
    public CCSPlayerController UserIdController { get; }

    /// <summary>
    /// player
    /// <br/>
    /// type: player_controller_and_pawn
    /// </summary>
    public CCSPlayerPawn UserIdPawn { get; }


    // player
    public IPlayer UserIdPlayer { get => Accessor.GetPlayer("userid"); }
    /// <summary>
    /// player
    /// <br/>
    /// type: player_controller_and_pawn
    /// </summary>
    public int UserId { get; set; }

    /// <summary>
    /// team id
    /// <br/>
    /// type: byte
    /// </summary>
    public byte Team { get; set; }

    /// <summary>
    /// old team id
    /// <br/>
    /// type: byte
    /// </summary>
    public byte OldTeam { get; set; }

    /// <summary>
    /// team change because player disconnects
    /// <br/>
    /// type: bool
    /// </summary>
    public bool Disconnect { get; set; }

    /// <summary>
    /// type: bool
    /// </summary>
    public bool Silent { get; set; }

    /// <summary>
    /// type: string
    /// </summary>
    public string Name { get; set; }

    /// <summary>
    /// true if player is a bot
    /// <br/>
    /// type: bool
    /// </summary>
    public bool IsBot { get; set; }

}
