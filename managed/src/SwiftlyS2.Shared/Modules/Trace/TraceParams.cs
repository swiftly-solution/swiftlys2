using SwiftlyS2.Shared.Natives;
using SwiftlyS2.Shared.SchemaDefinitions;

namespace SwiftlyS2.Shared.Trace;

/// <summary>
/// Unified parameters for shape tracing APIs.
/// This combines trace shape and filter settings in one object.
/// </summary>
public struct TraceParams
{
    public TraceParams()
    {
    }
    
    /// <summary>
    /// The ray definition used for tracing shape.
    /// </summary>
    public Ray_t Ray { get; set; } = new() {
        Type = RayType_t.RAY_TYPE_LINE
    };

    /// <summary>
    /// Indicates whether entity iteration is enabled for custom filtering.
    /// </summary>
    public bool IterateEntities { get; set; } = true;

    /// <summary>
    /// Which object groups should be included by the query.
    /// </summary>
    public RnQueryObjectSet ObjectQuery { get; set; } = RnQueryObjectSet.All;

    /// <summary>
    /// Interaction mask to include.
    /// </summary>
    public MaskTrace InteractWith { get; set; } = MaskTrace.Solid;

    /// <summary>
    /// Interaction mask to exclude.
    /// </summary>
    public MaskTrace InteractExclude { get; set; } = MaskTrace.Empty;

    /// <summary>
    /// Interaction mask for "as" behavior.
    /// </summary>
    public MaskTrace InteractAs { get; set; } = MaskTrace.Empty;

    /// <summary>
    /// Collision group used by the trace.
    /// </summary>
    public CollisionGroup Collision { get; set; } = CollisionGroup.Always;

    /// <summary>
    /// Entities that should be ignored by the trace.
    /// </summary>
    public List<CEntityInstance> EntitiesToIgnore { get; set; } = [];

    /// <summary>
    /// Entity owners that should be ignored by the trace.
    /// </summary>
    public List<CEntityInstance> OwnersToIgnore { get; set; } = [];

    /// <summary>
    /// Optional hierarchy ids used by the native query shape attributes (max 2 values used).
    /// </summary>
    public List<ushort> HierarchyIds { get; set; } = [];

    /// <summary>
    /// Included detail layers used by native query shape attributes.
    /// </summary>
    public ushort IncludedDetailLayers { get; set; } = ushort.MaxValue;

    /// <summary>
    /// Target detail layer used by native query shape attributes.
    /// </summary>
    public byte TargetDetailLayer { get; set; } = 0;

    /// <summary>
    /// Whether the trace should report solid hits.
    /// </summary>
    public bool HitSolid { get; set; } = true;

    /// <summary>
    /// Whether the trace should report hits requiring contact generation.
    /// </summary>
    public bool HitSolidRequiresGenerateContacts { get; set; } = false;

    /// <summary>
    /// Whether the trace should report trigger hits.
    /// </summary>
    public bool HitTrigger { get; set; } = false;

    /// <summary>
    /// Whether disabled collision pairs should be ignored.
    /// </summary>
    public bool ShouldIgnoreDisabledPairs { get; set; } = true;

    /// <summary>
    /// Whether hitboxes should be ignored when both sides interact with hitboxes.
    /// </summary>
    public bool IgnoreIfBothInteractWithHitboxes { get; set; } = false;

    /// <summary>
    /// Force the trace to hit everything.
    /// </summary>
    public bool ForceHitEverything { get; set; } = false;

    /// <summary>
    /// Optional callback to decide whether an entity should be hit.
    /// </summary>
    public Func<CEntityInstance, bool>? ShouldHitEntity { get; set; }

    /// <summary>
    /// Creates a deep copy of the current parameters.
    /// </summary>
    public TraceParams Clone()
    {
        return new TraceParams {
            Ray = Ray,
            IterateEntities = IterateEntities,
            ObjectQuery = ObjectQuery,
            InteractWith = InteractWith,
            InteractExclude = InteractExclude,
            InteractAs = InteractAs,
            Collision = Collision,
            EntitiesToIgnore = [..EntitiesToIgnore],
            OwnersToIgnore = [..OwnersToIgnore],
            HierarchyIds = [..HierarchyIds],
            IncludedDetailLayers = IncludedDetailLayers,
            TargetDetailLayer = TargetDetailLayer,
            HitSolid = HitSolid,
            HitSolidRequiresGenerateContacts = HitSolidRequiresGenerateContacts,
            HitTrigger = HitTrigger,
            ShouldIgnoreDisabledPairs = ShouldIgnoreDisabledPairs,
            IgnoreIfBothInteractWithHitboxes = IgnoreIfBothInteractWithHitboxes,
            ForceHitEverything = ForceHitEverything,
            ShouldHitEntity = ShouldHitEntity
        };
    }

    /// <summary>
    /// Converts this parameters instance into the native CTraceFilter.
    /// </summary>
    public CTraceFilter ToCTraceFilter()
    {
        var filter = new CTraceFilter {
            IterateEntities = IterateEntities
        };

        filter.QueryShapeAttributes.InteractsWith = InteractWith;
        filter.QueryShapeAttributes.InteractsExclude = InteractExclude;
        filter.QueryShapeAttributes.InteractsAs = InteractAs;
        filter.QueryShapeAttributes.IncludedDetailLayers = IncludedDetailLayers;
        filter.QueryShapeAttributes.TargetDetailLayer = TargetDetailLayer;
        filter.QueryShapeAttributes.ObjectSetMask = ObjectQuery;
        filter.QueryShapeAttributes.CollisionGroup = Collision;
        filter.QueryShapeAttributes.HitSolid = HitSolid;
        filter.QueryShapeAttributes.HitSolidRequiresGenerateContacts = HitSolidRequiresGenerateContacts;
        filter.QueryShapeAttributes.HitTrigger = HitTrigger;
        filter.QueryShapeAttributes.ShouldIgnoreDisabledPairs = ShouldIgnoreDisabledPairs;
        filter.QueryShapeAttributes.IgnoreIfBothInteractWithHitboxes = IgnoreIfBothInteractWithHitboxes;
        filter.QueryShapeAttributes.ForceHitEverything = ForceHitEverything;

        unsafe
        {
            filter.QueryShapeAttributes.HierarchyIds[0] = HierarchyIds.Count < 1 ? (ushort)0 : HierarchyIds[0];
            filter.QueryShapeAttributes.HierarchyIds[1] = HierarchyIds.Count < 2 ? (ushort)0 : HierarchyIds[1];
        }

        return filter;
    }

    /// <summary>
    /// Computes the end position for an angle-based trace.
    /// </summary>
    internal Vector ComputeAngleEndPoint( Vector start, QAngle angle, float distance )
    {
        angle.ToDirectionVectors(out var fwd, out var _, out var _);
        return start + new Vector(
            fwd.X * distance,
            fwd.Y * distance,
            fwd.Z * distance
        );
    }

    /// <summary>
    /// Creates a default parameters instance suitable for most line traces.
    /// </summary>
    public static TraceParams DefaultLine()
    {
        return new TraceParams {
            Ray = new Ray_t {
                Type = RayType_t.RAY_TYPE_LINE
            }
        };
    }

    /// <summary>
    /// Creates a new fluent builder.
    /// </summary>
    public static TraceParamsBuilder Builder()
    {
        return new TraceParamsBuilder();
    }

    /// <summary>
    /// Creates a new fluent builder seeded from an existing parameters object.
    /// </summary>
    public static TraceParamsBuilder Builder( TraceParams? seed )
    {
        return new TraceParamsBuilder(seed);
    }
}
