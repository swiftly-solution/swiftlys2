using SwiftlyS2.Core.GameEventDefinitions;
using SwiftlyS2.Shared.GameEvents;
using SwiftlyS2.Shared.Players;
using SwiftlyS2.Shared.SchemaDefinitions;

namespace SwiftlyS2.Shared.GameEventDefinitions;

/// <summary> 
/// Event "player_shoot"
/// player shoot his weapon
/// </summary>
public interface EventPlayerShoot : IGameEvent<EventPlayerShoot>
{

    static EventPlayerShoot IGameEvent<EventPlayerShoot>.Create( nint address ) => new EventPlayerShootImpl(address);

    static string IGameEvent<EventPlayerShoot>.GetName() => "player_shoot";

    static uint IGameEvent<EventPlayerShoot>.GetHash() => 0xE4EF0C38u;
    /// <summary>
    /// user ID on server
    /// <br/>
    /// type: player_controller_and_pawn
    /// </summary>
    public CCSPlayerController UserIdController { get; }

    /// <summary>
    /// user ID on server
    /// <br/>
    /// type: player_controller_and_pawn
    /// </summary>
    public CCSPlayerPawn UserIdPawn { get; }


    // user ID on server
    public IPlayer UserIdPlayer { get => Accessor.GetPlayer("userid"); }
    /// <summary>
    /// user ID on server
    /// <br/>
    /// type: player_controller_and_pawn
    /// </summary>
    public int UserId { get; set; }

    /// <summary>
    /// weapon ID
    /// <br/>
    /// type: byte
    /// </summary>
    public byte Weapon { get; set; }

    /// <summary>
    /// weapon mode
    /// <br/>
    /// type: byte
    /// </summary>
    public byte Mode { get; set; }

}
