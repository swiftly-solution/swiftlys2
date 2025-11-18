using SwiftlyS2.Shared.Natives;

namespace SwiftlyS2.Shared.Services;

public interface ITraceManager
{
    /// <summary>
    /// Performs a collision trace of a player-sized bounding box from the specified start position to the end position,
    /// using the given filter and bounding box dimensions. The result of the trace is stored in the provided trace
    /// object.
    /// </summary>
    /// <param name="start">The starting position of the trace, typically representing the player's initial location.</param>
    /// <param name="end">The ending position of the trace, representing the target location for the bounding box movement.</param>
    /// <param name="bounds">The dimensions of the player's bounding box to be traced.</param>
    /// <param name="filter">The trace filter used to determine which entities or surfaces are considered during the trace operation.</param>
    /// <param name="trace">A reference to a CGameTrace object that receives the results of the trace, including collision information and
    /// hit details.</param>
    public void TracePlayerBBox(Vector start, Vector end, BBox_t bounds, CTraceFilter filter, ref CGameTrace trace);
    /// <summary>
    /// Performs a trace operation from the specified start point to the end point using the given ray and filter, and
    /// populates the trace result with collision information.
    /// </summary>
    /// <param name="start">The starting position of the trace, represented as a vector.</param>
    /// <param name="end">The ending position of the trace, represented as a vector.</param>
    /// <param name="ray">The ray definition used for the trace, specifying direction and other ray properties.</param>
    /// <param name="filter">The filter that determines which entities or surfaces are considered during the trace.</param>
    /// <param name="trace">A reference to a CGameTrace structure that receives the results of the trace, including hit information and
    /// surface details.</param>
    public void TraceShape(Vector start, Vector end, Ray_t ray, CTraceFilter filter, ref CGameTrace trace);
}