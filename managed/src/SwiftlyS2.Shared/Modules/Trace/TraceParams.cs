using SwiftlyS2.Shared.Natives;
using SwiftlyS2.Shared.SchemaDefinitions;

namespace SwiftlyS2.Shared.Trace;

/// <summary>
/// Unified parameters for shape tracing APIs.
/// This combines trace shape and filter settings in one object.
/// </summary>
public struct TraceParams
{
    private Ray_t _ray = new() {
        Type = RayType_t.RAY_TYPE_LINE
    };
    private bool _iterateEntities = true;
    private RnQueryObjectSet _objectQuery = RnQueryObjectSet.All;
    private MaskTrace _interactWith = MaskTrace.Solid;
    private MaskTrace _interactExclude = MaskTrace.Empty;
    private MaskTrace _interactAs = MaskTrace.Empty;
    private CollisionGroup _collision = CollisionGroup.Always;
    private List<CEntityInstance> _entitiesToIgnore = [];
    private List<CEntityInstance> _ownersToIgnore = [];
    private List<ushort> _hierarchyIds = [];
    private ushort _includedDetailLayers = ushort.MaxValue;
    private byte _targetDetailLayer = 0;
    private bool _hitSolid = true;
    private bool _hitSolidRequiresGenerateContacts = false;
    private bool _hitTrigger = false;
    private bool _shouldIgnoreDisabledPairs = true;
    private bool _ignoreIfBothInteractWithHitboxes = false;
    private bool _forceHitEverything = false;
    private Func<CEntityInstance, bool>? _shouldHitEntity;
    private CTraceFilter _traceFilter;

    public TraceParams()
    {
        _traceFilter = new CTraceFilter();
        SyncAllToTraceFilter();
    }

    /// <summary>
    /// The ray definition used for tracing shape.
    /// </summary>
    public Ray_t Ray {
        get => _ray;
        set => _ray = value;
    }

    /// <summary>
    /// Indicates whether entity iteration is enabled for custom filtering.
    /// </summary>
    public bool IterateEntities {
        get => _iterateEntities;
        set
        {
            _iterateEntities = value;
            _traceFilter.IterateEntities = value;
            _traceFilter.EnsureValidNewFormat();
        }
    }

    /// <summary>
    /// Which object groups should be included by the query.
    /// </summary>
    public RnQueryObjectSet ObjectQuery {
        get => _objectQuery;
        set
        {
            _objectQuery = value;
            _traceFilter.QueryShapeAttributes.ObjectSetMask = value;
            _traceFilter.EnsureValidNewFormat();
        }
    }

    /// <summary>
    /// Interaction mask to include.
    /// </summary>
    public MaskTrace InteractWith {
        get => _interactWith;
        set
        {
            _interactWith = value;
            _traceFilter.QueryShapeAttributes.InteractsWith = value;
            _traceFilter.EnsureValidNewFormat();
        }
    }

    /// <summary>
    /// Interaction mask to exclude.
    /// </summary>
    public MaskTrace InteractExclude {
        get => _interactExclude;
        set
        {
            _interactExclude = value;
            _traceFilter.QueryShapeAttributes.InteractsExclude = value;
            _traceFilter.EnsureValidNewFormat();
        }
    }

    /// <summary>
    /// Interaction mask for "as" behavior.
    /// </summary>
    public MaskTrace InteractAs {
        get => _interactAs;
        set
        {
            _interactAs = value;
            _traceFilter.QueryShapeAttributes.InteractsAs = value;
            _traceFilter.EnsureValidNewFormat();
        }
    }

    /// <summary>
    /// Collision group used by the trace.
    /// </summary>
    public CollisionGroup Collision {
        get => _collision;
        set
        {
            _collision = value;
            _traceFilter.QueryShapeAttributes.CollisionGroup = value;
            _traceFilter.EnsureValidNewFormat();
        }
    }

    /// <summary>
    /// Entities that should be ignored by the trace.
    /// </summary>
    public List<CEntityInstance> EntitiesToIgnore {
        get => _entitiesToIgnore;
        set => _entitiesToIgnore = value ?? [];
    }

    /// <summary>
    /// Entity owners that should be ignored by the trace.
    /// </summary>
    public List<CEntityInstance> OwnersToIgnore {
        get => _ownersToIgnore;
        set => _ownersToIgnore = value ?? [];
    }

    /// <summary>
    /// Optional hierarchy ids used by the native query shape attributes (max 2 values used).
    /// </summary>
    public List<ushort> HierarchyIds {
        get => _hierarchyIds;
        set
        {
            _hierarchyIds = value ?? [];
            SyncHierarchyIdsToTraceFilter();
            _traceFilter.EnsureValidNewFormat();
        }
    }

    /// <summary>
    /// Included detail layers used by native query shape attributes.
    /// </summary>
    public ushort IncludedDetailLayers {
        get => _includedDetailLayers;
        set
        {
            _includedDetailLayers = value;
            _traceFilter.QueryShapeAttributes.IncludedDetailLayers = value;
            _traceFilter.EnsureValidNewFormat();
        }
    }

    /// <summary>
    /// Target detail layer used by native query shape attributes.
    /// </summary>
    public byte TargetDetailLayer {
        get => _targetDetailLayer;
        set
        {
            _targetDetailLayer = value;
            _traceFilter.QueryShapeAttributes.TargetDetailLayer = value;
            _traceFilter.EnsureValidNewFormat();
        }
    }

    /// <summary>
    /// Whether the trace should report solid hits.
    /// </summary>
    public bool HitSolid {
        get => _hitSolid;
        set
        {
            _hitSolid = value;
            _traceFilter.QueryShapeAttributes.HitSolid = value;
            _traceFilter.EnsureValidNewFormat();
        }
    }

    /// <summary>
    /// Whether the trace should report hits requiring contact generation.
    /// </summary>
    public bool HitSolidRequiresGenerateContacts {
        get => _hitSolidRequiresGenerateContacts;
        set
        {
            _hitSolidRequiresGenerateContacts = value;
            _traceFilter.QueryShapeAttributes.HitSolidRequiresGenerateContacts = value;
            _traceFilter.EnsureValidNewFormat();
        }
    }

    /// <summary>
    /// Whether the trace should report trigger hits.
    /// </summary>
    public bool HitTrigger {
        get => _hitTrigger;
        set
        {
            _hitTrigger = value;
            _traceFilter.QueryShapeAttributes.HitTrigger = value;
            _traceFilter.EnsureValidNewFormat();
        }
    }

    /// <summary>
    /// Whether disabled collision pairs should be ignored.
    /// </summary>
    public bool ShouldIgnoreDisabledPairs {
        get => _shouldIgnoreDisabledPairs;
        set
        {
            _shouldIgnoreDisabledPairs = value;
            _traceFilter.QueryShapeAttributes.ShouldIgnoreDisabledPairs = value;
            _traceFilter.EnsureValidNewFormat();
        }
    }

    /// <summary>
    /// Whether hitboxes should be ignored when both sides interact with hitboxes.
    /// </summary>
    public bool IgnoreIfBothInteractWithHitboxes {
        get => _ignoreIfBothInteractWithHitboxes;
        set
        {
            _ignoreIfBothInteractWithHitboxes = value;
            _traceFilter.QueryShapeAttributes.IgnoreIfBothInteractWithHitboxes = value;
            _traceFilter.EnsureValidNewFormat();
        }
    }

    /// <summary>
    /// Force the trace to hit everything.
    /// </summary>
    public bool ForceHitEverything {
        get => _forceHitEverything;
        set
        {
            _forceHitEverything = value;
            _traceFilter.QueryShapeAttributes.ForceHitEverything = value;
            _traceFilter.EnsureValidNewFormat();
        }
    }

    /// <summary>
    /// Optional callback to decide whether an entity should be hit.
    /// </summary>
    public Func<CEntityInstance, bool>? ShouldHitEntity {
        get => _shouldHitEntity;
        set => _shouldHitEntity = value;
    }

    /// <summary>
    /// Creates a deep copy of the current parameters.
    /// </summary>
    public TraceParams Clone()
    {
        var clone = new TraceParams {
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
        return clone;
    }

    internal CTraceFilter GetTraceFilter()
    {
        return _traceFilter;
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

    private void SyncAllToTraceFilter()
    {
        _traceFilter.IterateEntities = _iterateEntities;
        _traceFilter.QueryShapeAttributes.InteractsWith = _interactWith;
        _traceFilter.QueryShapeAttributes.InteractsExclude = _interactExclude;
        _traceFilter.QueryShapeAttributes.InteractsAs = _interactAs;
        _traceFilter.QueryShapeAttributes.IncludedDetailLayers = _includedDetailLayers;
        _traceFilter.QueryShapeAttributes.TargetDetailLayer = _targetDetailLayer;
        _traceFilter.QueryShapeAttributes.ObjectSetMask = _objectQuery;
        _traceFilter.QueryShapeAttributes.CollisionGroup = _collision;
        _traceFilter.QueryShapeAttributes.HitSolid = _hitSolid;
        _traceFilter.QueryShapeAttributes.HitSolidRequiresGenerateContacts = _hitSolidRequiresGenerateContacts;
        _traceFilter.QueryShapeAttributes.HitTrigger = _hitTrigger;
        _traceFilter.QueryShapeAttributes.ShouldIgnoreDisabledPairs = _shouldIgnoreDisabledPairs;
        _traceFilter.QueryShapeAttributes.IgnoreIfBothInteractWithHitboxes = _ignoreIfBothInteractWithHitboxes;
        _traceFilter.QueryShapeAttributes.ForceHitEverything = _forceHitEverything;
        SyncHierarchyIdsToTraceFilter();
        _traceFilter.EnsureValidNewFormat();
    }

    private void SyncHierarchyIdsToTraceFilter()
    {
        unsafe
        {
            _traceFilter.QueryShapeAttributes.HierarchyIds[0] = _hierarchyIds.Count < 1 ? (ushort)0 : _hierarchyIds[0];
            _traceFilter.QueryShapeAttributes.HierarchyIds[1] = _hierarchyIds.Count < 2 ? (ushort)0 : _hierarchyIds[1];
        }
    }
}
