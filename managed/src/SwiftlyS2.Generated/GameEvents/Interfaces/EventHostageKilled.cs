using SwiftlyS2.Core.GameEventDefinitions;
using SwiftlyS2.Shared.GameEvents;
using SwiftlyS2.Shared.Players;
using SwiftlyS2.Shared.SchemaDefinitions;

namespace SwiftlyS2.Shared.GameEventDefinitions;

/// <summary> 
/// Event "hostage_killed"
/// </summary>
public interface EventHostageKilled : IGameEvent<EventHostageKilled>
{

    static EventHostageKilled IGameEvent<EventHostageKilled>.Create( nint address ) => new EventHostageKilledImpl(address);

    static string IGameEvent<EventHostageKilled>.GetName() => "hostage_killed";

    static uint IGameEvent<EventHostageKilled>.GetHash() => 0x0B1DFB8Au;
    /// <summary>
    /// player who killed the hostage
    /// <br/>
    /// type: player_controller_and_pawn
    /// </summary>
    public CCSPlayerController UserIdController { get; }

    /// <summary>
    /// player who killed the hostage
    /// <br/>
    /// type: player_controller_and_pawn
    /// </summary>
    public CCSPlayerPawn UserIdPawn { get; }


    // player who killed the hostage
    public IPlayer UserIdPlayer { get => Accessor.GetPlayer("userid"); }
    /// <summary>
    /// player who killed the hostage
    /// <br/>
    /// type: player_controller_and_pawn
    /// </summary>
    public int UserId { get; set; }

    /// <summary>
    /// hostage entity index
    /// <br/>
    /// type: short
    /// </summary>
    public short Hostage { get; set; }

}
