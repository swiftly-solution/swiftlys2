using SwiftlyS2.Core.GameEventDefinitions;
using SwiftlyS2.Shared.GameEvents;

namespace SwiftlyS2.Shared.GameEventDefinitions;

/// <summary> 
/// Event "show_deathpanel"
/// </summary>
public interface EventShowDeathpanel : IGameEvent<EventShowDeathpanel>
{

    static EventShowDeathpanel IGameEvent<EventShowDeathpanel>.Create( nint address ) => new EventShowDeathpanelImpl(address);

    static string IGameEvent<EventShowDeathpanel>.GetName() => "show_deathpanel";

    static uint IGameEvent<EventShowDeathpanel>.GetHash() => 0x2AB9F7A1u;
    /// <summary>
    /// endindex of the one who was killed
    /// <br/>
    /// type: player_controller_and_pawn
    /// </summary>
    public int Victim { get; set; }

    /// <summary>
    /// entindex of the killer entity
    /// <br/>
    /// type: ehandle
    /// </summary>
    public nint Killer { get; set; }

    /// <summary>
    /// type: player_controller
    /// </summary>
    public int KillerController { get; set; }

    /// <summary>
    /// type: short
    /// </summary>
    public short HitsTaken { get; set; }

    /// <summary>
    /// type: short
    /// </summary>
    public short DamageTaken { get; set; }

    /// <summary>
    /// type: short
    /// </summary>
    public short HitsGiven { get; set; }

    /// <summary>
    /// type: short
    /// </summary>
    public short DamageGiven { get; set; }

}
