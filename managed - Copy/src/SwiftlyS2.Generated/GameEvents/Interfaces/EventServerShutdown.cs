using SwiftlyS2.Shared.SchemaDefinitions;
using SwiftlyS2.Shared.GameEvents;
using SwiftlyS2.Core.GameEventDefinitions;
using SwiftlyS2.Shared.Players;

namespace SwiftlyS2.Shared.GameEventDefinitions;

/// <summary> 
/// Event "server_shutdown"
/// server shut down
/// </summary>
public interface EventServerShutdown : IGameEvent<EventServerShutdown> {

  static EventServerShutdown IGameEvent<EventServerShutdown>.Create(nint address) => new EventServerShutdownImpl(address);

  static string IGameEvent<EventServerShutdown>.GetName() => "server_shutdown";

  static uint IGameEvent<EventServerShutdown>.GetHash() => 0x840A8CCDu;
  /// <summary>
  /// reason why server was shut down
  /// <br/>
  /// type: string
  /// </summary>
  string Reason { get; set; }

}
