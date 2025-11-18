using SwiftlyS2.Core.GameEventDefinitions;
using SwiftlyS2.Shared.GameEvents;
using SwiftlyS2.Shared.Players;
using SwiftlyS2.Shared.SchemaDefinitions;

namespace SwiftlyS2.Shared.GameEventDefinitions;

/// <summary> 
/// Event "survival_no_respawns_final"
/// </summary>
public interface EventSurvivalNoRespawnsFinal : IGameEvent<EventSurvivalNoRespawnsFinal>
{

    static EventSurvivalNoRespawnsFinal IGameEvent<EventSurvivalNoRespawnsFinal>.Create( nint address ) => new EventSurvivalNoRespawnsFinalImpl(address);

    static string IGameEvent<EventSurvivalNoRespawnsFinal>.GetName() => "survival_no_respawns_final";

    static uint IGameEvent<EventSurvivalNoRespawnsFinal>.GetHash() => 0xE88046CAu;
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
