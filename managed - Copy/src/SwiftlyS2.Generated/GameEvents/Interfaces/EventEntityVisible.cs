using SwiftlyS2.Shared.SchemaDefinitions;
using SwiftlyS2.Shared.GameEvents;
using SwiftlyS2.Core.GameEventDefinitions;
using SwiftlyS2.Shared.Players;

namespace SwiftlyS2.Shared.GameEventDefinitions;

/// <summary> 
/// Event "entity_visible"
/// </summary>
public interface EventEntityVisible : IGameEvent<EventEntityVisible> {

  static EventEntityVisible IGameEvent<EventEntityVisible>.Create(nint address) => new EventEntityVisibleImpl(address);

  static string IGameEvent<EventEntityVisible>.GetName() => "entity_visible";

  static uint IGameEvent<EventEntityVisible>.GetHash() => 0xC4D03823u;
  /// <summary>
  /// The player who sees the entity
  /// <br/>
  /// type: player_controller
  /// </summary>
  CCSPlayerController UserIdController { get; }

  /// <summary>
  /// The player who sees the entity
  /// <br/>
  /// type: player_controller
  /// </summary>
  CCSPlayerPawn UserIdPawn { get; }


  // The player who sees the entity
  public IPlayer UserIdPlayer
  { get => Accessor.GetPlayer("userid"); }
  /// <summary>
  /// The player who sees the entity
  /// <br/>
  /// type: player_controller
  /// </summary>
  int UserId { get; set; }

  /// <summary>
  /// Entindex of the entity they see
  /// <br/>
  /// type: long
  /// </summary>
  int Subject { get; set; }

  /// <summary>
  /// Classname of the entity they see
  /// <br/>
  /// type: string
  /// </summary>
  string ClassName { get; set; }

  /// <summary>
  /// name of the entity they see
  /// <br/>
  /// type: string
  /// </summary>
  string EntityName { get; set; }

}
