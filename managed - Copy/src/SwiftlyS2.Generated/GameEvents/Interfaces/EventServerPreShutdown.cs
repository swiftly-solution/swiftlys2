using SwiftlyS2.Shared.SchemaDefinitions;
using SwiftlyS2.Shared.GameEvents;
using SwiftlyS2.Core.GameEventDefinitions;
using SwiftlyS2.Shared.Players;

namespace SwiftlyS2.Shared.GameEventDefinitions;

/// <summary> 
/// Event "server_pre_shutdown"
/// server is about to be shut down
/// </summary>
public interface EventServerPreShutdown : IGameEvent<EventServerPreShutdown> {

  static EventServerPreShutdown IGameEvent<EventServerPreShutdown>.Create(nint address) => new EventServerPreShutdownImpl(address);

  static string IGameEvent<EventServerPreShutdown>.GetName() => "server_pre_shutdown";

  static uint IGameEvent<EventServerPreShutdown>.GetHash() => 0x2597A7B3u;
  /// <summary>
  /// reason why server is about to be shut down
  /// <br/>
  /// type: string
  /// </summary>
  string Reason { get; set; }

}
