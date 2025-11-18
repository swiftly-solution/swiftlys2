using SwiftlyS2.Shared.SchemaDefinitions;
using SwiftlyS2.Shared.GameEvents;
using SwiftlyS2.Core.GameEventDefinitions;
using SwiftlyS2.Shared.Players;

namespace SwiftlyS2.Shared.GameEventDefinitions;

/// <summary> 
/// Event "clientside_lesson_closed"
/// </summary>
public interface EventClientsideLessonClosed : IGameEvent<EventClientsideLessonClosed> {

  static EventClientsideLessonClosed IGameEvent<EventClientsideLessonClosed>.Create(nint address) => new EventClientsideLessonClosedImpl(address);

  static string IGameEvent<EventClientsideLessonClosed>.GetName() => "clientside_lesson_closed";

  static uint IGameEvent<EventClientsideLessonClosed>.GetHash() => 0x251ECF23u;
  /// <summary>
  /// type: string
  /// </summary>
  string LessonName { get; set; }

}
