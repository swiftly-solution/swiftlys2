using SwiftlyS2.Shared.SchemaDefinitions;
using SwiftlyS2.Shared.GameEvents;
using SwiftlyS2.Core.GameEventDefinitions;
using SwiftlyS2.Shared.Players;

namespace SwiftlyS2.Shared.GameEventDefinitions;

/// <summary> 
/// Event "grenade_bounce"
/// </summary>
public interface EventGrenadeBounce : IGameEvent<EventGrenadeBounce> {

  static EventGrenadeBounce IGameEvent<EventGrenadeBounce>.Create(nint address) => new EventGrenadeBounceImpl(address);

  static string IGameEvent<EventGrenadeBounce>.GetName() => "grenade_bounce";

  static uint IGameEvent<EventGrenadeBounce>.GetHash() => 0xF75C5166u;
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
