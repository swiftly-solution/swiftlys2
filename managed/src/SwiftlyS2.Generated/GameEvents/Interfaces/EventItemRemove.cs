using SwiftlyS2.Core.GameEventDefinitions;
using SwiftlyS2.Shared.GameEvents;
using SwiftlyS2.Shared.Players;
using SwiftlyS2.Shared.SchemaDefinitions;

namespace SwiftlyS2.Shared.GameEventDefinitions;

/// <summary> 
/// Event "item_remove"
/// </summary>
public interface EventItemRemove : IGameEvent<EventItemRemove>
{

    static EventItemRemove IGameEvent<EventItemRemove>.Create( nint address ) => new EventItemRemoveImpl(address);

    static string IGameEvent<EventItemRemove>.GetName() => "item_remove";

    static uint IGameEvent<EventItemRemove>.GetHash() => 0x4853B5C7u;
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
    /// either a weapon such as 'tmp' or 'hegrenade', or an item such as 'nvgs'
    /// <br/>
    /// type: string
    /// </summary>
    public string Item { get; set; }

    /// <summary>
    /// type: long
    /// </summary>
    public int DefIndex { get; set; }

}
