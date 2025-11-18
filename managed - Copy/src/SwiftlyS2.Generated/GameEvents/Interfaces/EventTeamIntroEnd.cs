using SwiftlyS2.Shared.SchemaDefinitions;
using SwiftlyS2.Shared.GameEvents;
using SwiftlyS2.Core.GameEventDefinitions;
using SwiftlyS2.Shared.Players;

namespace SwiftlyS2.Shared.GameEventDefinitions;

/// <summary> 
/// Event "team_intro_end"
/// </summary>
public interface EventTeamIntroEnd : IGameEvent<EventTeamIntroEnd> {

  static EventTeamIntroEnd IGameEvent<EventTeamIntroEnd>.Create(nint address) => new EventTeamIntroEndImpl(address);

  static string IGameEvent<EventTeamIntroEnd>.GetName() => "team_intro_end";

  static uint IGameEvent<EventTeamIntroEnd>.GetHash() => 0x46EF839Bu;
}
