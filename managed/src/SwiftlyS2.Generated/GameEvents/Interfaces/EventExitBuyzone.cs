using SwiftlyS2.Core.GameEventDefinitions;
using SwiftlyS2.Shared.GameEvents;
using SwiftlyS2.Shared.Players;
using SwiftlyS2.Shared.SchemaDefinitions;

namespace SwiftlyS2.Shared.GameEventDefinitions;

/// <summary> 
/// Event "exit_buyzone"
/// </summary>
public interface EventExitBuyzone : IGameEvent<EventExitBuyzone>
{

    static EventExitBuyzone IGameEvent<EventExitBuyzone>.Create( nint address ) => new EventExitBuyzoneImpl(address);

    static string IGameEvent<EventExitBuyzone>.GetName() => "exit_buyzone";

    static uint IGameEvent<EventExitBuyzone>.GetHash() => 0x9DF26040u;
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
