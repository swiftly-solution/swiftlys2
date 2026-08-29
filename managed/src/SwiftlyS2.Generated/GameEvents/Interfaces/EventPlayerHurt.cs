using SwiftlyS2.Shared.SchemaDefinitions;
using SwiftlyS2.Shared.GameEvents;
using SwiftlyS2.Core.GameEventDefinitions;
using SwiftlyS2.Shared.Players;

namespace SwiftlyS2.Shared.GameEventDefinitions;

/// <summary>
/// Event "player_hurt"
/// </summary>
public interface EventPlayerHurt : IGameEvent<EventPlayerHurt>
{

    static EventPlayerHurt IGameEvent<EventPlayerHurt>.Create(nint address) => new EventPlayerHurtImpl(address);

    static string IGameEvent<EventPlayerHurt>.GetName() => "player_hurt";

    static uint IGameEvent<EventPlayerHurt>.GetHash() => 0x1B30DDF0u;

    /// <summary>
    /// player who was hurt
    /// <br/>
    /// type: player_controller_and_pawn
    /// </summary>
    CCSPlayerController UserIdController { get; }

    /// <summary>
    /// player who was hurt
    /// <br/>
    /// type: player_controller_and_pawn
    /// </summary>
    CCSPlayerPawn UserIdPawn { get; }

    // player who was hurt
    public IPlayer? UserIdPlayer
    { get => Accessor.GetPlayer("userid"); }

    /// <summary>
    /// player who was hurt
    /// <br/>
    /// type: player_controller_and_pawn
    /// </summary>
    int UserId { get; set; }

    /// <summary>
    /// player who attacked
    /// <br/>
    /// type: player_controller_and_pawn
    /// </summary>
    CCSPlayerController AttackerController { get; }

    /// <summary>
    /// player who attacked
    /// <br/>
    /// type: player_controller_and_pawn
    /// </summary>
    CCSPlayerPawn AttackerPawn { get; }

    // player who attacked
    public IPlayer? AttackerPlayer
    { get => Accessor.GetPlayer("attacker"); }

    /// <summary>
    /// player who attacked
    /// <br/>
    /// type: player_controller_and_pawn
    /// </summary>
    int Attacker { get; set; }

    /// <summary>
    /// remaining health points
    /// <br/>
    /// type: byte
    /// <br/>
    /// See <see cref="ActualHealth"/>.
    /// </summary>
    [Obsolete("The declared type may not match the actual value. Use ActualHealth instead.")]
    byte Health { get; set; }

    /// <summary>
    /// remaining health points
    /// <br/>
    /// type: int
    /// </summary>
    int ActualHealth { get; set; }

    /// <summary>
    /// remaining armor points
    /// <br/>
    /// type: byte
    /// <br/>
    /// See <see cref="ActualArmor"/>.
    /// </summary>
    [Obsolete("The declared type may not match the actual value. Use ActualArmor instead.")]
    byte Armor { get; set; }

    /// <summary>
    /// remaining armor points
    /// <br/>
    /// type: int
    /// </summary>
    int ActualArmor { get; set; }

    /// <summary>
    /// weapon name attacker used, if not the world
    /// <br/>
    /// type: string
    /// </summary>
    string Weapon { get; set; }

    /// <summary>
    /// damage done to health
    /// <br/>
    /// type: short
    /// <br/>
    /// See <see cref="ActualDmgHealth"/>.
    /// </summary>
    [Obsolete("The declared type may not match the actual value. Use ActualDmgHealth instead.")]
    short DmgHealth { get; set; }

    /// <summary>
    /// damage done to health
    /// <br/>
    /// type: int
    /// </summary>
    int ActualDmgHealth { get; set; }

    /// <summary>
    /// damage done to armor
    /// <br/>
    /// type: byte
    /// <br/>
    /// See <see cref="ActualDmgArmor"/>.
    /// </summary>
    [Obsolete("The declared type may not match the actual value. Use ActualDmgArmor instead.")]
    byte DmgArmor { get; set; }

    /// <summary>
    /// damage done to armor
    /// <br/>
    /// type: int
    /// </summary>
    int ActualDmgArmor { get; set; }

    /// <summary>
    /// hitgroup that was damaged
    /// <br/>
    /// type: byte
    /// <br/>
    /// See <see cref="ActualHitGroup"/>.
    /// </summary>
    [Obsolete("The declared type may not match the actual value. Use ActualHitGroup instead.")]
    byte HitGroup { get; set; }

    /// <summary>
    /// hitgroup that was damaged
    /// <br/>
    /// type: HitGroup_t
    /// </summary>
    HitGroup_t ActualHitGroup { get; set; }
}
