using SwiftlyS2.Shared.SchemaDefinitions;
using SwiftlyS2.Shared.GameEvents;
using SwiftlyS2.Core.GameEventDefinitions;
using SwiftlyS2.Shared.Players;

namespace SwiftlyS2.Shared.GameEventDefinitions;

/// <summary> 
/// Event "hltv_chase"
/// shot of a single entity
/// </summary>
public interface EventHltvChase : IGameEvent<EventHltvChase> {

  static EventHltvChase IGameEvent<EventHltvChase>.Create(nint address) => new EventHltvChaseImpl(address);

  static string IGameEvent<EventHltvChase>.GetName() => "hltv_chase";

  static uint IGameEvent<EventHltvChase>.GetHash() => 0xEB73303Au;
  /// <summary>
  /// primary traget index
  /// <br/>
  /// type: player_controller
  /// </summary>
  int Target1 { get; set; }

  /// <summary>
  /// secondary traget index or 0
  /// <br/>
  /// type: player_controller
  /// </summary>
  int Target2 { get; set; }

  /// <summary>
  /// camera distance
  /// <br/>
  /// type: short
  /// </summary>
  short Distance { get; set; }

  /// <summary>
  /// view angle horizontal
  /// <br/>
  /// type: short
  /// </summary>
  short Theta { get; set; }

  /// <summary>
  /// view angle vertical
  /// <br/>
  /// type: short
  /// </summary>
  short Phi { get; set; }

  /// <summary>
  /// camera inertia
  /// <br/>
  /// type: byte
  /// </summary>
  byte Inertia { get; set; }

  /// <summary>
  /// diretcor suggests to show ineye
  /// <br/>
  /// type: byte
  /// </summary>
  byte InEye { get; set; }

}
