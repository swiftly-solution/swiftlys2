using SwiftlyS2.Shared.Natives;
using SwiftlyS2.Shared.SchemaDefinitions;

namespace SwiftlyS2.Shared.Trace;

public class SimpleTrace
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
    /// The type of ray used for the trace.
    /// </summary>
    public RayType_t RayKind { get; set; }
    /// <summary>
    /// The object query specifying which objects to consider during the trace.
    /// </summary>
    public RnQueryObjectSet ObjectQuery { get; set; }
    /// <summary>
    /// The interaction layer defining the types of surfaces or entities to include in the trace.
    /// </summary>
    public MaskTrace InteractWith { get; set; }
    /// <summary>
    /// The interaction layer defining the types of surfaces or entities to exclude from the trace.
    /// </summary>
    public MaskTrace InteractExclude { get; set; }
    /// <summary>
    /// The interaction layer defining the types of surfaces or entities to interact as during the trace.
    /// </summary>
    public MaskTrace InteractAs { get; set; }
    /// <summary>
    /// The collision group defining the collision behavior during the trace.
    /// </summary>
    public CollisionGroup Collision { get; set; }
    /// <summary>
    /// An optional list of entities to exclude from the trace.
    /// </summary>
    public List<CEntityInstance> EntitiesToIgnore { get; set; } = [];
    /// <summary>
    /// An optional custom callback function to determine whether a specific entity should be hit by the trace. This allows for dynamic and complex filtering logic beyond simple entity ID checks.
    /// </summary>
    public Func<CEntityInstance, bool>? ShouldHitEntity { get; set; } = null;

    public override string ToString()
    {
        return $"SimpleTrace {{ Start: {Start}, End: {End}, RayKind: {RayKind}, ObjectQuery: {ObjectQuery}, InteractWith: {InteractWith}, InteractExclude: {InteractExclude}, InteractAs: {InteractAs}, Collision: {Collision}, EntitiesToIgnore: {string.Join(", ", EntitiesToIgnore)} }}";
    }
}