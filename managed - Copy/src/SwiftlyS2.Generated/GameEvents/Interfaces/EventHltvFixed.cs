using SwiftlyS2.Shared.SchemaDefinitions;
using SwiftlyS2.Shared.GameEvents;
using SwiftlyS2.Core.GameEventDefinitions;
using SwiftlyS2.Shared.Players;

namespace SwiftlyS2.Shared.GameEventDefinitions;

/// <summary> 
/// Event "hltv_fixed"
/// show from fixed view
/// </summary>
public interface EventHltvFixed : IGameEvent<EventHltvFixed> {

  static EventHltvFixed IGameEvent<EventHltvFixed>.Create(nint address) => new EventHltvFixedImpl(address);

  static string IGameEvent<EventHltvFixed>.GetName() => "hltv_fixed";

  static uint IGameEvent<EventHltvFixed>.GetHash() => 0xCA86FB76u;
  /// <summary>
  /// camera position in world
  /// <br/>
  /// type: long
  /// </summary>
  int PosX { get; set; }

  /// <summary>
  /// type: long
  /// </summary>
  int Posy { get; set; }

  /// <summary>
  /// type: long
  /// </summary>
  int PosZ { get; set; }

  /// <summary>
  /// camera angles
  /// <br/>
  /// type: short
  /// </summary>
  short Theta { get; set; }

  /// <summary>
  /// type: short
  /// </summary>
  short Phi { get; set; }

  /// <summary>
  /// type: short
  /// </summary>
  short Offset { get; set; }

  /// <summary>
  /// type: float
  /// </summary>
  float FOv { get; set; }

  /// <summary>
  /// follow this player
  /// <br/>
  /// type: player_controller
  /// </summary>
  int Target { get; set; }

}
