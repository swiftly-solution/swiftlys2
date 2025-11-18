using SwiftlyS2.Core.GameEvents;
using SwiftlyS2.Shared.GameEvents;
using SwiftlyS2.Shared.SchemaDefinitions;
using SwiftlyS2.Shared.GameEventDefinitions;
using SwiftlyS2.Shared.Players;

namespace SwiftlyS2.Core.GameEventDefinitions;

// generated
/// <summary> 
/// Event "clientside_lesson_closed"
/// </summary>
internal class EventClientsideLessonClosedImpl : GameEvent<EventClientsideLessonClosed>, EventClientsideLessonClosed
{

  public EventClientsideLessonClosedImpl(nint address) : base(address)
  {
  }

  public string LessonName
  { get => Accessor.GetString("lesson_name"); set => Accessor.SetString("lesson_name", value); }
}
