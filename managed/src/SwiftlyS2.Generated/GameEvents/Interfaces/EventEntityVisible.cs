using SwiftlyS2.Core.GameEventDefinitions;
using SwiftlyS2.Shared.GameEvents;
using SwiftlyS2.Shared.Players;
using SwiftlyS2.Shared.SchemaDefinitions;

namespace SwiftlyS2.Shared.GameEventDefinitions;

/// <summary> 
/// Event "entity_visible"
/// </summary>
public interface EventEntityVisible : IGameEvent<EventEntityVisible>
{

    static EventEntityVisible IGameEvent<EventEntityVisible>.Create( nint address ) => new EventEntityVisibleImpl(address);

    static string IGameEvent<EventEntityVisible>.GetName() => "entity_visible";

    static uint IGameEvent<EventEntityVisible>.GetHash() => 0xC4D03823u;
    /// <summary>
    /// The player who sees the entity
    /// <br/>
    /// type: player_controller
    /// </summary>
    public CCSPlayerController UserIdController { get; }

    /// <summary>
    /// The player who sees the entity
    /// <br/>
    /// type: player_controller
    /// </summary>
    public CCSPlayerPawn UserIdPawn { get; }


    // The player who sees the entity
    public IPlayer UserIdPlayer { get => Accessor.GetPlayer("userid"); }
    /// <summary>
    /// The player who sees the entity
    /// <br/>
    /// type: player_controller
    /// </summary>
    public int UserId { get; set; }

    /// <summary>
    /// Entindex of the entity they see
    /// <br/>
    /// type: long
    /// </summary>
    public int Subject { get; set; }

    /// <summary>
    /// Classname of the entity they see
    /// <br/>
    /// type: string
    /// </summary>
    public string ClassName { get; set; }

    /// <summary>
    /// name of the entity they see
    /// <br/>
    /// type: string
    /// </summary>
    public string EntityName { get; set; }

}
