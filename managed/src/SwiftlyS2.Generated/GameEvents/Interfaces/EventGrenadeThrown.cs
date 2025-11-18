using SwiftlyS2.Core.GameEventDefinitions;
using SwiftlyS2.Shared.GameEvents;
using SwiftlyS2.Shared.Players;
using SwiftlyS2.Shared.SchemaDefinitions;

namespace SwiftlyS2.Shared.GameEventDefinitions;

/// <summary> 
/// Event "grenade_thrown"
/// </summary>
public interface EventGrenadeThrown : IGameEvent<EventGrenadeThrown>
{

    static EventGrenadeThrown IGameEvent<EventGrenadeThrown>.Create( nint address ) => new EventGrenadeThrownImpl(address);

    static string IGameEvent<EventGrenadeThrown>.GetName() => "grenade_thrown";

    static uint IGameEvent<EventGrenadeThrown>.GetHash() => 0x0F018978u;
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
    /// weapon name used
    /// <br/>
    /// type: string
    /// </summary>
    public string Weapon { get; set; }

}
