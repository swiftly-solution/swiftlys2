using SwiftlyS2.Core.GameEvents;
using SwiftlyS2.Shared.GameEventDefinitions;

namespace SwiftlyS2.Core.GameEventDefinitions;

// generated
/// <summary> 
/// Event "demo_skip"
/// </summary>
internal class EventDemoSkipImpl : GameEvent<EventDemoSkip>, EventDemoSkip
{

    public EventDemoSkipImpl( nint address ) : base(address)
    {
    }

    // current playback tick
    public int PlaybackTick { get => Accessor.GetInt32("playback_tick"); set => Accessor.SetInt32("playback_tick", value); }

    // tick we're going to
    public int SkiptoTick { get => Accessor.GetInt32("skipto_tick"); set => Accessor.SetInt32("skipto_tick", value); }
}
