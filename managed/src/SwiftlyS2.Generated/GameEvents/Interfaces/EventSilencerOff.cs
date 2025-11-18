using SwiftlyS2.Core.GameEventDefinitions;
using SwiftlyS2.Shared.GameEvents;
using SwiftlyS2.Shared.Players;
using SwiftlyS2.Shared.SchemaDefinitions;

namespace SwiftlyS2.Shared.GameEventDefinitions;

/// <summary> 
/// Event "silencer_off"
/// </summary>
public interface EventSilencerOff : IGameEvent<EventSilencerOff>
{

    static EventSilencerOff IGameEvent<EventSilencerOff>.Create( nint address ) => new EventSilencerOffImpl(address);

    static string IGameEvent<EventSilencerOff>.GetName() => "silencer_off";

    static uint IGameEvent<EventSilencerOff>.GetHash() => 0x572861ACu;
    /// <summary>
    /// <br/>
    /// type: player_controller
    /// </summary>
    public CCSPlayerController UserIdController { get; }

    /// <summary>
    /// <br/>
    /// type: player_controller
    /// </summary>
    public CCSPlayerPawn UserIdPawn { get; }


    public IPlayer UserIdPlayer { get => Accessor.GetPlayer("userid"); }
    /// <summary>
    /// <br/>
    /// type: player_controller
    /// </summary>
    public int UserId { get; set; }

}
