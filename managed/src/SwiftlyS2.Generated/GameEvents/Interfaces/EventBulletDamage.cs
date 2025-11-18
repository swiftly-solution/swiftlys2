using SwiftlyS2.Core.GameEventDefinitions;
using SwiftlyS2.Shared.GameEvents;

namespace SwiftlyS2.Shared.GameEventDefinitions;

/// <summary> 
/// Event "bullet_damage"
/// </summary>
public interface EventBulletDamage : IGameEvent<EventBulletDamage>
{

    static EventBulletDamage IGameEvent<EventBulletDamage>.Create( nint address ) => new EventBulletDamageImpl(address);

    static string IGameEvent<EventBulletDamage>.GetName() => "bullet_damage";

    static uint IGameEvent<EventBulletDamage>.GetHash() => 0xAB7EA51Fu;
    /// <summary>
    /// player index who was hurt
    /// <br/>
    /// type: player_controller_and_pawn
    /// </summary>
    public int Victim { get; set; }

    /// <summary>
    /// player index who attacked
    /// <br/>
    /// type: player_controller_and_pawn
    /// </summary>
    public int Attacker { get; set; }

    /// <summary>
    /// how far the bullet travelled before it hit the player
    /// <br/>
    /// type: float
    /// </summary>
    public float Distance { get; set; }

    /// <summary>
    /// direction vector of the bullet
    /// <br/>
    /// type: float
    /// </summary>
    public float DamageDirX { get; set; }

    /// <summary>
    /// direction vector of the bullet
    /// <br/>
    /// type: float
    /// </summary>
    public float DamageDirY { get; set; }

    /// <summary>
    /// direction vector of the bullet
    /// <br/>
    /// type: float
    /// </summary>
    public float DamageDirZ { get; set; }

    /// <summary>
    /// how many surfaces were penetrated
    /// <br/>
    /// type: byte
    /// </summary>
    public byte NumPenetrations { get; set; }

    /// <summary>
    /// was the shooter noscoped?
    /// <br/>
    /// type: bool
    /// </summary>
    public bool NoScope { get; set; }

    /// <summary>
    /// was the shooter jumping?
    /// <br/>
    /// type: bool
    /// </summary>
    public bool InAir { get; set; }

    /// <summary>
    /// shoot angle x
    /// <br/>
    /// type: float
    /// </summary>
    public float ShootAngX { get; set; }

    /// <summary>
    /// shoot angle y
    /// <br/>
    /// type: float
    /// </summary>
    public float ShootAngY { get; set; }

    /// <summary>
    /// shoot angle z
    /// <br/>
    /// type: float
    /// </summary>
    public float ShootAngZ { get; set; }

    /// <summary>
    /// aim punch x
    /// <br/>
    /// type: float
    /// </summary>
    public float AimPunchX { get; set; }

    /// <summary>
    /// aim punch y
    /// <br/>
    /// type: float
    /// </summary>
    public float AimPunchY { get; set; }

    /// <summary>
    /// aim punch z
    /// <br/>
    /// type: float
    /// </summary>
    public float AimPunchZ { get; set; }

    /// <summary>
    /// attack tick
    /// <br/>
    /// type: int
    /// </summary>
    public int AttackTickCount { get; set; }

    /// <summary>
    /// attack frac
    /// <br/>
    /// type: float
    /// </summary>
    public float AttackTickFrac { get; set; }

    /// <summary>
    /// render tick
    /// <br/>
    /// type: int
    /// </summary>
    public int RenderTickCount { get; set; }

    /// <summary>
    /// render frac
    /// <br/>
    /// type: float
    /// </summary>
    public float RenderTickFrac { get; set; }

    /// <summary>
    /// total inaccuracy
    /// <br/>
    /// type: float
    /// </summary>
    public float InaccuracyTotal { get; set; }

    /// <summary>
    /// move inaccuracy
    /// <br/>
    /// type: float
    /// </summary>
    public float InaccuracyMove { get; set; }

    /// <summary>
    /// air inaccuracy
    /// <br/>
    /// type: float
    /// </summary>
    public float InaccuracyAir { get; set; }

    /// <summary>
    /// recoil index. Yes this is really a float.
    /// <br/>
    /// type: float
    /// </summary>
    public float RecoilIndex { get; set; }

    /// <summary>
    /// lag compensation type
    /// <br/>
    /// type: int
    /// </summary>
    public int Type { get; set; }

}
