using SwiftlyS2.Core.GameEventDefinitions;
using SwiftlyS2.Shared.GameEvents;

namespace SwiftlyS2.Shared.GameEventDefinitions;

/// <summary> 
/// Event "finale_start"
/// </summary>
public interface EventFinaleStart : IGameEvent<EventFinaleStart>
{

    static EventFinaleStart IGameEvent<EventFinaleStart>.Create( nint address ) => new EventFinaleStartImpl(address);

    static string IGameEvent<EventFinaleStart>.GetName() => "finale_start";

    static uint IGameEvent<EventFinaleStart>.GetHash() => 0xA8BF9A49u;
    /// <summary>
    /// type: short
    /// </summary>
    public short Rushes { get; set; }

}
