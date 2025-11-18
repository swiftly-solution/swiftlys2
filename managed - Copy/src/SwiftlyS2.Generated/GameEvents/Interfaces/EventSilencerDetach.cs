using SwiftlyS2.Shared.SchemaDefinitions;
using SwiftlyS2.Shared.GameEvents;
using SwiftlyS2.Core.GameEventDefinitions;
using SwiftlyS2.Shared.Players;

namespace SwiftlyS2.Shared.GameEventDefinitions;

/// <summary> 
/// Event "silencer_detach"
/// </summary>
public interface EventSilencerDetach : IGameEvent<EventSilencerDetach> {

  static EventSilencerDetach IGameEvent<EventSilencerDetach>.Create(nint address) => new EventSilencerDetachImpl(address);

  static string IGameEvent<EventSilencerDetach>.GetName() => "silencer_detach";

  static uint IGameEvent<EventSilencerDetach>.GetHash() => 0x1670A6D0u;
  /// <summary>
  /// <br/>
  /// type: player_controller_and_pawn
  /// </summary>
  CCSPlayerController UserIdController { get; }

  /// <summary>
  /// <br/>
  /// type: player_controller_and_pawn
  /// </summary>
  CCSPlayerPawn UserIdPawn { get; }


  public IPlayer UserIdPlayer
  { get => Accessor.GetPlayer("userid"); }
  /// <summary>
  /// <br/>
  /// type: player_controller_and_pawn
  /// </summary>
  int UserId { get; set; }

}
