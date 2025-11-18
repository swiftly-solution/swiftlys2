using SwiftlyS2.Core.GameEventDefinitions;
using SwiftlyS2.Shared.GameEvents;
using SwiftlyS2.Shared.Players;
using SwiftlyS2.Shared.SchemaDefinitions;

namespace SwiftlyS2.Shared.GameEventDefinitions;

/// <summary> 
/// Event "bomb_abortdefuse"
/// </summary>
public interface EventBombAbortdefuse : IGameEvent<EventBombAbortdefuse>
{

    static EventBombAbortdefuse IGameEvent<EventBombAbortdefuse>.Create( nint address ) => new EventBombAbortdefuseImpl(address);

    static string IGameEvent<EventBombAbortdefuse>.GetName() => "bomb_abortdefuse";

    static uint IGameEvent<EventBombAbortdefuse>.GetHash() => 0x73B79332u;
    /// <summary>
    /// player who was defusing
    /// <br/>
    /// type: player_controller_and_pawn
    /// </summary>
    public CCSPlayerController UserIdController { get; }

    /// <summary>
    /// player who was defusing
    /// <br/>
    /// type: player_controller_and_pawn
    /// </summary>
    public CCSPlayerPawn UserIdPawn { get; }


    // player who was defusing
    public IPlayer UserIdPlayer { get => Accessor.GetPlayer("userid"); }
    /// <summary>
    /// player who was defusing
    /// <br/>
    /// type: player_controller_and_pawn
    /// </summary>
    public int UserId { get; set; }

}
