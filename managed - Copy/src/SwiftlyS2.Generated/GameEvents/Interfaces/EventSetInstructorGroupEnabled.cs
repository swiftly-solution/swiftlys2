using SwiftlyS2.Shared.SchemaDefinitions;
using SwiftlyS2.Shared.GameEvents;
using SwiftlyS2.Core.GameEventDefinitions;
using SwiftlyS2.Shared.Players;

namespace SwiftlyS2.Shared.GameEventDefinitions;

/// <summary> 
/// Event "set_instructor_group_enabled"
/// </summary>
public interface EventSetInstructorGroupEnabled : IGameEvent<EventSetInstructorGroupEnabled> {

  static EventSetInstructorGroupEnabled IGameEvent<EventSetInstructorGroupEnabled>.Create(nint address) => new EventSetInstructorGroupEnabledImpl(address);

  static string IGameEvent<EventSetInstructorGroupEnabled>.GetName() => "set_instructor_group_enabled";

  static uint IGameEvent<EventSetInstructorGroupEnabled>.GetHash() => 0x87A9E425u;
  /// <summary>
  /// type: string
  /// </summary>
  string Group { get; set; }

  /// <summary>
  /// type: short
  /// </summary>
  short Enabled { get; set; }

}
