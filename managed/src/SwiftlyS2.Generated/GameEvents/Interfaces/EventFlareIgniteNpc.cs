using SwiftlyS2.Core.GameEventDefinitions;
using SwiftlyS2.Shared.GameEvents;

namespace SwiftlyS2.Shared.GameEventDefinitions;

/// <summary> 
/// Event "flare_ignite_npc"
/// </summary>
public interface EventFlareIgniteNpc : IGameEvent<EventFlareIgniteNpc>
{

    static EventFlareIgniteNpc IGameEvent<EventFlareIgniteNpc>.Create( nint address ) => new EventFlareIgniteNpcImpl(address);

    static string IGameEvent<EventFlareIgniteNpc>.GetName() => "flare_ignite_npc";

    static uint IGameEvent<EventFlareIgniteNpc>.GetHash() => 0xDB89EE8Eu;
    /// <summary>
    /// entity ignited
    /// <br/>
    /// type: long
    /// </summary>
    public int EntIndex { get; set; }

}
