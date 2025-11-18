using SwiftlyS2.Shared.SchemaDefinitions;
using SwiftlyS2.Shared.GameEvents;
using SwiftlyS2.Core.GameEventDefinitions;
using SwiftlyS2.Shared.Players;

namespace SwiftlyS2.Shared.GameEventDefinitions;

/// <summary> 
/// Event "defuser_dropped"
/// </summary>
public interface EventDefuserDropped : IGameEvent<EventDefuserDropped> {

  static EventDefuserDropped IGameEvent<EventDefuserDropped>.Create(nint address) => new EventDefuserDroppedImpl(address);

  static string IGameEvent<EventDefuserDropped>.GetName() => "defuser_dropped";

  static uint IGameEvent<EventDefuserDropped>.GetHash() => 0xA5E094F6u;
  /// <summary>
  /// defuser's entity ID
  /// <br/>
  /// type: long
  /// </summary>
  int EntityID { get; set; }

}
