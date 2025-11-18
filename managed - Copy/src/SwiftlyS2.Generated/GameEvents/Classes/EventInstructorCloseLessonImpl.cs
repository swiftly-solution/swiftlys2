using SwiftlyS2.Core.GameEvents;
using SwiftlyS2.Shared.GameEvents;
using SwiftlyS2.Shared.SchemaDefinitions;
using SwiftlyS2.Shared.GameEventDefinitions;
using SwiftlyS2.Shared.Players;

namespace SwiftlyS2.Core.GameEventDefinitions;

// generated
/// <summary> 
/// Event "instructor_close_lesson"
/// </summary>
internal class EventInstructorCloseLessonImpl : GameEvent<EventInstructorCloseLesson>, EventInstructorCloseLesson
{

  public EventInstructorCloseLessonImpl(nint address) : base(address)
  {
  }

  // The player who this lesson is intended for
  public CCSPlayerController UserIdController
  { get => Accessor.GetPlayerController("userid"); }

  // The player who this lesson is intended for
  public CCSPlayerPawn UserIdPawn
  { get => Accessor.GetPlayerPawn("userid"); }

  // The player who this lesson is intended for
  public IPlayer UserIdPlayer
  { get => Accessor.GetPlayer("userid"); }

  // The player who this lesson is intended for
  public int UserId
  { get => Accessor.GetInt32("userid"); set => Accessor.SetInt32("userid", value); }

  // Name of the lesson to start.  Must match instructor_lesson.txt
  public string HintName
  { get => Accessor.GetString("hint_name"); set => Accessor.SetString("hint_name", value); }
}
