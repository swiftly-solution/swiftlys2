using SwiftlyS2.Core.GameEventDefinitions;
using SwiftlyS2.Shared.GameEvents;
using SwiftlyS2.Shared.Players;
using SwiftlyS2.Shared.SchemaDefinitions;

namespace SwiftlyS2.Shared.GameEventDefinitions;

/// <summary> 
/// Event "player_changename"
/// </summary>
public interface EventPlayerChangename : IGameEvent<EventPlayerChangename>
{

    static EventPlayerChangename IGameEvent<EventPlayerChangename>.Create( nint address ) => new EventPlayerChangenameImpl(address);

    static string IGameEvent<EventPlayerChangename>.GetName() => "player_changename";

    static uint IGameEvent<EventPlayerChangename>.GetHash() => 0xD955F966u;
    /// <summary>
    /// user ID on server
    /// <br/>
    /// type: player_controller
    /// </summary>
    public CCSPlayerController UserIdController { get; }

    /// <summary>
    /// user ID on server
    /// <br/>
    /// type: player_controller
    /// </summary>
    public CCSPlayerPawn UserIdPawn { get; }


    // user ID on server
    public IPlayer UserIdPlayer { get => Accessor.GetPlayer("userid"); }
    /// <summary>
    /// user ID on server
    /// <br/>
    /// type: player_controller
    /// </summary>
    public int UserId { get; set; }

    /// <summary>
    /// players old (current) name
    /// <br/>
    /// type: string
    /// </summary>
    public string OldName { get; set; }

    /// <summary>
    /// players new name
    /// <br/>
    /// type: string
    /// </summary>
    public string NewName { get; set; }

}
