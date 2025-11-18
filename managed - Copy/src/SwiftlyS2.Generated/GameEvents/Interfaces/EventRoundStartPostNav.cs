using SwiftlyS2.Shared.SchemaDefinitions;
using SwiftlyS2.Shared.GameEvents;
using SwiftlyS2.Core.GameEventDefinitions;
using SwiftlyS2.Shared.Players;

namespace SwiftlyS2.Shared.GameEventDefinitions;

/// <summary> 
/// Event "round_start_post_nav"
/// </summary>
public interface EventRoundStartPostNav : IGameEvent<EventRoundStartPostNav> {

  static EventRoundStartPostNav IGameEvent<EventRoundStartPostNav>.Create(nint address) => new EventRoundStartPostNavImpl(address);

  static string IGameEvent<EventRoundStartPostNav>.GetName() => "round_start_post_nav";

  static uint IGameEvent<EventRoundStartPostNav>.GetHash() => 0x0F2F9F25u;
}
