using SwiftlyS2.Core.GameEventDefinitions;
using SwiftlyS2.Shared.GameEvents;
using SwiftlyS2.Shared.Players;
using SwiftlyS2.Shared.SchemaDefinitions;

namespace SwiftlyS2.Shared.GameEventDefinitions;

/// <summary> 
/// Event "hostage_hurt"
/// </summary>
public interface EventHostageHurt : IGameEvent<EventHostageHurt>
{

    static EventHostageHurt IGameEvent<EventHostageHurt>.Create( nint address ) => new EventHostageHurtImpl(address);

    static string IGameEvent<EventHostageHurt>.GetName() => "hostage_hurt";

    static uint IGameEvent<EventHostageHurt>.GetHash() => 0x5F292C42u;
    /// <summary>
    /// player who hurt the hostage
    /// <br/>
    /// type: player_controller_and_pawn
    /// </summary>
    public CCSPlayerController UserIdController { get; }

    /// <summary>
    /// player who hurt the hostage
    /// <br/>
    /// type: player_controller_and_pawn
    /// </summary>
    public CCSPlayerPawn UserIdPawn { get; }


    // player who hurt the hostage
    public IPlayer UserIdPlayer { get => Accessor.GetPlayer("userid"); }
    /// <summary>
    /// player who hurt the hostage
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
