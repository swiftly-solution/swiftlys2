using SwiftlyS2.Shared.SchemaDefinitions;
using SwiftlyS2.Shared.GameEvents;
using SwiftlyS2.Core.GameEventDefinitions;
using SwiftlyS2.Shared.Players;

namespace SwiftlyS2.Shared.GameEventDefinitions;

/// <summary> 
/// Event "hostage_call_for_help"
/// </summary>
public interface EventHostageCallForHelp : IGameEvent<EventHostageCallForHelp> {

  static EventHostageCallForHelp IGameEvent<EventHostageCallForHelp>.Create(nint address) => new EventHostageCallForHelpImpl(address);

  static string IGameEvent<EventHostageCallForHelp>.GetName() => "hostage_call_for_help";

  static uint IGameEvent<EventHostageCallForHelp>.GetHash() => 0xADE57017u;
  /// <summary>
  /// hostage entity index
  /// <br/>
  /// type: short
  /// </summary>
  short Hostage { get; set; }

}
