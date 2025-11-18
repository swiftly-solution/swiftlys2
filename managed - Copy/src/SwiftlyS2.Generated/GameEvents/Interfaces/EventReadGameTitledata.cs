using SwiftlyS2.Shared.SchemaDefinitions;
using SwiftlyS2.Shared.GameEvents;
using SwiftlyS2.Core.GameEventDefinitions;
using SwiftlyS2.Shared.Players;

namespace SwiftlyS2.Shared.GameEventDefinitions;

/// <summary> 
/// Event "read_game_titledata"
/// read user titledata from profile
/// </summary>
public interface EventReadGameTitledata : IGameEvent<EventReadGameTitledata> {

  static EventReadGameTitledata IGameEvent<EventReadGameTitledata>.Create(nint address) => new EventReadGameTitledataImpl(address);

  static string IGameEvent<EventReadGameTitledata>.GetName() => "read_game_titledata";

  static uint IGameEvent<EventReadGameTitledata>.GetHash() => 0xACF56D4Du;
  /// <summary>
  /// Controller id of user
  /// <br/>
  /// type: short
  /// </summary>
  short ControllerId { get; set; }

}
