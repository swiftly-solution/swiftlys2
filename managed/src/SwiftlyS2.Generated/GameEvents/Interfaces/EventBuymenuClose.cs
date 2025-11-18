using SwiftlyS2.Core.GameEventDefinitions;
using SwiftlyS2.Shared.GameEvents;
using SwiftlyS2.Shared.Players;
using SwiftlyS2.Shared.SchemaDefinitions;

namespace SwiftlyS2.Shared.GameEventDefinitions;

/// <summary> 
/// Event "buymenu_close"
/// </summary>
public interface EventBuymenuClose : IGameEvent<EventBuymenuClose>
{

    static EventBuymenuClose IGameEvent<EventBuymenuClose>.Create( nint address ) => new EventBuymenuCloseImpl(address);

    static string IGameEvent<EventBuymenuClose>.GetName() => "buymenu_close";

    static uint IGameEvent<EventBuymenuClose>.GetHash() => 0xFFC1EF17u;
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
