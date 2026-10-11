
using SwiftlyS2.Core.Natives;
using SwiftlyS2.Shared.Natives;
using SwiftlyS2.Shared.Services;
using SwiftlyS2.Core.EntitySystem;
using SwiftlyS2.Shared.Trace;

namespace SwiftlyS2.Core.Services;

internal class TraceManager : ITraceManager
{
    private static TraceParams ResolveOptionsOrDefault( in TraceParams? options )
    {
        return options ?? new TraceParams();
    }

    internal static CTraceFilter FromTraceOptions( TraceParams options, out TraceParams callbackOptions, out Ray_t ray )
    {
        callbackOptions = options;
        ray = options.Ray;
        return options.GetTraceFilter();
    }

    internal static TraceResult FromCGameTrace( ref CGameTrace _traceResult )
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

    public TraceResult TracePlayerBBox( in Vector start, in Vector end, in BBox_t bounds, in TraceParams? options = default )
    {
        var resolvedOptions = ResolveOptionsOrDefault(in options);
    
        var ray = resolvedOptions.Ray;
        ray.Init(bounds.Mins, bounds.Maxs);
        resolvedOptions.Ray = ray;
    
        return TraceShapeLine(in start, in end, resolvedOptions);
    }

    public static TraceResult TraceShape( in Vector start, in Vector end, in TraceParams? options = default )
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
                GameFunctions.TraceShape(&ray, start, end, &traceFilter, &traceResult);
            }
            finally
            {
                CTraceFilterVTable.CustomTraceFilter = oldCustomTraceFilter;
            }
        }

        return FromCGameTrace(ref traceResult);
    }

    public TraceResult TraceShapeLine( in Vector start, in Vector end, in TraceParams? options = default )
    {
        return TraceShape(in start, in end, in options);
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
}
