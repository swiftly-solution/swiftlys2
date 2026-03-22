using SwiftlyS2.Shared.Natives;

namespace SwiftlyS2.Shared.Trace;

public class TracePlayerBBox
{
    /// <summary>
    /// The starting position of the trace, typically representing the player's initial location.
    /// </summary>
    public Vector Start { get; set; }
    /// <summary>
    /// The ending position of the trace, representing the target location for the bounding box movement.
    /// </summary>
    public Vector End { get; set; }
    /// <summary>
    /// The dimensions of the player's bounding box to be traced.
    /// </summary>
    public BBox_t Bounds { get; set; }
    /// <summary>
    /// The trace filter used to determine which entities or surfaces are considered during the trace operation.
    /// </summary>
    public TraceFilter Filter { get; set; } = new();

    public override string ToString()
    {
        return $"TracePlayerBBox {{ Start: {Start}, End: {End}, Bounds: {Bounds}, Filter: {Filter} }}";
    }
}