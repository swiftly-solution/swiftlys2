using SwiftlyS2.Shared.Natives;
using SwiftlyS2.Shared.SchemaDefinitions;

namespace SwiftlyS2.Shared.Trace;

/// <summary>
/// Fluent builder for TraceParams.
/// </summary>
public sealed class TraceParamsBuilder
{
    private TraceParams _params;

    public TraceParamsBuilder()
    {
        _params = new TraceParams();
    }

    public TraceParamsBuilder( TraceParams? seed )
    {
        _params = seed?.Clone() ?? new TraceParams();
    }

    public TraceParamsBuilder WithRay( in Ray_t ray )
    {
        _params.Ray = ray;
        return this;
    }

    public TraceParamsBuilder WithLineRay( Vector startOffset, float radius = 0f )
    {
        var ray = new Ray_t();
        if (radius > 0f)
        {
            ray.Init(startOffset, radius);
        }
        else
        {
            ray.Init(startOffset);
        }
        _params.Ray = ray;
        return this;
    }

    public TraceParamsBuilder WithLineRay()
    {
        return WithLineRay(Vector.Zero);
    }

    public TraceParamsBuilder WithSphereRay( Vector center, float radius )
    {
        var ray = new Ray_t();
        ray.Init(center, radius);
        _params.Ray = ray;
        return this;
    }

    public TraceParamsBuilder WithHullRay( Vector mins, Vector maxs )
    {
        var ray = new Ray_t();
        ray.Init(mins, maxs);
        _params.Ray = ray;
        return this;
    }

    public TraceParamsBuilder WithCapsuleRay( Vector centerA, Vector centerB, float radius )
    {
        var ray = new Ray_t();
        ray.Init(centerA, centerB, radius);
        _params.Ray = ray;
        return this;
    }

    /// <summary>
    /// Which object groups should be included by the query.
    /// </summary>
    public TraceParamsBuilder WithObjectQuery( RnQueryObjectSet objectQuery )
    {
        _params.ObjectQuery = objectQuery;
        return this;
    }

    /// <summary>
    /// Interaction mask to include/exclude and interaction mask for "as" behavior.
    /// </summary>
    public TraceParamsBuilder WithInteraction( MaskTrace interactWith, MaskTrace interactExclude = MaskTrace.Empty, MaskTrace interactAs = MaskTrace.Empty )
    {
        _params.InteractWith = interactWith;
        _params.InteractExclude = interactExclude;
        _params.InteractAs = interactAs;
        return this;
    }

    /// <summary>
    /// Interaction mask to include.
    /// </summary>
    public TraceParamsBuilder InteractWith( MaskTrace flags )
    {
        _params.InteractWith |= flags;
        return this;
    }

    /// <summary>
    /// Interaction mask to include.
    /// </summary>
    public TraceParamsBuilder RemoveInteractWith( MaskTrace flags )
    {
        _params.InteractWith &= ~flags;
        return this;
    }

    /// <summary>
    /// Interaction mask to exclude.
    /// </summary>
    public TraceParamsBuilder InteractExclude( MaskTrace flags )
    {
        _params.InteractExclude |= flags;
        return this;
    }

    /// <summary>
    /// Interaction mask to exclude.
    /// </summary>
    public TraceParamsBuilder RemoveInteractExclude( MaskTrace flags )
    {
        _params.InteractExclude &= ~flags;
        return this;
    }

    /// <summary>
    /// Interaction mask for "as" behavior.
    /// </summary>
    public TraceParamsBuilder InteractAs( MaskTrace flags )
    {
        _params.InteractAs |= flags;
        return this;
    }

    /// <summary>
    /// Interaction mask for "as" behavior.
    /// </summary>
    public TraceParamsBuilder RemoveInteractAs( MaskTrace flags )
    {
        _params.InteractAs &= ~flags;
        return this;
    }

    /// <summary>
    /// Collision group used by the trace.
    /// </summary>
    public TraceParamsBuilder WithCollisionGroup( CollisionGroup collisionGroup )
    {
        _params.Collision = collisionGroup;
        return this;
    }

    /// <summary>
    /// Indicates whether entity iteration is enabled for custom filtering.
    /// </summary>
    public TraceParamsBuilder WithIterateEntities( bool iterateEntities )
    {
        _params.IterateEntities = iterateEntities;
        return this;
    }

    /// <summary>
    /// Whether the trace should report solid hits.
    /// </summary>
    public TraceParamsBuilder HitSolid( bool enabled = true )
    {
        _params.HitSolid = enabled;
        return this;
    }

    /// <summary>
    /// Whether the trace should report trigger hits.
    /// </summary>
    public TraceParamsBuilder HitTrigger( bool enabled = true )
    {
        _params.HitTrigger = enabled;
        return this;
    }

    /// <summary>
    /// Whether the trace should report hits requiring contact generation.
    /// </summary>
    public TraceParamsBuilder HitSolidRequiresGenerateContacts( bool enabled = true )
    {
        _params.HitSolidRequiresGenerateContacts = enabled;
        return this;
    }

    /// <summary>
    /// Whether disabled collision pairs should be ignored.
    /// </summary>
    public TraceParamsBuilder IgnoreDisabledPairs( bool enabled = true )
    {
        _params.ShouldIgnoreDisabledPairs = enabled;
        return this;
    }

    /// <summary>
    /// Whether hitboxes should be ignored when both sides interact with hitboxes.
    /// </summary>
    public TraceParamsBuilder IgnoreIfBothInteractWithHitboxes( bool enabled = true )
    {
        _params.IgnoreIfBothInteractWithHitboxes = enabled;
        return this;
    }

    /// <summary>
    /// Force the trace to hit everything.
    /// </summary>
    public TraceParamsBuilder ForceHitEverything( bool enabled = true )
    {
        _params.ForceHitEverything = enabled;
        return this;
    }

    /// <summary>
    /// Entities that should be ignored by the trace.
    /// </summary>
    public TraceParamsBuilder IgnoreEntity( CEntityInstance entity )
    {
        _params.EntitiesToIgnore.Add(entity);
        return this;
    }

    /// <summary>
    /// Entities that should be ignored by the trace.
    /// </summary>
    public TraceParamsBuilder IgnoreEntities( IEnumerable<CEntityInstance> entities )
    {
        _params.EntitiesToIgnore.AddRange(entities);
        return this;
    }

    /// <summary>
    /// Entity owners that should be ignored by the trace.
    /// </summary>
    public TraceParamsBuilder IgnoreOwner( CEntityInstance owner )
    {
        _params.OwnersToIgnore.Add(owner);
        return this;
    }

    /// <summary>
    /// Entity owners that should be ignored by the trace.
    /// </summary>
    public TraceParamsBuilder IgnoreOwners( IEnumerable<CEntityInstance> owners )
    {
        _params.OwnersToIgnore.AddRange(owners);
        return this;
    }

    /// <summary>
    /// Optional hierarchy ids used by the native query shape attributes (max 2 values used).
    /// </summary>
    public TraceParamsBuilder WithHierarchyIds( params ushort[] hierarchyIds )
    {
        _params.HierarchyIds = [..hierarchyIds];
        return this;
    }

    /// <summary>
    /// Included detail layers and target detail layer used by native query shape attributes.
    /// </summary>
    public TraceParamsBuilder WithDetailLayers( ushort includedDetailLayers, byte targetDetailLayer = 0 )
    {
        _params.IncludedDetailLayers = includedDetailLayers;
        _params.TargetDetailLayer = targetDetailLayer;
        return this;
    }

    /// <summary>
    /// Optional callback to decide whether an entity should be hit.
    /// </summary>
    public TraceParamsBuilder WithShouldHitEntity( Func<CEntityInstance, bool>? shouldHitEntity )
    {
        _params.ShouldHitEntity = shouldHitEntity;
        return this;
    }

    public TraceParams Build()
    {
        return _params.Clone();
    }
}
