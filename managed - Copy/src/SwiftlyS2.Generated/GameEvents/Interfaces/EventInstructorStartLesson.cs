using SwiftlyS2.Shared.SchemaDefinitions;
using SwiftlyS2.Shared.GameEvents;
using SwiftlyS2.Core.GameEventDefinitions;
using SwiftlyS2.Shared.Players;

namespace SwiftlyS2.Shared.GameEventDefinitions;

/// <summary> 
/// Event "instructor_start_lesson"
/// </summary>
public interface EventInstructorStartLesson : IGameEvent<EventInstructorStartLesson> {

  static EventInstructorStartLesson IGameEvent<EventInstructorStartLesson>.Create(nint address) => new EventInstructorStartLessonImpl(address);

  static string IGameEvent<EventInstructorStartLesson>.GetName() => "instructor_start_lesson";

  static uint IGameEvent<EventInstructorStartLesson>.GetHash() => 0x03846AA2u;
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

  /// <summary>
  /// entity id that the hint should display at. Leave empty if controller target
  /// <br/>
  /// type: long
  /// </summary>
  int HintTarget { get; set; }

  /// <summary>
  /// type: byte
  /// </summary>
  byte VrMovementType { get; set; }

  /// <summary>
  /// type: bool
  /// </summary>
  bool VrSingleController { get; set; }

  /// <summary>
  /// type: byte
  /// </summary>
  byte VrControllerType { get; set; }

}
