using SwiftlyS2.Core.GameEventDefinitions;
using SwiftlyS2.Shared.GameEvents;

namespace SwiftlyS2.Shared.GameEventDefinitions;

/// <summary> 
/// Event "sfuievent"
/// </summary>
public interface EventSfuievent : IGameEvent<EventSfuievent>
{

    static EventSfuievent IGameEvent<EventSfuievent>.Create( nint address ) => new EventSfuieventImpl(address);

    static string IGameEvent<EventSfuievent>.GetName() => "sfuievent";

    static uint IGameEvent<EventSfuievent>.GetHash() => 0xA20ACD22u;
    /// <summary>
    /// type: string
    /// </summary>
    public string Action { get; set; }

    /// <summary>
    /// type: string
    /// </summary>
    public string Data { get; set; }

    /// <summary>
    /// type: byte
    /// </summary>
    public byte Slot { get; set; }

}
