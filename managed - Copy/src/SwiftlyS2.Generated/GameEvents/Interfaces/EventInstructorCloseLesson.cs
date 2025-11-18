using SwiftlyS2.Shared.SchemaDefinitions;
using SwiftlyS2.Shared.GameEvents;
using SwiftlyS2.Core.GameEventDefinitions;
using SwiftlyS2.Shared.Players;

namespace SwiftlyS2.Shared.GameEventDefinitions;

/// <summary> 
/// Event "instructor_close_lesson"
/// </summary>
public interface EventInstructorCloseLesson : IGameEvent<EventInstructorCloseLesson> {

  static EventInstructorCloseLesson IGameEvent<EventInstructorCloseLesson>.Create(nint address) => new EventInstructorCloseLessonImpl(address);

  static string IGameEvent<EventInstructorCloseLesson>.GetName() => "instructor_close_lesson";

  static uint IGameEvent<EventInstructorCloseLesson>.GetHash() => 0x2C472152u;
  /// <summary>
  /// The player who this lesson is intended for
  /// <br/>
  /// type: player_controller
  /// </summary>
  CCSPlayerController UserIdController { get; }

  /// <summary>
  /// The player who this lesson is intended for
  /// <br/>
  /// type: player_controller
  /// </summary>
  CCSPlayerPawn UserIdPawn { get; }


  // The player who this lesson is intended for
  public IPlayer UserIdPlayer
  { get => Accessor.GetPlayer("userid"); }
  /// <summary>
  /// The player who this lesson is intended for
  /// <br/>
  /// type: player_controller
  /// </summary>
  int UserId { get; set; }

  /// <summary>
  /// Name of the lesson to start.  Must match instructor_lesson.txt
  /// <br/>
  /// type: string
  /// </summary>
  string HintName { get; set; }

}
