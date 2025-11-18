using SwiftlyS2.Shared.SchemaDefinitions;
using SwiftlyS2.Shared.GameEvents;
using SwiftlyS2.Core.GameEventDefinitions;
using SwiftlyS2.Shared.Players;

namespace SwiftlyS2.Shared.GameEventDefinitions;

/// <summary> 
/// Event "gameinstructor_draw"
/// </summary>
public interface EventGameinstructorDraw : IGameEvent<EventGameinstructorDraw> {

  static EventGameinstructorDraw IGameEvent<EventGameinstructorDraw>.Create(nint address) => new EventGameinstructorDrawImpl(address);

  static string IGameEvent<EventGameinstructorDraw>.GetName() => "gameinstructor_draw";

  static uint IGameEvent<EventGameinstructorDraw>.GetHash() => 0x6E89B5D7u;
}
