using SwiftlyS2.Core.GameEventDefinitions;
using SwiftlyS2.Shared.GameEvents;

namespace SwiftlyS2.Shared.GameEventDefinitions;

/// <summary> 
/// Event "hltv_changed_mode"
/// </summary>
public interface EventHltvChangedMode : IGameEvent<EventHltvChangedMode>
{

    static EventHltvChangedMode IGameEvent<EventHltvChangedMode>.Create( nint address ) => new EventHltvChangedModeImpl(address);

    static string IGameEvent<EventHltvChangedMode>.GetName() => "hltv_changed_mode";

    static uint IGameEvent<EventHltvChangedMode>.GetHash() => 0x11795622u;
    /// <summary>
    /// type: long
    /// </summary>
    public int OldMode { get; set; }

    /// <summary>
    /// type: long
    /// </summary>
    public int NewMode { get; set; }

    /// <summary>
    /// type: long
    /// </summary>
    public int ObsTarget { get; set; }

}
