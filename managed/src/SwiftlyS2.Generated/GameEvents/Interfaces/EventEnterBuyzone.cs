using SwiftlyS2.Core.GameEventDefinitions;
using SwiftlyS2.Shared.GameEvents;
using SwiftlyS2.Shared.Players;
using SwiftlyS2.Shared.SchemaDefinitions;

namespace SwiftlyS2.Shared.GameEventDefinitions;

/// <summary> 
/// Event "enter_buyzone"
/// </summary>
public interface EventEnterBuyzone : IGameEvent<EventEnterBuyzone>
{

    static EventEnterBuyzone IGameEvent<EventEnterBuyzone>.Create( nint address ) => new EventEnterBuyzoneImpl(address);

    static string IGameEvent<EventEnterBuyzone>.GetName() => "enter_buyzone";

    static uint IGameEvent<EventEnterBuyzone>.GetHash() => 0x9E49E798u;
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
    /// type: bool
    /// </summary>
    public bool CanBuy { get; set; }

}
