using SwiftlyS2.Shared.SchemaDefinitions;
using SwiftlyS2.Shared.GameEvents;
using SwiftlyS2.Core.GameEventDefinitions;
using SwiftlyS2.Shared.Players;

namespace SwiftlyS2.Shared.GameEventDefinitions;

/// <summary> 
/// Event "server_cvar"
/// a server console var has changed
/// </summary>
public interface EventServerCvar : IGameEvent<EventServerCvar> {

  static EventServerCvar IGameEvent<EventServerCvar>.Create(nint address) => new EventServerCvarImpl(address);

  static string IGameEvent<EventServerCvar>.GetName() => "server_cvar";

  static uint IGameEvent<EventServerCvar>.GetHash() => 0x11BA3D6Du;
  /// <summary>
  /// cvar name, eg "mp_roundtime"
  /// <br/>
  /// type: string
  /// </summary>
  string CVarName { get; set; }

  /// <summary>
  /// new cvar value
  /// <br/>
  /// type: string
  /// </summary>
  string CVarValue { get; set; }

}
