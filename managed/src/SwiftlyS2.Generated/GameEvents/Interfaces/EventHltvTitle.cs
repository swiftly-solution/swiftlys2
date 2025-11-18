using SwiftlyS2.Core.GameEventDefinitions;
using SwiftlyS2.Shared.GameEvents;

namespace SwiftlyS2.Shared.GameEventDefinitions;

/// <summary> 
/// Event "hltv_title"
/// </summary>
public interface EventHltvTitle : IGameEvent<EventHltvTitle>
{

    static EventHltvTitle IGameEvent<EventHltvTitle>.Create( nint address ) => new EventHltvTitleImpl(address);

    static string IGameEvent<EventHltvTitle>.GetName() => "hltv_title";

    static uint IGameEvent<EventHltvTitle>.GetHash() => 0xA9B9262Au;
    /// <summary>
    /// type: string
    /// </summary>
    public string Text { get; set; }

}
