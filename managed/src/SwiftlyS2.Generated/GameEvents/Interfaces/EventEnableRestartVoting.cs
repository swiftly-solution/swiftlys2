using SwiftlyS2.Core.GameEventDefinitions;
using SwiftlyS2.Shared.GameEvents;

namespace SwiftlyS2.Shared.GameEventDefinitions;

/// <summary> 
/// Event "enable_restart_voting"
/// </summary>
public interface EventEnableRestartVoting : IGameEvent<EventEnableRestartVoting>
{

    static EventEnableRestartVoting IGameEvent<EventEnableRestartVoting>.Create( nint address ) => new EventEnableRestartVotingImpl(address);

    static string IGameEvent<EventEnableRestartVoting>.GetName() => "enable_restart_voting";

    static uint IGameEvent<EventEnableRestartVoting>.GetHash() => 0x786801D0u;
    /// <summary>
    /// type: bool
    /// </summary>
    public bool Enable { get; set; }

}
