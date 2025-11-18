using SwiftlyS2.Core.GameEventDefinitions;
using SwiftlyS2.Shared.GameEvents;
using SwiftlyS2.Shared.Players;
using SwiftlyS2.Shared.SchemaDefinitions;

namespace SwiftlyS2.Shared.GameEventDefinitions;

/// <summary> 
/// Event "dz_item_interaction"
/// </summary>
public interface EventDzItemInteraction : IGameEvent<EventDzItemInteraction>
{

    static EventDzItemInteraction IGameEvent<EventDzItemInteraction>.Create( nint address ) => new EventDzItemInteractionImpl(address);

    static string IGameEvent<EventDzItemInteraction>.GetName() => "dz_item_interaction";

    static uint IGameEvent<EventDzItemInteraction>.GetHash() => 0x4C0C7044u;
    /// <summary>
    /// player entindex
    /// <br/>
    /// type: player_controller
    /// </summary>
    public CCSPlayerController UserIdController { get; }

    /// <summary>
    /// player entindex
    /// <br/>
    /// type: player_controller
    /// </summary>
    public CCSPlayerPawn UserIdPawn { get; }


    // player entindex
    public IPlayer UserIdPlayer { get => Accessor.GetPlayer("userid"); }
    /// <summary>
    /// player entindex
    /// <br/>
    /// type: player_controller
    /// </summary>
    public int UserId { get; set; }

    /// <summary>
    /// crate entindex
    /// <br/>
    /// type: short
    /// </summary>
    public short Subject { get; set; }

    /// <summary>
    /// type of crate (metal, wood, or paradrop)
    /// <br/>
    /// type: string
    /// </summary>
    public string Type { get; set; }

}
