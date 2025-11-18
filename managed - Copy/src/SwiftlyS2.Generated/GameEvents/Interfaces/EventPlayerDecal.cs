using SwiftlyS2.Shared.SchemaDefinitions;
using SwiftlyS2.Shared.GameEvents;
using SwiftlyS2.Core.GameEventDefinitions;
using SwiftlyS2.Shared.Players;

namespace SwiftlyS2.Shared.GameEventDefinitions;

/// <summary> 
/// Event "player_decal"
/// </summary>
public interface EventPlayerDecal : IGameEvent<EventPlayerDecal> {

  static EventPlayerDecal IGameEvent<EventPlayerDecal>.Create(nint address) => new EventPlayerDecalImpl(address);

  static string IGameEvent<EventPlayerDecal>.GetName() => "player_decal";

  static uint IGameEvent<EventPlayerDecal>.GetHash() => 0xC7978ED6u;
  /// <summary>
  /// <br/>
  /// type: player_pawn
  /// </summary>
  CCSPlayerController UserIdController { get; }

  /// <summary>
  /// <br/>
  /// type: player_pawn
  /// </summary>
  CCSPlayerPawn UserIdPawn { get; }


  public IPlayer UserIdPlayer
  { get => Accessor.GetPlayer("userid"); }
  /// <summary>
  /// <br/>
  /// type: player_pawn
  /// </summary>
  int UserId { get; set; }

}
