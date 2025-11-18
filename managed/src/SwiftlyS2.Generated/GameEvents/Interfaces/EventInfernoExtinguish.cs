using SwiftlyS2.Core.GameEventDefinitions;
using SwiftlyS2.Shared.GameEvents;

namespace SwiftlyS2.Shared.GameEventDefinitions;

/// <summary> 
/// Event "inferno_extinguish"
/// </summary>
public interface EventInfernoExtinguish : IGameEvent<EventInfernoExtinguish>
{

    static EventInfernoExtinguish IGameEvent<EventInfernoExtinguish>.Create( nint address ) => new EventInfernoExtinguishImpl(address);

    static string IGameEvent<EventInfernoExtinguish>.GetName() => "inferno_extinguish";

    static uint IGameEvent<EventInfernoExtinguish>.GetHash() => 0x9A4147B1u;
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
