using SwiftlyS2.Shared.SchemaDefinitions;
using SwiftlyS2.Shared.GameEvents;
using SwiftlyS2.Core.GameEventDefinitions;
using SwiftlyS2.Shared.Players;

namespace SwiftlyS2.Shared.GameEventDefinitions;

/// <summary> 
/// Event "endmatch_cmm_start_reveal_items"
/// </summary>
public interface EventEndmatchCmmStartRevealItems : IGameEvent<EventEndmatchCmmStartRevealItems> {

  static EventEndmatchCmmStartRevealItems IGameEvent<EventEndmatchCmmStartRevealItems>.Create(nint address) => new EventEndmatchCmmStartRevealItemsImpl(address);

  static string IGameEvent<EventEndmatchCmmStartRevealItems>.GetName() => "endmatch_cmm_start_reveal_items";

  static uint IGameEvent<EventEndmatchCmmStartRevealItems>.GetHash() => 0xCB866623u;
}
