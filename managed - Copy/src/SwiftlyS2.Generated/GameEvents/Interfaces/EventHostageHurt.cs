using SwiftlyS2.Shared.SchemaDefinitions;
using SwiftlyS2.Shared.GameEvents;
using SwiftlyS2.Core.GameEventDefinitions;
using SwiftlyS2.Shared.Players;

namespace SwiftlyS2.Shared.GameEventDefinitions;

/// <summary> 
/// Event "hostage_hurt"
/// </summary>
public interface EventHostageHurt : IGameEvent<EventHostageHurt> {

  static EventHostageHurt IGameEvent<EventHostageHurt>.Create(nint address) => new EventHostageHurtImpl(address);

  static string IGameEvent<EventHostageHurt>.GetName() => "hostage_hurt";

  static uint IGameEvent<EventHostageHurt>.GetHash() => 0x5F292C42u;
  /// <summary>
  /// player who hurt the hostage
  /// <br/>
  /// type: player_controller_and_pawn
  /// </summary>
  CCSPlayerController UserIdController { get; }

  /// <summary>
  /// player who hurt the hostage
  /// <br/>
  /// type: player_controller_and_pawn
  /// </summary>
  CCSPlayerPawn UserIdPawn { get; }


  // player who hurt the hostage
  public IPlayer UserIdPlayer
  { get => Accessor.GetPlayer("userid"); }
  /// <summary>
  /// player who hurt the hostage
  /// <br/>
  /// type: player_controller_and_pawn
  /// </summary>
  int UserId { get; set; }

  /// <summary>
  /// hostage entity index
  /// <br/>
  /// type: short
  /// </summary>
  short Hostage { get; set; }

}
