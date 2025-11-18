using SwiftlyS2.Shared.SchemaDefinitions;
using SwiftlyS2.Shared.GameEvents;
using SwiftlyS2.Core.GameEventDefinitions;
using SwiftlyS2.Shared.Players;

namespace SwiftlyS2.Shared.GameEventDefinitions;

/// <summary> 
/// Event "begin_new_match"
/// </summary>
public interface EventBeginNewMatch : IGameEvent<EventBeginNewMatch> {

  static EventBeginNewMatch IGameEvent<EventBeginNewMatch>.Create(nint address) => new EventBeginNewMatchImpl(address);

  static string IGameEvent<EventBeginNewMatch>.GetName() => "begin_new_match";

  static uint IGameEvent<EventBeginNewMatch>.GetHash() => 0xE804A3CDu;
}
