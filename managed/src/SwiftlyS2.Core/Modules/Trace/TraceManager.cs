
using SwiftlyS2.Core.Natives;
using SwiftlyS2.Shared.Natives;
using SwiftlyS2.Shared.Services;
using SwiftlyS2.Shared.SchemaDefinitions;
using SwiftlyS2.Core.EntitySystem;
using SwiftlyS2.Shared.Trace;

namespace SwiftlyS2.Core.Services;

internal class TraceManager : ITraceManager
{
    private static TraceParams ResolveOptionsOrDefault( in TraceParams? options )
    {
        return options ?? new TraceParams();
    }

    internal CTraceFilter FromTraceOptions( TraceParams options, out TraceParams callbackOptions, out Ray_t ray )
    {
        callbackOptions = options;
        ray = options.Ray;
        return options.GetTraceFilter();
    }

    internal TraceResult FromCGameTrace( ref CGameTrace _traceResult )
    {
        unsafe
        {
            TraceResult traceResult = new() {
                SurfaceProperties = _traceResult.SurfaceProperties != null ? new() {
                    Name = _traceResult.SurfaceProperties->Name,
                    NameHash = _traceResult.SurfaceProperties->NameHash,
                    BaseNameHash = _traceResult.SurfaceProperties->BaseNameHash,
                    ListIndex = _traceResult.SurfaceProperties->ListIndex,
                    BaseListIndex = _traceResult.SurfaceProperties->BaseListIndex,
                    Hidden = _traceResult.SurfaceProperties->Hidden,
                    Description = _traceResult.SurfaceProperties->Description,
                    Physics = new() {
                        Friction = _traceResult.SurfaceProperties->Physics.Friction,
                        Elasticity = _traceResult.SurfaceProperties->Physics.Elasticity,
                        Density = _traceResult.SurfaceProperties->Physics.Density,
                        Thickness = _traceResult.SurfaceProperties->Physics.Thickness,
                        SoftContactFrequency = _traceResult.SurfaceProperties->Physics.SoftContactFrequency,
                        SoftContactDampingRatio = _traceResult.SurfaceProperties->Physics.SoftContactDampingRatio,
                        WheelDrag = _traceResult.SurfaceProperties->Physics.WheelDrag,
                        HeatConductivity = _traceResult.SurfaceProperties->Physics.HeatConductivity,
                        Flashpoint = _traceResult.SurfaceProperties->Physics.Flashpoint
                    },
                    AudioSounds = new() {
                        ImpactSoft = _traceResult.SurfaceProperties->AudioSounds.ImpactSoft,
                        ImpactHard = _traceResult.SurfaceProperties->AudioSounds.ImpactHard,
                        ScrapeSmooth = _traceResult.SurfaceProperties->AudioSounds.ScrapeSmooth,
                        ScrapeRough = _traceResult.SurfaceProperties->AudioSounds.ScrapeRough,
                        BulletImpact = _traceResult.SurfaceProperties->AudioSounds.BulletImpact,
                        Rolling = _traceResult.SurfaceProperties->AudioSounds.Rolling,
                        Break = _traceResult.SurfaceProperties->AudioSounds.Break,
                        Strain = _traceResult.SurfaceProperties->AudioSounds.Strain,
                        MeleeImpact = _traceResult.SurfaceProperties->AudioSounds.MeleeImpact,
                        PushOff = _traceResult.SurfaceProperties->AudioSounds.PushOff,
                        SkidStop = _traceResult.SurfaceProperties->AudioSounds.SkidStop
                    },
                    AudioParams = new() {
                        Reflectivity = _traceResult.SurfaceProperties->AudioParams.Reflectivity,
                        HardnessFactor = _traceResult.SurfaceProperties->AudioParams.HardnessFactor,
                        RoughnessFactor = _traceResult.SurfaceProperties->AudioParams.RoughnessFactor,
                        RoughThreshold = _traceResult.SurfaceProperties->AudioParams.RoughThreshold,
                        HardThreshold = _traceResult.SurfaceProperties->AudioParams.HardThreshold,
                        HardVelocityThreshold = _traceResult.SurfaceProperties->AudioParams.HardVelocityThreshold,
                        StaticImpactVolume = _traceResult.SurfaceProperties->AudioParams.StaticImpactVolume,
                        OcclusionFactor = _traceResult.SurfaceProperties->AudioParams.OcclusionFactor,
                    },
                } : null,
                HitBox = _traceResult.HitBox != null ? new() {
                    Name = _traceResult.HitBox->m_name,
                    SurfaceProperty = _traceResult.HitBox->m_sSurfaceProperty,
                    BoneName = _traceResult.HitBox->m_sBoneName,
                    MinBounds = _traceResult.HitBox->m_vMinBounds,
                    MaxBounds = _traceResult.HitBox->m_vMaxBounds,
                    ShapeRadius = _traceResult.HitBox->m_flShapeRadius,
                    BoneNameHash = _traceResult.HitBox->m_nBoneNameHash,
                    GroupId = _traceResult.HitBox->m_nGroupId,
                    ShapeType = _traceResult.HitBox->m_nShapeType,
                    TranslationOnly = _traceResult.HitBox->m_bTranslationOnly,
                    CRC = _traceResult.HitBox->m_CRC,
                    RenderColor = _traceResult.HitBox->m_cRenderColor,
                    HitBoxIndex = _traceResult.HitBox->m_nHitBoxIndex,
                    ForcedTransform = _traceResult.HitBox->m_bForcedTransform,
                    ForcedTransformObject = _traceResult.HitBox->m_forcedTransform,
                } : null,
                Contents = _traceResult.Contents,
                BodyTransform = _traceResult.BodyTransform,
                ShapeAttributes = new() {
                    InteractsAs = _traceResult.ShapeAttributes.InteractsAs,
                    InteractsWith = _traceResult.ShapeAttributes.InteractsWith,
                    InteractsExclude = _traceResult.ShapeAttributes.InteractsExclude,
                    EntityId = _traceResult.ShapeAttributes.EntityId,
                    OwnerId = _traceResult.ShapeAttributes.OwnerId,
                    HierarchyId = _traceResult.ShapeAttributes.HierarchyId,
                    DetailLayerMask = _traceResult.ShapeAttributes.DetailLayerMask,
                    DetailLayerMaskType = _traceResult.ShapeAttributes.DetailLayerMaskType,
                    TargetDetailLayer = _traceResult.ShapeAttributes.TargetDetailLayer,
                    CollisionGroup = _traceResult.ShapeAttributes.CollisionGroup,
                    CollisionFunctionMask = _traceResult.ShapeAttributes.CollisionFunctionMask,
                },
                StartPos = _traceResult.StartPos,
                EndPos = _traceResult.EndPos,
                HitNormal = _traceResult.HitNormal,
                HitPoint = _traceResult.HitPoint,
                HitOffset = _traceResult.HitOffset,
                Fraction = _traceResult.Fraction,
                Triangle = _traceResult.Triangle,
                HitboxBoneIndex = _traceResult.HitboxBoneIndex,
                RayType = _traceResult.RayType,
                StartInSolid = _traceResult.StartInSolid,
                ExactHitPoint = _traceResult.ExactHitPoint,
                Entity = _traceResult.pEntity != null ? EntityManager.GetEntityByAddress((nint)_traceResult.pEntity) : null
            };

            return traceResult;
        }
    }

    public void TracePlayerBBox( Vector start, Vector end, BBox_t bounds, CTraceFilter filter, ref CGameTrace trace )
    {
        unsafe
        {
            fixed (CGameTrace* tracePtr = &trace)
            {
                filter.EnsureValid();
                GameFunctions.TracePlayerBBox(start, end, bounds, &filter, tracePtr);
            }
        }
    }

    public void TraceShape( Vector start, Vector end, Ray_t ray, CTraceFilter filter, ref CGameTrace trace )
    {
        unsafe
        {
            fixed (CGameTrace* tracePtr = &trace)
            {
                filter.EnsureValid();
                GameFunctions.TraceShape(NativeEngineHelpers.GetTraceManager(), &ray, start, end, &filter, tracePtr);
            }
        }
    }

    public TraceResult TracePlayerBBox( in Vector start, in Vector end, in BBox_t bounds, in TraceParams? options = default )
    {
        CGameTrace _traceResult = new();
        var resolvedOptions = ResolveOptionsOrDefault(in options);
        var _traceFilter = FromTraceOptions(resolvedOptions, out var callbackFilter, out _);

        unsafe
        {
            var oldCustomTraceFilter = CTraceFilterVTable.CustomTraceFilter;
            CTraceFilterVTable.CustomTraceFilter = callbackFilter;

            try
            {
                GameFunctions.TracePlayerBBox(start, end, bounds, &_traceFilter, &_traceResult);
            }
            finally
            {
                CTraceFilterVTable.CustomTraceFilter = oldCustomTraceFilter;
            }
        }

        return FromCGameTrace(ref _traceResult);
    }

    public TraceResult TraceShapeLine( in Vector start, in Vector end, in TraceParams? options = default )
    {
        var resolvedOptions = ResolveOptionsOrDefault(in options);
        CGameTrace traceResult = new();
        var traceFilter = FromTraceOptions(resolvedOptions, out var callbackFilter, out var ray);

        unsafe
        {
            var oldCustomTraceFilter = CTraceFilterVTable.CustomTraceFilter;
            CTraceFilterVTable.CustomTraceFilter = callbackFilter;

            try
            {
                GameFunctions.TraceShape(NativeEngineHelpers.GetTraceManager(), &ray, start, end, &traceFilter, &traceResult);
            }
            finally
            {
                CTraceFilterVTable.CustomTraceFilter = oldCustomTraceFilter;
            }
        }

        return FromCGameTrace(ref traceResult);
    }

    public TraceResult TraceShapeAngle( in Vector start, in QAngle angle, in TraceParams? options = default )
    {
        return TraceShapeAngle(in start, in angle, 8192f, in options);
    }

    public TraceResult TraceShapeAngle( in Vector start, in QAngle angle, float maxDistance = 8192f, in TraceParams? options = default )
    {
        var resolvedOptions = ResolveOptionsOrDefault(in options);
        var end = resolvedOptions.ComputeAngleEndPoint(start, angle, maxDistance);
        return TraceShapeLine(in start, in end, resolvedOptions);
    }

    private static void SimpleTraceNative( Vector start, Vector end, RayType_t rayKind, RnQueryObjectSet objectQuery, MaskTrace interactWith, MaskTrace interactExclude, MaskTrace interactAs, CollisionGroup collision, ref CGameTrace trace, nint filterEntity, nint filterSecondEntity )
    {
        var filter = new CTraceFilter(true) {
            IterateEntities = true,
            QueryShapeAttributes = new RnQueryShapeAttr_t() {
                ObjectSetMask = objectQuery,
                InteractsWith = interactWith,
                InteractsExclude = interactExclude,
                InteractsAs = interactAs,
                CollisionGroup = collision,
                HitSolid = true
            }
        };

        var ray = new Ray_t {
            Type = rayKind
        };

        unsafe
        {
            if (filterEntity != nint.Zero)
            {
                var entity = EntityManager.GetEntityByAddress(filterEntity) ?? throw new InvalidCastException("Invalid entity.");
                filter.QueryShapeAttributes.EntityIdsToIgnore[0] = entity.Index;
            }
            if (filterSecondEntity != nint.Zero)
            {
                var entity = EntityManager.GetEntityByAddress(filterSecondEntity) ?? throw new InvalidCastException("Invalid entity.");
                filter.QueryShapeAttributes.EntityIdsToIgnore[1] = entity.Index;
            }

            fixed (CGameTrace* tracePtr = &trace)
            {
                GameFunctions.TraceShape(NativeEngineHelpers.GetTraceManager(), &ray, start, end, &filter, tracePtr);
            }
        }
    }

    public static void SimpleTrace( Vector start, Vector end, RayType_t rayKind, RnQueryObjectSet objectQuery, MaskTrace interactWith, MaskTrace interactExclude, MaskTrace interactAs, CollisionGroup collision, ref CGameTrace trace, nint filterEntity, nint filterSecondEntity )
    {
        SimpleTraceNative(start, end, rayKind, objectQuery, interactWith, interactExclude, interactAs, collision, ref trace, filterEntity, filterSecondEntity);
    }

    public void SimpleTrace( Vector start, Vector end, RayType_t rayKind, RnQueryObjectSet objectQuery, MaskTrace interactWith, MaskTrace interactExclude, MaskTrace interactAs, CollisionGroup collision, ref CGameTrace trace, CBaseEntity? filterEntity = null, CBaseEntity? filterSecondEntity = null )
    {
        var entityPtr = filterEntity?.Address ?? nint.Zero;
        var entitySecondPtr = filterSecondEntity?.Address ?? nint.Zero;
        SimpleTraceNative(start, end, rayKind, objectQuery, interactWith, interactExclude, interactAs, collision, ref trace, entityPtr, entitySecondPtr);
    }

    public void SimpleTrace( Vector start, QAngle angle, RayType_t rayKind, RnQueryObjectSet objectQuery, MaskTrace interactWith, MaskTrace interactExclude, MaskTrace interactAs, CollisionGroup collision, ref CGameTrace trace, CBaseEntity? filterEntity = null, CBaseEntity? filterSecondEntity = null )
    {
        angle.ToDirectionVectors(out var fwd, out var _, out var _);
        var end = start + new Vector(
            fwd.X * 8192f,
            fwd.Y * 8192f,
            fwd.Z * 8192f
        );
        var entityPtr = filterEntity?.Address ?? nint.Zero;
        var entitySecondPtr = filterSecondEntity?.Address ?? nint.Zero;
        SimpleTraceNative(start, end, rayKind, objectQuery, interactWith, interactExclude, interactAs, collision, ref trace, entityPtr, entitySecondPtr);
    }

}
