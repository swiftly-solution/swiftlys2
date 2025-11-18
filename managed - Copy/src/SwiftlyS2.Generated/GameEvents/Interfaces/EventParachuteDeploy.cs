using SwiftlyS2.Shared.SchemaDefinitions;
using SwiftlyS2.Shared.GameEvents;
using SwiftlyS2.Core.GameEventDefinitions;
using SwiftlyS2.Shared.Players;

namespace SwiftlyS2.Shared.GameEventDefinitions;

/// <summary> 
/// Event "parachute_deploy"
/// </summary>
public interface EventParachuteDeploy : IGameEvent<EventParachuteDeploy> {

  static EventParachuteDeploy IGameEvent<EventParachuteDeploy>.Create(nint address) => new EventParachuteDeployImpl(address);

  static string IGameEvent<EventParachuteDeploy>.GetName() => "parachute_deploy";

  static uint IGameEvent<EventParachuteDeploy>.GetHash() => 0xE34D70F2u;
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
