using SwiftlyS2.Core.GameEventDefinitions;
using SwiftlyS2.Shared.GameEvents;
using SwiftlyS2.Shared.Players;
using SwiftlyS2.Shared.SchemaDefinitions;

namespace SwiftlyS2.Shared.GameEventDefinitions;

/// <summary> 
/// Event "inspect_weapon"
/// </summary>
public interface EventInspectWeapon : IGameEvent<EventInspectWeapon>
{

    static EventInspectWeapon IGameEvent<EventInspectWeapon>.Create( nint address ) => new EventInspectWeaponImpl(address);

    static string IGameEvent<EventInspectWeapon>.GetName() => "inspect_weapon";

    static uint IGameEvent<EventInspectWeapon>.GetHash() => 0x211A0C2Cu;
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

}
