using SwiftlyS2.Core.GameEventDefinitions;
using SwiftlyS2.Shared.GameEvents;

namespace SwiftlyS2.Shared.GameEventDefinitions;

/// <summary> 
/// Event "ragdoll_dissolved"
/// </summary>
public interface EventRagdollDissolved : IGameEvent<EventRagdollDissolved>
{

    static EventRagdollDissolved IGameEvent<EventRagdollDissolved>.Create( nint address ) => new EventRagdollDissolvedImpl(address);

    static string IGameEvent<EventRagdollDissolved>.GetName() => "ragdoll_dissolved";

    static uint IGameEvent<EventRagdollDissolved>.GetHash() => 0x633046FAu;
    /// <summary>
    /// type: long
    /// </summary>
    public int EntIndex { get; set; }

}
