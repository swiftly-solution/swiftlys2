using SwiftlyS2.Shared.SchemaDefinitions;
using SwiftlyS2.Shared.GameEvents;
using SwiftlyS2.Core.GameEventDefinitions;
using SwiftlyS2.Shared.Players;

namespace SwiftlyS2.Shared.GameEventDefinitions;

/// <summary>
/// Event "show_deathpanel"
/// </summary>
public interface EventShowDeathpanel : IGameEvent<EventShowDeathpanel>
{

    static EventShowDeathpanel IGameEvent<EventShowDeathpanel>.Create(nint address) => new EventShowDeathpanelImpl(address);

    static string IGameEvent<EventShowDeathpanel>.GetName() => "show_deathpanel";

    static uint IGameEvent<EventShowDeathpanel>.GetHash() => 0x2AB9F7A1u;

    /// <summary>
    /// endindex of the one who was killed
    /// <br/>
    /// type: player_controller_and_pawn
    /// </summary>
    CCSPlayerController VictimController { get; }

    /// <summary>
    /// endindex of the one who was killed
    /// <br/>
    /// type: player_controller_and_pawn
    /// </summary>
    CCSPlayerPawn VictimPawn { get; }

    // endindex of the one who was killed
    public IPlayer? VictimPlayer
    { get => Accessor.GetPlayer("victim"); }

    /// <summary>
    /// endindex of the one who was killed
    /// <br/>
    /// type: player_controller_and_pawn
    /// </summary>
    int Victim { get; set; }

    /// <summary>
    /// entindex of the killer entity
    /// <br/>
    /// type: ehandle
    /// </summary>
    nint Killer { get; set; }

    /// <summary>
    /// <br/>
    /// type: player_controller
    /// </summary>
    CCSPlayerController KillerControllerController { get; }

    /// <summary>
    /// <br/>
    /// type: player_controller
    /// </summary>
    CCSPlayerPawn KillerControllerPawn { get; }

    public IPlayer? KillerControllerPlayer
    { get => Accessor.GetPlayer("killer_controller"); }

    /// <summary>
    /// <br/>
    /// type: player_controller
    /// </summary>
    int KillerController { get; set; }

    /// <summary>
    /// type: short
    /// </summary>
    short HitsTaken { get; set; }

    /// <summary>
    /// type: short
    /// </summary>
    short DamageTaken { get; set; }

    /// <summary>
    /// type: short
    /// </summary>
    short HitsGiven { get; set; }

    /// <summary>
    /// type: short
    /// </summary>
    short DamageGiven { get; set; }
}
