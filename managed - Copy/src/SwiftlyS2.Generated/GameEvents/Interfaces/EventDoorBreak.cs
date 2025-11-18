using SwiftlyS2.Shared.SchemaDefinitions;
using SwiftlyS2.Shared.GameEvents;
using SwiftlyS2.Core.GameEventDefinitions;
using SwiftlyS2.Shared.Players;

namespace SwiftlyS2.Shared.GameEventDefinitions;

/// <summary> 
/// Event "door_break"
/// </summary>
public interface EventDoorBreak : IGameEvent<EventDoorBreak> {

  static EventDoorBreak IGameEvent<EventDoorBreak>.Create(nint address) => new EventDoorBreakImpl(address);

  static string IGameEvent<EventDoorBreak>.GetName() => "door_break";

  static uint IGameEvent<EventDoorBreak>.GetHash() => 0x79A0A2E9u;
  /// <summary>
  /// type: long
  /// </summary>
  int EntIndex { get; set; }

  /// <summary>
  /// type: long
  /// </summary>
  int DMgState { get; set; }

}
