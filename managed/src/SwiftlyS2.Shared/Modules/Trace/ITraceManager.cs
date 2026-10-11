
using SwiftlyS2.Shared.Natives;
using SwiftlyS2.Shared.Trace;

namespace SwiftlyS2.Shared.Services;

public interface ITraceManager
{
    /// <summary>
    /// Performs a collision trace of a player-sized bounding box from the specified start position to the end position,
    /// using the given filter and bounding box dimensions. The result of the trace is stored in the provided trace
    /// object.
    /// </summary>
    public TraceResult TracePlayerBBox( in Vector start, in Vector end, in BBox_t bounds, in TraceParams? param = default );

    /// <summary>
    /// Traces from <paramref name="start"/> to <paramref name="end"/> with the provided shape options.
    /// </summary>
    /// <param name="start">Trace start position.</param>
    /// <param name="end">Trace end position.</param>
    /// <param name="param">Optional trace params for ray type and filtering.</param>
    public TraceResult TraceShapeLine( in Vector start, in Vector end, in TraceParams? param = default );

    /// <summary>
    /// Traces from <paramref name="start"/> towards <paramref name="angle"/> with the provided shape options.
    /// </summary>
    /// <param name="start">Trace start position.</param>
    /// <param name="angle">Trace direction.</param>
    /// <param name="param">Optional trace params for ray type and filtering.</param>
    public TraceResult TraceShapeAngle( in Vector start, in QAngle angle, in TraceParams? param = default );

    /// <summary>
    /// Traces from <paramref name="start"/> towards <paramref name="angle"/> with the provided shape options.
    /// </summary>
    /// <param name="start">Trace start position.</param>
    /// <param name="angle">Trace direction.</param>
    /// <param name="maxDistance">Maximum distance to trace.</param>
    /// <param name="param">Optional trace params for ray type and filtering.</param>
    public TraceResult TraceShapeAngle( in Vector start, in QAngle angle, float maxDistance = 8192f, in TraceParams? param = default );
}
