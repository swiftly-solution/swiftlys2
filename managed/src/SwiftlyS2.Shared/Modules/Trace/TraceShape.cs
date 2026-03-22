using SwiftlyS2.Shared.Natives;

namespace SwiftlyS2.Shared.Trace;

public class TraceShape
{
    /// <summary>
    /// The starting position of the trace, represented as a vector.
    /// </summary>
    public Vector Start { get; set; }
    /// <summary>
    /// The ending position of the trace, represented as a vector.
    /// </summary>
    public Vector End { get; set; }
    /// <summary>
    /// The ray definition used for the trace, specifying direction and other ray properties.
    /// </summary>
    public Ray_t Ray { get; set; }
    /// <summary>
    /// The filter that determines which entities or surfaces are considered during the trace.
    /// </summary>
    public TraceFilter Filter { get; set; } = new();

    public override string ToString()
    {
        return $"TraceShape {{ Start: {Start}, End: {End}, Ray: {Ray}, Filter: {Filter} }}";
    }
}