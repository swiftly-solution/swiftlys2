using SwiftlyS2.Core.GameEvents;
using SwiftlyS2.Shared.GameEvents;
using SwiftlyS2.Shared.SchemaDefinitions;
using SwiftlyS2.Shared.GameEventDefinitions;
using SwiftlyS2.Shared.Players;

namespace SwiftlyS2.Core.GameEventDefinitions;

// generated
/// <summary> 
/// Event "gameinstructor_draw"
/// </summary>
internal class EventGameinstructorDrawImpl : GameEvent<EventGameinstructorDraw>, EventGameinstructorDraw
{

  public EventGameinstructorDrawImpl(nint address) : base(address)
  {
  }
}
