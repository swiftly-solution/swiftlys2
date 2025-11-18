using SwiftlyS2.Shared.SchemaDefinitions;
using SwiftlyS2.Shared.GameEvents;
using SwiftlyS2.Core.GameEventDefinitions;
using SwiftlyS2.Shared.Players;

namespace SwiftlyS2.Shared.GameEventDefinitions;

/// <summary> 
/// Event "silencer_off"
/// </summary>
public interface EventSilencerOff : IGameEvent<EventSilencerOff> {

  static EventSilencerOff IGameEvent<EventSilencerOff>.Create(nint address) => new EventSilencerOffImpl(address);

  static string IGameEvent<EventSilencerOff>.GetName() => "silencer_off";

  static uint IGameEvent<EventSilencerOff>.GetHash() => 0x572861ACu;
  /// <summary>
  /// <br/>
  /// type: player_controller
  /// </summary>
  CCSPlayerController UserIdController { get; }

  /// <summary>
  /// <br/>
  /// type: player_controller
  /// </summary>
  CCSPlayerPawn UserIdPawn { get; }


  public IPlayer UserIdPlayer
  { get => Accessor.GetPlayer("userid"); }
  /// <summary>
  /// <br/>
  /// type: player_controller
  /// </summary>
  int UserId { get; set; }

}
