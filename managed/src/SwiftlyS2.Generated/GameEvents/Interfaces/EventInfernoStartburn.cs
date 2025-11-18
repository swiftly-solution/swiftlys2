using SwiftlyS2.Core.GameEventDefinitions;
using SwiftlyS2.Shared.GameEvents;

namespace SwiftlyS2.Shared.GameEventDefinitions;

/// <summary> 
/// Event "inferno_startburn"
/// </summary>
public interface EventInfernoStartburn : IGameEvent<EventInfernoStartburn>
{

    static EventInfernoStartburn IGameEvent<EventInfernoStartburn>.Create( nint address ) => new EventInfernoStartburnImpl(address);

    static string IGameEvent<EventInfernoStartburn>.GetName() => "inferno_startburn";

    static uint IGameEvent<EventInfernoStartburn>.GetHash() => 0xD080B17Au;
    /// <summary>
    /// type: short
    /// </summary>
    public short EntityID { get; set; }

    /// <summary>
    /// type: float
    /// </summary>
    public float X { get; set; }

    /// <summary>
    /// type: float
    /// </summary>
    public float Y { get; set; }

    /// <summary>
    /// type: float
    /// </summary>
    public float Z { get; set; }

}
