using SwiftlyS2.Core.GameEventDefinitions;
using SwiftlyS2.Shared.GameEvents;
using SwiftlyS2.Shared.Players;
using SwiftlyS2.Shared.SchemaDefinitions;

namespace SwiftlyS2.Shared.GameEventDefinitions;

/// <summary> 
/// Event "silencer_on"
/// </summary>
public interface EventSilencerOn : IGameEvent<EventSilencerOn>
{

    static EventSilencerOn IGameEvent<EventSilencerOn>.Create( nint address ) => new EventSilencerOnImpl(address);

    static string IGameEvent<EventSilencerOn>.GetName() => "silencer_on";

    static uint IGameEvent<EventSilencerOn>.GetHash() => 0xA834DFDAu;
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
