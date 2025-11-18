using SwiftlyS2.Core.GameEventDefinitions;
using SwiftlyS2.Shared.GameEvents;

namespace SwiftlyS2.Shared.GameEventDefinitions;

/// <summary> 
/// Event "game_newmap"
/// send when new map is completely loaded
/// </summary>
public interface EventGameNewmap : IGameEvent<EventGameNewmap>
{

    static EventGameNewmap IGameEvent<EventGameNewmap>.Create( nint address ) => new EventGameNewmapImpl(address);

    static string IGameEvent<EventGameNewmap>.GetName() => "game_newmap";

    static uint IGameEvent<EventGameNewmap>.GetHash() => 0xF0D60440u;
    /// <summary>
    /// map name
    /// <br/>
    /// type: string
    /// </summary>
    public string MapName { get; set; }

    /// <summary>
    /// true if this is a transition from one map to another
    /// <br/>
    /// type: bool
    /// </summary>
    public bool Transition { get; set; }

}
