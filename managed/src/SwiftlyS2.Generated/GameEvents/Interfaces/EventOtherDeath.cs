using SwiftlyS2.Core.GameEventDefinitions;
using SwiftlyS2.Shared.GameEvents;

namespace SwiftlyS2.Shared.GameEventDefinitions;

/// <summary> 
/// Event "other_death"
/// </summary>
public interface EventOtherDeath : IGameEvent<EventOtherDeath>
{

    static EventOtherDeath IGameEvent<EventOtherDeath>.Create( nint address ) => new EventOtherDeathImpl(address);

    static string IGameEvent<EventOtherDeath>.GetName() => "other_death";

    static uint IGameEvent<EventOtherDeath>.GetHash() => 0x8638AEECu;
    /// <summary>
    /// other entity ID who died
    /// <br/>
    /// type: short
    /// </summary>
    public short OtherID { get; set; }

    /// <summary>
    /// other entity type
    /// <br/>
    /// type: string
    /// </summary>
    public string OtherType { get; set; }

    /// <summary>
    /// user ID who killed
    /// <br/>
    /// type: short
    /// </summary>
    public short Attacker { get; set; }

    /// <summary>
    /// weapon name killer used
    /// <br/>
    /// type: string
    /// </summary>
    public string Weapon { get; set; }

    /// <summary>
    /// inventory item id of weapon killer used
    /// <br/>
    /// type: string
    /// </summary>
    public string WeaponItemid { get; set; }

    /// <summary>
    /// faux item id of weapon killer used
    /// <br/>
    /// type: string
    /// </summary>
    public string WeaponFauxitemid { get; set; }

    /// <summary>
    /// type: string
    /// </summary>
    public string WeaponOriginalownerXuid { get; set; }

    /// <summary>
    /// singals a headshot
    /// <br/>
    /// type: bool
    /// </summary>
    public bool Headshot { get; set; }

    /// <summary>
    /// number of objects shot penetrated before killing target
    /// <br/>
    /// type: short
    /// </summary>
    public short Penetrated { get; set; }

    /// <summary>
    /// kill happened without a scope, used for death notice icon
    /// <br/>
    /// type: bool
    /// </summary>
    public bool NoScope { get; set; }

    /// <summary>
    /// hitscan weapon went through smoke grenade
    /// <br/>
    /// type: bool
    /// </summary>
    public bool ThruSmoke { get; set; }

    /// <summary>
    /// attacker was blind from flashbang
    /// <br/>
    /// type: bool
    /// </summary>
    public bool AttackerBlind { get; set; }

}
