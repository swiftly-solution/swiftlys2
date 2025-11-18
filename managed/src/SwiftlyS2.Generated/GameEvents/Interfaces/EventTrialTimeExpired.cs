using SwiftlyS2.Core.GameEventDefinitions;
using SwiftlyS2.Shared.GameEvents;
using SwiftlyS2.Shared.Players;
using SwiftlyS2.Shared.SchemaDefinitions;

namespace SwiftlyS2.Shared.GameEventDefinitions;

/// <summary> 
/// Event "trial_time_expired"
/// </summary>
public interface EventTrialTimeExpired : IGameEvent<EventTrialTimeExpired>
{

    static EventTrialTimeExpired IGameEvent<EventTrialTimeExpired>.Create( nint address ) => new EventTrialTimeExpiredImpl(address);

    static string IGameEvent<EventTrialTimeExpired>.GetName() => "trial_time_expired";

    static uint IGameEvent<EventTrialTimeExpired>.GetHash() => 0xA80BA2FFu;
    /// <summary>
    /// player whose time has expired
    /// <br/>
    /// type: player_controller
    /// </summary>
    public CCSPlayerController UserIdController { get; }

    /// <summary>
    /// player whose time has expired
    /// <br/>
    /// type: player_controller
    /// </summary>
    public CCSPlayerPawn UserIdPawn { get; }


    // player whose time has expired
    public IPlayer UserIdPlayer { get => Accessor.GetPlayer("userid"); }
    /// <summary>
    /// player whose time has expired
    /// <br/>
    /// type: player_controller
    /// </summary>
    public int UserId { get; set; }

}
