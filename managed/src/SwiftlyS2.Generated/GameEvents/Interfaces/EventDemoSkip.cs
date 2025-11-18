using SwiftlyS2.Core.GameEventDefinitions;
using SwiftlyS2.Shared.GameEvents;

namespace SwiftlyS2.Shared.GameEventDefinitions;

/// <summary> 
/// Event "demo_skip"
/// </summary>
public interface EventDemoSkip : IGameEvent<EventDemoSkip>
{

    static EventDemoSkip IGameEvent<EventDemoSkip>.Create( nint address ) => new EventDemoSkipImpl(address);

    static string IGameEvent<EventDemoSkip>.GetName() => "demo_skip";

    static uint IGameEvent<EventDemoSkip>.GetHash() => 0x3A36ECC0u;
    /// <summary>
    /// current playback tick
    /// <br/>
    /// type: long
    /// </summary>
    public int PlaybackTick { get; set; }

    /// <summary>
    /// tick we're going to
    /// <br/>
    /// type: long
    /// </summary>
    public int SkiptoTick { get; set; }

}
