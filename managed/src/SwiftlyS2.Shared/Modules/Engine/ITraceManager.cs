using SwiftlyS2.Shared.Natives;
using SwiftlyS2.Shared.SchemaDefinitions;

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
    public void TracePlayerBBox( Vector start, Vector end, BBox_t bounds, CTraceFilter filter, ref CGameTrace trace );
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
    public void TraceShape( Vector start, Vector end, Ray_t ray, CTraceFilter filter, ref CGameTrace trace );

    /// <summary>
    /// Performs a simple trace shape operation from the specified start point to the end point, using the provided
    /// object query and trace mask. The result of the trace is stored in the provided trace object.
    /// </summary>
    /// <param name="start">The starting position of the trace, represented as a vector.</param>
    /// <param name="end">The ending position of the trace, represented as a vector.</param>
    /// <param name="rayKind">The type of ray used for the trace.</param>
    /// <param name="objectQuery">The object query specifying which objects to consider during the trace.</param>
    /// <param name="interactWith">The interaction layer defining the types of surfaces or entities to include in the trace.</param>
    /// <param name="interactExclude">The interaction layer defining the types of surfaces or entities to exclude from the trace.</param>
    /// <param name="interactAs">The interaction layer defining the types of surfaces or entities to interact as during the trace.</param>
    /// <param name="collision">The collision group defining the collision behavior during the trace.</param>
    /// <param name="trace">A reference to a CGameTrace structure that receives the results of the trace, including hit information and
    /// surface details.</param>
    /// <param name="filterEntity">An optional entity to exclude from the trace.</param>
    /// <param name="filterSecondEntity">An optional second entity to exclude from the trace.</param>
    public void SimpleTrace( Vector start, Vector end, RayType_t rayKind, RnQueryObjectSet objectQuery, MaskTrace interactWith, MaskTrace interactExclude, MaskTrace interactAs, CollisionGroup collision, ref CGameTrace trace, CBaseEntity? filterEntity = null, CBaseEntity? filterSecondEntity = null );

    /// <summary>
    /// Performs a simple trace shape operation from the specified start point in the direction defined by the given angle,
    /// using the provided object query and trace mask. The result of the trace is stored in the provided trace object.
    /// </summary>
    /// <param name="start">The starting position of the trace, represented as a vector.</param>
    /// <param name="angle">The direction of the trace, represented as a QAngle.</param>
    /// <param name="rayKind">The type of ray used for the trace.</param>
    /// <param name="objectQuery">The object query specifying which objects to consider during the trace.</param>
    /// <param name="interactWith">The interaction layer defining the types of surfaces or entities to include in the trace.</param>
    /// <param name="interactExclude">The interaction layer defining the types of surfaces or entities to exclude from the trace.</param>
    /// <param name="interactAs">The interaction layer defining the types of surfaces or entities to interact as during the trace.</param>
    /// <param name="collision">The collision group defining the collision behavior during the trace.</param>
    /// <param name="trace">A reference to a CGameTrace structure that receives the results of the trace, including hit information and
    /// surface details.</param>
    /// <param name="filterEntity">An optional entity to exclude from the trace.</param>
    /// <param name="filterSecondEntity">An optional second entity to exclude from the trace.</param>
    public void SimpleTrace( Vector start, QAngle angle, RayType_t rayKind, RnQueryObjectSet objectQuery, MaskTrace interactWith, MaskTrace interactExclude, MaskTrace interactAs, CollisionGroup collision, ref CGameTrace trace, CBaseEntity? filterEntity = null, CBaseEntity? filterSecondEntity = null );
}