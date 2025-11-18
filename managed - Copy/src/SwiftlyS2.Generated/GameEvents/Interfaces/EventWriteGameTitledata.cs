using SwiftlyS2.Shared.SchemaDefinitions;
using SwiftlyS2.Shared.GameEvents;
using SwiftlyS2.Core.GameEventDefinitions;
using SwiftlyS2.Shared.Players;

namespace SwiftlyS2.Shared.GameEventDefinitions;

/// <summary> 
/// Event "write_game_titledata"
/// write user titledata in profile
/// </summary>
public interface EventWriteGameTitledata : IGameEvent<EventWriteGameTitledata> {

  static EventWriteGameTitledata IGameEvent<EventWriteGameTitledata>.Create(nint address) => new EventWriteGameTitledataImpl(address);

  static string IGameEvent<EventWriteGameTitledata>.GetName() => "write_game_titledata";

  static uint IGameEvent<EventWriteGameTitledata>.GetHash() => 0x6ECEB462u;
  /// <summary>
  /// Controller id of user
  /// <br/>
  /// type: short
  /// </summary>
  short ControllerId { get; set; }

}
