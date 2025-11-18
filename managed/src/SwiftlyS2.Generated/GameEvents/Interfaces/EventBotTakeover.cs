using SwiftlyS2.Core.GameEventDefinitions;
using SwiftlyS2.Shared.GameEvents;
using SwiftlyS2.Shared.Players;
using SwiftlyS2.Shared.SchemaDefinitions;

namespace SwiftlyS2.Shared.GameEventDefinitions;

/// <summary> 
/// Event "bot_takeover"
/// </summary>
public interface EventBotTakeover : IGameEvent<EventBotTakeover>
{

    static EventBotTakeover IGameEvent<EventBotTakeover>.Create( nint address ) => new EventBotTakeoverImpl(address);

    static string IGameEvent<EventBotTakeover>.GetName() => "bot_takeover";

    static uint IGameEvent<EventBotTakeover>.GetHash() => 0x6F5C9FCAu;
    /// <summary>
    /// <br/>
    /// type: player_controller_and_pawn
    /// </summary>
    public CCSPlayerController UserIdController { get; }

    /// <summary>
    /// <br/>
    /// type: player_controller_and_pawn
    /// </summary>
    public CCSPlayerPawn UserIdPawn { get; }


    public IPlayer UserIdPlayer { get => Accessor.GetPlayer("userid"); }
    /// <summary>
    /// <br/>
    /// type: player_controller_and_pawn
    /// </summary>
    public int UserId { get; set; }

    /// <summary>
    /// type: player_controller
    /// </summary>
    public int BotID { get; set; }

    /// <summary>
    /// type: float
    /// </summary>
    public float P { get; set; }

    /// <summary>
    /// type: float
    /// </summary>
    public float Y { get; set; }

    /// <summary>
    /// type: float
    /// </summary>
    public float R { get; set; }

}
